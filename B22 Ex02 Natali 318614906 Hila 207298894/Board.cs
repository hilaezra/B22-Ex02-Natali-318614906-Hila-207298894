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
        public void InitBoard()
        {
            int playersNumOfRow = (m_BoardSize - 2) / 2;

            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    Point newPoint = new Point(row, col);
                    if (row < playersNumOfRow)
                    {
                        InitXAndOCells(m_GameBoard, row, col, 2, newPoint);
                    }
                    else if (row >= playersNumOfRow && row < playersNumOfRow + 2)
                    {
                        m_GameBoard[row, col] = new CellInBoard(newPoint, true, 3);
                    }
                    else
                    {
                        InitXAndOCells(m_GameBoard, row, col, 1, newPoint);
                    }
                }
            }
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
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (lineOfBoard.Length / 2)) + "}", lineOfBoard));
               // Console.WriteLine("{0}", lineOfBoard);
            }

            for (int i = 0; i < i_LineSize - 1; i++)
            {
                dividingLine.Insert(i, "=");
            }
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dividingLine.Length / 2)) + "}", dividingLine));
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
                        lineOfBoard.Insert(j, (m_GameBoard[i, matrixIndex].PlayerInBoard.SignOfPlayerInBoard).ToString());
                        matrixIndex++;
                    }
                    else
                    {
                        lineOfBoard.Insert(j, " ");
                    }
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (lineOfBoard.Length / 2)) + "}", lineOfBoard));
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
