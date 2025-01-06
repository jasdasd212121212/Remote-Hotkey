using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_KeyboardCapturer.LocalServer
{
    public class PackegeFormatter
    {
        private const string SEPARATOR = "|:|";

        public byte[] Pack(string key, bool isDown)
        {
            byte action = isDown ? (byte)1 : (byte)0;
            byte[] packedKey = Encoding.ASCII.GetBytes(key);

            byte[] result = packedKey.Concat(Encoding.ASCII.GetBytes(SEPARATOR)).Concat(new byte[1] { action }).ToArray();

            return result;
        }
    }
}