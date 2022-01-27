using System;
using System.IO;
using System.Text;

namespace WPFChat.Net.IO
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

        public void WriteString(string msg)
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
