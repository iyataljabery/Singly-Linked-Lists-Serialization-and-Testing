using System;
using System.IO;
using System.Text.Json;

namespace Utility
{
    // SLL implementation
    public class SLL<T> : ILinkedListADT<T>
    {
        private Node<T> head;
        private int count;

        public void Add(T item)
        {
            AddLast(item);
        }

        public void AddFirst(T item)
        {
            Node<T> newNode = new Node<T>(item);
            newNode.Next = head;
            head = newNode;
            count++;
        }

        public void AddLast(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        public void Replace(int index, T newValue)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Invalid index.");

            Node<T> current = head;
            for (int i = 0; i < index; i++)
                current = current.Next;

            current.Value = newValue;
        }

        public int Count()
        {
            return count;
        }

        public T GetValue(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Invalid index.");

            Node<T> current = head;
            for (int i = 0; i < index; i++)
                current = current.Next;

            return current.Value;
        }

        public int IndexOf(T item)
        {
            Node<T> current = head;
            int index = 0;
            while (current != null)
            {
                if (current.Value.Equals(item))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public void Remove(T item)
        {
            if (IsEmpty()) throw new InvalidOperationException("List is empty.");

            if (head.Value.Equals(item))
            {
                head = head.Next;
                count--;
                return;
            }

            Node<T> current = head;
            while (current.Next != null && !current.Next.Value.Equals(item))
            {
                current = current.Next;
            }

            if (current.Next == null)
                throw new ArgumentException("Item not found.");

            current.Next = current.Next.Next;
            count--;
        }

        public void RemoveFirst()
        {
            if (IsEmpty()) throw new InvalidOperationException("List is empty.");
            head = head.Next;
            count--;
        }

        public void RemoveLast()
        {
            if (IsEmpty()) throw new InvalidOperationException("List is empty.");
            if (head.Next == null)
            {
                head = null;
            }
            else
            {
                Node<T> current = head;
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }
                current.Next = null;
            }
            count--;
        }

        // New method to serialize the linked list using JsonSerializer
        public static byte[] Serialize(SLL<T> list)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                JsonSerializer.Serialize(ms, list);
                return ms.ToArray();
            }
        }

        // New method to deserialize the linked list using JsonSerializer
        public static SLL<T> DeserializeSLL(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return JsonSerializer.Deserialize<SLL<T>>(ms);
            }
        }

        // New method to divide the linked list at a given index
        public static (SLL<T> firstHalf, SLL<T> secondHalf) Devide(int index, SLL<T> list)
        {
            SLL<T> firstHalf = new SLL<T>();
            SLL<T> secondHalf = new SLL<T>();

            Node<T> current = list.head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex < index)
                {
                    firstHalf.AddLast(current.Value);
                }
                else
                {
                    secondHalf.AddLast(current.Value);
                }
                currentIndex++;
                current = current.Next;
            }

            return (firstHalf, secondHalf);
        }
    }

    public interface ILinkedListADT<T>
    {
    }

    // Node class for the singly linked list
    [Serializable]
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}
