using System;
using System.Collections;
using System.Collections.Generic;

namespace Feralnex.Networking
{
    class OrderedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _orderedList;

        public int Count
        {
            get
            {
                return _orderedList.Count;
            }
        }

        public T this[int index]
        {
            get
            {
                return _orderedList[index];
            }
        }

        public OrderedList()
        {
            _orderedList = new List<T>();
        }

        public OrderedList(T item)
        {
            _orderedList = new List<T>();

            Add(item);
        }

        public OrderedList(IEnumerable<T> items)
        {
            _orderedList = new List<T>(items);
        }

        public void Add(T item)
        {
            if (_orderedList.Count == 0)
            {
                _orderedList.Add(item);

                return;
            }
            if (_orderedList[_orderedList.Count - 1].CompareTo(item) <= 0)
            {
                _orderedList.Add(item);

                return;
            }
            if (_orderedList[0].CompareTo(item) >= 0)
            {
                _orderedList.Insert(0, item);

                return;
            }

            int index = _orderedList.BinarySearch(item);

            if (index < 0) index = ~index;

            _orderedList.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _orderedList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _orderedList.RemoveAt(index);
        }

        public int IndexOf(T item)
        {
            return _orderedList.IndexOf(item);
        }

        public bool Contains(T item)
        {
            return _orderedList.Contains(item);
        }

        public void Clear()
        {
            _orderedList.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _orderedList)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
