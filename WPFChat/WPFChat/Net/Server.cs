﻿using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using WPFChat.Net.IO;

namespace WPFChat.Net
{
    internal class Server
    {
        TcpClient _client;
        PacketBuilder _packetBuilder;
        public PacketReader PacketReader;

        public event Action connectedEvent;

        public Server()
        {
            _client = new TcpClient();
        }

        public void ConnectToServer(string username)
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 7891);
                PacketReader = new PacketReader(_client.GetStream());

                if (!string.IsNullOrEmpty(username))
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteString(username);
                    _client.Client.Send(connectPacket.GetPacketBytes());
                }

                ReadPackets();
            }
        }

        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while(true)
                {
                    var opcode = PacketReader.ReadByte();
                    switch (opcode)
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;

                        default:
                            Console.WriteLine("ah yes...");
                            break;
                    }
                }
            });
        }
    }
}
