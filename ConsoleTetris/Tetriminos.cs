using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTetris
{
    public enum TetriminoEnum { I = 0, J = 1, L = 2, O = 3, S = 4, T = 5, Z = 6 }
    public class Tetrimino
    {
        private TetriminoEnum tetrimino;
        private byte rotation;//0 or 1 or 2 or 3

        public Tetrimino(TetriminoEnum tetrimino)
        {
            this.tetrimino = tetrimino;
            this.rotation = 0;
        }

        public int GetHeight()
        {
            return GetTetrimino().GetLength(0);
        }

        public int GetWidth()
        {
            return GetTetrimino().GetLength(1);
        }

        public TetriminoEnum GetTetriminoEnum()
        {
            return tetrimino;
        }

        public Tetrimino Rotate()
        {
            rotation = (byte)(rotation == 3 ? 0 : (rotation + 1));
            return this;
        }

        public byte[,] GetTetrimino()
        {
            switch (tetrimino)
            {
                case TetriminoEnum.I: return rotation == 0 ? Tetriminos.I : rotation == 1 ? Tetriminos.I90 : rotation == 2 ? Tetriminos.I180 : Tetriminos.I270;
                case TetriminoEnum.J:return rotation == 0 ? Tetriminos.J : rotation == 1 ? Tetriminos.J90 : rotation == 2 ? Tetriminos.J180 : Tetriminos.J270;
                case TetriminoEnum.L:return rotation == 0 ? Tetriminos.L : rotation == 1 ? Tetriminos.L90 : rotation == 2 ? Tetriminos.L180 : Tetriminos.L270;
                case TetriminoEnum.O:return rotation == 0 ? Tetriminos.O : rotation == 1 ? Tetriminos.O90 : rotation == 2 ? Tetriminos.O180 : Tetriminos.O270;
                case TetriminoEnum.S:return rotation == 0 ? Tetriminos.S : rotation == 1 ? Tetriminos.S90 : rotation == 2 ? Tetriminos.S180 : Tetriminos.S270;
                case TetriminoEnum.T:return rotation == 0 ? Tetriminos.T : rotation == 1 ? Tetriminos.T90 : rotation == 2 ? Tetriminos.T180 : Tetriminos.T270;
                case TetriminoEnum.Z:return rotation == 0 ? Tetriminos.Z : rotation == 1 ? Tetriminos.Z90 : rotation == 2 ? Tetriminos.Z180 : Tetriminos.Z270;
            }
            return null;
        }

        public string Draw()
        {
            byte[,] tet = GetTetrimino();
            string str = "";
            for (int i = 0; i < tet.GetLength(0); i++)
            {
                for (int j = 0; j < tet.GetLength(1); j++)
                {
                    str += tet[i, j] == 1 ? Program.BLOCK : Program.SPACE;
                }
                str += "\n";
            }
            return str;
        }
    }

    public static class Tetriminos
    {
        //0 degrees
        public static readonly byte[,] I = { { 1, 1, 1, 1 } };

        public static readonly byte[,] J = { { 1, 1, 1 },
                                             { 0, 0, 1} };

        public static readonly byte[,] L = { { 1, 1, 1 },
                                             { 1, 0, 0} };

        public static readonly byte[,] O = { { 1, 1 },
                                             { 1, 1 } };

        public static readonly byte[,] S = { { 0, 1, 1 }, 
                                             { 1, 1, 0 } };

        public static readonly byte[,] T = { { 1, 1, 1 },
                                             { 0, 1, 0 } };

        public static readonly byte[,] Z = { { 1, 1, 0 },
                                             { 0, 1, 1 } };

        //90 degrees
        public static readonly byte[,] I90 = { { 1 },
                                               { 1 },
                                               { 1 },
                                               { 1 } };

        public static readonly byte[,] J90 = { { 0, 1 },
                                               { 0, 1 },
                                               { 1, 1 } };

        public static readonly byte[,] L90 = { { 1, 1 },
                                               { 0, 1 },
                                               { 0, 1 } };

        public static readonly byte[,] O90 = { { 1, 1 },
                                               { 1, 1 } };

        public static readonly byte[,] S90 = { { 1, 0 },
                                               { 1, 1 },
                                               { 0, 1 } };

        public static readonly byte[,] T90 = { { 0, 1 },
                                               { 1, 1 },
                                               { 0, 1 } };

        public static readonly byte[,] Z90 = { { 0, 1 },
                                               { 1, 1 },
                                               { 1, 0 } };

        //180 degrees
        public static readonly byte[,] I180 = { { 1, 1, 1, 1 } };

        public static readonly byte[,] J180 = { { 1, 0, 0 },
                                                { 1, 1, 1} };

        public static readonly byte[,] L180 = { { 0, 0, 1 },
                                                { 1, 1, 1 } };

        public static readonly byte[,] O180 = { { 1, 1 },
                                                { 1, 1 } };

        public static readonly byte[,] S180 = { { 0, 1, 1 }, 
                                                { 1, 1, 0 } };

        public static readonly byte[,] T180 = { { 0, 1, 0 },
                                                { 1, 1, 1 } };

        public static readonly byte[,] Z180 = { { 1, 1, 0 },
                                                { 0, 1, 1 } };


        //270 degrees
        public static readonly byte[,] I270 = { { 1 },
                                                { 1 },
                                                { 1 },
                                                { 1 } };

        public static readonly byte[,] J270 = { { 1, 1 },
                                                { 1, 0 },
                                                { 1, 0 } };

        public static readonly byte[,] L270 = { { 1, 0 },
                                                { 1, 0 },
                                                { 1, 1 } };

        public static readonly byte[,] O270 = { { 1, 1 },
                                                { 1, 1 } };

        public static readonly byte[,] S270 = { { 1, 0 },
                                                { 1, 1 },
                                                { 0, 1 } };

        public static readonly byte[,] T270 = { { 1, 0 },
                                                { 1, 1 },
                                                { 1, 0 } };

        public static readonly byte[,] Z270 = { { 0, 1 },
                                                { 1, 1 },
                                                { 1, 0 } };
    }
}
