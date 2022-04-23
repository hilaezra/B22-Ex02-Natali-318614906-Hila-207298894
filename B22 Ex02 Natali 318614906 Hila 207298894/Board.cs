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

        public Board(int i_SizeOfBoard)
        {
            m_BoardSize = i_SizeOfBoard;
            m_GameBoard = new CellInBoard[m_BoardSize, m_BoardSize];
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

        public void InitXAndOCells(int i_Row, int i_Col, int i_PlayerSing, Player io_Player1, Player io_Player2)
        {
            Point newPoint = new Point(i_Row, i_Col);
            if ((i_Row % 2 == 0 && i_Col % 2 != 0) || (i_Row % 2 != 0 && i_Col % 2 == 0))
            {
                m_GameBoard[i_Row, i_Col] = new CellInBoard(false, i_PlayerSing);  ////1==O , 2 == X
                if(i_PlayerSing == 1)
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

        public void PrintBoard(Player i_PlayerNumber1, Player i_PlayerNumber2)
        {
            int lineSize = (m_BoardSize * 4) + 3;

            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoardFrameAndDividingLine(lineSize, 0, false);
            PrintBoardSquares(lineSize);
        }

        public void PrintBoardFrameAndDividingLine(int i_LineSize, int i_LetterIndex, bool i_DividingLine)
        {
            StringBuilder lineOfBoard = new StringBuilder(i_LineSize);
            StringBuilder dividingLine = new StringBuilder(i_LineSize);

            if (i_DividingLine != true)
            {
                for (int i = 0; i < i_LineSize; i++)
                {
                    if ((i + 1) % 4 == 0 || i == 3)
                    {
                        lineOfBoard.Insert(i, (Column)i_LetterIndex);
                        i_LetterIndex++;
                    }
                    else
                    {
                        lineOfBoard.Insert(i, " ");
                    }
                }

                Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + (lineOfBoard.Length / 2)) + "}", lineOfBoard));
            }

            for (int i = 0; i < i_LineSize - 1; i++)
            {
                dividingLine.Insert(i, "=");
            }

            Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + (dividingLine.Length / 2)) + "}", dividingLine));
        }

        public void PrintBoardSquares(int i_LineSize)
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                int matrixIndex = 0;
                StringBuilder lineOfBoard = new StringBuilder(i_LineSize);

                for (int j = 0; j < i_LineSize; j++)
                {
                    if (j == 0)
                    {
                        lineOfBoard.Insert(j, (Row)i);
                    }
                    else if ((j - 1) % 4 == 0)
                    {
                        lineOfBoard.Insert(j, "|");
                    }
                    else if ((j + 1) % 4 == 0)
                    {
                        lineOfBoard.Insert(j, m_GameBoard[i, matrixIndex].PlayerInBoard.SignOfPlayerInBoard.ToString());
                        matrixIndex++;
                    }
                    else
                    {
                        lineOfBoard.Insert(j, " ");
                    }
                }

                Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + (lineOfBoard.Length / 2)) + "}", lineOfBoard));
                PrintBoardFrameAndDividingLine(i_LineSize, 0, true);
            }
        }

        public CellInBoard[,] GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }
    }
}