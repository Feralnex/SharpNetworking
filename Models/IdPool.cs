namespace Feralnex.Networking
{
    class IdPool
    {
        private int _nextIdValue;
        private OrderedList<int> _availablePool;
        private OrderedList<int> _inUsePool;

        public IdPool()
        {
            _nextIdValue = 0;

            _availablePool = new OrderedList<int>();
            _inUsePool = new OrderedList<int>();
        }

        public virtual int GetId()
        {
            lock (_availablePool)
            {
                if (_availablePool.Count != 0)
                {
                    int id = _availablePool[0];

                    _inUsePool.Add(id);
                    _availablePool.RemoveAt(0);

                    return id;
                }
                else
                {
                    while (_inUsePool.Contains(_nextIdValue)) _nextIdValue++;

                    _inUsePool.Add(_nextIdValue);

                    return _nextIdValue;
                }
            }
        }

        public virtual void ReserveId(int id)
        {
            lock (_availablePool)
            {
                if (_inUsePool.Contains(id)) return;
                if (_availablePool.Contains(id)) _availablePool.Remove(id);

                _inUsePool.Add(id);
            }
        }

        public void ReleaseId(int id)
        {
            lock (_availablePool)
            {
                _availablePool.Add(id);
                _inUsePool.Remove(id);
            }
        }

        public void Reset()
        {
            lock (_availablePool)
            {
                _availablePool.Clear();
                _inUsePool.Clear();
            }
        }
    }
}
