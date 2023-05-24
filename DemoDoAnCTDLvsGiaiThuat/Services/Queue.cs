using System;
using System.Collections.Generic;
using System.Text;

namespace DemoDoAnCTDLvsGiaiThuat.Services
{
    internal class Queue<T>
    {

        //fields
        private Node<T> first;
        private Node<T> last;
        private int count;

        //Properties
        public int Count { get => count; }
        //Constructor
        public Queue()
        {
            this.count = 0;
        }

        public Node<T> Enqueue(T value)
        {
            Node<T> result = new Node<T>(value);
            if (first == null)
            {
                this.first = result;
                this.last = result;
            }
            else
            {
                this.last.Next = result;
                this.last = result;
            }
            this.count++;
            return result;
        }
        public T Dequeue()
        {
            T result = default;
            if (count == 1)
            {
                result = this.first.Value;
                this.first = null;
                this.last = null;
                count--;
            }
            else if (count == 2)
            {
                result = this.first.Value;
                this.first = this.first.Next;
                this.last = this.first;
                count--;
            }
            else if (count > 2)
            {
                result = this.first.Value;
                this.first = this.first.Next;
                count--;
            }
            return result;
        }
        public T Peek()
        {
            T result = default;
            if (count!=0)
            {
                result = this.first.Value;
            }
            return result;
        }
    }

}
