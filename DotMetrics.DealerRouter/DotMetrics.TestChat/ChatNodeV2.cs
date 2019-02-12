using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;
using FistCore.Common.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DotMetrics.TestChat
{
    public class ChatNodeV2
    {
        public static ZContext Context { get; private set; }

        public bool IsServer { get; private set; }
        public bool IsStarted { get; private set; }

        public static string Endpoint { get; private set; }

        private ZSocket activeSocket;

        public ChatNodeV2(bool isServer)
        {
            this.IsServer = isServer;
            if (ChatNodeV2.Context == null)
            {
                ChatNodeV2.Context = new ZContext();
                this._sendList = new List<ZFrame>();
                this._receiveList = new List<ZFrame>();
                this._output = new List<ZFrame>();
            }
        }

        private List<ZFrame> _receiveList;
        private List<ZFrame> _sendList;
        private List<ZFrame> _output;

        public void AsyncClient(string endpoint)
        // It connects to the server, and then sends a request once per second
        // It collects responses as they arrive, and it prints them out.
        {
            using (var client = new ZSocket(ChatNodeV2.Context, ZSocketType.DEALER))
            {
                ChatNodeV2.Endpoint = endpoint;
                // ID
                Random rnd = new Random();
                client.Identity = Encoding.UTF8.GetBytes("CLIENT " + "[" + rnd.Next(1, 9999) + "]"); //set random client id

                client.Connect(Endpoint);
                activeSocket = client;
                ZError error;
                ZMessage incoming;
                var poll = ZPollItem.CreateReceiver();

                int requests = 0;
                while (true)
                {
                    // Tick once per second, pulling in arriving messages
                    for (int centitick = 0; centitick < 100; ++centitick)
                    {
                        if (!client.PollIn(poll, out incoming, out error, TimeSpan.FromMilliseconds(10)))
                        {
                            if (error == ZError.EAGAIN)
                            {
                                Thread.Sleep(1);
                                continue;
                            }
                            if (error == ZError.ETERM)
                                return;    // Interrupted
                            throw new ZException(error);
                        }
                        IsStarted = true;

                        using (incoming)
                        {
                            string messageText = incoming[0].ReadString();
                            //List<string> primljenePoruke = new List<string>();
                            //primljenePoruke.Add(messageText);
                        }
                    }
                    using (var outgoing = new ZMessage())
                    {
                        outgoing.Add(new ZFrame(client.Identity));
                        outgoing.Add(new ZFrame("Response: " + "Request " + (++requests) + " at " + DateTime.Now.ToLongTimeString()));
                        List<ZFrame> primljenePoruke = new List<ZFrame>();
                        primljenePoruke.AddRange(outgoing);
                        _sendList.AddRange(primljenePoruke);
                        _output.AddRange(_sendList);
                        _sendList.Clear();

                        if (!client.Send(outgoing, out error))
                        {
                            if (error == ZError.ETERM)
                                return;    // Interrupted
                            throw new ZException(error);
                        }
                    }
                }
            }
        }

        // It uses the multithreaded server model to deal requests out to a pool
        // of workers and route replies back to clients. One worker can handle
        // one request at a time but one client can talk to multiple workers at
        // once.
        public void AsyncServerTask(string endpoint)
        {
            Endpoint = endpoint;
            using (var frontend = new ZSocket(ChatNodeV2.Context, ZSocketType.ROUTER)) //for clients to connect to
            using (var backend = new ZSocket(ChatNodeV2.Context, ZSocketType.DEALER)) //for workers to connect to
            {
                // Frontend socket talks to clients over TCP
                frontend.Bind(Endpoint);
                // Backend socket talks to workers over inproc(inproc sockets, which are the fastest way to connect threads in one process)
                backend.Bind("tcp://127.0.0.1:5571");

                //launch worker thread

                new Thread(() => AsyncServerWorker()).Start();
                // Connect backend to frontend via a proxy
                ZError error;
                if (!ZContext.Proxy(frontend, backend, out error))
                {
                    if (error == ZError.ETERM)
                        return;    // Interrupted
                    throw new ZException(error);
                }
            }
        }

        // Each worker task works on one request at a time
        public void AsyncServerWorker()
        {
            //using (var frontendWorker = new ZSocket(ChatNodeV2.Context, ZSocketType.ROUTER)) not needed !
            using (var worker = new ZSocket(ChatNodeV2.Context, ZSocketType.DEALER))
            {
                //frontendWorker.Connect(Endpoint); not needed !
                worker.Connect("tcp://127.0.0.1:5571");

                ZError error;
                ZMessage request;

                while (true)
                {
                    if (null == (request = worker.ReceiveMessage(out error)))
                    {
                        if (error == ZError.ETERM)
                            return;    // Interrupted
                        throw new ZException(error);
                    }
                    using (request)
                    {
                        // The DEALER socket gives us the reply envelope and message
                        string identity = request[1].ReadString();
                        string content = request[2].ReadString();

                        using (var response = new ZMessage())
                        {
                            response.Add(new ZFrame(identity));
                            response.Add(new ZFrame(content));

                            List<ZFrame> primljenePoruke = new List<ZFrame>();
                            primljenePoruke.AddRange(response);
                            _receiveList.AddRange(primljenePoruke);
                            _output.AddRange(_receiveList);
                            _receiveList.Clear();

                            if (!worker.Send(response, out error))
                            {
                                if (error == ZError.ETERM)
                                    return;    // Interrupted
                                throw new ZException(error);
                            }
                        }
                    }
                }
            }
        }

        public void Stop(string endpoint)
        {
            Endpoint = endpoint;

            if (this.IsStarted == true)
            {
                if (this.IsServer)
                {
                    try
                    {
                        this.activeSocket.Unbind(Endpoint);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }
                    this.IsStarted = false;
                }
                else
                {
                    try
                    {
                        this.activeSocket.Disconnect(Endpoint);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }
                    this.IsStarted = false;
                }
            }
        }

        public void InputTextFromUI(ZFrame inputMsg)
        {
            _sendList.Add(inputMsg);
        }

        public void OutputTextToUI(ZFrame outputMsg)
        {
            _output.Add(outputMsg);
        }

        public string RefreshOutput()
        {
            string chatText = "";
            //List<Message> sortedOutput = _output.OrderBy(item => item.Timestamp).ToList();
            foreach (ZFrame frame in _output.ToList()) //Calling _sendList.ToList() copies the values of _sendList to a separate list at the start of the foreach.Nothing else has access to this list(it doesn't even have a variable name!), so nothing can modify it inside the loop.
            {
                chatText += frame + Environment.NewLine;
            }
            return chatText;
        }
    }
}