namespace Feralnex.Networking
{
    static class Consts
    {
        public const string SERVER_WILL_TERMINATE_SCHEMA = "The server will terminate. Reason: {0}";
        public const string INCOMING_CONNECTION_SCHEMA = "Incoming connection from {0}...";
        public const string PROVIDING_ID_TO_CLIENT_SCHEMA = "Providing Id to client {0}...";
        public const string PACKET_SCHEMA = "[PacketType: {0}, SenderType: {1}, Supervised: {2}, Id: {3}]";
        public const string DISCONNECTING_CLIENT_SCHEMA = "Diconnecting client {0}";
        public const string CLIENT_DISCONNECTED_SCHEMA = "Client {0} has disconnected";

        public const string INITIALIZING_SERVER = "Initializing server...";
        public const string SERVER_INITIALIZED = "Server initialized...";
        public const string LAUNCHING_SERVER = "Launching server...";
        public const string SERVER_LAUNCHED = "Server launched...";
        public const string TERMINATING_SERVER = "Terminating server...";
        public const string SERVER_TERMINATED = "Server terminated...";

        public const int INITIAL_CLIENT_ID = -1;
        public const int TCP_BUFFER_SIZE = 4096;

        public const int BYTE_LENGTH_IN_BYTES = 1;
        public const int BOOL_LENGTH_IN_BYTES = 1;
        public const int INT16_LENGTH_IN_BYTES = 2;
        public const int INT32_LENGTH_IN_BYTES = 4;
        public const int INT64_LENGTH_IN_BYTES = 8;
        public const int FLOAT_LENGTH_IN_BYTES = 4;
        public const int DOUBLE_LENGTH_IN_BYTES = 8;
    }
}
