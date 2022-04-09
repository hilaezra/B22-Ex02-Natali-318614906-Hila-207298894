using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class Board
    {
        private CellInBoard[,] m_GameBoard;
        public enum Column
        {
            A, B, C, D, E, F, G, H, I, J
        }
        public enum Row
        {
            a, b, c, d, e, f, g, h, i, j
        }
        public Board(int i_SizeOfBoard)
        {
            m_GameBoard = new CellInBoard[i_SizeOfBoard, i_SizeOfBoard];
        }

        public void InitBoard(int i_SizeOfBoard)
        {
            int playersNumOfRow = (i_SizeOfBoard - 2) / 2;
            //init O 
            for (int row = 0; row < playersNumOfRow; row++)
            {
                for (int col = 0; col < i_SizeOfBoard; col++)
                {
                    if(row % 2 == 0 && col % 2 != 0)
                    {
                        Point newPoint = new Point(row, col);
                        m_GameBoard
                    }
                }
            }
            //init 2 empty rows
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < i_SizeOfBoard; j++)
                {

                }
            }
            //init X
            for (int row = 0; row < playersNumOfRow; row++)
            {
                for (int col = 0; col < i_SizeOfBoard; col++)
                {

                }
            }
        }


    }
}
