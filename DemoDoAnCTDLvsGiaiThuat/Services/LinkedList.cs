using System;
using System.Collections.Generic;
using System.Text;

namespace DemoDoAnCTDLvsGiaiThuat.Services
{
    internal class LinkedList<T>
    {
        //fields
        private Node<T> first;
        private Node<T> last;
        private int count;

        //Properties
        public int Count { get => count; }
        internal Node<T> First { get => first; }
        internal Node<T> Last { get => last; }
        //Constructor
        public LinkedList()
        {
            this.count = 0;
        }

        /// <summary>
        /// Them phan tu dau
        /// </summary>
        /// <param name="value">gia tri phan tu can them vao dau</param>
        /// <returns>Node vua moi them vao</returns>
        public Node<T> AddFirst(T value)
        {
            Node<T> result = new Node<T>(value);
            if (first == null)
            {
                this.first = result;
                this.last = result;
            }
            else
            {
                result.Next = this.first;
                this.first = result;
            }
            this.count++;
            return result;
        }

        public Node<T> AddLast(T value)
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
        public T Get(int index)
        {
            T result = default;
            int _count = 0;

            if (index >= this.count)
            {
                throw new IndexOutOfRangeException();
            }
            for (Node<T> i = this.first; i != null; i = i.Next)
            {
                if (_count == index)
                {
                    result = i.Value;
                    break;
                }
                _count++;
            }
            return result;
        }
        //Tim vi tri cua thang co gia tri dau tien xuat hien trong mang
        public int FindFirst(T value)
        {
            int result = -1;

            for (int i = 0; i < this.count; i++)
            {
                if (this.Get(i).Equals(value))
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        //Tim vi tri cua thang co gia tri cuoi cung xuat hien trong mang
        public int FindLast(T value)
        {
            int result = -1;

            for (int i = 0; i < this.count; i++)
            {
                if (this.Get(i).Equals(value))
                {
                    result = i;
                }
            }
            return result;
        }
        //Remove tai vi tri
        public Node<T> GetNode(int index)
        {
            Node<T> result = default;
            int _count = 0;

            if (index >= this.count)
            {
                throw new IndexOutOfRangeException();
            }
            for (Node<T> i = this.first; i != null; i = i.Next)
            {
                if (_count == index)
                {
                    result = i;
                    break;
                }
                _count++;
            }
            return result;
        }
        //Remove tai vi tri
        public bool Remove(int index)
        {
            bool result = true;

            if (this.count == 0)
            {
                result = false;
            }
            else
            {

                if (index == 0)
                {
                    this.first = this.first.Next;
                }
                else if (index == this.count - 1)
                {
                    this.last = this.GetNode(count - 2);
                }
                else
                {
                    this.GetNode(index - 1).Next = this.GetNode(index + 1);
                }
                this.count--;
            }
            return result;
        }
        //chuyen tu LinkedList qua Array
        public T[] ToArray()
        {
            T[] result = new T[this.count];
            for (int i = 0; i < this.count; i++)
            {
                result[i] = this.Get(i);
            }
            return result;
        }
        //Chuyen tu Array qua lai LinkedList
        public void ParseLinkedList(T[] array)
        {

            for (int i = 0; i < array.Length; i++)
            {
                this.AddLast(array[i]);
            }
        }
    }
}
