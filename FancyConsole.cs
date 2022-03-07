using System;
using Mindmagma.Curses;

public class FancyConsole
{
    private static IntPtr Screen;

    private static readonly short[] color_table = {
            CursesColor.RED,
            CursesColor.BLUE,
            CursesColor.GREEN,
            CursesColor.CYAN,
            CursesColor.RED,
            CursesColor.MAGENTA,
            CursesColor.YELLOW,
            CursesColor.WHITE
        };

    static FancyConsole(){
        Screen = NCurses.InitScreen();
        NCurses.NoDelay(Screen, true);
        NCurses.NoEcho();
        NCurses.StartColor();
        FancyColor[] colors = {FancyColor.RED, FancyColor.BLUE, FancyColor.GREEN, FancyColor.CYAN, FancyColor.MAGENTA, FancyColor.YELLOW, FancyColor.WHITE};
        foreach (FancyColor c in colors)
        {
            NCurses.InitPair(GetColorCode(c), GetCursesColor(c), CursesColor.BLACK);
        }
    }

    public static void SetColor(FancyColor color)
    {
        NCurses.AttributeSet(NCurses.ColorPair((int)(color + 1)));
    }

    public static void Write(int row, int col, string output)
    {
        if (row < 0 || col < 0 || row > NCurses.Lines - 1 || col > NCurses.Columns - 1){
            return;
        }
        NCurses.MoveAddString(row, col, output);
    }

    public static void Sleep(int millis) 
    {
        NCurses.Nap(millis);
    }

    public static int GetChar()
    {
        return NCurses.GetChar();
    }

    public static void Refresh()
    {
        NCurses.Move(NCurses.Lines - 1, NCurses.Columns - 1);
        NCurses.Refresh();        
    }

    public static void Clear()
    {
        NCurses.Clear();
    }

    private static short GetColorCode(FancyColor c)
    {
        return (short)(c + 1);
    }

    private static short GetCursesColor(FancyColor c)
    {
        return c switch {
            FancyColor.RED => CursesColor.RED,
            FancyColor.BLUE => CursesColor.BLUE,
            FancyColor.GREEN => CursesColor.GREEN,
            FancyColor.CYAN => CursesColor.CYAN,
            FancyColor.MAGENTA => CursesColor.MAGENTA,
            FancyColor.YELLOW => CursesColor.YELLOW,
            FancyColor.WHITE => CursesColor.WHITE,
            _ => throw new ArgumentException($"Illegal color {c}")
        };
    }

}

public enum FancyColor
{
    RED,
    BLUE,
    GREEN,
    CYAN,
    MAGENTA,
    YELLOW,
    WHITE
}