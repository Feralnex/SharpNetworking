using System;
using System.Net;
using System.Net.Sockets;

namespace Feralnex.Networking
{
    public class Server
    {
        public event Action<string> NewMessage;
        public event StatusChangedHandler<ServerStatus> StatusChanged;
        public event Action Initializing;
        public event Action Launching;
        public event Action Terminating;
        public event PacketReceivedHandler<Client, Packet> PacketReceived;
        public event PacketReceivedHandler<IPEndPoint, Packet> UdpPacketReceived;
        public event BoundHandler<Client> Bound;
        public event DisconnectedHandler<Client> Disconnected;
        public event FailedToBindHandler<Client> FailedToBind;
        public event FailedToSendHandler<Client> FailedToSendPacket;
        public event FailedToSendHandler<IPEndPoint> FailedToSendUdpPacket;
        public event FailedToReceiveHandler<Client> FailedToReceivePacket;
        public event FailedToReceiveHandler FailedToReceiveUdpPacket;

        private IdPool _idPool;
        private ServerStatus _serverStatus;

        public int Port { get; private set; }

        public Clients<int, Client> Clients { get; private set; }
       
        public TcpListener TcpListener { get; private set; }
        public UdpListener UdpListener { get; private set; }

        public ServerStatus ServerStatus
        {
            get => _serverStatus;
            private set
            {
                _serverStatus = value;

                StatusChanged?.Invoke(value);
            }
        }
        public bool WasInitialized { get; private set; }

        public Server(int port)
        {
            Port = port;
            ServerStatus = ServerStatus.Idle;
            WasInitialized = false;
        }

        public void Initialize()
        {
            ServerStatus currentStatus = ServerStatus;

            try
            {
                if (ServerStatus != ServerStatus.Idle &&
                    ServerStatus != ServerStatus.Terminated) return;

                ServerStatus = ServerStatus.Initializing;

                OnNewMessage(Consts.INITIALIZING_SERVER);

                InitializeListeners();
                if (!WasInitialized) InitializeData();

                Initializing?.Invoke();

                WasInitialized = true;
                ServerStatus = ServerStatus.Initialized;

                OnNewMessage(Consts.SERVER_INITIALIZED);
            }
            catch (Exception exception)
            {
                ServerStatus = currentStatus;

                OnNewMessage(exception.ToString());
            }
        }

        public void Launch()
        {
            ServerStatus currentStatus = ServerStatus;

            try
            {
                if (ServerStatus != ServerStatus.Initialized) return;

                ServerStatus = ServerStatus.Launching;

                OnNewMessage(Consts.LAUNCHING_SERVER);

                RunListeners();

                Launching?.Invoke();

                ServerStatus = ServerStatus.Launched;

                OnNewMessage(Consts.SERVER_LAUNCHED);
            }
            catch (Exception exception)
            {
                ServerStatus = currentStatus;

                OnNewMessage(exception.ToString());
            }
        }

        public void Terminate()
        {
            ServerStatus currentStatus = ServerStatus;

            try
            {
                if (ServerStatus != ServerStatus.Launched) return;

                ServerStatus = ServerStatus.Terminating;

                OnNewMessage(Consts.TERMINATING_SERVER);

                TerminateListeners();

                foreach (Client client in Clients)
                {
                    OnNewMessage(string.Format(Consts.DISCONNECTING_CLIENT_SCHEMA, client.Id));

                    client.Disconnect();
                }

                Terminating?.Invoke();

                ServerStatus = ServerStatus.Terminated;

                OnNewMessage(Consts.SERVER_TERMINATED);
            }
            catch (Exception exception)
            {
                ServerStatus = currentStatus;

                OnNewMessage(exception.ToString());
            }
        }

        public void Terminate(string reason)
        {
            OnNewMessage(string.Format(Consts.SERVER_WILL_TERMINATE_SCHEMA, reason));

            Terminate();
        }

        protected void OnNewMessage(string message) =>  NewMessage?.Invoke(message);

        private void InitializeListeners()
        {
            TcpListener = new TcpListener(IPAddress.Any, Port);
            UdpListener = new UdpListener(Port);

            AddEventListeners(UdpListener);
        }

        private void InitializeData()
        {
            _idPool = new IdPool();

            Clients = new Clients<int, Client>();
        }

        private void RunListeners()
        {
            TcpListener.Start();
            UdpListener.Start();
            TcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
        }

        private void TerminateListeners()
        {
            TcpListener.Stop();
            UdpListener.Stop();

            RemoveEventListeners(UdpListener);
        }

        private void TCPConnectCallback(IAsyncResult result)
        {
            if (ServerStatus != ServerStatus.Launched) return;

            TcpClient client = TcpListener.EndAcceptTcpClient(result);
            int clientId = _idPool.GetId();
            TcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            OnNewMessage(string.Format(Consts.INCOMING_CONNECTION_SCHEMA, client.Client.RemoteEndPoint));

            EstablishConnectionWithClient(client, clientId);
        }

        private void EstablishConnectionWithClient(TcpClient tcpClient, int clientId)
        {
            Client client = new Client(clientId, UdpListener);

            Clients.Add(clientId, client);
            AddEventListeners(client);

            client.Bind(tcpClient.Client);
        }

        private void AddEventListeners(UdpListener udpListener)
        {
            udpListener.PacketReceived += OnPacketReceived;
            udpListener.FailedToSend += OnFailedToSend;
            udpListener.FailedToReceive += OnFailedToReceive;
        }

        private void AddEventListeners(Client client)
        {
            client.PacketReceived += OnPacketReceived;
            client.Bound += OnBound;
            client.Disconnected += OnDisconnected;
            client.FailedToBind += OnFailedToBind;
            client.FailedToSend += OnFailedToSend;
            client.FailedToReceive += OnFailedToReceive;
        }

        private void RemoveEventListeners(UdpListener udpListener)
        {
            udpListener.PacketReceived -= OnPacketReceived;
            udpListener.FailedToSend -= OnFailedToSend;
            udpListener.FailedToReceive -= OnFailedToReceive;
        }

        private void RemoveEventListeners(Client client)
        {
            client.PacketReceived -= OnPacketReceived;
            client.Bound -= OnBound;
            client.Disconnected -= OnDisconnected;
            client.FailedToBind -= OnFailedToBind;
            client.FailedToSend -= OnFailedToSend;
            client.FailedToReceive -= OnFailedToReceive;
        }

        private void OnPacketReceived(Client client, Packet packet) => PacketReceived?.Invoke(client, packet);

        private void OnPacketReceived(IPEndPoint endPoint, Packet packet) => UdpPacketReceived?.Invoke(endPoint, packet);

        private void OnBound(Client client, Socket socket)
        {
            Id(client);

            Bound?.Invoke(client, socket);
        }

        private void OnDisconnected(Client client)
        {
            OnNewMessage(string.Format(Consts.CLIENT_DISCONNECTED_SCHEMA, client.Id));

            RemoveEventListeners(client);

            Clients.Remove(client.Id);

            _idPool.ReleaseId(client.Id);

            Disconnected?.Invoke(client);
        }

        private void OnFailedToBind(Client client, Socket socket, Exception exception)
        {
            RemoveEventListeners(client);

            _idPool.ReleaseId(client.Id);

            FailedToBind?.Invoke(client, socket, exception);
        }

        private void OnFailedToSend(Client client, Packet packet, Exception exception) => FailedToSendPacket?.Invoke(client, packet, exception);

        private void OnFailedToSend(IPEndPoint client, Packet packet, Exception exception) => FailedToSendUdpPacket?.Invoke(client, packet, exception);

        private void OnFailedToReceive(Client client, Exception exception) => FailedToReceivePacket?.Invoke(client, exception);

        private void OnFailedToReceive(Exception exception) => FailedToReceiveUdpPacket?.Invoke(exception);

        private void Id(Client client)
        {
            using (Request request = new Request(SenderType.Server, ServerPacket.Id))
            {
                OnNewMessage(string.Format(Consts.PROVIDING_ID_TO_CLIENT_SCHEMA, client.Id));

                request.Write(client.Id);

                client.Tcp.Send(request);
            }
        }
    }
}
