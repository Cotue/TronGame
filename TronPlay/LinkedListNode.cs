namespace TronPlay
{
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

    public class LinkedList<T>
    {
        private LinkedListNode<T> _head;
        private LinkedListNode<T> _tail;

        public LinkedList()
        {
            _head = null;
            _tail = null;
        }

        public void AddFirst(T value)
        {
            var newNode = new LinkedListNode<T>(value);
            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head = newNode;
            }
        }

        public void AddLast(T value)
        {
            var newNode = new LinkedListNode<T>(value);
            if (_tail == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }
        }

        public void RemoveLast()
        {
            if (_head == null)
                return;

            if (_head == _tail)
            {
                _head = null;
                _tail = null;
                return;
            }

            var current = _head;
            while (current.Next != _tail)
            {
                current = current.Next;
            }

            current.Next = null;
            _tail = current;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
        }

        public LinkedListNode<T> GetHead()
        {
            return _head;
        }

        public LinkedListNode<T> GetTail()
        {
            return _tail;
        }

        public int Count()
        {
            int count = 0;
            var current = _head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }
}

