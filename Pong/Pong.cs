using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pong
{
    class Pong
    {
        private static int consoleWidth = Console.WindowWidth;
        private static int consoleHeight = Console.WindowHeight;
        private static int firstPlayerPadSize = 4;
        private static int secondPlayerPadSize = 4;
        private static int ballPositionX = 0;
        private static int ballPositionY = 0;
        private static bool ballDirectionUp = true;
        private static bool ballDirectionRight = true;
        private static int firstPlayerPosition = consoleHeight / 2 - (firstPlayerPadSize / 2);
        private static int secondPlayerPosition = consoleHeight / 2 - (secondPlayerPadSize / 2);
        private static int firstPlayerScore = 0;
        private static int secondPlayerScore = 0;
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            RemoveScrollBars();
            ShowStartScreen();
            ResetBallPosition();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.W)
                    {
                        MoveFirstPlayerUp();
                    }

                    if (keyInfo.Key == ConsoleKey.S)
                    {
                        MoveFirstPlayerDown();
                    }

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveSecondPlayerUp();
                    }

                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveSecondPlayerDown();
                    }
                }

                MoveBall();
                Console.Clear();
                DrawFirstPlayer();
                DrawSecondPlayer();
                DrawBall();
                PrintResult();

                Thread.Sleep(60);
            }
        }

        private static void ShowStartScreen()
        {
            Console.SetCursorPosition(consoleWidth / 2 - 6, 1);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("How to play?");
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("First player (Left pad) - press 'W' to move UP and press 'S' to move DOWN");
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Second player (Right pad) - press 'Up Arrow' to move UP and press'Down Arrow' to move DOWN");
            Console.SetCursorPosition(consoleWidth / 2 - 15, 12);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to start the game");

            string author = "Created by Tsvetomir Kirchev";
            string site = "www.tsvetomir-kirchev.com";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(consoleWidth - author.Length, consoleHeight - 2);
            Console.Write(author);
            Console.SetCursorPosition(consoleWidth - site.Length, consoleHeight - 1);
            Console.Write(site);
            Console.ReadKey();
        }

        private static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BufferHeight = consoleHeight;
            Console.BufferWidth = consoleWidth;
        }

        private static void ResetBallPosition()
        {
            ballPositionX = consoleWidth / 2;
            ballPositionY = consoleHeight / 2;
        }

        private static void MoveBall()
        {
            if (ballDirectionUp)
            {
                ballPositionY--;
            }
            else
            {
                ballPositionY++;
            }

            if (ballDirectionRight)
            {
                ballPositionX++;
            }
            else
            {
                ballPositionX--;
            }

            CheckForCollisions();
        }

        private static void CheckForCollisions()
        {
            if (ballPositionY == 0)
            {
                ballDirectionUp = false;
            }
            if (ballPositionY == consoleHeight - 1)
            {
                ballDirectionUp = true;
            }

            if (ballPositionX == consoleWidth - 1)
            {
                firstPlayerScore++;
                ballDirectionRight = false;
                ResetBallPosition();
            }
            if (ballPositionX == 0)
            {
                secondPlayerScore++;
                ballDirectionRight = true;
                ResetBallPosition();
            }

            if (ballPositionX < 3)
            {
                if (ballPositionY >= firstPlayerPosition &&
                    ballPositionY <= firstPlayerPosition + firstPlayerPadSize)
                {
                    ballDirectionRight = true;
                }
            }
            if (ballPositionX > consoleWidth - 4)
            {
                if (ballPositionY >= secondPlayerPosition &&
                    ballPositionY <= secondPlayerPosition + secondPlayerPadSize)
                {
                    ballDirectionRight = false;
                }
            }

        }

        private static void DrawBall()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintAtPosition(ballPositionX, ballPositionY, '*');
        }

        private static void DrawFirstPlayer()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int y = firstPlayerPosition; y < firstPlayerPosition + firstPlayerPadSize; y++)
            {
                PrintAtPosition(0, y, '[');
                PrintAtPosition(1, y, ']');
            }
        }

        private static void DrawSecondPlayer()
        {
            for (int y = secondPlayerPosition; y < secondPlayerPosition + secondPlayerPadSize; y++)
            {
                PrintAtPosition(consoleWidth - 1, y, ']');
                PrintAtPosition(consoleWidth - 2, y, '[');
            }
        }

        private static void MoveFirstPlayerUp()
        {
            if (firstPlayerPosition != 0)
            {
                firstPlayerPosition--;
            }
        }

        private static void MoveFirstPlayerDown()
        {
            if (firstPlayerPosition != consoleHeight - firstPlayerPadSize)
            {
                firstPlayerPosition++;
            }
        }

        private static void MoveSecondPlayerUp()
        {
            if (secondPlayerPosition != 0)
            {
                secondPlayerPosition--;
            }
        }

        private static void MoveSecondPlayerDown()
        {
            if (secondPlayerPosition != consoleHeight - secondPlayerPadSize)
            {
                secondPlayerPosition++;
            }
        }

        private static void PrintResult()
        {
            string result = String.Format("{0} - {1}", firstPlayerScore, secondPlayerScore);
            Console.SetCursorPosition((consoleWidth / 2) - (result.Length / 2), 0);
            Console.WriteLine(result);
        }

        private static void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}
