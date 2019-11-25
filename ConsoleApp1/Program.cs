using System;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("使用するポートを入力(2001,2002):");
            string inputText = Console.ReadLine();

            int myPort = int.Parse(inputText);


            Console.WriteLine("送信するポートを入力(2001,2002):");
            inputText = Console.ReadLine();

            int sendPort = int.Parse(inputText);



            for (; ; )
            {

                //送信するデータを作成する
                Console.WriteLine("送信する文字列を入力してください。");
                string sendMsg = Console.ReadLine();
                byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendMsg);





                //"exit"と入力されたら終了
                if (sendMsg.Equals("exit"))
                {
                    break;
                }
            }
        }




        //データを送信した時
        private static void SendCallback(IAsyncResult ar)
        {
            System.Net.Sockets.UdpClient udp =
                (System.Net.Sockets.UdpClient)ar.AsyncState;

            //非同期送信を終了する
            try
            {
                udp.EndSend(ar);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("送信エラー({0}/{1})",
                    ex.Message, ex.ErrorCode);
            }
            catch (ObjectDisposedException ex)
            {
                //すでに閉じている時は終了
                Console.WriteLine("Socketは閉じられています。");
            }
        }

    }
}
