using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTetris
{
    public static class TetrisBoard
    {
        public static byte[,] board = new byte[Program.BOARD_WIDTH, Program.BOARD_HEIGHT];

        public static Tetrimino activeTermino;
        public static Point[] activeCoords;
        public static Point topLeft = new Point() {x = byte.MaxValue, y = byte.MaxValue };

        public static void DoTurn()
        {
            for (int i = 0; i < Program.BOARD_WIDTH; i++)
            {
                for (int j = 1; j < Program.BOARD_HEIGHT; j++)
                {
                    if (board[i, j] == 1 && board[i, j - 1] == 0)
                    {
                        board[i, j] = 0;
                        board[i, j - 1] = 1;
                    }//endif
                }//endfor
            }//endfor
            if (!CheckIfHit())
            {
                topLeft.y++;
                UpdateActiveCoords();
            }
            else
            {
                Program.debugLine = topLeft.y + "";
                if (topLeft.y <= 1)
                    Program.Game_Over();
                SwitchActiveTetrimino(Program.SwitchTetrimino());
            }
            int[] rows;
            if ((rows = CheckForRows()) != null)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    for (int j = 0; j < Program.BOARD_WIDTH; j++)
                    {
                        board[j, i] = 0;
                    }
                }
                Program.score += rows.Length * 100;
            }
        }//endmethod

        public static bool CheckIfHit()
        {
            if (topLeft.y + activeTermino.GetHeight() > Program.BOARD_HEIGHT)
                return true;
            for (int i = 0; i < activeCoords.Length; i++)
            {
                if (board[activeCoords[i].x, activeCoords[i].y - 1] == 1)
                    return true;
            }
            return false; 
        }

        public static int[] CheckForRows()
        {
            List<int> indicies = new List<int>();
            for (int i = 0; i < Program.BOARD_HEIGHT; i++)
            {
                bool rowFull = true;
                for (int j = 0; j < Program.BOARD_WIDTH; j++)
                {
                    if (board[j, i] == 0)
                    {
                        rowFull = false;
                        break;
                    }
                }
                if (rowFull)
                    indicies.Add(i);
            }
            if (indicies.Count == 0)
                return null;
            else
                return indicies.ToArray();
        }

        public static string Draw()
        {
            string draw = "";
            AddActiveCoords();
            for (int i = Program.BOARD_HEIGHT - 1; i >= 0; i--)
            {
                draw += "\t#";
                for (int j = 0; j < Program.BOARD_WIDTH; j++)
                {
                    draw += (board[j, i] == 1 ? Program.BLOCK : Program.SPACE);
                }
                draw += "#\n";
            }
            draw += "\t##";
            for (int i = 0; i < Program.BOARD_WIDTH; i++)
            {
                draw += "#";
            }
            RemoveActiveCoords();
            return draw;
        }

        public static void SwitchActiveTetrimino(Tetrimino newTetrimino)
        {
            for (int i = 0; i < activeCoords.Length; i++)
            {
                board[activeCoords[i].x, activeCoords[i].y] = 1;
            }
            activeTermino = newTetrimino;
            topLeft = new Point() { x = byte.MaxValue, y = byte.MaxValue };
            UpdateActiveCoords();
        }

        public static void AddActiveCoords()
        {
            for (int i = 0; i < activeCoords.Length; i++)
            {
                Point point = activeCoords[i];
                board[point.x, point.y] = 1;
            }
        }

        public static void RemoveActiveCoords()
        {
            for (int i = 0; i < activeCoords.Length; i++)
            {
                Point point = activeCoords[i];
                board[point.x, point.y] = 0;
            }
        }

        public static void UpdateActiveCoords()
        {
            topLeft.x = (byte)(topLeft.x == byte.MaxValue ? Program.BOARD_WIDTH / 2 - 1 : topLeft.x);
            topLeft.y = (byte)(topLeft.y == byte.MaxValue ? 1 : topLeft.y);
            byte[,] tet = activeTermino.GetTetrimino();
            List<TetrisBoard.Point> coords = new List<TetrisBoard.Point>();
            for (int i = tet.GetLength(1) - 1; i >= 0; i--)
            {
                for (int j = 0; j < tet.GetLength(0); j++)
                {
                    if (tet[j, i] == 1)
                        coords.Add(new TetrisBoard.Point() { x = (byte)(i + topLeft.x), y = (byte)(Program.BOARD_HEIGHT - topLeft.y - j) });
                }
            }
            TetrisBoard.activeCoords = coords.ToArray();
        }

        public static bool CompareMatrix(byte[,] matrix1, byte[,] matrix2)
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (matrix1[i, j] != matrix2[i, j])
                        return false;
                }
            }
            return true;
        }

        public struct Point
        {
            public byte x;
            public byte y;
        }
    }
}
