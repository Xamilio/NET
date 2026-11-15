using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsTasks
{
    public class PriorityNode<T>
    {
        public T Data { get; set; }
        public int Priority { get; set; }

        public PriorityNode(T data, int priority)
        {
            Data = data;
            Priority = priority;
        }
    }

    public class PriorityQueue<T>
    {
        private List<PriorityNode<T>> items;

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsEmpty
        {
            get { return items.Count == 0; }
        }

        public PriorityQueue()
        {
            items = new List<PriorityNode<T>>();
        }

        public void Enqueue(T item, int priority)
        {
            PriorityNode<T> newNode = new PriorityNode<T>(item, priority);

            int insertIndex = items.Count;
            for (int i = 0; i < items.Count; i++)
            {
                if (priority > items[i].Priority)
                {
                    insertIndex = i;
                    break;
                }
            }

            items.Insert(insertIndex, newNode);
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Черга з пріоритетами порожня.");
            }

            T value = items[0].Data;
            items.RemoveAt(0);
            return value;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Черга з пріоритетами порожня.");
            }

            return items[0].Data;
        }

        public void Clear()
        {
            items.Clear();
        }

        public void Display()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Черга з пріоритетами порожня.");
                return;
            }

            Console.Write("Черга з пріоритетами (кількість: " + Count + "): ");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("[" + items[i].Data + " (пріоритет: " + items[i].Priority + ")] ");
            }
            Console.WriteLine();
        }
    }

    public class CircularQueue<T>
    {
        private T[] buffer;
        private int head;
        private int tail;
        private int count;
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
        }

        public int Count
        {
            get { return count; }
        }

        public bool IsEmpty
        {
            get { return count == 0; }
        }

        public bool IsFull
        {
            get { return count == capacity; }
        }

        public CircularQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Розмір кільцевої черги повинен бути більшим за нуль.");
            }

            this.capacity = capacity;
            buffer = new T[capacity];
            head = 0;
            tail = 0;
            count = 0;
        }

        public void Enqueue(T item)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Кільцева черга заповнена.");
            }

            buffer[tail] = item;
            tail = (tail + 1) % capacity;
            count++;
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Кільцева черга порожня.");
            }

            T item = buffer[head];
            buffer[head] = default!;
            head = (head + 1) % capacity;
            count--;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Кільцева черга порожня.");
            }

            return buffer[head];
        }

        public void Clear()
        {
            buffer = new T[capacity];
            head = 0;
            tail = 0;
            count = 0;
        }

        public void Display()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Кільцева черга порожня.");
                return;
            }

            Console.Write("Кільцева черга (" + Count + "/" + Capacity + "): ");
            for (int i = 0; i < count; i++)
            {
                int index = (head + i) % capacity;
                Console.Write(buffer[index] + " ");
            }
            Console.WriteLine();
        }
    }

    public class SinglyNode<T>
    {
        public T Data { get; set; }
        public SinglyNode<T>? Next { get; set; }

        public SinglyNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class SinglyLinkedList<T>
    {
        private SinglyNode<T>? head;
        private SinglyNode<T>? tail;
        private int count;

        public int Count
        {
            get { return count; }
        }

        public bool IsEmpty
        {
            get { return count == 0; }
        }

        public void AddFirst(T data)
        {
            SinglyNode<T> newNode = new SinglyNode<T>(data);

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

        public void AddLast(T data)
        {
            SinglyNode<T> newNode = new SinglyNode<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail!.Next = newNode;
                tail = newNode;
            }

            count++;
        }

        public T RemoveFirst()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Однозв'язний список порожній.");
            }

            T removedData = head.Data;
            head = head.Next;
            count--;

            if (head == null)
            {
                tail = null;
            }

            return removedData;
        }

        public T RemoveLast()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Однозв'язний список порожній.");
            }

            if (head.Next == null)
            {
                T singleData = head.Data;
                head = null;
                tail = null;
                count = 0;
                return singleData;
            }

            SinglyNode<T> current = head;
            while (current.Next != null && current.Next != tail)
            {
                current = current.Next;
            }

            T removedData = tail!.Data;
            current.Next = null;
            tail = current;
            count--;

            return removedData;
        }

        public bool Remove(T data)
        {
            if (head == null)
            {
                return false;
            }

            if (EqualityComparer<T>.Default.Equals(head.Data, data))
            {
                RemoveFirst();
                return true;
            }

            SinglyNode<T> current = head;
            while (current.Next != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Next.Data, data))
                {
                    if (current.Next == tail)
                    {
                        tail = current;
                    }

                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public bool Contains(T data)
        {
            SinglyNode<T>? current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, data))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void Display()
        {
            Console.Write("Однозв'язний список (елементів: " + count + "): ");
            SinglyNode<T>? current = head;
            while (current != null)
            {
                Console.Write(current.Data + " -> ");
                current = current.Next;
            }
            Console.WriteLine("null");
        }
    }

    public class DoublyNode<T>
    {
        public T Data { get; set; }
        public DoublyNode<T>? Previous { get; set; }
        public DoublyNode<T>? Next { get; set; }

        public DoublyNode(T data)
        {
            Data = data;
            Previous = null;
            Next = null;
        }
    }

    public class DoublyLinkedList<T>
    {
        private DoublyNode<T>? head;
        private DoublyNode<T>? tail;
        private int count;

        public int Count
        {
            get { return count; }
        }

        public bool IsEmpty
        {
            get { return count == 0; }
        }

        public void AddFirst(T data)
        {
            DoublyNode<T> newNode = new DoublyNode<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }

            count++;
        }

        public void AddLast(T data)
        {
            DoublyNode<T> newNode = new DoublyNode<T>(data);

            if (tail == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }

            count++;
        }

        public T RemoveFirst()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Двозв'язний список порожній.");
            }

            T removedData = head.Data;
            head = head.Next;

            if (head != null)
            {
                head.Previous = null;
            }
            else
            {
                tail = null;
            }

            count--;
            return removedData;
        }

        public T RemoveLast()
        {
            if (tail == null)
            {
                throw new InvalidOperationException("Двозв'язний список порожній.");
            }

            T removedData = tail.Data;
            tail = tail.Previous;

            if (tail != null)
            {
                tail.Next = null;
            }
            else
            {
                head = null;
            }

            count--;
            return removedData;
        }

        public bool Remove(T data)
        {
            DoublyNode<T>? current = head;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, data))
                {
                    if (current == head)
                    {
                        RemoveFirst();
                    }
                    else if (current == tail)
                    {
                        RemoveLast();
                    }
                    else
                    {
                        current.Previous!.Next = current.Next;
                        current.Next!.Previous = current.Previous;
                        count--;
                    }

                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public bool Contains(T data)
        {
            DoublyNode<T>? current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, data))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void DisplayForward()
        {
            Console.Write("Двозв'язний список (прямий порядок, елементів: " + count + "): null <- ");
            DoublyNode<T>? current = head;
            while (current != null)
            {
                Console.Write(current.Data);
                if (current.Next != null)
                {
                    Console.Write(" <-> ");
                }
                current = current.Next;
            }
            Console.WriteLine(" -> null");
        }

        public void DisplayBackward()
        {
            Console.Write("Двозв'язний список (зворотний порядок): null <- ");
            DoublyNode<T>? current = tail;
            while (current != null)
            {
                Console.Write(current.Data);
                if (current.Previous != null)
                {
                    Console.Write(" <-> ");
                }
                current = current.Previous;
            }
            Console.WriteLine(" -> null");
        }
    }

    class Program
    {
        public static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Console.WriteLine("--- ЗАВДАННЯ 1: Generic-метод Swap ---");

                int a = 15;
                int b = 42;
                Console.WriteLine("До обміну (int): a = " + a + ", b = " + b);
                Swap(ref a, ref b);
                Console.WriteLine("Після обміну (int): a = " + a + ", b = " + b);

                string str1 = "Привіт";
                string str2 = "Світ";
                Console.WriteLine("До обміну (string): str1 = " + str1 + ", str2 = " + str2);
                Swap(ref str1, ref str2);
                Console.WriteLine("Після обміну (string): str1 = " + str1 + ", str2 = " + str2);
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 2: Generic-клас «Черга з пріоритетами» ---");

                PriorityQueue<string> priorityQueue = new PriorityQueue<string>();
                priorityQueue.Enqueue("Звичайне завдання", 1);
                priorityQueue.Enqueue("Критичне виправлення", 3);
                priorityQueue.Enqueue("Важливий звіт", 2);
                priorityQueue.Enqueue("Терміновий виклик", 3);

                priorityQueue.Display();
                Console.WriteLine("Перший елемент (Peek): " + priorityQueue.Peek());
                Console.WriteLine("Вилучено (Dequeue): " + priorityQueue.Dequeue());
                priorityQueue.Display();
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 3: Generic-клас «Кільцева Черга» ---");

                CircularQueue<int> circularQueue = new CircularQueue<int>(4);
                circularQueue.Enqueue(10);
                circularQueue.Enqueue(20);
                circularQueue.Enqueue(30);
                circularQueue.Enqueue(40);

                circularQueue.Display();
                Console.WriteLine("Черга повна (IsFull): " + circularQueue.IsFull);
                Console.WriteLine("Вилучено (Dequeue): " + circularQueue.Dequeue());
                Console.WriteLine("Вилучено (Dequeue): " + circularQueue.Dequeue());

                circularQueue.Enqueue(50);
                circularQueue.Enqueue(60);
                circularQueue.Display();
                Console.WriteLine("Перший у черзі (Peek): " + circularQueue.Peek());
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 4: Generic-клас «Однозв'язний список» ---");

                SinglyLinkedList<int> singlyList = new SinglyLinkedList<int>();
                singlyList.AddLast(10);
                singlyList.AddLast(20);
                singlyList.AddLast(30);
                singlyList.AddFirst(5);

                singlyList.Display();
                Console.WriteLine("Чи містить 20: " + singlyList.Contains(20));
                Console.WriteLine("Видалено перший елемент: " + singlyList.RemoveFirst());
                Console.WriteLine("Видалено останній елемент: " + singlyList.RemoveLast());
                singlyList.Remove(20);
                singlyList.Display();
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 5: Generic-клас «Двозв'язний список» ---");

                DoublyLinkedList<string> doublyList = new DoublyLinkedList<string>();
                doublyList.AddLast("Київ");
                doublyList.AddLast("Львів");
                doublyList.AddLast("Одеса");
                doublyList.AddFirst("Харків");

                doublyList.DisplayForward();
                doublyList.DisplayBackward();

                Console.WriteLine("Видалено елемент 'Львів': " + doublyList.Remove("Львів"));
                Console.WriteLine("Видалено перший елемент: " + doublyList.RemoveFirst());
                doublyList.DisplayForward();
                doublyList.DisplayBackward();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
