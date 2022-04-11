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
            int boardSize = m_GameBoard.GetLength(0);

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Point newPoint = new Point(row, col);
                    if (row < playersNumOfRow)
                    {
                        InitXAndOCells(m_GameBoard, row, col, 2, newPoint);
                    }
                    else if (row > playersNumOfRow + 1 && row < playersNumOfRow + 2)
                    {
                        m_GameBoard[row, col] = new CellInBoard(newPoint, false, 3);
                    }
                    else
                    {
                        InitXAndOCells(m_GameBoard, row, col, 1, newPoint);
                    }
                }
            }
            PrintBoard(); //checkkkkkkkkkkkkk
        }

        public void InitXAndOCells(CellInBoard[,] i_GameBoard, int i_Row, int i_Col, int i_PlayerSing, Point i_NewPoint)
        {
            if ((i_Row % 2 == 0 && i_Col % 2 != 0) || (i_Row % 2 != 0 && i_Col % 2 == 0))
            {
                i_GameBoard[i_Row, i_Col] = new CellInBoard(i_NewPoint, false, i_PlayerSing);
            }
            else
            {
                i_GameBoard[i_Row, i_Col] = new CellInBoard(i_NewPoint, false, 3);
            }
        }

        ///////// Only check!!!!!! ////////
        public void PrintBoard()
        {
            int bordSize = m_GameBoard.GetLength(0);
            StringBuilder lineOfBoard = new StringBuilder(bordSize * 4);

            for (int i = 0; i < m_GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_GameBoard.GetLength(0); j++)
                {
                    Console.Write(m_GameBoard[i, j].PlayerInBoard.signOfPlayerInBoard);
                }
                Console.Write(Environment.NewLine);
            }
        }
        public CellInBoard[,] GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }
    }
}
