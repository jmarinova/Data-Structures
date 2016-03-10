using System;
using System.Threading;

public class CircularQueue<T>
{
    private const int InitialCapacity = 16;

    private int head = 0;
    private int tail = 0;
    private T[] elements;
    public int Count { get; private set; }

    public CircularQueue()
    {
        this.elements = new T[InitialCapacity];
    }

    public CircularQueue(int capacity)
    {
        this.elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length)
        {
            var newArray = new T[this.elements.Length*2];
            this.CopyAllElementsTo(newArray);
            this.elements = newArray;
            head = 0;
            tail = this.Count;
        }
        this.elements[this.tail] = element;
        this.tail = (tail + 1)%elements.Length;
        this.Count++;
    }

    private void CopyAllElementsTo(T[] resultArray)
    {

        var startIndex = head;
        for (int i = 0; i < resultArray.Length; i++)
        {
            resultArray[i] = this.elements[startIndex];
            startIndex = (startIndex + 1) % this.elements.Length;
        }

    }

    public T Dequeue()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Queue is empty!");
        }
        var resultElement = this.elements[head];
        head = (head + 1)%this.elements.Length;
        this.Count--;
        return resultElement;
    }

    public T[] ToArray()
    {
        var arraQueue = new T[this.Count];
        Array.Copy(this.elements, arraQueue, this.Count);
        return arraQueue;
    }
}


class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
