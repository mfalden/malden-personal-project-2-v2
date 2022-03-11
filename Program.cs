using System;
using System.Collections.Generic;
using Mindmagma.Curses;

public class Program
{
    public static int baseRow = 7;
    public static int start = 0;
    public static int totalLength = 0;
    public static int spaces = 0;
    public static int lastobstaclelength = 0;
    public static int ticks = 0;
    public static List<Obstacle> obstacles = new List<Obstacle>();
    public static bool isGameOver = false;
    public static char asChar;
    public static bool hasGameStarted = true;
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

            // false for testing
            while (hasGameStarted == true)
            {
                if (isGameOver)
                {
                    gameOver();
                    hasGameStarted = false;
                }
                if (isGameOver == false)
                {
                    FancyConsole.Clear();
                    MoveScreen();
                    DrawAllObstacles2(obstacles);
                    DisplayDebugInfo();
                    Player.DrawPlayer();
                    Instructions();
                    FancyConsole.Refresh();
                    FancyConsole.Sleep(50);
                    ticks++;
                }
            }


        }

    }

    public static void Instructions()
    {
        FancyConsole.Write(1, 20, "Type 'j' to single jump.");
        FancyConsole.Write(2, 20, "Type 'd' to double jump.");
    }
    public static void gameOver()
    {
        while (true)
        {
            FancyConsole.Clear();
            FancyConsole.Refresh();
            FancyConsole.SetColor(FancyColor.WHITE);
            FancyConsole.Write(10, 10, "Game Over!");
            FancyConsole.Write(12, 10, "Type 'R' to replay.");
            FancyConsole.Write(13, 10, "Type 'S' to add your score to the high score tracker.");
            asChar = (char)FancyConsole.GetChar();
            if (asChar == 'R' || asChar == 'r')
            {
                InitializeScreen();
                Player.PlayerReset();
                isGameOver = false;
            }
            if (asChar == 'S' || asChar == 's')
            {
                    AddHighScore();
            }
            FancyConsole.Sleep(50);
        }
    }
    public static void AddHighScore()
    {
        FancyConsole.Clear();
        FancyConsole.SetColor(FancyColor.WHITE);
        FancyConsole.Write(6, 10, "Please type in your name: ");
        string fileName = "scoresFile.txt";
        bool testing = false;
        if (!testing)
        {
            List<string> scoreList = HighScoreTracker.LoadScoresFile(fileName);
            List<int> scoresOnly = HighScoreTracker.ScoreSplit(scoreList);
            string userName;
            FancyConsole.Refresh();
            NCurses.Move(6, 10 + "Please type in your name: ".Length);

            userName = Console.ReadLine().Replace(" ", "");
            int insertAt = HighScoreTracker.ScoreCompare(scoresOnly, spaces);
            List<string> finalScoreList = HighScoreTracker.AddScore(userName, spaces, insertAt, scoreList, fileName);
            int row = 7;
            foreach (string line in finalScoreList)
            {
                FancyConsole.Write(row, 10, $"{line}");
                row++;
            }
            
        }
        FancyConsole.Refresh();
        Console.ReadKey();

    }
    public static void InitializeScreen()
    {
        // Feedback(jcollard 2022-04-07): I changes this to reference the global obstacles list
        // Previously, this variable was local to this method. It was:
        // List<Obstacle> obstacles = new List<Obstacle>();
        obstacles = new List<Obstacle>();
        int times = 10;
        start = 0;
        totalLength = 0;
        spaces = 0;
        lastobstaclelength = 0;
        ticks = 0;
        obstacles.Add(Obstacle.Obstacle00());
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
        spaces++;
        AddObstacle(obstacles);
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


    public static void DrawAllObstacles2(List<Obstacle> obstacles)
    {
        int totalLength = 0;
        // Feedback(jcollard 2022-04-07): I like to keep breaking things up
        // until they seem so simple they cannot be incorrect. Reading through
        // your nested while loops is really challenging. So, I've broken it
        // down into simpler components: 
        // 1. Loop through all the obstacles
        // 2. Within that loop, draw a single element
        if (IsFirstElementRemovable())
        {
            totalLength += obstacles[0].Length;
            obstacles.RemoveAt(0);
        }
        foreach (Obstacle o in obstacles)
        {
            DrawObstacle(o, totalLength);
            totalLength += o.Length;
        }
    }

    public static bool IsFirstElementRemovable()
    {
        Obstacle o = obstacles[0];

        if (totalLength - o.Length >= 250)
        {
            return true;
        }
        return false;
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
            if ((start - spaces + o.X + offsetX == Player.startPosition) && (baseRow - Player.jumpHeight == baseRow - drawHeight))
            {
                isGameOver = true;
            }
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

