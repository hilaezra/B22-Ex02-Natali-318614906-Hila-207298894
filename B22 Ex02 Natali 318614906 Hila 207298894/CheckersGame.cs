using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class CheckersGame
    {
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
            int i = 0, indexWhoEat;
            string userMoveInString;
            List<int> userMoverInInt = new List<int>(4);
            while (!endGame)
            {
                isEaten = false;
                Ex02.ConsoleUtils.Screen.Clear();
                i_Board.PrintBoard(i_PlayerNumber1, i_PlayerNumber2);
                i_PlayerNumber1.PrintPlayersDetails();
                i_PlayerNumber2.PrintPlayersDetails();
                if (i % 2 == 0)
                {
                    indexWhoEat = 1;
                    userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize, i, i_PlayerNumber1, i_PlayerNumber2);
                    userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
                    CheckPositionAndMove(i_PlayerNumber1, i_Board, userMoverInInt, ref isEaten, ref i, indexWhoEat);
                }
                else
                {
                    if (i_PlayerNumber2.NameOfPlayer == "computer")
                    {
                        GetRandomPosition(i_Board, userMoverInInt, i_PlayerNumber2);
                    }
                    else
                    {
                        userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize, i, i_PlayerNumber1, i_PlayerNumber2);
                        userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
                    }

                    indexWhoEat = 2;
                    CheckPositionAndMove(i_PlayerNumber2, i_Board, userMoverInInt, ref isEaten, ref i, indexWhoEat);
                }

                if (isEaten)
                {
                    CheckWhowEatAndUpdate(i_PlayerNumber1, i_PlayerNumber2, ref endGame, indexWhoEat);
                }

                i++;
                System.Threading.Thread.Sleep(500);
            }
        }

        /*
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
        }*/

        public static void GetRandomPosition(Board i_GameBoard, List<int> io_FromWhereToWhereToEatAndMove, Player i_PlayerNumber2)
        {
            bool validRandomMove = false;

            for (int i = 1; i < i_GameBoard.BoardSize && validRandomMove != true; i++)
            {
                for (int j = 0; j < i_GameBoard.BoardSize && validRandomMove != true; j++)
                {
                    if (i_GameBoard.GameBoard[i, j].PlayerInBoard.SignOfPlayerInBoard == 'X')
                    {
                        if (j == 0 && i_GameBoard.GameBoard[i - 1, j + 1].IsEmpty == true)
                        {
                            UpdateArrOfPosition(io_FromWhereToWhereToEatAndMove, j, i, j + 1, i - 1);
                            validRandomMove = true;
                        }
                        else if (j == i_GameBoard.BoardSize - 1 && i_GameBoard.GameBoard[i - 1, j - 1].IsEmpty == true)
                        {
                            UpdateArrOfPosition(io_FromWhereToWhereToEatAndMove, j, i, j - 1, i - 1);
                            validRandomMove = true;
                        }
                        else if (j != i_GameBoard.BoardSize - 1 && j != 0)
                        {
                            bool validChoice = false;

                            for (int n = 0; n < 2 && !validChoice; n++)
                            {
                                Random rand = new Random();
                                List<int> possibleCols = new List<int> { j - 1, j + 1 };
                                int randomCol = rand.Next(possibleCols.Count);
                                if (i_GameBoard.GameBoard[i - 1, possibleCols[randomCol]].IsEmpty == true)
                                {
                                    UpdateArrOfPosition(io_FromWhereToWhereToEatAndMove, j, i, possibleCols[randomCol], i - 1);
                                    validChoice = true;
                                    validRandomMove = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void UpdateArrOfPosition(List<int> io_FromWhereToWhereToEatAndMove, int i_CurrRowNum, int i_CurrColNum, int i_NextRowNum, int i_NextColNum)
        {
            io_FromWhereToWhereToEatAndMove.Clear();
            io_FromWhereToWhereToEatAndMove.Add(i_CurrRowNum);
            io_FromWhereToWhereToEatAndMove.Add(i_CurrColNum);
            io_FromWhereToWhereToEatAndMove.Add(i_NextRowNum);
            io_FromWhereToWhereToEatAndMove.Add(i_NextColNum);
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

        public static bool CheckThatWeDontGoBeyondBoundaries(int i_SizeOfTheBoard, int i_WantedRow, int i_WantedCol, List<int> i_MustEatOneOfThem)
        {
            bool returnAnswer = false;
            if (i_WantedCol < i_SizeOfTheBoard && i_WantedRow < i_SizeOfTheBoard)
            {
                i_MustEatOneOfThem.Add(i_WantedRow);
                i_MustEatOneOfThem.Add(i_WantedCol);
                returnAnswer = true;
            }
            
            return returnAnswer;
        }
    }
}