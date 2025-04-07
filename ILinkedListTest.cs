using NUnit.Framework;
using System;

namespace LinkedListTests
{
    public class LinkedListTest
    {
        [Test]
        public void Test_EmptyList_ShouldBeEmpty()
        {
            var list = new SLL<string>();
            Assert.IsTrue(list.IsEmpty());
        }

        [Test]
        public void Test_Prepend_AddFirst()
        {
            var list = new SLL<string>();
            list.AddFirst("B");
            list.AddFirst("A");
            Assert.AreEqual("A", list.GetValue(0));
        }

        [Test]
        public void Test_Append_AddLast()
        {
            var list = new SLL<string>();
            list.AddLast("A");
            list.AddLast("B");
            Assert.AreEqual("B", list.GetValue(1));
        }

        [Test]
        public void Test_InsertAtIndex()
        {
            var list = new SLL<string>();
            list.AddLast("A");
            list.Add("B", 1);
            Assert.AreEqual("B", list.GetValue(1));
        }

        [Test]
        public void Test_Replace()
        {
            var list = new SLL<string>();
            list.AddLast("A");
            list.Replace(0, "Z");
            Assert.AreEqual("Z", list.GetValue(0));
        }

        [Test]
        public void Test_RemoveFirst()
        {
            var list = new SLL<string>();
            list.AddLast("A");
            list.AddLast("B");
            list.RemoveFirst();
            Assert.AreEqual("B", list.GetValue(0));
        }

        [Test]
        public void Test_RemoveLast()
        {
            var list = new SLL<string>();
            list.AddLast("A");
            list.AddLast("B");
            list.RemoveLast();
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual("A", list.GetValue(0));
        }

        [Test]
        public void Test_RemoveFromMiddle()
        {
            var list = new SLL<string>();
            list.AddLast("A");
            list.AddLast("B");
            list.AddLast("C");
            list.Remove("B");
            Assert.AreEqual("C", list.GetValue(1));
        }

        [Test]
        public void Test_GetExistingItem()
        {
            var list = new SLL<string>();
            list.AddLast("X");
            var item = list.GetValue(0);
            Assert.AreEqual("X", item);
        }

        [Test]
        public void Test_AdditionalFeature_Divide()
        {
            var list = new SLL<string>();
            list.AddLast("A");
            list.AddLast("B");
            list.AddLast("C");
            list.AddLast("D");

            var (first, second) = SLL<string>.Divide(2, list);
            Assert.AreEqual("A", first.GetValue(0));
            Assert.AreEqual("C", second.GetValue(0));
        }
    }

    internal class SLL<T>
    {
        private Node<T> head;
        private int count;

        public SLL()
        {
            head = null;
            count = 0;
        }

        internal static (SLL<T> first, SLL<T> second) Divide(int index, SLL<T> list)
        {
            var firstList = new SLL<T>();
            var secondList = new SLL<T>();

            var current = list.head;
            int i = 0;

            while (current != null)
            {
                if (i < index)
                    firstList.AddLast(current.Value);
                else
                    secondList.AddLast(current.Value);

                current = current.Next;
                i++;
            }

            return (firstList, secondList);
        }

        internal void AddFirst(T value)
        {
            var newNode = new Node<T>(value);
            newNode.Next = head;
            head = newNode;
            count++;
        }

        internal void AddLast(T value)
        {
            var newNode = new Node<T>(value);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                var current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        internal void Add(T value, int index)
        {
            if (index > count || index < 0) throw new IndexOutOfRangeException();

            if (index == 0)
            {
                AddFirst(value);
                return;
            }

            var newNode = new Node<T>(value);
            var current = head;
            for (int i = 0; i < index - 1; i++)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
            count++;
        }

        internal T GetValue(int index)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();

            var current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Value;
        }

        internal void Replace(int index, T value)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();

            var current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            current.Value = value;
        }

        internal bool IsEmpty()
        {
            return count == 0;
        }

        internal void RemoveFirst()
        {
            if (head != null)
            {
                head = head.Next;
                count--;
            }
        }

        internal void RemoveLast()
        {
            if (head == null) return;

            if (head.Next == null)
            {
                head = null;
            }
            else
            {
                var current = head;
                while (current.Next != null && current.Next.Next != null)
                {
                    current = current.Next;
                }
                current.Next = null;
            }
            count--;
        }

        internal void Remove(T value)
        {
            if (head == null) return;

            if (head.Value.Equals(value))
            {
                head = head.Next;
                count--;
                return;
            }

            var current = head;
            while (current.Next != null && !current.Next.Value.Equals(value))
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
                count--;
            }
        }

        internal int Count()
        {
            return count;
        }

        internal class Node<TValue>
        {
            public TValue Value { get; set; }
            public Node<TValue> Next { get; set; }

            public Node(TValue value)
            {
                Value = value;
                Next = null;
            }
        }
    }
}
