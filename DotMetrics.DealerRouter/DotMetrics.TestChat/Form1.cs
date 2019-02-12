using System;
using System.Threading;
using System.Windows.Forms;
using ZeroMQ;

namespace DotMetrics.TestChat
{
    public partial class WindowsLiveMessenger : Form
    {
        private bool isServer = true;
        private string endpoint = "tcp://127.0.0.1:5570"; //localhost

        private string sendText = "";

        private ChatNodeV2 cNodeV2;

        public WindowsLiveMessenger()
        {
            InitializeComponent();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (isServer)
            {
                sendText = "Server:     " + sendTxtbox.Text;
            }
            else
            {
                sendText = "Client:       " + sendTxtbox.Text;
            }

            sendTxtbox.Clear();

            ZFrame zf = new ZFrame(sendText);
            if (cNodeV2 != null)
            {
                cNodeV2.InputTextFromUI(zf);

                cNodeV2.OutputTextToUI(zf);
            }
        }

        private void connectionBtn_Click(object sender, EventArgs e)
        {
            if (cNodeV2 == null)
            {
                if (isServer = checkBox1.Checked)
                {
                    cNodeV2 = new ChatNodeV2(isServer);
                    using (var context = new ZContext())
                    {
                        new Thread(() => cNodeV2.AsyncServerTask(endpoint)).Start();
                    }
                }
                else
                {
                    cNodeV2 = new ChatNodeV2(isServer);
                    using (var context = new ZContext())
                    {
                        new Thread(() => cNodeV2.AsyncClient(endpoint)).Start();
                    }
                }
                timer.Start();
            }
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            if (cNodeV2 != null)
                cNodeV2.Stop(endpoint);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            receiveTxtbox.Text = cNodeV2.RefreshOutput();
            receiveTxtbox.SelectionStart = receiveTxtbox.Text.Length;
            receiveTxtbox.ScrollToCaret();
        }

        private void sendTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendBtn_Click(this, new EventArgs());
            }
        }

        private void btn_Quit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}