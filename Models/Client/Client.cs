using System;
using System.Net;
using System.Net.Sockets;

namespace Feralnex.Networking
{
    public class Client
    {
        public event Action<Client, int> IdProvided;
        public event StatusChangedHandler<ConnectionStatus> ConnectionStatusChanged;
        public event PacketReceivedHandler<Client, Packet> PacketReceived;
        public event ConnectedHandler<Client> Connected;
        public event BoundHandler<Client> Bound;
        public event DisconnectedHandler<Client> Disconnected;
        public event FailedToConnectHandler<Client> FailedToConnect;
        public event FailedToBindHandler<Client> FailedToBind;
        public event FailedToSendHandler<Client> FailedToSend;
        public event FailedToReceiveHandler<Client> FailedToReceive;

        private ConnectionStatus _connectionStatus;

        private bool OutgoingConnection => Udp.Listener.LocalEndPoint == Tcp.Socket.Client.LocalEndPoint;

        public int Id { get; private set; }
        public TCP Tcp { get; private set; }
        public UDP Udp { get; private set; }

        public ConnectionStatus ConnectionStatus
        {
            get => _connectionStatus;
            protected set
            {
                _connectionStatus = value;

                ConnectionStatusChanged?.Invoke(value);
            }
        }
        public bool IsConnected => _connectionStatus == ConnectionStatus.Connected;

        public Client(int tcpBufferSize = Consts.TCP_BUFFER_SIZE)
        {
            UdpListener udpListener = new UdpListener();

            udpListener.Start();

            _connectionStatus = ConnectionStatus.Disconnected;

            Id = Consts.INITIAL_CLIENT_ID;
            Tcp = new TCP(tcpBufferSize);
            Udp = new UDP(udpListener);

            AddEventListeners(Tcp);
            AddEventListeners(Udp);
        }

        public Client(int id, int tcpBufferSize = Consts.TCP_BUFFER_SIZE)
        {
            UdpListener udpListener = new UdpListener();

            udpListener.Start();

            _connectionStatus = ConnectionStatus.Disconnected;

            Id = id;
            Tcp = new TCP(tcpBufferSize);
            Udp = new UDP(udpListener);

            AddEventListeners(Tcp);
            AddEventListeners(Udp);
        }

        public Client(UdpListener udpListener, int tcpBufferSize = Consts.TCP_BUFFER_SIZE)
        {
            _connectionStatus = ConnectionStatus.Disconnected;

            Id = Consts.INITIAL_CLIENT_ID;
            Tcp = new TCP(tcpBufferSize);
            Udp = new UDP(udpListener);

            AddEventListeners(Tcp);
            AddEventListeners(Udp);
        }

        public Client(int id, UdpListener udpListener, int tcpBufferSize = Consts.TCP_BUFFER_SIZE)
        {
            _connectionStatus = ConnectionStatus.Disconnected;

            Id = id;
            Tcp = new TCP(tcpBufferSize);
            Udp = new UDP(udpListener);

            AddEventListeners(Tcp);
            AddEventListeners(Udp);
        }

        public Client(Client client)
        {
            ConnectionStatusChanged = client.ConnectionStatusChanged;
            PacketReceived = client.PacketReceived;
            Connected = client.Connected;
            Bound = client.Bound;
            Disconnected = client.Disconnected;
            FailedToConnect = client.FailedToConnect;
            FailedToBind = client.FailedToBind;
            FailedToSend = client.FailedToSend;
            FailedToReceive = client.FailedToReceive;

            _connectionStatus = client._connectionStatus;

            Id = client.Id;
            Tcp = client.Tcp;
            Udp = client.Udp;

            AddEventListeners(Tcp);
            AddEventListeners(Udp);

            client.RemoveEventListeners(Tcp);
            client.RemoveEventListeners(Udp);
        }

        ~Client()
        {
            RemoveEventListeners(Tcp);
            RemoveEventListeners(Udp);
        }

        public void Connect(string address, int port)
        {
            ConnectionStatus = ConnectionStatus.Connecting;

            if (address.TryGetIPAddress(out IPAddress ipAddress)) Tcp.Connect(ipAddress, port);
            else ConnectionStatus = ConnectionStatus.FailedToConnect;
        }   

        public void Connect(IPAddress address, int port)
        {
            ConnectionStatus = ConnectionStatus.Connecting;

            Tcp.Connect(address, port);
        }

        public void Bind(Socket socket)
        {
            Tcp.Bind(socket);
        }

        public void Disconnect()
        {
            Tcp.Disconnect();
        }

        private void AddEventListeners(TCP tcp)
        {
            tcp.IdProvided += OnIdProvided;
            tcp.PacketReceived += OnPacketReceived;
            tcp.Connected += OnConnected;
            tcp.Bound += OnBound;
            tcp.Disconnected += OnDisconnected;
            tcp.FailedToConnect += OnFailedToConnect;
            tcp.FailedToBind += OnFailedToBind;
            tcp.FailedToSend += OnFailedToSend;
            tcp.FailedToReceive += OnFailedToReceive;
        }

        private void AddEventListeners(UDP udp)
        {
            udp.PacketReceived += OnPacketReceived;
            udp.FailedToSend += OnFailedToSend;
            udp.FailedToReceive += OnFailedToReceive;
        }

        private void RemoveEventListeners(TCP tcp)
        {
            tcp.IdProvided -= OnIdProvided;
            tcp.PacketReceived -= OnPacketReceived;
            tcp.Connected -= OnConnected;
            tcp.Bound -= OnBound;
            tcp.Disconnected -= OnDisconnected;
            tcp.FailedToConnect -= OnFailedToConnect;
            tcp.FailedToBind -= OnFailedToBind;
            tcp.FailedToSend -= OnFailedToSend;
            tcp.FailedToReceive -= OnFailedToReceive;
        }

        private void RemoveEventListeners(UDP udp)
        {
            udp.PacketReceived -= OnPacketReceived;
            udp.FailedToSend -= OnFailedToSend;
            udp.FailedToReceive -= OnFailedToReceive;
        }

        private void OnIdProvided(TCP sender, int id)
        {
            Id = id;

            IdProvided?.Invoke(this, id);
        }

        private void OnPacketReceived(object sender, Packet packet)
        {
            PacketReceived?.Invoke(this, packet);
        }

        private void OnConnected(TCP sender, Socket socket)
        {
            IPEndPoint localEndPoint = socket.LocalEndPoint as IPEndPoint;
            IPEndPoint remoteEndPoint = socket.RemoteEndPoint as IPEndPoint;

            Udp.Bind(localEndPoint, remoteEndPoint);

            Connected?.Invoke(this, socket);

            ConnectionStatus = ConnectionStatus.Connected;
        }

        private void OnBound(TCP sender, Socket socket)
        {
            IPEndPoint remoteEndPoint = socket.RemoteEndPoint as IPEndPoint;

            Udp.Bind(remoteEndPoint);

            Bound?.Invoke(this, socket);

            ConnectionStatus = ConnectionStatus.Connected;
        }

        private void OnDisconnected(TCP sender)
        {
            if (OutgoingConnection) Udp.Listener.Stop();

            Udp.Disconnect();

            Disconnected?.Invoke(this);

            ConnectionStatus = ConnectionStatus.Disconnected;
        }

        private void OnFailedToConnect(TCP sender, IPEndPoint endPoint, Exception exception)
        {
            FailedToConnect?.Invoke(this, endPoint, exception);

            ConnectionStatus = ConnectionStatus.FailedToConnect;
        }

        private void OnFailedToBind(TCP sender, Socket socket, Exception exception)
        {
            FailedToBind?.Invoke(this, socket, exception);

            ConnectionStatus = ConnectionStatus.FailedToConnect;
        }

        private void OnFailedToSend(object sender, Packet packet, Exception exception)
        {
            FailedToSend?.Invoke(this, packet, exception);
        }

        private void OnFailedToReceive(object sender, Exception exception)
        {
            FailedToReceive?.Invoke(this, exception);
        }
    }
}
