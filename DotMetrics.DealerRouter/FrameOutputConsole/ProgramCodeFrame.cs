using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotMetrics.TestChat;

namespace FrameOutputConsole
{
    internal class ProgramCodeFrame
    {
        private static void Main(string[] args)
        {
            ChatNode cn = new ChatNode(true);
            Console.WriteLine(cn.OutputAllFrames());
            Console.ReadKey();
        }
    }
}