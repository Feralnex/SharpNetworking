namespace Feralnex.Networking
{
    public class MissingDataException : PacketException
    {
        public MissingDataException() : base() { }
        public MissingDataException(string message) : base(message) { }
    }
}
