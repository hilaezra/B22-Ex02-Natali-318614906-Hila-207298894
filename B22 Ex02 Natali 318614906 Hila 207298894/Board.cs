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
           
            InitORows(i_SizeOfBoard, playersNumOfRow);
            Init2EmptyRows(i_SizeOfBoard, playersNumOfRow);
            InitXRows(i_SizeOfBoard, playersNumOfRow);
        }

        public void InitORows(int i_SizeOfBoard, int i_PlayersNumOfRow)
        {
            for (int row = 0; row < i_PlayersNumOfRow; row++)
            {
                for (int col = 0; col < i_SizeOfBoard; col++)
                {
                    Point newPoint = new Point(row, col);
                    m_GameBoard[row, col]._point = newPoint;
                    m_GameBoard[row, col].playerInBoard.isKing = false;

                    if (row % 2 == 0 && col % 2 != 0)
                    {
                        m_GameBoard[row, col].isEmpty = false;
                        m_GameBoard[row, col].playerInBoard.signOfPlayerInBoard = 'O';
                    }
                    else
                    {
                        m_GameBoard[row, col].isEmpty = true;
                        m_GameBoard[row, col].playerInBoard.signOfPlayerInBoard = ' ';
                    }
                }
            }
        }

        public void Init2EmptyRows(int i_SizeOfBoard, int i_PlayersNumOfRow)
        {
            for (int row = i_PlayersNumOfRow; row < i_PlayersNumOfRow + 2; row++)
            {
                for (int col = 0; col < i_SizeOfBoard; col++)
                {
                    Point newPoint = new Point(row, col);
                    m_GameBoard[row, col]._point = newPoint;
                    m_GameBoard[row, col].playerInBoard.isKing = false;
                    m_GameBoard[row, col].isEmpty = true;
                    m_GameBoard[row, col].playerInBoard.signOfPlayerInBoard = ' ';
                }
            }
        }

        public void InitXRows(int i_SizeOfBoard, int i_PlayersNumOfRow)
        {
            for (int row = i_PlayersNumOfRow + 2; row < i_SizeOfBoard; row++)
            {
                for (int col = 0; col < i_SizeOfBoard; col++)
                {
                    Point newPoint = new Point(row, col);
                    m_GameBoard[row, col]._point = newPoint;
                    m_GameBoard[row, col].playerInBoard.isKing = false;

                    if (row % 2 != 0 && col % 2 == 0)
                    {
                        m_GameBoard[row, col].isEmpty = false;
                        m_GameBoard[row, col].playerInBoard.signOfPlayerInBoard = 'X';
                    }
                    else
                    {
                        m_GameBoard[row, col].isEmpty = true;
                        m_GameBoard[row, col].playerInBoard.signOfPlayerInBoard = ' ';
                    }
                }
            }
        }


        ///////// Only check!!!!!! ////////
        public void PrintBoard(int i_SizeOfBoard)
        {
            for(int i =0;i<i_SizeOfBoard;i++)
            {
                for(int j=0;j<i_SizeOfBoard;j++)
                {
                    Console.WriteLine(m_GameBoard[i, j].playerInBoard.signOfPlayerInBoard);
                }
            }
        }
    }
}
