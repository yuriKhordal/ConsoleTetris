using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTetris
{
    public static class Input
    {
        public static void CheckForKey()
        {
            ConsoleKeyInfo info;
            while (!Program.terminate)
            {
                if (!Program.haveARest)
                {
                    if (Console.KeyAvailable)
                    {
                        info = Console.ReadKey(true);
                        if (info.Key == ConsoleKey.W || info.Key == ConsoleKey.UpArrow)
                        {
                            TetrisBoard.activeTermino.Rotate();
                            TetrisBoard.UpdateActiveCoords();
                        }
                        else if (info.Key == ConsoleKey.D || info.Key == ConsoleKey.RightArrow)
                        {
                            if (TetrisBoard.activeTermino.GetWidth() + TetrisBoard.topLeft.x < Program.BOARD_WIDTH)
                            {
                                TetrisBoard.topLeft.x++;
                                TetrisBoard.UpdateActiveCoords();
                            }
                        }
                        else if (info.Key == ConsoleKey.A || info.Key == ConsoleKey.LeftArrow)
                        {
                            if (TetrisBoard.topLeft.x >= 1)
                            {
                                TetrisBoard.topLeft.x--;
                                TetrisBoard.UpdateActiveCoords();
                            }
                        }
                        else if (info.Key == ConsoleKey.S || info.Key == ConsoleKey.DownArrow)
                        {
                            if (!TetrisBoard.CheckIfHit())
                                TetrisBoard.topLeft.y++;
                            else
                            {

                            }
                        }
                        else if (info.Key == ConsoleKey.Escape)
                        {
                            //Program.Game_Over();
                        }
                    }//endif
                }//endif
            }//endwhile
        }//endmethod
    }
}
