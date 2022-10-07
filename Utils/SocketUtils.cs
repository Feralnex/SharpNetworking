using System.Net.Sockets;

namespace Feralnex.Networking
{
    public static class SocketUtils
    {
        public static bool IsConnected(this Socket socket)
        {
            if (socket == null) return false;

            bool isReadable = socket.Poll(1000, SelectMode.SelectRead);
            bool isDataToReadAvailable = (socket.Available == 0);

            if ((isReadable && isDataToReadAvailable) || !socket.Connected) return false;
            else return true;
        }
    }
}
