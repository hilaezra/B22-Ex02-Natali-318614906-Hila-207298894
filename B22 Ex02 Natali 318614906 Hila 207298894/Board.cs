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
            Console.Clear();
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

        public void PrintBoard()
        {
            int lineSize = (m_BoardSize * 4) + 3;
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            PrintBoardFrameAndDividingLine(lineSize, 0, false, letters);
            PrintBoardSquares( lineSize, letters);
        }

        public void PrintBoardFrameAndDividingLine(int i_LineSize, int i_LetterIndex, bool i_DividingLine, string[] i_LettersArry)
        {
            StringBuilder lineOfBoard = new StringBuilder(i_LineSize);

            if (i_DividingLine != true)
            {
                for (int i = 0; i < i_LineSize; i++)
                {
                    if ((i + 1) % 4 == 0 || i == 3)
                    {
                        lineOfBoard.Insert(i, i_LettersArry[i_LetterIndex]);
                        i_LetterIndex++;
                    }
                    else
                    {
                        lineOfBoard.Insert(i, " ");
                    }
                }
                Console.WriteLine("{0}", lineOfBoard);
            }

            for (int i = 0; i < i_LineSize - 1; i++)
            {
                Console.Write("=");
            }
            Console.Write(Environment.NewLine);
        }

        public void PrintBoardSquares( int i_LineSize, string[] i_LettersArry)
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                int matrixIndex = 0;
                StringBuilder lineOfBoard = new StringBuilder(i_LineSize);

                for (int j = 0; j < i_LineSize; j++)
                {
                    if (j == 0)
                    {
                        lineOfBoard.Insert(j, i_LettersArry[i].ToLower());
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
                Console.WriteLine(lineOfBoard);
                PrintBoardFrameAndDividingLine(i_LineSize, 0, true, i_LettersArry);
            }
        }

        public CellInBoard[,] GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }
    }
}
