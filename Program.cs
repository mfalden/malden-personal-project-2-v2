using System;
using System.Collections.Generic;


public class Program
{
    public static int baseRow = 7;
    public static int start = 0;
    public static int totalLength = 0;
    public static int spaces = 0;
    public static int lastobstaclelength = 0;
    public static List<Obstacle> obstacles = new List<Obstacle>();
    public static void Main(string[] args)
    {

        // Feedback(jcollard 2022-03-06): Fancy console is a bit weird, you need
        // to put it inside of a while loop to get it to work properly.
        while (true)
        {
            
            //FancyConsole.Write(1, 1, "Hello World!");

            // TODO(jcollard 2022-03-06): You need to call refresh after you've
            // specified all the things you want to print to the screen. Nothing
            // is displayed until Refresh() is called. This helps reduce
            // flickering.
            InitializeScreen();
            bool hasGameStarted = true;
            while (hasGameStarted == true)
            {

            MoveScreen();
            FancyConsole.Refresh();

            // TODO(jcollard 2022-03-06): You want to put at least a small delay
            // at the end of the loop otherwise you will see flickering.
            FancyConsole.Sleep(10);
            }
        }

    }

    public static void InitializeScreen()
    {
        obstacles.Add(Obstacle.GetRandomObstacle());
        obstacles.Add(Obstacle.GetRandomObstacle());
        obstacles.Add(Obstacle.GetRandomObstacle());
        obstacles.Add(Obstacle.GetRandomObstacle());
        DrawAllObstacles(obstacles);
    }

    public static void MoveScreen()
    {
        int input = FancyConsole.GetChar();
        if (input == null)
        {
            throw new Exception("Input does not exist!");
        }
        char asChar = (char)input;
        if (asChar == ' ')
        {
            spaces++;
            DrawAllObstacles(AddObstacle(obstacles));
        }
    }

    public static List<Obstacle> AddObstacle(List<Obstacle> obstacles)
    {
        if (spaces > totalLength + lastobstaclelength)
        {
            obstacles.Add(Obstacle.GetRandomObstacle());
            totalLength += lastobstaclelength;
        }
        return obstacles;
    }

    public static void DrawAllObstacles(List<Obstacle> obstacles)
    {
        foreach (Obstacle o in obstacles)
        {
            int columnNumber = 0;
            int drawnHeight = 0;
            int column = (start - spaces + columnNumber + totalLength);
            while (columnNumber <= o.Length)
                {
                    while (columnNumber < o.X || columnNumber > o.X)
                    {
                        FancyConsole.Write(baseRow, column, "_");
                    }
                    if (columnNumber == o.X)
                        {
                            while (drawnHeight < o.Height)
                                {
                                    FancyConsole.Write(baseRow - drawnHeight, column, "#");
                                    drawnHeight++;
                                }
                        }
                    columnNumber++;
                    lastobstaclelength = o.Length;
                }
        }
    }

}

