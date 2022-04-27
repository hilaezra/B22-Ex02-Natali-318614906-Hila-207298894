using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class Board
    {
        private CellInBoard[,] m_GameBoard;
        private int m_BoardSize;

        public enum Column
        {
            A, B, C, D, E, F, G, H, I, J
        }

        public enum Row
        {
            a, b, c, d, e, f, g, h, i, j
        }

        public static bool CheckThatWeDontGoBeyondBoundaries(int i_SizeOfTheBoard, int i_WantedRow, int i_WantedCol)
        {
            bool returnAnswer = false;
            if ((i_WantedCol < i_SizeOfTheBoard && i_WantedCol >= 0) && (i_WantedRow < i_SizeOfTheBoard && i_WantedRow >= 0))
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }

        public void InitBoard(Player io_Player1, Player io_Player2)
        {
            int playersNumOfRow = (m_BoardSize - 2) / 2;
            io_Player1.Positions.Clear();
            io_Player2.Positions.Clear();

            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    if (row < playersNumOfRow)
                    {
                        InitXAndOCells(row, col, 1, io_Player1, io_Player2);
                    }
                    else if (row >= playersNumOfRow && row < playersNumOfRow + 2)
                    {
                        m_GameBoard[row, col] = new CellInBoard(true, 3);
                    }
                    else
                    {
                        InitXAndOCells(row, col, 2, io_Player1, io_Player2);
                    }
                }
            }
        }

        private void InitXAndOCells(int i_Row, int i_Col, int i_PlayerSing, Player io_Player1, Player io_Player2)
        {
            Point newPoint = new Point(i_Row, i_Col);
            if ((i_Row % 2 == 0 && i_Col % 2 != 0) || (i_Row % 2 != 0 && i_Col % 2 == 0))
            {
                m_GameBoard[i_Row, i_Col] = new CellInBoard(false, i_PlayerSing);
                if (i_PlayerSing == (int)Player.PlayerNumber.One)
                {
                    io_Player1.Positions.Add(newPoint);
                }
                else
                {
                    io_Player2.Positions.Add(newPoint);
                }
            }
            else
            {
                m_GameBoard[i_Row, i_Col] = new CellInBoard(true, 3);
            }
        }

        public CellInBoard[,] GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }

        public Board(int i_SizeOfBoard)
        {
            m_BoardSize = i_SizeOfBoard;
            m_GameBoard = new CellInBoard[m_BoardSize, m_BoardSize];
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }
    }
}