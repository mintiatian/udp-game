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

            }
            else
            {
                Console.WriteLine("myPort input:");
                string myPortText = Console.ReadLine();
                UDPNetwork udp = new UDPNetwork(int.Parse(myPortText), 2001);

                while (true)

                {

                    Console.WriteLine("送信する文字列を入力してください。");
                    string sendMsg = Console.ReadLine();
                    byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendMsg);
                    udp.Send(sendBytes);
                }
            }


        }

    }
}
