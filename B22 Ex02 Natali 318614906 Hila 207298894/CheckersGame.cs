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
            bool check = true, isEaten = false;//סתם....
            int i = 0;
            while (check)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                i_Board.PrintBoard();
                string userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize);
                List<int> userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
                if (i % 2 == 0)
                {
                    if (i_PlayerNumber1.CheckIfTheCurrPositionIsMine(i_Board, userMoverInInt, ref isEaten))//להכניס לפונקציה-יש שכפול
                    {
                        i_PlayerNumber1.MovePlayerOnBoard(i_Board, userMoverInInt, isEaten, 1);
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    if (i_PlayerNumber2.CheckIfTheCurrPositionIsMine(i_Board, userMoverInInt, ref isEaten))
                    {
                        i_PlayerNumber2.MovePlayerOnBoard(i_Board, userMoverInInt, isEaten, 2);
                    }
                    else
                    {
                        check = false;
                    }
                }
                i++;
                System.Threading.Thread.Sleep(1200);
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
