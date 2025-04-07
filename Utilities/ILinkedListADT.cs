using System;
using UnitTest.Models;
using Utilites;
using Utility;

namespace Utilites
{
    public class SinglyLinkedList : ILinkedListADT<User>
    {
        private Node head;
        private int size;

        public SinglyLinkedList()
        {
            head = null;
            size = 0;
        }

        public bool IsEmpty() => size == 0;

        public void Clear()
        {
            head = null;
            size = 0;
        }

        public void AddLast(User value)
        {
            Node newNode = new Node(value);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            size++;
        }

        public void AddFirst(User value)
        {
            Node newNode = new Node(value);
            newNode.Next = head;
            head = newNode;
            size++;
        }

        public void Add(User value, int index)
        {
            if (index < 0 || index > size)
                throw new IndexOutOfRangeException("Index out of bounds");

            if (index == 0)
            {
                AddFirst(value);
                return;
            }

            Node newNode = new Node(value);
            Node current = head;
            for (int i = 1; i < index; i++)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
            size++;
        }

        public void Replace(User value, int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException("Index out of bounds");

            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            current.Data = value;
        }

        public int Count() => size;

        public void RemoveFirst()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot remove from an empty list");

            head = head.Next;
            size--;
        }

        public void RemoveLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot remove from an empty list");

            if (size == 1)
            {
                head = null;
            }
            else
            {
                Node current = head;
                while (current.Next?.Next != null)
                {
                    current = current.Next;
                }
                current.Next = null;
            }
            size--;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException("Index out of bounds");

            if (index == 0)
            {
                RemoveFirst();
                return;
            }

            Node current = head;
            for (int i = 1; i < index; i++)
            {
                current = current.Next;
            }
            current.Next = current.Next?.Next;
            size--;
        }

        public User GetValue(int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException("Index out of bounds");

            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        public int IndexOf(User value)
        {
            Node current = head;
            int index = 0;
            while (current != null)
            {
                if (current.Data.Equals(value))
                {
                    return index;
                }
                current = current.Next;
                index++;
            }
            return -1;
        }

        public bool Contains(User value)
        {
            return IndexOf(value) != -1;
        }
    }
}
