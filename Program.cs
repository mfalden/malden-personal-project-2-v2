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

            // obstacles.Add(Obstacle.GetRandomObstacle());
            // DrawAllObstacles(obstacles);

            // TODO(jcollard 2022-03-06): You need to call refresh after you've
            // specified all the things you want to print to the screen. Nothing
            // is displayed until Refresh() is called. This helps reduce
            // flickering.

            InitializeScreen();
            

            bool hasGameStarted = true; // false for testing
            while (hasGameStarted == true)
            {
                FancyConsole.Clear();
                MoveScreen();
                DrawAllObstacles2(obstacles);
                DisplayDebugInfo();
                FancyConsole.Refresh();

                // TODO(jcollard 2022-03-06): You want to put at least a small delay
                // at the end of the loop otherwise you will see flickering.
                FancyConsole.Sleep(20);
            }
        }

    }

    public static void InitializeScreen()
    {
        // Feedback(jcollard 2022-04-07): I changes this to reference the global obstacles list
        // Previously, this variable was local to this method. It was:
        // List<Obstacle> obstacles = new List<Obstacle>();
        obstacles = new List<Obstacle>();
        int times = 10;
        while (times > 0)
        {
            obstacles.Add(Obstacle.GetRandomObstacle());
            times = times - 1;
        }
        //Obstacle firstElement = obstacles[0];

        DrawAllObstacles2(obstacles);

    }

    public static void MoveScreen()
    {
        int input = FancyConsole.GetChar();
        // if (input == null)
        // {
        //     throw new Exception("Input does not exist!");
        // }
        char asChar = (char)input;
        // if (asChar == ' ')
        // {

            spaces++;
            AddObstacle(obstacles);
        // }
    }

    public static List<Obstacle> AddObstacle(List<Obstacle> obstacles)
    {
        if (spaces > totalLength + lastobstaclelength)
        {
            obstacles.Add(Obstacle.GetRandomObstacle());
            // Obstacle lastElement = obstacles[obstacles.Count-1];
            // totalLength += lastElement.Length;
        }
        if (spaces == 1)
        {
            obstacles.Add(Obstacle.GetRandomObstacle());
        }
        return obstacles;
    }

    // NOTE: DRAWALLOBSTACLES BEING EDITED. REFERENCE "Added Method Exception"
    public static void DrawAllObstacles(List<Obstacle> obstacles)
    {
        foreach (Obstacle o in obstacles)
        {
            int columnNumber = 0;
            int drawnHeight = 0;
            while (columnNumber <= o.Length)
            {
                FancyConsole.Write(1, 0, $"Here: {spaces}");
                while (columnNumber < o.X)
                {
                    FancyConsole.Write(2, 0, $"Here: {spaces}");

                    FancyConsole.Write(baseRow, start - spaces + columnNumber + totalLength, "_");
                    columnNumber++;
                }
                if (columnNumber == o.X)
                {
                    while (drawnHeight <= o.Height)
                    {
                        FancyConsole.Write(3, 0, $"Here: {spaces}");
                        FancyConsole.Write(baseRow - drawnHeight, start - spaces + columnNumber + totalLength, "#");
                        drawnHeight++;
                    }
                    columnNumber++;
                }
                FancyConsole.Write(baseRow, start - spaces + columnNumber + totalLength, "_");
                columnNumber++;
            }
            totalLength += o.Length;
        }
    }

    public static void DrawAllObstacles2(List<Obstacle> obstacles)
    {
        int totalLength = 0;
        // Feedback(jcollard 2022-04-07): I like to keep breaking things up
        // until they seem so simple they cannot be incorrect. Reading through
        // your nested while loops is really challenging. So, I've broken it
        // down into simpler components: 
        // 1. Loop through all the obstacles
        // 2. Within that loop, draw a single element
        foreach(Obstacle o in obstacles)
        {
            DrawObstacle(o, totalLength);
            totalLength += o.Length;
        }
    }
    public static void DrawObstacle(Obstacle o, int offsetX)
    {
        // Drawing a single obstacles becomes pretty simple. First,
        // Draw the floor.
        DrawFloor(o, offsetX);
        // Then draw the obstacle. In the first loop, we draw an extra '_' but
        // then we draw over it here anyway so it's not a big deal.
        DrawColumn(o, offsetX);        
    }

    public static void DrawFloor(Obstacle o, int offsetX)
    {
        int column = 0;
        while (column < o.Length)
        {
            FancyConsole.Write(baseRow, start - spaces + column + offsetX, "_");
            column++;
        }
    }

    public static void DrawColumn(Obstacle o, int offsetX)
    {
        int drawHeight = 0;
        while (drawHeight <= o.Height)
        {
            FancyConsole.Write(baseRow - drawHeight, start - spaces + o.X + offsetX, "#");
            drawHeight++;
        }
    }

    public static void DisplayDebugInfo()
    {
        FancyConsole.SetColor(FancyColor.WHITE);
        FancyConsole.Write(0, 0, $"Spaces: {spaces}");
    }
}

