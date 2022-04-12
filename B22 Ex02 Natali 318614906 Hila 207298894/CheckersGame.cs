using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class CheckersGame
    {
        private bool m_IsPlayingAgainstComputer;

        public static void RunGame(Board i_Board, Player i_PlayerNumber1, Player i_PlayerNumber2)
        {
            bool check = true;//סתם....
            int i = 0;
            while (check)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                i_Board.PrintBoard();
                string userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize);//כל זה יהיה בתוך לולאה כמובן. זה רק לשם הבדיקה :)
                List<int> userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
                if (i % 2 == 0)
                {
                    if (i_PlayerNumber1.CheckIfTheCurrPositionIsMine(i_Board, userMoverInInt))
                    {
                        i_PlayerNumber1.MovePlayerOnBoard(i_Board, userMoverInInt);
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    if (i_PlayerNumber2.CheckIfTheCurrPositionIsMine(i_Board, userMoverInInt))
                    {
                        i_PlayerNumber2.MovePlayerOnBoard(i_Board, userMoverInInt);
                    }
                    else
                    {
                        check = false;
                    }
                }
                i++;
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Enter move"));
                System.Threading.Thread.Sleep(2000);
            }
        }

        public static void InitializationGame()
        {
            bool firstPlayer = true;
            string nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
            int sizeOfBoard = UserInputManagement.GetAndCheckValidBoardSize();
            Player playerNumber1 = new Player(nameOfPlayer, sizeOfBoard, 'O');//PlayerNumber1IsReady            
            Player playerNumber2 = new Player("computer", sizeOfBoard, 'X'); ;
            Board board = new Board(sizeOfBoard);
            if (!UserInputManagement.PlayAgainstComputer())
            {
                firstPlayer = false;
                nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
                playerNumber2.NameOfPlayer = nameOfPlayer;//SET
            }
            board.InitBoard();
            RunGame(board, playerNumber1, playerNumber2);
        }
    }
}
