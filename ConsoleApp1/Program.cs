using Network;
using System;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            UDPNetwork udp = new UDPNetwork();

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
