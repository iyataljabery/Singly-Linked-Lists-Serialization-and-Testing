using System;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Utilites.Tests
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Serializes (encodes) users.
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="fileName">File name to serialize to</param>
        public static void SerializeUsers(ILinkedListADT<User> users, string fileName)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<User>));
            using (FileStream stream = File.Create(fileName))
            {
                var userList = new List<User>();

                // Convert the ILinkedListADT to a list
                var current = users.Head;
                while (current != null)
                {
                    userList.Add(current.Value);
                    current = current.Next;
                }

                serializer.WriteObject(stream, userList);
            }
        }

        /// <summary>
        /// Deserializes (decodes) users.
        /// </summary>
        /// <param name="fileName">File name to deserialize from</param>
        /// <returns>Deserialized list of users</returns>
        public static ILinkedListADT<User> DeserializeUsers(string fileName)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<User>));
            using (FileStream stream = File.OpenRead(fileName))
            {
                var userList = (List<User>)serializer.ReadObject(stream);
                ILinkedListADT<User> users = new SinglyLinkedList();

                foreach (var user in userList)
                {
                    users.AddLast(user);
                }

                return users;
            }
        }
    }

    public interface ILinkedListADT<T>
    {
        void AddLast(T item);
        void Clear();
        int Count();
        T GetValue(int index);
        LinkedListNode<T> Head { get; }
    }

    public class LinkedListNode<T>
    {
        public T Value { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public class SinglyLinkedList : ILinkedListADT<User>
    {
        public LinkedListNode<User> Head { get; private set; }
        private int count;

        public SinglyLinkedList()
        {
            Head = null;
            count = 0;
        }

        public void AddLast(User user)
        {
            var newNode = new LinkedListNode<User>(user);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                var current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        public void Clear()
        {
            Head = null;
            count = 0;
        }

        public int Count() => count;

        public User GetValue(int index)
        {
            var current = Head;
            int currentIndex = 0;
            while (current != null && currentIndex < index)
            {
                current = current.Next;
                currentIndex++;
            }

            if (current == null) throw new IndexOutOfRangeException();

            return current.Value;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
