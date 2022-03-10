using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Obstacle
{

    public int X;
    public int Height;
    public int SpaceBefore, SpaceAfter;
    public int Length => SpaceAfter + SpaceBefore + 1;

    public static Obstacle GetRandomObstacle() 
    {
        List<Func<Obstacle>> possibleObstacles = new List<Func<Obstacle>>();

        possibleObstacles.Add(Obstacle1x);
        possibleObstacles.Add(Obstacle0x);
        possibleObstacles.Add(Obstacle0x);
        possibleObstacles.Add(Obstacle2x);
        possibleObstacles.Add(Obstacle2x);

        Random generator;
        generator = new Random();
        int randomNumber = generator.Next(0, possibleObstacles.Count);
        return possibleObstacles[randomNumber].Invoke();
    }
public static Obstacle Obstacle00()
    {
        Obstacle o = new Obstacle();
        o.X = 0; // total coordinate - 1
        o.Height = -1; // total height - 1
        o.SpaceBefore = 1; // total length - 1
        o.SpaceAfter = 30; // total length
        return o;
    }
public static Obstacle Obstacle0x()
    {
        Obstacle o = new Obstacle();
        o.X = 0; // total coordinate - 1
        o.Height = -1; // total height - 1
        o.SpaceBefore = 1; // total length - 1
        o.SpaceAfter = 19; // total length
        return o;
    }

public static Obstacle Obstacle1x()
    {
        Obstacle o = new Obstacle();
        o.X = 3; // total coordinate - 1
        o.Height = 2; // total height - 1
        o.SpaceBefore = 2; // total length - 1
        o.SpaceAfter = 15; // total length
        return o;
    }
    
       public static Obstacle Obstacle2x()
    {
        Obstacle o = new Obstacle();
        o.X = 8; // total coordinate - 1
        o.Height = 1; // total height - 1
        o.SpaceBefore = 7; // total length - 1
        o.SpaceAfter = 15; // total length
        return o;
    }
}