namespace Feralnex.Networking
{
    public class IncompletePacketException : PacketException
    {
        public IncompletePacketException() : base() { }
        public IncompletePacketException(string message) : base(message) { }
    }
}
