using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class CheckersGame
    {
        private bool m_IsPlayingAgainstComputer;

        public static void InitializationGame()
        {
            bool firstPlayer = true;
            string nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
            int sizeOfBoard = UserInputManagement.GetAndCheckValidBoardSize();
            Player playerNumber1 = new Player(nameOfPlayer, sizeOfBoard, 'O', 1); ////PlayerNumber1IsReady
            Player playerNumber2 = new Player("computer", sizeOfBoard, 'X', 2);
            Board board = new Board(sizeOfBoard);
            if (!UserInputManagement.PlayAgainstComputer())
            {
                firstPlayer = false;
                nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
                playerNumber2.NameOfPlayer = nameOfPlayer; ////SET
            }

            board.InitBoard();
            RunGame(board, playerNumber1, playerNumber2);
        }
        public static void RunGame(Board i_Board, Player i_PlayerNumber1, Player i_PlayerNumber2)
        {
            bool endGame = false, isEaten = false;
            int i = 0;
            while (!endGame)
            {
                isEaten = false;
                Ex02.ConsoleUtils.Screen.Clear();
                i_Board.PrintBoard(i_PlayerNumber1, i_PlayerNumber2);
                i_PlayerNumber1.PrintPlayersDetails();
                i_PlayerNumber2.PrintPlayersDetails();

                if (i_PlayerNumber2.NameOfPlayer == "computer")
                {
                    RunGameAgainstComputer(ref i, i_Board, i_PlayerNumber1, i_PlayerNumber2, ref isEaten, ref endGame);
                }
                else
                {
                    RunGameAgainstOtherPlayer(ref i, i_Board, i_PlayerNumber1, i_PlayerNumber2, ref isEaten, ref endGame);
                }

                i++;
                System.Threading.Thread.Sleep(500);
            }
        }

        public static void RunGameAgainstOtherPlayer(ref int i_WhichPlayer, Board i_Board, Player i_PlayerNumber1, Player i_PlayerNumber2, ref bool i_IsEaten, ref bool i_EndGame)
        {
            int indexWhoEat = 1;
            string userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize, i_WhichPlayer, i_PlayerNumber1, i_PlayerNumber2);
            List<int> userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);

            if (i_WhichPlayer % 2 == 0) ///O
            {
                indexWhoEat = 1;
                CheckPositionAndMove(i_PlayerNumber1, i_Board, userMoverInInt, ref i_IsEaten, ref i_WhichPlayer, indexWhoEat);
            }

            else ///X
            {
                indexWhoEat = 2;
                CheckPositionAndMove(i_PlayerNumber2, i_Board, userMoverInInt, ref i_IsEaten, ref i_WhichPlayer, indexWhoEat);
            }

            if (i_IsEaten)
            {
                CheckWhowEatAndUpdate(i_PlayerNumber1, i_PlayerNumber2, ref i_EndGame, indexWhoEat);
            }
        }

        public static void RunGameAgainstComputer(ref int i_WhichPlayer, Board i_Board, Player i_PlayerNumber1, Player i_PlayerNumber2, ref bool i_IsEaten, ref bool i_EndGame)
        {
            if (i_WhichPlayer % 2 == 0) ///O
            {
                int indexWhoEat = 1;
                string userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize, i_WhichPlayer, i_PlayerNumber1, i_PlayerNumber2);
                List<int> userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
                indexWhoEat = 1;
                CheckPositionAndMove(i_PlayerNumber1, i_Board, userMoverInInt, ref i_IsEaten, ref i_WhichPlayer, indexWhoEat);
            }
            else ///computer turn
            {
                ComputerMove(i_Board, i_PlayerNumber2, ref i_EndGame);
            }
        }

        public static void ComputerMove(Board i_GameBoard, Player i_PlayerNumber1, Player i_PlayerNumber2, ref bool i_EndGame)
        {
            List<int> positionOfPlayerToEat = new List<int>(2);
            bool someoneToEat = CheckIfThereIsSomeoneToEat(i_GameBoard, positionOfPlayerToEat);

            if (someoneToEat == true)
            {
                Move(i_GameBoard, positionOfPlayerToEat);
                UpdatePlayersAfterEaten(i_PlayerNumber2, i_PlayerNumber1, ref i_EndGame);
            }
            else
            {
                GetRandomPosition();
                Move(i_GameBoard, positionOfPlayerToEat);
            }

        }

        public static void CheckWhowEatAndUpdate(Player i_PlayerNumber1, Player i_PlayerNumber2, ref bool io_endGame, int i_NumberOfPlayer)
        {
            if (i_NumberOfPlayer == 1)
            {
                UpdatePlayersAfterEaten(i_PlayerNumber1, i_PlayerNumber2, ref io_endGame);
            }
            else if (i_NumberOfPlayer == 2)
            {
                UpdatePlayersAfterEaten(i_PlayerNumber2, i_PlayerNumber1, ref io_endGame);
            }
        }

        public static void CheckPositionAndMove(Player i_Player, Board i_Board, List<int> i_UserMoveInt, ref bool io_IsEaten, ref int i, int i_NumberOfPlayer)
        {
            bool eatBackWord = false;
            if (!i_Player.CheckIfTheCurrPositionIsMine(i_Board, i_UserMoveInt, ref io_IsEaten, ref eatBackWord))
            {
                i--;
            }
            else
            {
                i_Player.MovePlayerOnBoard(i_Board, i_UserMoveInt, io_IsEaten, i_Player.NumberOfPlayer, ref eatBackWord);
            }
        }

        public static void UpdatePlayersAfterEaten(Player i_WinPlayer, Player i_LoserPlayer, ref bool i_EndGame)
        {
            AddPointAfterEat(i_WinPlayer);
            i_EndGame = SubPieceOfTheRemainPiece(i_LoserPlayer);
        }

        public static bool SubPieceOfTheRemainPiece(Player i_Player)
        {
            bool returnAnswer = false;
            int remainPieces = i_Player.RemainPieces--;
            if (remainPieces == 0)
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }

        public static void AddPointAfterEat(Player i_Player)
        {
            i_Player.PointsOfPlayer++;
        }


    }
}
