using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Net.IO
{
    internal class PacketBuilder
    {
        MemoryStream _ms;
        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            _ms.WriteByte(opcode); // tells server how to read message
        }

        public void WriteMessage(string msg)
        {
            var msgLength = msg.Length; // message length for payload character count
            _ms.Write(BitConverter.GetBytes(msgLength)); // add message length to packet
            _ms.Write(Encoding.ASCII.GetBytes(msg)); // add payload to packet
        }

        public byte[] GetPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}
