using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        // Константы, задающие размер сегментов памяти
        const int STACK_SEGMENT_SIZE = 3;
        const int QUEUE_SEGMENT_SIZE = 3;
        const int DEQUE_SEGMENT_SIZE = 3;
        static void Main(string[] args)
        {
            // Создаем структуру для стека
            Stack stack = new Stack(STACK_SEGMENT_SIZE);

            // Создаем структуру для очереди
            Queue queue = new Queue(QUEUE_SEGMENT_SIZE);

            // Создаем структуру для дека
            Deque deque = new Deque(DEQUE_SEGMENT_SIZE);

            // Добавляем три элемента в стек
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            // Выводим содержимое сегментов памяти стека
            Console.WriteLine("Stack memory segments after pushing 3 elements:");
            stack.PrintSegments();

            // Выбираем и удаляем элемент из стека
            int stackElement = stack.Pop();

            // Выводим выбранный элемент и содержимое сегментов памяти стека
            Console.WriteLine($"Selected element from stack: {stackElement}");
            Console.WriteLine("Stack memory segments after popping 1 element:");
            stack.PrintSegments();

            // Добавляем три элемента в очередь
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Выводим содержимое сегментов памяти очереди
            Console.WriteLine("Queue memory segments after enqueueing 3 elements:");
            queue.PrintSegments();

            // Выбираем и удаляем элемент из очереди
            int queueElement = queue.Dequeue();

            // Выводим выбранный элемент и содержимое сегментов памяти очереди
            Console.WriteLine($"Selected element from queue: {queueElement}");
            Console.WriteLine("Queue memory segments after dequeuing 1 element:");
            queue.PrintSegments();

            // Добавляем два элемента в начало дека и один в конец
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushBack(3);

            // Выводим содержимое сегментов памяти дека
            Console.WriteLine("Deque memory segments after adding 3 elements:");
            deque.PrintSegments();

            // Выбираем и удаляем элемент из начала дека
            int dequeFrontElement = deque.PopFront();

            // Выводим выбранный элемент и содержимое сегментов памяти дека
            Console.WriteLine($"Selected element from front of deque: {dequeFrontElement}");
            Console.WriteLine("Deque memory segments after popping front element:");
            deque.PrintSegments();

            // Выбираем и удаляем элемент из конца дека
            int dequeBackElement1 = deque.PopBack();

            // Выводим выбранный элемент и содержимое сегментов памяти дека
            Console.WriteLine($"Selected element from back of deque: {dequeBackElement1}");
            Console.WriteLine("Deque memory segments after popping back element:");
            deque.PrintSegments();
            // Выбираем и удаляем элемент из начала дека
            int dequeFrontElement2 = deque.PopFront();

            // Выводим выбранный элемент и содержимое сегментов памяти дека
            Console.WriteLine($"Selected element from front of deque: {dequeFrontElement2}");
            Console.WriteLine("Deque memory segments after popping front element:");
            deque.PrintSegments();

            // Пытаемся выбрать и удалить элемент из пустого дека
            try
            {
                int emptyDequeElement = deque.PopFront();
                Console.WriteLine($"Selected element from front of empty deque: {emptyDequeElement}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    // Класс, представляющий стек
    class Stack
    {
        private int[] memory; // Память для хранения элементов стека
        private int segmentSize; // Размер сегмента памяти
        private int top; // Индекс верхнего элемента стека

        public Stack(int segmentSize)
        {
            this.segmentSize = segmentSize;
            memory = new int[segmentSize];
            top = -1;
        }

        public void Push(int element)
        {
            if (top == segmentSize - 1)
            {
                Console.WriteLine("Stack overflow");
                return;
            }

            top++;
            memory[top] = element;
        }

        public int Pop()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack underflow");
                return -1;
            }

            int element = memory[top];
            top--;
            return element;
        }

        public void PrintSegments()
        {
            for (int i = 0; i <= top; i++)
            {
                Console.WriteLine($"Segment {i}: {memory[i]}");
            }

            for (int i = top + 1; i < segmentSize; i++)
            {
                Console.WriteLine($"Segment {i}: Not used");
            }
        }
    }

    // Класс, представляющий очередь
    class Queue
    {
        private int[] memory; // Память для хранения элементов очереди
        private int segmentSize; // Размер сегмента памяти
        private int front; // Индекс начала очереди
        private int rear; // Индекс конца очереди
        private int count; // Количество элементов в очереди

        public Queue(int segmentSize)
        {
            this.segmentSize = segmentSize;
            memory = new int[segmentSize];
            front = 0;
            rear = -1;
            count = 0;
        }

        public void Enqueue(int element)
        {
            if (count == segmentSize)
            {
                Console.WriteLine("Queue overflow");
                return;
            }

            rear = (rear + 1) % segmentSize;
            memory[rear] = element;
            count++;
        }

        public int Dequeue()
        {
            if (count == 0)
            {
                Console.WriteLine("Queue underflow");
                return -1;
            }

            int element = memory[front];
            front = (front + 1) % segmentSize;
            count--;
            return element;
        }

        public void PrintSegments()
        {
            for (int i = front; i <= rear; i++)
            {
                Console.WriteLine($"Segment {i}: {memory[i]}");
            }

            for (int i = count; i < segmentSize; i++)
            {
                Console.WriteLine($"Segment {i}: Not used");
            }
        }
    }

    // Класс, представляющий дек
    class Deque
    {
        private int[] memory; // Память для хранения элементов дека
        private int segmentSize; // Размер сегмента памяти
        private int front; // Индекс начала дека
        private int rear; // Индекс конца дека
        private int count; // Количество элементов в дека

        public Deque(int segmentSize)
        {
            this.segmentSize = segmentSize;
            memory = new int[segmentSize];
            front = -1;
            rear = -1;
            count = 0;
        }

        public void PushFront(int element)
        {
            if (count == segmentSize)
            {
                Console.WriteLine("Deque overflow");
                return;
            }

            if (front == -1)
            {
                front = 0;
                rear = 0;
            }
            else
            {
                front = (front - 1 + segmentSize) % segmentSize;
            }

            memory[front] = element;
            count++;
        }

        public void PushBack(int element)
        {
            if (count == segmentSize)
            {
                Console.WriteLine("Deque overflow");
                return;
            }

            if (rear == -1)
            {
                front = 0;
                rear = 0;
            }
            else
            {
                rear = (rear + 1) % segmentSize;
            }

            memory[rear] = element;
            count++;
        }

        public int PopFront()
        {
            if (count == 0)
            {
                Console.WriteLine("Deque underflow");
                return -1;
            }

            int element = memory[front];
            if (front == rear)
            {
                front = -1;
                rear = -1;
            }
            else
            {
                front = (front + 1) % segmentSize;
            }

            count--;
            return element;
        }

        public int PopBack()
        {
            if (count == 0)
            {
                Console.WriteLine("Deque underflow");
                return -1;
            }

            int element = memory[rear];
            if (front == rear)
            {
                front = -1;
                rear = -1;
            }
            else
            {
                rear = (rear - 1 + segmentSize) % segmentSize;
            }

            count--;
            return element;
        }

        public void PrintSegments()
        {
            for (int i = 0; i < segmentSize; i++)
            {
                if (i >= front && i <= rear)
                {
                    Console.WriteLine($"Segment {i}: {memory[i]}");
                }
                else
                {
                    Console.WriteLine($"Segment {i}: Not used");
                }
            }
        }
    }
}
