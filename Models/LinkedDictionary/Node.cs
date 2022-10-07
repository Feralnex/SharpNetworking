namespace Feralnex.Networking
{
    class Node<T>
    {
        public T Value { get; private set; }
        public Node<T> Next { get; set; }

        public bool HasNext => Next != null;

        public Node(T value)
        {
            Value = value;
            Next = null;
        }

        public Node(T value, Node<T> next)
        {
            Value = value;
            Next = next;
        }
    }
}
