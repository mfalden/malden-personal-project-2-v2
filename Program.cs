using System;
using System.Collections.Generic;


public class Program
{
    public static void Main(string[] args)
    {
        // Feedback(jcollard 2022-03-06): Fancy console is a bit weird, you need
        // to put it inside of a while loop to get it to work properly.
        while (true)
        {
            FancyConsole.Write(1, 1, "Hello World!");

            // TODO(jcollard 2022-03-06): You need to call refresh after you've
            // specified all the things you want to print to the screen. Nothing
            // is displayed until Refresh() is called. This helps reduce
            // flickering.
            FancyConsole.Refresh();

            // TODO(jcollard 2022-03-06): You want to put at least a small delay
            // at the end of the loop otherwise you will see flickering.
            FancyConsole.Sleep(10);
        }

    }
}

