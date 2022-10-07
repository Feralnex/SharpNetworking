using System.Collections;
using System.Collections.Generic;

namespace Feralnex.Networking
{
    class LinkedDictionary<T, Y> : IEnumerable<Y>
    {
        private object _padlock;
        private Dictionary<T, Y> _ids;
        private List<Y> _list;
        private List<Node<Y>> _nodes;

        public int Count => _list.Count;
        public bool IsEmpty => _list.Count == 0;

        public Y this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public Y this[T key]
        {
            get => _ids[key];
            set => _ids[key] = value;
        }

        public LinkedDictionary()
        {
            _padlock = new object();
            _ids = new Dictionary<T, Y>();
            _list = new List<Y>();
            _nodes = new List<Node<Y>>();
        }

        public void Add(T key, Y value)
        {
            lock (_padlock)
            {
                Node<Y> node = new Node<Y>(value);

                if (_list.Count != 0)
                {
                    Node<Y> currentNode = _nodes[_nodes.Count - 1];

                    currentNode.Next = node;
                }

                _ids.Add(key, value);
                _list.Add(value);
                _nodes.Add(node);
            }
        }

        public void Remove(T key)
        {
            lock (_padlock)
            {
                if (!_ids.ContainsKey(key)) return;

                Y value = _ids[key];
                int index = _list.IndexOf(value);

                if (index > 1)
                {
                    Node<Y> previousNode = _nodes[index - 1];

                    if (index < _nodes.Count - 1)
                    {
                        Node<Y> nextNode = _nodes[index + 1];

                        previousNode.Next = nextNode;
                    }
                    else previousNode.Next = null;
                }

                _ids.Remove(key);
                _list.RemoveAt(index);
                _nodes.RemoveAt(index);
            }
        }

        public bool TryGetFirst(out Node<Y> first)
        {
            lock (_padlock)
            {
                first = default;

                if (Count > 0)
                {
                    first = _nodes[0];

                    return true;
                }

                return false;
            }
        }

        public bool TryGet(T key, out Y value)
        {
            lock (_padlock) return _ids.TryGetValue(key, out value);
        }

        public bool Contains(T key)
        {
            lock (_padlock) return _ids.ContainsKey(key);
        }

        public void Clear()
        {
            lock (_padlock)
            {
                _ids.Clear();
                _list.Clear();
                _nodes.Clear();
            }
        }

        public IEnumerator<Y> GetEnumerator()
        {
            if (TryGetFirst(out Node<Y> node))
            {
                yield return node.Value;

                while (node.HasNext)
                {
                    node = node.Next;

                    yield return node.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
