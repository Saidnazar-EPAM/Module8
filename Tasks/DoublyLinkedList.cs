using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        public int Length { get; private set; }
        public DoublyLinkedListNode<T> Head { get; set; }
        public DoublyLinkedListNode<T> Tail { get; set; }

        public void Add(T e)
        {
            AddAt(Length, e);
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException($"Max index is {Length - 1}");
            }

            var node = new DoublyLinkedListNode<T>(e);

            if (Head is null)
            {
                Head = node;
                Tail = node;
                Length = 1;
                return;
            }

            if (index == 0)
            {
                Head.Previous = node;
                node.Next = Head;
                node.Previous = null;
                Head = node;
            }
            else if (index >= Length)
            {
                node.Next = null;
                Tail.Next = node;
                node.Previous = Tail;
                Tail = node;
            }
            else
            {
                var tempNode = Head;
                int i = 0;
                while (i < index - 1)
                {
                    tempNode = tempNode.Next;
                    i++;
                }
                node.Next = tempNode.Next;
                node.Previous = tempNode;
                tempNode.Next = node;
                node.Next.Previous = node;
            }
            Length++;
        }


        public T ElementAt(int index)
        {
            var node = FindNodeByIndex(index);
            return node.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnum<T>(this);
        }

        public void Remove(T item)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Data == null && item == null || current.Data != null && current.Data.Equals(item))
                {
                    RemoveNode(current);
                    return;
                }
                current = current.Next;
            }
        }

        public T RemoveAt(int index)
        {
            var node = FindNodeByIndex(index);
            RemoveNode(node);
            return node.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            i++;
            Current = Current.Next;
            return i < Length;
        }
        private int i = -1;
        public DoublyLinkedListNode<T> Current { get; set; }

        private void RemoveNode(DoublyLinkedListNode<T> node)
        {
            if (node == Head)
            {
                Head = Head.Next;
                Head.Previous = null;
                Length--;
                return;
            }

            if (node == Tail)
            {
                Tail = Tail.Previous;
                Tail.Next = null;
                Length--;
                return;
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            Length--;
        }

        private DoublyLinkedListNode<T> FindNodeByIndex(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException($"Max index is {Length - 1}");
            }

            var current = Head;
            for (var i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current;
        }
    }

    public class DoublyLinkedListNode<T>
    {

        public DoublyLinkedListNode(T data) => Data = data;

        public T Data { get; }

        public DoublyLinkedListNode<T> Next { get; set; }

        public DoublyLinkedListNode<T> Previous { get; set; }
    }

    public class DoublyLinkedListEnum<T> : IEnumerator<T>
    {
        public DoublyLinkedList<T> list;
        private DoublyLinkedListNode<T> _current;
        int position = -1;

        public DoublyLinkedListEnum(DoublyLinkedList<T> list)
        {
            this.list = list;
        }

        public bool MoveNext()
        {
            position++;
            if (_current == null)
            {
                _current = list.Head;
            }
            else
            {
                _current = _current.Next;
            }
            return (position < list.Length);
        }

        public void Reset()
        {
            position = -1;
            _current = null;
        }

        public void Dispose()
        {
 
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        T IEnumerator<T>.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return _current.Data;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
