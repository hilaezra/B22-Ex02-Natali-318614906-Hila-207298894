﻿using System;
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

            board.InitBoard(playerNumber1, playerNumber2);
            RunGame(board, playerNumber1, playerNumber2);
        }

        public static void RunGame(Board i_Board, Player i_PlayerNumber1, Player i_PlayerNumber2)
        {
            bool endGame = false, isEaten = false, quit = false;
            int i = 0, indexWhoEat;
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
                    userMoverInInt = CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(i_Board, ref quit, i_PlayerNumber1, i_PlayerNumber2, ref endGame, ref isEaten, ref i);
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
                        userMoverInInt = CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(i_Board, ref quit, i_PlayerNumber2, i_PlayerNumber1, ref endGame, ref isEaten, ref i);
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

        public static void UpdatePlayerIfKing(Board i_Board, Player i_Player, List<int> i_Position)///יכנס לפה רק אם כל הבדיקות הקודמות עבדו
        {
            int numberOfPlayer = i_Player.NumberOfPlayer;
            if (numberOfPlayer == 1)
            {
                if(i_Position[3]==i_Board.BoardSize-1)
                {
                    i_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.IsKing = true;
                    i_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.SignOfPlayerInBoard = 'U';
                }                
            }
            else
            {
                if (i_Position[3] == 0)
                {
                    i_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.IsKing = true;
                    i_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.SignOfPlayerInBoard = 'K';
                }
            }
        }
        public static List<int> CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(Board i_Board, ref bool i_Quit, Player i_CurrPlayer, Player i_NextPlayer, ref bool io_EndGame, ref bool io_IsEaten, ref int i_Index)
        {
            List<int> userMoverInInt = new List<int>(4);
            string userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize, i_Index, i_CurrPlayer, i_NextPlayer, ref i_Quit);
            if (i_Quit)
            {
                i_Quit = false;
                if (CheckIfThePlayerWantToQuit(i_Board, i_CurrPlayer, i_NextPlayer))
                {
                    io_EndGame = true;
                }
            }
            else
            {
                userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
            }
            return userMoverInInt;
        }
        public static bool CheckIfThePlayerWantToQuit(Board i_Board, Player i_PlayerQuit, Player i_Player)
        {
            bool returnAnswer = false;
            string answer;
            i_Player.Winning++;
            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                StringBuilder checkIfContinueGame = new StringBuilder();
                checkIfContinueGame.Insert(0, "GAME OVER!!" + Environment.NewLine + i_PlayerQuit.NameOfPlayer + ", Do you want to play another round??" + Environment.NewLine + "Prass 1)Yes  2)No");
                Console.WriteLine(checkIfContinueGame);
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2");
            if (answer == "1")
            {
                i_Board.InitBoard(i_PlayerQuit, i_Player);
            }
            else
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }

        public static void InitNewGame(Player i_Player1, Player i_Player2, int i_SizeOfBoard)
        {
            i_Player1.RemainPieces = (((i_SizeOfBoard * i_SizeOfBoard) - 2) * i_SizeOfBoard) / 4; ;
        }
        public static void GivePointsToTheWinner(Player i_LoserPlayer, Player i_WinnerPlayer)
        {
            int piecesOfLoserPlayer = i_LoserPlayer.RemainPieces, piecesOfWinnerPlayer = i_WinnerPlayer.RemainPieces;
            i_WinnerPlayer.PointsOfPlayer += piecesOfWinnerPlayer - piecesOfLoserPlayer;///ההפרש בין השחקנים של המנצח למפסיד הולך למנצח
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
                UpdatePlayerIfKing(i_Board, i_Player, i_UserMoveInt);//לעדכן אם זה מלך
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