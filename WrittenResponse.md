# Guessing Game Written Response

The format for this document are taken directly from the AP Computer Science
Principles [Student Handout](../support/ap-csp-student-task-directions.pdf).

## 3a

Provide a written response that does all three of the following:

### 3a i.

Describes the overall purpose of the program.

This program can be used by a person looking to play a game for entertainment.

### 3a ii.

Describes what functionality of the program is demonstrated in the video.

This video shows how the program accepts a user's input and moves the '@' accordingly to avoid the randomly-generated obstacles. It then shows how the user can either choose to replay or add their score to a high score tracker.

### 3a iii.

Describes the input and output of the program demonstrated in the video.

The inputs to the main game program are 'j' and 'd', and the output is an image on the terminal.

## 3b

Capture and paste two program code segments you developed during the
administration of this task that contain a list (or other collection type) being
used to manage complexity in your program.

### 3b i.

The first program code segment must show how data have been stored in the list.

```csharp
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
```

### 3b ii.

The second program code segment must show the data in the same list being used,
such as creating new data from the existing data or accessing multiple elements
in the list, as part of fulfilling the program's purpose.

```csharp
 foreach (Obstacle o in obstacles)
        {
            DrawObstacle(o, totalLength);
            totalLength += o.Length;
        }
```

### 3b iii.

Then provide a written response that does all three of the following:

Identifies the name of the list being used in this response

    The list is stored in the variable Obstacles.

### 3b iv.

Describes what the data contained in the list represents in your program

    The Obstacle list provides an obstacle's parameters, like its total length, the position of the obstacle, and the height of the obstacle which are all necessary to drawing the obstacle in the terminal.

### 3b v.

Explains how the selected list manages complexity in your program code by
explaining why your program code could not be written, or how it would be
written differently, if you did not use the list.

   A list allowed me to randomly select obstacles. Without a list I would have not been able to randomly select obstacles, as I would have to create a pre-made sequence of obstacles to draw and would have no way to replicate the random obstacle choice from refresh to refresh.

## 3c.

Capture and paste two program code segments you developed during the
administration of this task that contain a student-developed procedure that
implements an algorithm used in your program and a call to that procedure.

### 3c i.

The first program code segment must be a student-developed procedure that:

- [ ] Defines the procedure's name and return type (if necessary)
- [ ] Contains and uses one or more parameters that have an effect on the functionality of the procedure
- [ ] Implements an algorithm that includes sequencing, selection, and iteration

```csharp
public static void DrawAllObstacles2(List<Obstacle> obstacles)
    {
        int totalLength = 0;
        if (obstacles == null)
        {
            throw new Exception("Obstacles is null!");
        }
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
```

### 3c ii.

The second program code segment must show where your student-developed procedure is being called in your program.

```csharp
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
```

### 3c iii.

Describes in general what the identified procedure does and how it contributes to the overall functionality of the program.

This method takes a list of obstacles and draws each obstacle in its correct position. This is needed to load each obstacle into the correct position. 

### 3c iv.

Explains in detailed steps how the algorithm implemented in the identified procedure works. Your explanation must be detailed enough for someone else to recreate it.

**TODO: In English, explain step by step what your procedure does. Be sure to use the word `Selection` and `Iteration` to explain what it does.**
1. Create a new integer totalLength
2. Check if the list obstacles is null. If it is, throw an exception saying it is null.
3. Select the first element and check if it is removable. If it is, add its length to totalLength and remove it from the list obstacles.
4. Iterate each obstacle o in obstacles. Input o and totalLength into the drawObstacle method, and add o's length to the total length. 
## 3d

Provide a written response that does all three of the following:

### 3d i.

Describes two calls to the procedure identified in written response 3c. Each call must pass a different argument(s) that causes a different segment of code in the algorithm to execute.

First call:

```csharp
DrawAllObstacles2(null);
```

Second call:

```csharp
DrawAllObstacles2(obstacles);
```

### 3d ii.

Describes what condition(s) is being tested by each call to the procedure

Condition(s) tested by the first call:
 
Testing if inputting null, which represents the lack of a list, triggers the program exception and closes the program.

Condition(s) tested by the second call:

Testing if inputting an actual list does not trigger the program exception, runs the code, and draws each element in the list. 

### 3d iii.

Result of the first call:

"Obstacles is null!"

Result of the second call:

The list Obstacles is rendered on the terminal screen. 