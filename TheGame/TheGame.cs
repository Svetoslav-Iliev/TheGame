using System;
using System.Linq;
using System.Text;
using System.Threading;

internal class TheGame
{
        static int padSize = 8;
        static int ballPositionX = 0;
        static int ballPositionY = 0;
        static int playerPosition = 0;
        static int result = 0;
        static int bars = 3;
        static bool BallDirectionUp = true;
        static bool BallDirectionRight = true;
        static bool[,] wall = new bool[3, Console.WindowWidth / 3];


    private static void Main()
    {
        RemoveScrollBars();
        SetInitialPositions();
        Console.OutputEncoding = Encoding.UTF8;


        //StartBlock();
         
        while (true)
        {
            
            MovePlayerPad();
            PrinWall();
            MoveBall();           
            DrawPlayer(padSize);
            DrawBall();
            PrintPlayerData();
            Thread.Sleep(150);
            Console.Clear();

            if (bars == 0)
            {
                break;
            }
        }

        FinalBlock();

        
}

    private static void StartBlock()
    {
        for (int i = 1; i <= 3; i++)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.BufferHeight / 2 - 2);
            Console.Write("GET READY!");
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.BufferHeight / 2);
            Console.WriteLine("{0}", i);
            Thread.Sleep(1000);
        }

        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.BufferHeight / 2 - 1);
        Console.Write("S T A R T");
        Thread.Sleep(1000);
    }

    private static void FinalBlock()
    {
        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.BufferHeight / 2 - 1);
        Console.Write("GAME OVER");
        Thread.Sleep(1000);
        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.BufferHeight / 2 - 1);
        Console.Write(String.Format("Your score: {0}", result));
        Console.SetCursorPosition(0, Console.BufferHeight - 1);
    }

    private static void MovePlayerPad()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                MovePlayerLeft();
            }
            if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                MovePlayerRight();
            }
        }
    }      
    

    private static void MoveBall()
    {

        if (ballPositionX == Console.WindowWidth-1)
        {
            BallDirectionRight = false;
        }
        if (ballPositionX == 0)
        {
            BallDirectionRight = true;
        }
        if (ballPositionY == 2)
        {
            BallDirectionUp = false;
        }
        if (ballPositionY == Console.WindowHeight-1)
        {
            Console.Clear();
            PrintPlayerData();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.BufferHeight / 2 - 1);
            Console.Write("MISS!");
            Thread.Sleep(1000);
            SetTheBallAndPlayerInStartPosition();
            bars--;
        }
        if (ballPositionY == Console.WindowHeight - 3 && ballPositionX >= playerPosition && ballPositionX <= playerPosition + padSize)
        {
            BallDirectionUp = true;
        }

        if ((BallDirectionUp) && (ballPositionY <= 7) && (ballPositionY >= 5) && (ballPositionX >= Console.WindowWidth / 3) && (ballPositionX <= Console.WindowWidth / 3 * 2) && (wall[ballPositionY - 5, ballPositionX - 27] == false))
        {
            wall[ballPositionY - 5, ballPositionX - 27] = true;
            BallDirectionUp = false;
            result++;
        }
        else if ((BallDirectionUp == false) && (ballPositionY >= 3) && (ballPositionY <= 6) && (ballPositionX >= Console.WindowWidth / 3) && (ballPositionX <= Console.WindowWidth / 3 * 2) && (wall[ballPositionY - 3, ballPositionX - 27] == false))
        {
            wall[ballPositionY - 3, ballPositionX - Console.WindowWidth / 3] = true;
            BallDirectionUp = true;
            result++;
        }

        
        


        if (BallDirectionRight)
        {
            ballPositionX++;
        }
        else
        {
            ballPositionX--;
        }

        if (BallDirectionUp)
        {
            ballPositionY--;
        }
        else
        {
            ballPositionY++;
        }


    }

    private static void SetTheBallAndPlayerInStartPosition()
    {
        SetInitialPositions();
        BallDirectionUp = true;
        BallDirectionRight = true;
    }

    private static void PrinWall()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(Console.WindowWidth / 3, 4+i);

            for (int j = 0; j < Console.WindowWidth / 3; j++)
            {
                if (wall[i, j] == false)
                {
                    Console.Write("Ш");
                }
                else
                {
                    Console.Write(' ');
                }
            }
        }
    }

    private static void MovePlayerRight()
    {
        if (playerPosition+padSize < Console.WindowWidth)
        {
            playerPosition += 2;    
        }
        
    }

    private static void MovePlayerLeft()
    {
        if (playerPosition > 0)
        {
            playerPosition -= 2;
        }
        
    }

    private static void PrintPlayerData()
    {
        Console.SetCursorPosition(0,0);
        Console.Write("Bars:");
        for (int i = 0; i < bars; i++)
        {
            Console.Write(" =");
        }
        Console.SetCursorPosition(Console.WindowWidth/2-5, 0);
        Console.Write("Result: {0}", result);
        Console.SetCursorPosition(0, 1);
        var line = String.Concat(Enumerable.Repeat("-", Console.WindowWidth));
        Console.WriteLine(line);
    }

    private static void DrawBall()
    {
        PrintAtPosition(ballPositionX, ballPositionY , '@');
    }

    private static void SetInitialPositions()
    {
        playerPosition = Console.WindowWidth / 2 - padSize / 2;
        ballPositionX = 6;//Console.WindowWidth / 2;
        ballPositionY = Console.WindowHeight - 3;


        //playerPosition = Console.WindowWidth/2 - padSize/2;
        //ballPositionX = Console.WindowWidth/2;
        //ballPositionY = Console.WindowHeight - 3;
    }

    private static void DrawPlayer(int padSize)
    {
        for (int x = playerPosition; x < playerPosition+padSize; x++)
        {
            PrintAtPosition(x, Console.WindowHeight-2, '=');
        }              
    }

    private static void PrintAtPosition(int x, int y, char symbol)
    {
        
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }

    private static void RemoveScrollBars()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
        Console.Title = "Brick Destroyer - Team \"IRIDONIA\"";
    }
}
