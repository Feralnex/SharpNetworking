using System;
using System.Collections.Generic;

namespace Feralnex.Networking
{
    public struct Event<T> where T : Delegate
    {
        private T _handler;

        public T Handler => _handler;

        public static Event<T> operator +(Event<T> eventHandler, T listener)
        {
            if (listener == null) return eventHandler;
            if (eventHandler._handler == null) eventHandler._handler = listener;
            else eventHandler._handler = Delegate.Combine(eventHandler._handler, listener) as T;

            return eventHandler;
        }

        public static Event<T> operator -(Event<T> eventHandler, T listener)
        {
            if (listener != null && eventHandler._handler != null) eventHandler._handler = Delegate.Remove(eventHandler._handler, listener) as T;

            return eventHandler;
        }
    }

    public struct Event<T, Y> where Y : Delegate
    {
        private Y _handler;
        private Dictionary<T, Event<Y>> _handlers;

        public Y Handler => _handler;

        public Event<Y> this[T key]
        {
            get
            {
                if (_handlers is null) _handlers = new Dictionary<T, Event<Y>>();
                if (!_handlers.TryGetValue(key, out Event<Y> handler)) return new Event<Y>();

                return handler;
            }
            set
            {
                if (_handlers is null) _handlers = new Dictionary<T, Event<Y>>();

                _handlers[key] = value;
            }
        }

        public static Event<T, Y> operator +(Event<T, Y> eventHandler, Y listener)
        {
            if (listener == null) return eventHandler;
            if (eventHandler._handler == null) eventHandler._handler = listener;
            else eventHandler._handler = Delegate.Combine(eventHandler._handler, listener) as Y;

            return eventHandler;
        }

        public static Event<T, Y> operator -(Event<T, Y> eventHandler, Y listener)
        {
            if (listener != null && eventHandler._handler != null) eventHandler._handler = Delegate.Remove(eventHandler._handler, listener) as Y;

            return eventHandler;
        }

        public bool Contains(T key)
        {
            if (_handlers is null) _handlers = new Dictionary<T, Event<Y>>();

            return _handlers.ContainsKey(key);
        }

        public bool TryGet(T key, out Y handler)
        {
            handler = null;

            if (!Contains(key)) return false;

            handler = _handlers[key].Handler;

            return true;
        }
    }
}
