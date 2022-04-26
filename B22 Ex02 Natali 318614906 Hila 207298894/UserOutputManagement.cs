using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class UserOutputManagement
    {
        public static string UpdateThePlayerThatWeHaveGameOverAndAskWhatToDoNext(Player i_PlayerQuit)
        {
            string returnAnswer;
            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                StringBuilder checkIfContinueGame = new StringBuilder();
                checkIfContinueGame.Insert(0, "GAME OVER!!" + Environment.NewLine + i_PlayerQuit.NameOfPlayer + ", Do you want to play another round??" + Environment.NewLine + "Prass 1)Yes  2)No");
                Console.WriteLine(checkIfContinueGame);
                returnAnswer = Console.ReadLine();
            }
            while (returnAnswer != "1" && returnAnswer != "2");

            return returnAnswer;
        }

        private static void PrintToTheScreenWhoWinAndAskHowToContinue(CheckersGame io_CheckersGame,Player i_Winner, Player i_Loser,ref bool io_EndGame,ref int io_IndexOfFirstPlayer)
        {
            string msg = string.Format("Congratulations {0} !!!! You are the    W I N N E R    !", i_Winner.NameOfPlayer);
            Console.WriteLine(msg);
            System.Threading.Thread.Sleep(2500);
            io_EndGame = UserInputManagement.CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(io_CheckersGame,i_Winner, i_Loser, ref io_IndexOfFirstPlayer);
        }

        private static void PrintBoardFrameAndDividingLine(int i_LineSize, int i_LetterIndex, bool i_DividingLine)
        {
            StringBuilder lineOfBoard = new StringBuilder(i_LineSize);
            StringBuilder dividingLine = new StringBuilder(i_LineSize);

            if (i_DividingLine != true)
            {
                for (int i = 0; i < i_LineSize; i++)
                {
                    if ((i + 1) % 4 == 0 || i == 3)
                    {
                        lineOfBoard.Insert(i, (Board.Column)i_LetterIndex);
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

        public static void PrintBoard(Player i_PlayerNumber1, Player i_PlayerNumber2, int i_BoardSize, Board i_Board)
        {
            int lineSize = (i_BoardSize * 4) + 3;

            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoardFrameAndDividingLine(lineSize, 0, false);
            PrintBoardSquares(lineSize, i_Board);
        }

        private static void PrintBoardSquares(int i_LineSize,Board i_Board)
        {
            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                int matrixIndex = 0;
                StringBuilder lineOfBoard = new StringBuilder(i_LineSize);

                for (int j = 0; j < i_LineSize; j++)
                {
                    if (j == 0)
                    {
                        lineOfBoard.Insert(j, (Board.Row)i);
                    }
                    else if ((j - 1) % 4 == 0)
                    {
                        lineOfBoard.Insert(j, "|");
                    }
                    else if ((j + 1) % 4 == 0)
                    {
                        lineOfBoard.Insert(j, i_Board.GameBoard[i, matrixIndex].PlayerInBoard.SignOfPlayerInBoard.ToString());
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

        public static void CheckWhoWinAndAnnounceToThePlayer(CheckersGame io_CheckersGame,int i_Index, Player i_Player1, Player i_Player2, ref bool io_EndGame, ref int io_IndexOfFirstPlayer)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.SetCursorPosition((Console.WindowWidth / 2) - 50, Console.CursorTop + 1);

            if (i_Index == 1)
            {
                PrintToTheScreenWhoWinAndAskHowToContinue(io_CheckersGame, i_Player1, i_Player2, ref io_EndGame,ref io_IndexOfFirstPlayer);
            }
            else
            {
                PrintToTheScreenWhoWinAndAskHowToContinue(io_CheckersGame,i_Player2, i_Player1, ref io_EndGame,ref io_IndexOfFirstPlayer);
            }

        }

        public static void PrintPlayersDetails(Player i_Player)
        {
            StringBuilder playerDetails = new StringBuilder();
            if (i_Player.SignOfPlayer == 'O')
            {
                Console.SetCursorPosition(0, 0);
                playerDetails.Insert(0, "First player name: " + i_Player.NameOfPlayer);
            }
            else
            {
                Console.SetCursorPosition(0, 3);
                playerDetails.Insert(0, Environment.NewLine + "Second player name: " + i_Player.NameOfPlayer);
            }

            playerDetails.Insert(playerDetails.Length, Environment.NewLine + "    Score in totall = " + i_Player.PointsOfPlayer);
            playerDetails.Insert(playerDetails.Length, Environment.NewLine + "    Last move was: = " + i_Player.LastMove);
            Console.WriteLine(playerDetails);
        }
    }
}
