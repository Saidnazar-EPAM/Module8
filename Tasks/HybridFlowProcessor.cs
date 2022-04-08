using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        DoublyLinkedList<T> _doublyLinkedList = new DoublyLinkedList<T>();

        public T Dequeue()
        {
            if (_doublyLinkedList.Length <= 0)
            {
                throw new InvalidOperationException();
            }
            return _doublyLinkedList.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            _doublyLinkedList.Add(item);
        }

        public T Pop()
        {
            if(_doublyLinkedList.Length <= 0)
            {
                throw new InvalidOperationException();
            }
            return _doublyLinkedList.RemoveAt(_doublyLinkedList.Length - 1);
        }

        public void Push(T item)
        {
            _doublyLinkedList.Add(item);
        }
    }
}
