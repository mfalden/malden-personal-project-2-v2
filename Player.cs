public class Player
{
    public static int jumpHeight;
    public static int lastTicks;
    public static char lastChar;
    public static int lastInput;
    public static char asChar;
    public static bool freeToJump = true;
    public static int startPosition = 10;
    public static void PlayerReset()
    {
        jumpHeight = 0;
        lastTicks = 0;
        lastChar = 'o';
        lastInput = 0;
        asChar = 'o';
    }
    public static void DrawPlayer()
    {
        PlayerJump();
        DrawStats();
        FancyConsole.Write(Program.baseRow - jumpHeight, startPosition, "@");
    }
    public static void DrawStats()
    {
        FancyConsole.Write(2, 1, $"LastTicks: {lastTicks}");
        FancyConsole.Write(3, 1, $"lastChar: {lastChar}");
        FancyConsole.Write(4, 1, $"lastInput: {lastInput}");
    }
    public static void PlayerJump()
    {
        asChar = (char)FancyConsole.GetChar();
        freeToJump = true;
        if (asChar == 'j')
        {
            recentInput();
            if (freeToJump)
            {
                SingleJump();
                lastChar = asChar;
            }
            asChar = 'o';
            lastInput = Program.ticks;
        }
       if (asChar == 'd')
        {
            recentInput();
            if (freeToJump)
            {
                DoubleJump();
                lastChar = asChar;
            }
            asChar = 'o';
            lastInput = Program.ticks;
        }
        if ((lastChar == 'd') && (Program.ticks - lastTicks >= 10))
        {
            lastChar = 'o';
            jumpHeight = 0;
        }
        if ((lastChar == 'j') && (Program.ticks - lastTicks >= 7))
        {
            lastChar = 'o';
            jumpHeight = 0;
        }
    }
    public static void SingleJump()
    {
        jumpHeight = 2;
        lastTicks = Program.ticks;
    }
    public static void DoubleJump()
    {
        jumpHeight = 3;
        lastTicks = Program.ticks;
    }
    public static void recentInput()
    {
            if (Program.ticks - lastInput <= 5)
            {
                freeToJump = false;
            }
    }
}