using System;
using UnitTest.Models;
using Utility;

namespace LinkedListAssignment.Utility
{
    public interface ILinkedListADT
    {
        void Add(SLL value);
        void AddLast(SLL value);
        void AddFirst(SLL value);
        void Replace(SLL value, int index);
        int Count();
        SLL GetValue(int index);
        int IndexOf(SLL value);
        bool Contains(SLL value);
        bool IsEmpty();
        void Clear();
        void Remove(int index);
        void RemoveFirst();
        void RemoveLast();
        void AddLast(User user);
    }
}