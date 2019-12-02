using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Network
{
    class UDPServer : UDPBase
    {

        List<ClientStatus> clients = new List<ClientStatus>();
        public UDPServer(int setPort) : base(setPort)
        {
        }

        protected override void RecieveData(IPAddress address, int port, byte[] rcvBytes)
        {
            base.RecieveData(address, port, rcvBytes);


            ClientStatus sender = null;
            if (rcvBytes[0] != Message.IdSessionStart)
            {
                sender = clients.Where(client => client.address.ToString() == address.ToString() && client.port == port).First();
            }

            //データを文字列に変換する
            switch (rcvBytes[0])
            {
                case Message.IdSessionStart:
                    RecieveSessionStartClient(address, port, rcvBytes);
                    break;
                default:
                    //データを文字列に変換する
                    string rcvMsg = System.Text.Encoding.UTF8.GetString(rcvBytes);
                    SendClient(sender.nickName + ":" + rcvMsg);
                    break;
            }
        }


        private void RecieveSessionStartClient(IPAddress address, int port, byte[] rcvBytes)
        {
            ClientStatus newClient = null;
            using (var memory = new MemoryStream(rcvBytes))
            {
                using (BinaryReader binaryStream = new BinaryReader(memory, System.Text.Encoding.UTF8))
                {
                    byte id = binaryStream.ReadByte();
                    string name = binaryStream.ReadString();
                    Console.WriteLine(name);

                    newClient = new ClientStatus();
                    newClient.SetData(address, port, name);
                    newClient.SetNetworkId(clients.Count);
                    clients.Add(newClient);
                }
            }


            {   // networkidを返す

                SendNetworkId(newClient);
            }

            foreach (var client in clients)
            {
                string sendText = "" + newClient.nickName + " is join.";

                byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendText);

                Send(client, sendBytes);
            }
        }


        private void SendNetworkId(ClientStatus client)
        {

            using (var memory = new MemoryStream())
            {

                using (BinaryWriter binaryStream = new BinaryWriter(memory, System.Text.Encoding.UTF8))
                {
                    binaryStream.Write(Message.IdGetNetworkId);
                    binaryStream.Write(client.networkId);


                    Console.WriteLine("return msg: ---------------");
                    Console.WriteLine("nickName:" + client.nickName);
                    Console.WriteLine("address:" + client.address);
                    Console.WriteLine("port:" + client.port);
                    Console.WriteLine("networkId:" + client.networkId);
                    Console.WriteLine("---------------------------");
                }

                Send(client, memory.ToArray());
            }
        }

        private void SendClient(string sendText)
        {
            byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendText);
            foreach (var client in clients)
            {
                Send(client, sendBytes);
            }
        }
    }
}
