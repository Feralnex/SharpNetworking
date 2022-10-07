using System;

namespace Feralnex.Networking
{
    public class PacketException : Exception
    {
        public PacketException() : base() { }
        public PacketException(string message) : base(message) { }
    }

}
