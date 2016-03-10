using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class EscapeFromLabyrinth
{
    const char VisitedCell = 's';

    static int width = 9;
    static int height = 7;

    private static char[,] labyrinth =
    {
        { '*', '*', '*', '*', '*', '*', '*', '*', '*'},
        { '*', '-', '-', '-', '-', '*', '*', '-', '-'},
        { '*', '*', '-', '*', '-', '-', '-', '-', '*'},
        { '*', '-', '-', '*', '-', '*', '-', '*', '*'},
        { '*', 's', '*', '-', '-', '*', '-', '*', '*'},
        { '*', '*', '-', '-', '-', '-', '-', '-', '*'},
        { '*', '*', '*', '*', '*', '*', '*', '-', '*'}
    };
    static Point FindStartPosition()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (labyrinth[y, x] == VisitedCell)
                {
                    return new Point() {X = x, Y = y};
                }
            }
        }

        return null;
    }

    static bool IsExit(Point currentCell)
    {
        bool isOnBorderX = currentCell.X == 0 || currentCell.X == width -1;
        bool isOnBorderY = currentCell.Y == 0 || currentCell.Y == height -1;
        return isOnBorderX || isOnBorderY;  
    }

    static void TryDirection(Queue<Point> queue, Point currentCell, string direction, int deltaX, int deltaY)
    {
        int newX = currentCell.X + deltaX;
        int newY = currentCell.Y + deltaY;

        if (newX >= 0 && newX < width && newY >= 0 && newY < height && labyrinth[newX, newY] == '-')
        {
            labyrinth[newX, newY] = VisitedCell;

            var nextCell = new Point()
            {
                X = newX,
                Y = newY,
                Direction = direction,
                PreviousPoint = currentCell
            };
            queue.Enqueue(nextCell);
        }
    }

    static string TraceBackPath(Point currentCell)
    {
        var path = new StringBuilder();
        while (currentCell.PreviousPoint != null)
        {
            path.Append(currentCell.Direction);
            currentCell = currentCell.PreviousPoint;
        }
        var pathReversed = new StringBuilder(path.Length);
        for (int i = path.Length -1; i >= 0; i--)
        {
            pathReversed.Append(path[i]);
        }
        return pathReversed.ToString();
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
        public Point PreviousPoint { get; set; }
    }

    

    static string FindShortestPathToExit()
    {
        var queue = new Queue<Point>();
        var startPosition = FindStartPosition();

        if (startPosition == null)
        {
            return null;
        }
        queue.Enqueue(startPosition);
        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();
            if (IsExit(currentCell))
            {
                return TraceBackPath(currentCell);
            }
            TryDirection(queue, currentCell, "U", 0, -1);
            TryDirection(queue, currentCell, "R", +1, 0);
            TryDirection(queue, currentCell, "D", 0, +1);
            TryDirection(queue, currentCell, "L", -1, 0);
        }

        return null;
    }

    //static void ReadLabyrinth()
    //{
    //    int width = int.Parse(Console.ReadLine());
    //    int height = int.Parse(Console.ReadLine());

    //    labyrinth = new char[width,height];
    //    for (int row = 0; row < height; row++)
    //    {
    //        var currentChars = Console.ReadLine();
    //        int lengthArrayOfChars = currentChars.Length;
    //        var arrayOfChars = new char[lengthArrayOfChars];
    //        for (int i = 0; i < arrayOfChars.Length; i++)
    //        {
    //            arrayOfChars[i] = currentChars[i];
    //        }

    //        for (int col = 0; col < width; col++)
    //        {
    //            labyrinth[row, col] = arrayOfChars[col];
    //        }
    //    }
    //}
    public static void Main()
    {
        string shortestPathToExit = FindShortestPathToExit();

        if (shortestPathToExit == null)
        {
            Console.WriteLine("no exit!");
        }
        else if (shortestPathToExit == "")
        {
            Console.WriteLine("start is at the exit!");
        }
        else
        {
            Console.WriteLine("shortest exit: "+ shortestPathToExit);
        }
    }
}
