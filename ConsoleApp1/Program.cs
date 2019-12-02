using Network;
using System;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("server?y/n:");
            string isServer = Console.ReadLine();
            if (isServer.Equals("y"))
            {
                UDPBase udp = new UDPServer(2001);

                while (true)
                {
                }
            }
            else
            {
                Console.WriteLine("input usePort:");
                string myPortText = Console.ReadLine();

                Console.WriteLine("input name:");
                string name = Console.ReadLine();
                UDPClient udp = new UDPClient(int.Parse(myPortText), 2001, name);


                while (true)
                {
                    Console.WriteLine("送信する文字列を入力してください。");
                    string sendMsg = Console.ReadLine();

                    if (sendMsg.Equals("test001"))
                    {
                        udp.Test001();
                    }
                    else if (sendMsg.Equals("test002"))
                    {
                        udp.Test002();
                    }
                    else
                    {
                        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendMsg);
                        udp.Send(sendBytes);

                    }
                }
            }


        }

    }
}
