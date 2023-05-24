using System;
using System.Collections.Generic;
using System.Text;

namespace DemoDoAnCTDLvsGiaiThuat.Services
{
    internal class Node<T>
    {
        private T value;
        private Node<T> next;

        public T Value { get => value; set => this.value = value; }
        internal Node<T> Next { get => next; set => next = value; }
        public Node(T value)
        {
            this.value = value;
            this.next = null;

        }
    }
}

