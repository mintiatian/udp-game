using System;
using System.Net.Sockets;

namespace Network
{
    class UDPNetwork
    {
        public UDPNetwork()
        {
            CreateReceive();
        }
        private int myPort = 2002;
        private int sendPort = 2001;

        private UdpClient udpClient = null;

        private void CreateReceive()
        {
            //UdpClientを作成し、指定したポート番号にバインドする
            System.Net.IPEndPoint localEP = new System.Net.IPEndPoint(System.Net.IPAddress.Any, myPort);
            udpClient = new System.Net.Sockets.UdpClient(localEP);

            //非同期的なデータ受信を開始する
            udpClient.BeginReceive(ReceiveCallback, udpClient);
        }



        //データを受信した時
        private void ReceiveCallback(IAsyncResult ar)
        {
            System.Net.Sockets.UdpClient udp = (System.Net.Sockets.UdpClient)ar.AsyncState;

            //非同期受信を終了する
            System.Net.IPEndPoint remoteEP = null;
            byte[] rcvBytes;
            try
            {
                rcvBytes = udp.EndReceive(ar, ref remoteEP);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("受信エラー({0}/{1})",
                    ex.Message, ex.ErrorCode);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                //すでに閉じている時は終了
                Console.WriteLine("Socketは閉じられています。");
                return;
            }

            //データを文字列に変換する
            string rcvMsg = System.Text.Encoding.UTF8.GetString(rcvBytes);

            //受信したデータと送信者の情報をRichTextBoxに表示する
            string displayMsg = string.Format("[{0} ({1})] > {2}",
                remoteEP.Address, remoteEP.Port, rcvMsg);
            //RichTextBox1.BeginInvoke(new Action<string>(ShowReceivedString), displayMsg);
            Console.WriteLine(displayMsg);

            //再びデータ受信を開始する
            udp.BeginReceive(ReceiveCallback, udp);
        }


        public void Send(byte[] sendBytes)
        {

            //UdpClientを作成する
            if (udpClient == null)
            {
                udpClient = new System.Net.Sockets.UdpClient();
            }



            //非同期的にデータを送信する
            udpClient.BeginSend(sendBytes, sendBytes.Length, "localhost", sendPort, SendCallback, udpClient);
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
