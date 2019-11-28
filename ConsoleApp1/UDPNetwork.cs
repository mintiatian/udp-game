using System;

namespace Network
{
    class UDPNetwork : UDPBase
    {
        public UDPNetwork(int setPort, int setServerPort) : base(setPort)
        {
            sendPort = setServerPort;

            Console.WriteLine("MyPort:" + myPort);
            Console.WriteLine("ServerPort:" + sendPort);
        }
    }
}
