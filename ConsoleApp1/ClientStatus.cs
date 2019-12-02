using System.Net;

namespace Network
{
    class ClientStatus
    {
        public IPAddress address;
        public int port;
        public string nickName;
        public int networkId;

        public void SetData(IPAddress address, int port, string nickName)
        {
            this.address = address;
            this.port = port;
            this.nickName = nickName;
        }

        public void SetNetworkId(int networkId)
        {
            this.networkId = networkId;
        }
    }
}
