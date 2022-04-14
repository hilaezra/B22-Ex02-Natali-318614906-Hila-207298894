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
                ComputerMove(i_Board, i_PlayerNumber1, i_PlayerNumber2, ref i_EndGame);
            }
        }

        public static void ComputerMove(Board i_GameBoard, Player i_PlayerNumber1, Player i_PlayerNumber2, ref bool i_EndGame)
        {
            List<int> fromWhereToWhereToEatAndMove = new List<int>(4);

            int rowNumOfTheOneWhoEaten = 0, colNumOfTheOneWhoEaten = 0;
            bool someoneToEat = CheckIfThereIsSomeoneToEat(i_GameBoard, i_PlayerNumber1, i_PlayerNumber2, fromWhereToWhereToEatAndMove, ref rowNumOfTheOneWhoEaten, ref colNumOfTheOneWhoEaten);
            if (someoneToEat == true)
            {
                Move(i_GameBoard, fromWhereToWhereToEatAndMove, someoneToEat, rowNumOfTheOneWhoEaten, colNumOfTheOneWhoEaten);
                UpdatePlayersAfterEaten(i_PlayerNumber2, i_PlayerNumber1, ref i_EndGame);
            }
            else
            {
                GetRandomPosition(i_GameBoard, fromWhereToWhereToEatAndMove, i_PlayerNumber2); ///To Do!!!!!!!!!!!!!!!!!
                Move(i_GameBoard, fromWhereToWhereToEatAndMove, someoneToEat, rowNumOfTheOneWhoEaten, colNumOfTheOneWhoEaten);
            }

        }

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
                            updateArrOfPosition(io_FromWhereToWhereToEatAndMove, i, j, i - 1, j + 1);
                        }
                        else if (j == i_GameBoard.BoardSize - 1 && i_GameBoard.GameBoard[i - 1, j - 1].IsEmpty == true)
                        {
                            updateArrOfPosition(io_FromWhereToWhereToEatAndMove, i, j, i - 1, j + 1);
                        }
                        else
                        {
                            bool validChoice = false;
                            while (validChoice != true)
                            {
                                Random rand = new Random();
                                List<int> possibleCols = new List<int> { j - 1, j + 1 };
                                int randomCol = rand.Next(possibleCols.Count);
                                if (i_GameBoard.GameBoard[i - 1, randomCol].IsEmpty == true)
                                {
                                    updateArrOfPosition(io_FromWhereToWhereToEatAndMove, i, j, i - 1, possibleCols[randomCol]);
                                    validChoice = true;
                                }
                            }
                        }
                        validRandomMove = true;
                    }
                }
            }
        }

        public static void updateArrOfPosition(List<int> io_FromWhereToWhereToEatAndMove, int i_CurrRowNum, int i_CurrColNum, int i_NextRowNum, int i_NextColNum)
        {
            io_FromWhereToWhereToEatAndMove.Clear();
            io_FromWhereToWhereToEatAndMove.Add(i_CurrRowNum);
            io_FromWhereToWhereToEatAndMove.Add(i_CurrColNum);
            io_FromWhereToWhereToEatAndMove.Add(i_NextRowNum);
            io_FromWhereToWhereToEatAndMove.Add(i_NextColNum);
        }

        public static bool CheckIfThereIsSomeoneToEat(Board i_GameBoard, Player i_PlayerNumber1, Player i_PlayerNumber2, List<int> io_FromWhereToWhereToEatAndMove, ref int io_RowNumOfTheOneWhoEaten, ref int io_ColNumOfTheOneWhoEaten)
        {
            bool someOneToEat = false;

            for (int i = 1; i < i_GameBoard.BoardSize - 1; i++)
            {
                for (int j = 1; j < i_GameBoard.BoardSize - 1; j++)
                {
                    if (i_GameBoard.GameBoard[i, j].PlayerInBoard.SignOfPlayerInBoard == 'O')
                    {
                        someOneToEat = CheckIfThereIsAnyoneToEatForEachO(i_GameBoard, i, j, io_FromWhereToWhereToEatAndMove);
                        if (someOneToEat == true)
                        {
                            io_RowNumOfTheOneWhoEaten = i;
                            io_ColNumOfTheOneWhoEaten = j;
                        }
                    }
                }
            }

            return someOneToEat;
        }

        public static bool CheckIfThereIsAnyoneToEatForEachO(Board i_GameBoard, int i_ORow, int i_OCol, List<int> io_FromWhereToWhereToEatAndMove)
        {
            bool someoneToEat = false;
            ///check the left side 
            if (i_GameBoard.GameBoard[i_ORow + 1, i_OCol - 1].PlayerInBoard.SignOfPlayerInBoard == 'X' &&
                i_GameBoard.GameBoard[i_ORow - 1, i_OCol + 1].IsEmpty == true)
            {
                io_FromWhereToWhereToEatAndMove.Clear();
                io_FromWhereToWhereToEatAndMove.Add( i_ORow + 1);
                io_FromWhereToWhereToEatAndMove.Add(i_OCol - 1);
                io_FromWhereToWhereToEatAndMove.Add( i_ORow - 1);
                io_FromWhereToWhereToEatAndMove.Add(i_OCol + 1);
                someoneToEat = true;
            }
            ///check the right side
            if (i_GameBoard.GameBoard[i_ORow + 1, i_OCol + 1].PlayerInBoard.SignOfPlayerInBoard == 'X' &&
                i_GameBoard.GameBoard[i_ORow - 1, i_OCol - 1].IsEmpty == true)
            {
                io_FromWhereToWhereToEatAndMove.Clear();
                io_FromWhereToWhereToEatAndMove.Add(i_ORow + 1);
                io_FromWhereToWhereToEatAndMove.Add(i_OCol + 1);
                io_FromWhereToWhereToEatAndMove.Add(i_ORow - 1);
                io_FromWhereToWhereToEatAndMove.Add(i_OCol - 1);
                someoneToEat = true;
            }

            return someoneToEat;
        }

        public static void Move(Board i_GameBoard, List<int> i_CurrPosAndNextPos, bool i_MoveWithEat, int i_RowNumOfTheOneWhoEaten, int i_ColNumOfTheOneWhoEaten)
        {
            i_GameBoard.GameBoard[i_CurrPosAndNextPos[0], i_CurrPosAndNextPos[1]].IsEmpty = true;
            i_GameBoard.GameBoard[i_CurrPosAndNextPos[0], i_CurrPosAndNextPos[1]].PlayerInBoard.SignOfPlayerInBoard = ' ';
            i_GameBoard.GameBoard[i_CurrPosAndNextPos[2], i_CurrPosAndNextPos[3]].IsEmpty = false;
            i_GameBoard.GameBoard[i_CurrPosAndNextPos[2], i_CurrPosAndNextPos[3]].PlayerInBoard.SignOfPlayerInBoard = 'X';

            if (i_MoveWithEat == true)
            {
                i_GameBoard.GameBoard[i_CurrPosAndNextPos[i_RowNumOfTheOneWhoEaten], i_CurrPosAndNextPos[i_ColNumOfTheOneWhoEaten]].IsEmpty = true;
                i_GameBoard.GameBoard[i_CurrPosAndNextPos[i_RowNumOfTheOneWhoEaten], i_CurrPosAndNextPos[i_ColNumOfTheOneWhoEaten]].PlayerInBoard.SignOfPlayerInBoard = ' ';
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
