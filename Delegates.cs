using System;
using System.Net;
using System.Net.Sockets;

namespace Feralnex.Networking
{
    public delegate void StatusChangedHandler<T>(T status);
    public delegate void PacketReceivedHandler<T, Y>(T sender, Y packet) where Y: Packet;
    public delegate void ConnectedHandler<T>(T sender, Socket socket);
    public delegate void BoundHandler<T>(T sender, Socket socket);
    public delegate void DisconnectedHandler<T>(T sender);
    public delegate void FailedToConnectHandler<T>(T sender, IPEndPoint endPoint, Exception exception);
    public delegate void FailedToBindHandler<T>(T sender, Socket socket, Exception exception);
    public delegate void FailedToSendHandler(Packet packet, Exception exception);
    public delegate void FailedToSendHandler<T>(T sender, Packet packet, Exception exception);
    public delegate void FailedToReceiveHandler(Exception exception);
    public delegate void FailedToReceiveHandler<T>(T sender, Exception exception);
}
