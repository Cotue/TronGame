using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace TronPlay
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        public Node<T> head;
        public Node<T> tail;
        public int count;

        public LinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }

            count++;
        }

        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head = newNode;
            }

            count++;
        }

        public bool Remove(T data)
        {
            if (head == null) return false;

            if (head.Data.Equals(data))
            {
                head = head.Next;
                if (head == null) tail = null;
                count--;
                return true;
            }

            Node<T> current = head;
            while (current.Next != null && !current.Next.Data.Equals(data))
            {
                current = current.Next;
            }

            if (current.Next == null) return false;

            current.Next = current.Next.Next;
            if (current.Next == null) tail = current;
            count--;
            return true;
        }

        public void PrintList()
        {
            Node<T> current = head;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public int Count
        {
            get { return count; }
        }
        public bool RemoveLast()
        {
            if (head == null)
            {
                // La lista está vacía
                return false;
            }

            if (head == tail)
            {
                // Solo hay un nodo en la lista
                head = null;
                tail = null;
            }
            else
            {
                // Hay más de un nodo en la lista
                Node<T> current = head;
                while (current.Next != tail)
                {
                    current = current.Next;
                }

                // Eliminar el nodo final
                current.Next = null;
                tail = current;
            }

            count--;
            return true;
        }

    }

}

