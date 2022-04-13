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
            bool endGame = false, isEaten = false;//סתם....
            int i = 0,indexWhoEat=1;
            while (!endGame)
            {
                isEaten = false;
                Ex02.ConsoleUtils.Screen.Clear();
                i_Board.PrintBoard(i_PlayerNumber1, i_PlayerNumber2);
                i_PlayerNumber1.PrintPlayersDetails();
                i_PlayerNumber2.PrintPlayersDetails();
                string userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize,i,i_PlayerNumber1,i_PlayerNumber2);
                List<int> userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
                if (i % 2 == 0)
                {
                    indexWhoEat = 1;
                    CheckPositionAndMove(i_PlayerNumber1, i_Board, userMoverInInt, ref isEaten, ref i, indexWhoEat);
                          
                }
                else
                {
                    indexWhoEat = 2;
                    CheckPositionAndMove(i_PlayerNumber2, i_Board, userMoverInInt, ref isEaten, ref i, indexWhoEat);

                }
                if (isEaten)
                {
                    if (indexWhoEat == 1)
                    {
                        UpdatePlayersAfterEaten(i_PlayerNumber1, i_PlayerNumber2,ref endGame);
                    }
                    else if(indexWhoEat==2)
                    {
                        UpdatePlayersAfterEaten(i_PlayerNumber2, i_PlayerNumber1, ref endGame);
                    }
                }
                i++;
                
                System.Threading.Thread.Sleep(500);
            }
        }
        public static void CheckPositionAndMove(Player i_Player, Board i_Board,List<int> i_UserMoveInt,ref bool io_IsEaten,ref int i,int i_NumberOfPlayer)
        {
            if (!i_Player.CheckIfTheCurrPositionIsMine(i_Board, i_UserMoveInt, ref io_IsEaten))
            {
                i--;
            }
            else
            {
                i_Player.MovePlayerOnBoard(i_Board, i_UserMoveInt, io_IsEaten, i_NumberOfPlayer);
            }
        }
        public static void UpdatePlayersAfterEaten(Player i_WinPlayer,Player i_LoserPlayer,ref bool i_EndGame)
        {
            AddPointAfterEat(i_WinPlayer);
            i_EndGame=SubPieceOfTheRemainPiece(i_LoserPlayer);
        }
        public static bool SubPieceOfTheRemainPiece(Player i_Player)
        {
            bool returnAnswer = false;//True only if there isnt remain piece after the sub
            int remainPieces = i_Player.RemainPieces--;
            if (remainPieces == 0)
                returnAnswer = true;

            return returnAnswer;
        }
        public static void AddPointAfterEat(Player i_Player)
        {
            i_Player.PointsOfPlayer++;
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
