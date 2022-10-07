using System.Collections;
using System.Collections.Generic;

namespace Feralnex.Networking
{
    public class Clients<T, Y> : IEnumerable<Y> where Y: Client
    {
        private LinkedDictionary<T, Y> _clients;

        public int Count => _clients.Count;
        public bool IsEmpty => _clients.Count == 0;

        public Y this[int index]
        {
            get => _clients[index];
            set => _clients[index] = value;
        }

        public Y this[T key]
        {
            get => _clients[key];
            set => _clients[key] = value;
        }

        public Clients()
        {
            _clients = new LinkedDictionary<T, Y>();
        }

        public void Add(T key, Y value) => _clients.Add(key, value);

        public void Remove(T key) => _clients.Remove(key);

        public bool TryGet(T key, out Y value) => _clients.TryGet(key, out value);

        public bool Contains(T key) => _clients.Contains(key);

        public void Clear() => _clients.Clear();

        public IEnumerator<Y> GetEnumerator()
        {
            return _clients.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
