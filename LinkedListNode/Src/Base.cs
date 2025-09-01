using System.Collections;
class ElementNotInListException(string message) : Exception(message) { }
class LengthNotEqualException(string message) : Exception(message) {}


namespace LinkedListNode.Src
{
    class BaseList<T> : IEnumerable<T>
    {
        public class Node
        {
            public T? Value { get; set; } = default!;
            public Node? Next { get; set; }
        }
        public Node? Base = null;

        public void Add(T item)
        {
            if (Base == null)
            {
                Base = new()
                {
                    Value = item,
                    Next = new()
                };
            }
            else
            {
                Node x = Base;
                while (x.Next != null)
                {
                    x = x.Next;
                }
                x.Value = item;
                x.Next = new();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node? x = Base;

            if (x != null)
            {
                while (x.Next != null)
                {
                    if (x.Value != null)
                        yield return x.Value;
                    x = x.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Length
        {
            get
            {
                if (Base == null)
                {
                    return 0;
                }
                else if (Base.Next == null)
                {
                    return 1;
                }
                else
                {
                    Node current = Base;
                    int i = 1;
                    while (current.Next != null)
                    {
                        i++;
                        current = current.Next;
                    }
                    return i;
                }
            }
        }

        public void RemoveAt(int index)
        {
            if (Base == null || index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the list.");
            }
            if (index == 0)
            {
                Base = Base.Next;
            }
            else if (index == 1)
            {
                if (Base.Next != null)
                {
                    Base.Next = Base.Next.Next;
                }
            }
            else
            {
                Node current = Base;
                int i = 1;
                while (current.Next != null)
                {
                    i++;
                    current = current.Next;
                    if (i == index && current.Next != null)
                    {
                        current.Next = current.Next.Next;
                        break;
                    }
                }
            }
        }

        public void Remove(T value)
        {
            int i = 0;
            if (value != null)
            {
                foreach (T item in this)
                {
                    if (value.Equals(item))
                    {
                        RemoveAt(i);
                        break;
                    }
                    i++;
                }
                throw new ElementNotInListException("Value must be in list.");
            }
        }

        public void RemoveFirst()
        {
            if (Length != 0)
            {
                RemoveAt(0);
            }
        }

        public void RemoveLast()
        {
            if (Length != 0)
            {
                RemoveAt(Length - 1);
            }
        }

        public T this[int index]
        {
            get
            {
                if (Base == null || index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException("Index was outside the bounds of the list.");
                }
                if (index == 0 && Base.Value != null)
                {
                    return Base.Value;
                }
                else
                {
                    Node current = Base;
                    int i = 0;
                    while (current.Next != null)
                    {
                        i++;
                        current = current.Next;
                        if (i == index && current.Next != null)
                        {
                            if (current.Value != null)
                                return current.Value;
                        }
                    }
                }
                throw new IndexOutOfRangeException("Index was outside the bounds of the list.");
            }

            set
            {
                if (Base == null || index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException("Index was outside the bounds of the list.");
                }
                if (index == 0 && Base.Value != null)
                {
                    Base.Value = value;
                }
                else
                {
                    Node current = Base;
                    int i = 0;
                    while (current.Next != null)
                    {
                        i++;
                        current = current.Next;
                        if (i == index && current.Next != null)
                        {
                            current.Value = value;
                        }
                    }
                }
                throw new IndexOutOfRangeException("Index was outside the bounds of the list.");
            }
        }

        public void AddLeft(T value)
        {
            if (Length == 0)
            {
                Base = new()
                {
                    Value = value,
                    Next = new()
                };
            }
            else
            {
                if (Base != null)
                {
                    Base = new()
                    {
                        Value = value,
                        Next = Base
                    };

                }
            }
        }

        public override string ToString()
        {
            if (Base != null)
            {
                switch (Length)
                {
                    case 1:
                        return $"{Base.Value} -> end;";
                    default:
                        string join = "";
                        foreach (T i in this)
                        {
                            join += $"{i} -> ";
                        }
                        return join += "end;";
                }
            }
            else
            {
                return "";
            }
        }
        public bool Contains(T value)
        {
            foreach (T item in this)
            {
                if (item != null)
                {
                    if (item.Equals(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int Index(T value)
        {
            int i = 0;
            foreach (T x in this)
            {
                if (value != null && value.Equals(x))
                {
                    return i;
                }
                i++;
            }
            throw new ElementNotInListException($"Item, {value} doesn't exist in list.");
        }

        public static implicit operator BaseList<T>(List<T> values)
        {
            BaseList<T> x = [.. values];
            return x;
        }

        public void Clear() => Base = null;

        public void InsertAt(int index, T value)
        {
            if (Base != null)
            {
                if (index == 0)
                {
                    AddLeft(value);
                }
                else if (index == Length - 1)
                {
                    Add(value);
                }
                else
                {
                    Node current = Base;
                    int i = 1;
                    while (current.Next != null)
                    {
                        if (i == index)
                        {
                            Node node = new(){Value=value,Next = current.Next};
                            current.Next = node;
                            break;
                        }
                        current = current.Next;
                        i++;
                    }
                }
            }
        }
    }

    interface ILinkedIterable<T,U>
    {
        public void Add(T item);
        public bool Contains(T value);
        public void RemoveAt(int index);
        public void Remove(T item);
        public int Index(T item);

        public T this[U index] { get; set; }
    }

    static class BaseList
    {
        public static BaseList<T> Select<T>(BaseList<T> values, Func<T, bool> x)
        {
            BaseList<T> new_list = [];
            foreach (T i in values)
            {
                if (x(i))
                {
                    new_list.Add(i);
                }
            }
            return new_list;
        }

        public static void Select<T>(ref BaseList<T> values, Func<T, bool> x)
        {
            values.Clear();
            foreach (T i in values)
            {
                if (x(i))
                {
                    values.Add(i);
                }
            }
        }

        public static BaseList<T> OppositeSelect<T>(BaseList<T> values, Func<T, bool> x)
        {
            BaseList<T> new_list = [];
            foreach (T i in values)
            {
                if (!x(i))
                {
                    new_list.Add(i);
                }
            }
            return new_list;
        }

        public static void OppositeSelect<T>(ref BaseList<T> values, Func<T, bool> x)
        {
            values.Clear();
            foreach (T i in values)
            {
                if (!x(i))
                {
                    values.Add(i);
                }
            }
        }


        public static void InRange(int range, Action<int> action)
        {
            for (int i = 0; i < range; i++)
            {
                action(i);
            }
        }

        public static BaseList<int> Range(int range)
        {
            BaseList<int> x = [];
            for (int i = 0; i < range; i++)
            {
                x.Add(i);
            }
            return x;
        }

        public static BaseList<int> Range(int starting_range, int ending_range)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(starting_range, ending_range);
            BaseList<int> x = [];
            for (int i = starting_range; i < ending_range; i++)
            {
                x.Add(i);
            }
            return x;
        }

        public static bool Equals<T>(BaseList<T> list1, BaseList<T> list2)
        {
            if (list1.Length != list2.Length)
            {
                throw new LengthNotEqualException("The two lists to compare must be equal.");
            }
            else
            {
                foreach ((T, T) i in Enumerable.Zip(list1, list2))
                {
                    if (i.Item1 != null && i.Item2 != null)
                    {
                        if (!i.Item1.Equals(i.Item2))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public static BaseList<T> Reverse<T>(BaseList<T> values)
        {
            BaseList<T> to_return = [];
            foreach (T i in values)
            {
                to_return.AddLeft(i);
            }
            return to_return;
        }

        public static void Reverse<T>(ref BaseList<T> values)
        {
            BaseList<T> to_return = [];
            foreach (T i in values)
            {
                to_return.AddLeft(i);
            }
            values = to_return;
        }

        public static void CopyFrom<T>(BaseList<T> refrence, out BaseList<T> copied)
        {
            copied = [];
            copied.Clear();
            foreach (T i in refrence)
            {
                copied.Add(i);
            }
        }

        public static IEnumerable<int> GenRange(int range)
        {
            for (int i = 0; i < range; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<int> GenRange(int starting_range, int ending_range)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(starting_range, ending_range);
            for (int i = starting_range; i < ending_range; i++)
            {
                yield return i;
            }
        }
    }
}