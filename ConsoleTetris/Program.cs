using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleTetris
{
    class Program
    {
        public const char BLOCK = '#';
        public const char SPACE = ' ';
        public const byte BOARD_HEIGHT = 22;
        public const byte BOARD_WIDTH = 12;
        public const double FPS_CAP = 15;
        public const double SECOND = 1000;

        public static double TimePerTurn = SECOND;
        public static DateTime lastTurn;
        public static DateTime currentTurn;
        public static TimeSpan deltaTurn;

        static DateTime start;
        static DateTime lastFrame = DateTime.Now;
        static DateTime currentFrame = DateTime.Now;

        public static string firstLine = "";
        public static string secondLine = "";
        public static int score = 0;
        public static string debugLine = "";

        public static Tetrimino currentTetrimino;
        public static Tetrimino nextTetrimino;

        public static bool terminate = false;
        public static bool haveARest = false;
        public static Thread KeyListener;

        public static void Main(string[] args)
        {
            /*TetrisBoard.board[0, 0] = 0;
            TetrisBoard.board[0, 1] = 0;
            TetrisBoard.board[0, 2] = 1;
            TetrisBoard.board[1, 0] = 0;
            TetrisBoard.board[1, 1] = 1;
            TetrisBoard.board[1, 2] = 0;
            TetrisBoard.board[2, 0] = 1;
            TetrisBoard.board[2, 1] = 0;
            TetrisBoard.board[2, 2] = 1;
            Console.WriteLine(TetrisBoard.Draw());
            TetrisBoard.Next();
            Console.WriteLine("\n" + TetrisBoard.Draw());*/
            Console.WindowHeight = 40;
            Console.WindowWidth = 40;
            start = DateTime.Now;
            lastFrame = start;
            KeyListener = new Thread(new ThreadStart(Input.CheckForKey));
            KeyListener.Start();
            currentTetrimino = GenerateRandomTetrimino();
            TetrisBoard.activeTermino = currentTetrimino;
            TetrisBoard.UpdateActiveCoords();
            currentTetrimino = new Tetrimino(currentTetrimino.GetTetriminoEnum());
            nextTetrimino = GenerateRandomTetrimino();
            lastTurn = DateTime.Now;
            while (!terminate)
                Update();
            Console.ReadKey();
        }

        static void Draw()
        {
            if (!terminate)
            {
                Console.Clear();
                Console.WriteLine(firstLine);
                Console.WriteLine(secondLine);
                Console.WriteLine("Score: " + score);
                Console.WriteLine(debugLine + "\n");
                Console.WriteLine("Now:\n" + currentTetrimino.Draw() + "\nNext:\n" + nextTetrimino.Draw());
                Console.WriteLine(TetrisBoard.Draw());
            }
        }

        static void Pre_Update()
        {
            currentFrame = DateTime.Now;
            TimeSpan delta = currentFrame.Subtract(lastFrame);
            lastFrame = currentFrame;
            TimeSpan duration = currentFrame.Subtract(start);
            currentTurn = DateTime.Now;
            deltaTurn = currentTurn.Subtract(lastTurn);
            if (deltaTurn.TotalMilliseconds >= TimePerTurn)
            {
                lastTurn = currentTurn;
                TetrisBoard.DoTurn();
            }
            while (delta.TotalMilliseconds < (1 / FPS_CAP) * SECOND)
            {
                currentFrame = DateTime.Now;
                delta = currentFrame.Subtract(lastFrame);
            }
            firstLine = duration.Hours + ":" + duration.Minutes + ":" + duration.Seconds;
            secondLine = (1 / delta.TotalSeconds) + " FPS\t" + delta.TotalMilliseconds + " ms";
            haveARest = true;
        }

        static void Update()
        {
            Pre_Update();
            TetrisBoard.UpdateActiveCoords();
            Draw();
            Post_Update();
        }

        static void Post_Update()
        {
            haveARest = false;
        }

        static Tetrimino GenerateRandomTetrimino()
        {
            Random random = new Random();
            int num = random.Next(0, 7);
            Tetrimino tetrimino = new Tetrimino((TetriminoEnum)num);
            return tetrimino;
        }

        public static Tetrimino SwitchTetrimino()
        {
            currentTetrimino = nextTetrimino;
            nextTetrimino = GenerateRandomTetrimino();
            return new Tetrimino(currentTetrimino.GetTetriminoEnum());
        }

        public static void Game_Over()
        {
            Console.Clear();
            terminate = true;
            int final_score = score;
            Console.WriteLine("\n\n\n\n\n\n\n\n");
            Console.WriteLine("   ##     ##     ###      ##      ## \n" + 
                              "    ##   ##    ##   ##    ##      ## \n" + 
                              "     ## ##    ##     ##   ##      ## \n" + 
                              "      ###     ##     ##   ##      ## \n" + 
                              "      ###     ##     ##    ##    ### \n" + 
                              "      ###      ##   ##      ##  #### \n" +
                              "      ###        ###         #### ## \n");
            Console.WriteLine("##          ###        ######     ###### \n" + 
                              "##        ##   ##     ##         ##      \n" + 
                              "##       ##     ##   ##          ##      \n" + 
                              "##       ##     ##    #######    ######  \n" + 
                              "##       ##     ##          ##   ##      \n" + 
                              "##        ##   ##            ##  ##      \n" + 
                              "#######     ###       ########    ###### \n" );
            Console.WriteLine("\n\n\nYour score is: " + final_score);
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
