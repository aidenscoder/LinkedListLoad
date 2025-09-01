
using System.Collections;
class KeyNotInDictException(string message) : Exception(message) { }

namespace LinkedListNode.Src
{
    class Dict<T, U> : IEnumerable<KeyValuePair<T, U>>
    {
        protected BaseList<KeyValuePair<T, U>> dict = [];

        public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T key, U value)
        {
            if (!dict.Contains(new(key, value)))
                dict.Add(new(key, value));
        }

        public void Add((T, U) kvp)
        {
            if (!dict.Contains(new(kvp.Item1, kvp.Item2)))
                dict.Add(new(kvp.Item1, kvp.Item2));
        }

        public void Remove(T key)
        {
            int i = 0;
            foreach (KeyValuePair<T, U> kvp in this)
            {
                if (kvp.Key != null)
                    if (kvp.Key.Equals(key))
                    {
                        dict.Remove(new(kvp.Key, kvp.Value));
                    }
                i++;
            }
            throw new KeyNotInDictException("Key not in dict.");
        }

        public U this[T key]
        {
            get
            {
                foreach (KeyValuePair<T, U> kvp in this)
                {
                    if (kvp.Key != null)
                        if (kvp.Key.Equals(key))
                        {
                            return kvp.Value;
                        }
                }
                throw new KeyNotInDictException("Key not in dict.");
            }
            set
            {
                int i = 0;
                foreach (KeyValuePair<T, U> kvp in this)
                {
                    if (kvp.Key != null)
                        if (kvp.Key.Equals(key))
                        {
                            dict[i] = new(kvp.Key, value);
                            return;
                        }
                    i++;
                }
                dict.Add(new(key, value));
            }
        }

        public override string ToString()
        {
            string Repr = "{";
            foreach (KeyValuePair<T, U> kvp in dict)
            {
                Repr += $"({kvp.Key}:{kvp.Value})";
            }
            return Repr + "}";
        }
    }

}