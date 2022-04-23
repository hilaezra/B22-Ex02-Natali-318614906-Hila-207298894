using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * מה נותר לעשות:
 * צריך לסדר את הפונק הקיימות ולייפות אותן
 * צריך להדפיס על המסך מחוץ מנקודות לכל שחקן גם ניצחונות לכל שחקן
 * מי שלוחץ על יציאה ההפרש בין השחקנים שלו לשני (לבדוק!!!)
 * תיקווווו (כלומר צריך לבדוק האם אין ל2 השחקנים מהלכים חוקיים לעשות) בנוסף צריך לבדוק על כל אחד מהשחקנים כדאי לראות האם מישהו הפסיד ונשאר לו שחקנים
 * זהו?!?!??!? סיימנו?
 */
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
            bool endGame = false, isEaten = false, quit = false, draw = false, playerWithoutPieces = false;
            int i = 0, indexWhoEat;
            List<int> userMoverInInt = new List<int>(4);
            List<List<int>> listOfPositionOptionToEat = new List<List<int>>();
            while (!endGame)
            {
                isEaten = false;
                Ex02.ConsoleUtils.Screen.Clear();
                i_Board.PrintBoard(i_PlayerNumber1, i_PlayerNumber2);
                i_PlayerNumber1.PrintPlayersDetails();
                i_PlayerNumber2.PrintPlayersDetails();
                quit = false;
                if (i % 2 == 0)
                {
                    indexWhoEat = 1;
                    userMoverInInt = CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(i_Board, ref quit, i_PlayerNumber1, i_PlayerNumber2, ref endGame, ref isEaten, ref i);
                    if (!quit)
                    {
                        CheckPositionAndMove(i_PlayerNumber1, i_PlayerNumber2, i_Board, userMoverInInt, ref isEaten, ref i, indexWhoEat, listOfPositionOptionToEat);
                        CheckIfDrawOrIfSomeoneCantMove(ref endGame, ref draw, i_PlayerNumber1, i_PlayerNumber2, i_Board);
                    }
                }
                else
                {
                    if (i_PlayerNumber2.NameOfPlayer == "computer")
                    {
                        GetRandomPosition(i_Board, userMoverInInt, i_PlayerNumber2, listOfPositionOptionToEat);
                    }
                    else
                    {
                        userMoverInInt = CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(i_Board, ref quit, i_PlayerNumber2, i_PlayerNumber1, ref endGame, ref isEaten, ref i);
                    }

                    indexWhoEat = 2;
                    if (!quit)
                    {
                        CheckPositionAndMove(i_PlayerNumber2, i_PlayerNumber1, i_Board, userMoverInInt, ref isEaten, ref i, indexWhoEat, listOfPositionOptionToEat);
                        CheckIfDrawOrIfSomeoneCantMove(ref endGame, ref draw, i_PlayerNumber2, i_PlayerNumber1, i_Board);
                    }
                }

                if (isEaten)
                {
                    CheckWhowEatAndUpdate(i_PlayerNumber1, i_PlayerNumber2, ref endGame, indexWhoEat);
                    if (endGame == true)///במקרה ומישהו הפסיד לגמרי
                    {
                        playerWithoutPieces = true;
                        if (indexWhoEat == 1)
                        {
                            i_PlayerNumber1.UpdatePointsAfterEachGame(i_PlayerNumber2, i_Board);
                        }
                        else
                        {
                            i_PlayerNumber2.UpdatePointsAfterEachGame(i_PlayerNumber1, i_Board);
                        }
                        CheckWhoWinAndAnnounceToThePlayer(i_Board, indexWhoEat, i_PlayerNumber1, i_PlayerNumber2, ref endGame);
                    }
                }

                if (endGame && !playerWithoutPieces && !quit)
                {
                    if (indexWhoEat == 1)
                    {
                        if (!draw)
                        {
                            i_PlayerNumber2.UpdatePointsAfterEachGame(i_PlayerNumber1, i_Board);
                        }
                    }
                    else
                    {
                        if (!draw)
                        {
                            i_PlayerNumber1.UpdatePointsAfterEachGame(i_PlayerNumber2, i_Board);
                        }
                    }

                    CheckWhoWinAndAnnounceToThePlayer(i_Board, indexWhoEat, i_PlayerNumber1, i_PlayerNumber2, ref endGame);
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
                if (i_Position[3] == i_Board.BoardSize - 1)
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

        public static List<int> CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(Board i_Board, ref bool io_Quit, Player i_CurrPlayer, Player i_NextPlayer, ref bool io_EndGame, ref bool io_IsEaten, ref int i_Index)
        {
            List<int> userMoverInInt = new List<int>(4);
            string userMoveInString = UserInputManagement.PartOfTheBoardSquares(i_Board.BoardSize, i_Index, i_CurrPlayer, ref io_Quit);
            if (io_Quit)
            {
                i_NextPlayer.UpdatePointsAfterEachGame(i_CurrPlayer, i_Board);
                if (CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(i_Board, i_CurrPlayer, i_NextPlayer))
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

        public static bool CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(Board i_Board, Player i_PlayerQuit, Player i_Player)
        {
            bool returnAnswer = false;
            string answer;
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

        public static void GetRandomPosition(Board i_GameBoard, List<int> io_FromWhereToWhereToEatAndMove, Player i_PlayerNumber2, List<List<int>> io_ListOfPositionOptionToEat)
        {
            List<List<int>> listOfPositionOptionToMove = new List<List<int>>();
            int randomPos;
            bool someoneToEat = CheckIfThereIsAnyoneToEatAndReturnOptions(i_GameBoard, i_PlayerNumber2, io_ListOfPositionOptionToEat);
            Random rand = new Random();

            if (someoneToEat == true)
            {
                randomPos = rand.Next(io_ListOfPositionOptionToEat.Count() - 1);
                UpdateArrOfPosition(io_FromWhereToWhereToEatAndMove, io_ListOfPositionOptionToEat[randomPos][1], io_ListOfPositionOptionToEat[randomPos][0], io_ListOfPositionOptionToEat[randomPos][3], io_ListOfPositionOptionToEat[randomPos][2]);
            }
            else
            {
                UpdateOptionsOfValidMovements(i_PlayerNumber2, i_GameBoard, listOfPositionOptionToMove, 2);
                randomPos = rand.Next(listOfPositionOptionToMove.Count() - 1);
                UpdateArrOfPosition(io_FromWhereToWhereToEatAndMove, listOfPositionOptionToMove[randomPos][1], listOfPositionOptionToMove[randomPos][0], listOfPositionOptionToMove[randomPos][3], listOfPositionOptionToMove[randomPos][2]);
            }
        }

        public static void UpdateOptionsOfValidMovements(Player i_Player, Board i_Board, List<List<int>> io_ListOfPositionOptionToMove, int i_NumberOfPlayer)
        {
            int currRow, currCol, direction = -1;
            if (i_NumberOfPlayer == 1)
            {
                direction = 1;
            }
            for (int i = 0; i < i_Player.Positions.Count; i++)
            {
                currRow = i_Player.Positions[i].X;
                currCol = i_Player.Positions[i].Y;
                if (CheckThatWeDontGoBeyondBoundaries(i_Board.BoardSize, currRow + direction, currCol - 1) &&
                    i_Board.GameBoard[currRow + direction, currCol - 1].IsEmpty)
                {
                    List<int> possiblePosition = new List<int>(4);
                    UpdateArrOfPosition(possiblePosition, currRow, currCol, currRow + direction, currCol - 1);
                    io_ListOfPositionOptionToMove.Add(possiblePosition);
                }

                if (CheckThatWeDontGoBeyondBoundaries(i_Board.BoardSize, currRow + direction, currCol + 1) &&
                    i_Board.GameBoard[currRow + direction, currCol + 1].IsEmpty)
                {
                    List<int> possiblePosition = new List<int>(4);
                    UpdateArrOfPosition(possiblePosition, currRow, currCol, currRow + direction, currCol + 1);
                    io_ListOfPositionOptionToMove.Add(possiblePosition);
                }
            }
        }

        public static void CheckIfDrawOrIfSomeoneCantMove(ref bool o_EndGame, ref bool o_Draw, Player i_CurrPlayer, Player i_NextPlayer, Board i_Board)
        {
            bool validMovesOfCurrPlayer, validMovesOfNextPlayer;
            validMovesOfCurrPlayer = i_CurrPlayer.CheckIfThereIsNoValidMoveForPlayer(i_Board);
            validMovesOfNextPlayer = i_NextPlayer.CheckIfThereIsNoValidMoveForPlayer(i_Board);

            if (validMovesOfCurrPlayer && validMovesOfNextPlayer)
            {
                o_Draw = true;
                o_EndGame = true;
            }
            else if (validMovesOfCurrPlayer)
            {
                o_EndGame = true;
            }
        }

        public static void UpdateArrOfPosition(List<int> io_FromWhereToWhereToMove, int i_CurrRowNum, int i_CurrColNum, int i_NextRowNum, int i_NextColNum)
        {
            io_FromWhereToWhereToMove.Clear();
            io_FromWhereToWhereToMove.Add(i_CurrColNum);
            io_FromWhereToWhereToMove.Add(i_CurrRowNum);
            io_FromWhereToWhereToMove.Add(i_NextColNum);
            io_FromWhereToWhereToMove.Add(i_NextRowNum);
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

        public static void CheckPositionAndMove(Player i_CurrPlayer, Player i_NextPlayer, Board i_Board, List<int> i_UserMoveInt, ref bool io_IsEaten, ref int i, int i_NumberOfPlayer, List<List<int>> io_PossiblePositionToEat)
        {
            bool eatBackWord = false, validEatMove = true;
            bool isThereSomeoneToEat = CheckIfThereIsAnyoneToEatAndReturnOptions(i_Board, i_CurrPlayer, io_PossiblePositionToEat);
            if (isThereSomeoneToEat)
            {
                validEatMove = CheckIfTheWantedPositionIsMustEatenPosition(io_PossiblePositionToEat, i_UserMoveInt);
            }

            if (!i_CurrPlayer.CheckIfTheCurrPositionIsMine(i_Board, i_UserMoveInt, ref io_IsEaten, ref eatBackWord) || !validEatMove)
            {
                i--;
            }
            else
            {
                i_CurrPlayer.MovePlayerOnBoard(i_Board, i_UserMoveInt, io_IsEaten, i_CurrPlayer.NumberOfPlayer, ref eatBackWord);
                UpdateListPointsOfPlayer(i_CurrPlayer, i_NextPlayer, i_UserMoveInt, io_IsEaten);
                UpdatePlayerIfKing(i_Board, i_CurrPlayer, i_UserMoveInt);//לעדכן אם זה מלך
                if (CheckIfThereAreMoreToEatForThatPiece(i_Board, i_UserMoveInt, i_CurrPlayer.NumberOfPlayer, i_CurrPlayer) && io_IsEaten)
                {
                    i--;
                }
            }
        }

        public static bool CheckIfTheWantedPositionIsMustEatenPosition(List<List<int>> i_PossiblePositionToEat, List<int> i_WantedPosition)
        {
            bool returnAnswer = false;
            for (int i = 0; i < i_PossiblePositionToEat.Count; i++)
            {
                if (i_PossiblePositionToEat[i][0] == i_WantedPosition[0] && i_PossiblePositionToEat[i][1] == i_WantedPosition[1]
                    && i_PossiblePositionToEat[i][2] == i_WantedPosition[2] && i_PossiblePositionToEat[i][3] == i_WantedPosition[3])
                {
                    returnAnswer = true;
                    i = i_PossiblePositionToEat.Count;
                }
            }

            return returnAnswer;
        }

        public static void CheckForEachRemainPieceIfCanEat(ref bool o_ReturnAns, Board i_GameBoard, Player i_CurrPlayer, int i_IndexOfPiece, List<List<int>> o_PossiblePositionToEat, int i_Row, int i_Col, int i_NumberOfPlayer)
        {
            bool canEat = false, eat = false;
            if (CheckThatWeDontGoBeyondBoundaries(i_GameBoard.BoardSize, i_CurrPlayer.Positions[i_IndexOfPiece].X + i_Row, i_CurrPlayer.Positions[i_IndexOfPiece].Y + i_Col))
            {
                List<int> position = new List<int> { i_CurrPlayer.Positions[i_IndexOfPiece].Y, i_CurrPlayer.Positions[i_IndexOfPiece].X, i_CurrPlayer.Positions[i_IndexOfPiece].Y + i_Col, i_CurrPlayer.Positions[i_IndexOfPiece].X + i_Row };
                canEat = i_CurrPlayer.CheckIfCanEaten(i_GameBoard, position, i_NumberOfPlayer, ref eat);
                if (canEat)
                {
                    o_PossiblePositionToEat.Add(position);
                    o_ReturnAns = true;
                }
            }
        }

        public static bool CheckIfThereIsAnyoneToEatAndReturnOptions(Board i_Board, Player i_CurrPlayer, List<List<int>> o_PossiblePositionToEat)
        {
            bool returnAnswer = false, isKing = false;
            o_PossiblePositionToEat.Clear();
            for (int k = 0; k < i_CurrPlayer.RemainPieces; k++)
            {
                isKing = i_Board.GameBoard[i_CurrPlayer.Positions[k].X, i_CurrPlayer.Positions[k].Y].PlayerInBoard.IsKing;
                if (i_CurrPlayer.NumberOfPlayer == 2)
                {
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, -2, 2, 2);
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, -2, -2, 2);
                    if (isKing)
                    {
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, 2, 2, 1);
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, 2, -2, 1);
                    }
                }
                else
                {
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, 2, 2, 1);
                    CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, 2, -2, 1);
                    if (isKing)
                    {
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, -2, 2, 2);
                        CheckForEachRemainPieceIfCanEat(ref returnAnswer, i_Board, i_CurrPlayer, k, o_PossiblePositionToEat, -2, -2, 2);
                    }

                }

            }

            return returnAnswer;
        }

        public static void UpdateListPointsOfPlayer(Player io_CurrPlayer, Player io_NextPlayer, List<int> i_FromWhereToWhere, bool i_IsEaten)
        {
            int sizeOfPositionCurr = io_CurrPlayer.RemainPieces, sizeOfPositionNext = io_NextPlayer.RemainPieces;
            int intMoveCol = 0, addOrSub = 0, indexMiddle = 0;
            int backWard = io_CurrPlayer.NumberOfPlayer;
            for (int k = 0; k < sizeOfPositionCurr; k++)
            {
                if (i_FromWhereToWhere[0] == io_CurrPlayer.Positions[k].Y && i_FromWhereToWhere[1] == io_CurrPlayer.Positions[k].X)
                {
                    io_CurrPlayer.Positions[k].Y = i_FromWhereToWhere[2];
                    io_CurrPlayer.Positions[k].X = i_FromWhereToWhere[3];
                    k = sizeOfPositionCurr;
                }
            }

            if (i_IsEaten)
            {
                Player.FindMiddlePosition(i_FromWhereToWhere, ref intMoveCol, ref addOrSub, ref indexMiddle, backWard);
                int colToDelete = i_FromWhereToWhere[0] + intMoveCol;
                int rowToDelete = i_FromWhereToWhere[1] + indexMiddle;
                for (int j = 0; j < sizeOfPositionNext; j++)
                {
                    if (colToDelete == io_NextPlayer.Positions[j].Y && rowToDelete == io_NextPlayer.Positions[j].X)
                    {
                        io_NextPlayer.Positions.RemoveAt(j);
                        j = sizeOfPositionNext;
                    }
                }
                if (backWard == 1)
                {
                    backWard = 2;
                }
                else
                {
                    backWard = 1;
                }
                sizeOfPositionNext = io_NextPlayer.Positions.Count;
                Player.FindMiddlePosition(i_FromWhereToWhere, ref intMoveCol, ref addOrSub, ref indexMiddle, backWard);
                colToDelete = i_FromWhereToWhere[0] + intMoveCol;
                rowToDelete = i_FromWhereToWhere[1] + indexMiddle;
                for (int j = 0; j < sizeOfPositionNext; j++)
                {
                    if (colToDelete == io_NextPlayer.Positions[j].Y && rowToDelete == io_NextPlayer.Positions[j].X)
                    {
                        io_NextPlayer.Positions.RemoveAt(j);
                        j = sizeOfPositionNext;
                    }
                }
            }
        }

        public static void UpdatePlayersAfterEaten(Player i_WinPlayer, Player i_LoserPlayer, ref bool o_EndGame)
        {
            o_EndGame = SubPieceOfTheRemainPiece(i_LoserPlayer);
        }

        public static bool SubPieceOfTheRemainPiece(Player i_Player)//מחזיר אמת אם נגמר השחקנים אחרי האכילה
        {
            bool returnAnswer = false;
            int remainPieces = --i_Player.RemainPieces;
            if (remainPieces == 0)
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }

        public static bool CheckThatWeDontGoBeyondBoundaries(int i_SizeOfTheBoard, int i_WantedRow, int i_WantedCol)
        {
            bool returnAnswer = false;
            if ((i_WantedCol < i_SizeOfTheBoard && i_WantedCol >= 0) && (i_WantedRow < i_SizeOfTheBoard && i_WantedRow >= 0))
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }

        public static bool CheckIfThereAreMoreToEatForThatPiece(Board i_Board, List<int> i_Position, int i_NumberOfPlayer, Player i_Player)
        {
            bool isKing = i_Board.GameBoard[i_Position[3], i_Position[2]].PlayerInBoard.IsKing;
            bool returnAnswer = false;
            if (i_NumberOfPlayer == 1)
            {
                returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Board, i_Position, i_Player, 1, 1);
            }
            else
            {
                returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Board, i_Position, i_Player, -1, 1);
            }
            if (isKing && !returnAnswer)
            {
                if (i_NumberOfPlayer == 1)
                {
                    returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Board, i_Position, i_Player, -1, 1);
                }
                else
                {
                    returnAnswer = CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(i_Board, i_Position, i_Player, 1, 1);
                }
            }

            return returnAnswer;
        }

        public static bool CheckForTheCurrPositionAfterMoveIfThereIsMoreToEat(Board i_Board, List<int> i_Position, Player i_Player, int i_Row, int i_Col)
        {
            bool returnAnswer = false;
            if (CheckThatWeDontGoBeyondBoundaries(i_Board.BoardSize, i_Position[3] + i_Row, i_Position[2] + i_Col) &&
                     i_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] + i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfKing &&
                     i_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] + i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfPlayer &&
                     i_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] + i_Col].PlayerInBoard.SignOfPlayerInBoard != ' ')
            {
                if (CheckThatWeDontGoBeyondBoundaries(i_Board.BoardSize, i_Position[3] + i_Row * 2, i_Position[2] + i_Col * 2) &&
                    i_Board.GameBoard[i_Position[3] + i_Row * 2, i_Position[2] + i_Col * 2].IsEmpty)
                {
                    returnAnswer = true;
                }

            }
            else if (CheckThatWeDontGoBeyondBoundaries(i_Board.BoardSize, i_Position[3] + i_Row, i_Position[2] - i_Col) &&
                     i_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] - i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfKing &&
                     i_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] - i_Col].PlayerInBoard.SignOfPlayerInBoard != i_Player.SignOfPlayer &&
                     i_Board.GameBoard[i_Position[3] + i_Row, i_Position[2] - i_Col].PlayerInBoard.SignOfPlayerInBoard != ' ')
            {
                if (CheckThatWeDontGoBeyondBoundaries(i_Board.BoardSize, i_Position[3] + i_Row * 2, i_Position[2] - i_Col * 2) &&
                    i_Board.GameBoard[i_Position[3] + i_Row * 2, i_Position[2] - i_Col * 2].IsEmpty)
                {
                    returnAnswer = true;
                }
            }

            return returnAnswer;
        }

        public static void CheckWhoWinAndAnnounceToThePlayer(Board i_Board, int i_Index, Player i_Player1, Player i_Player2, ref bool io_EndGame)//צריך לשים בפונקציותתתת
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.SetCursorPosition((Console.WindowWidth / 2) - 50, Console.CursorTop + 1);

            string msg;
            if (i_Index == 1)
            {
                msg = string.Format("Congratulations {0} !!!! You are the    W I N N E R    !", i_Player1.NameOfPlayer);
                Console.WriteLine(msg);
                System.Threading.Thread.Sleep(2500);
                io_EndGame = CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(i_Board, i_Player1, i_Player2);
            }
            else
            {
                msg = string.Format("Congratulations {0} !!!! You are the    W I N N E R    !", i_Player2.NameOfPlayer);
                Console.WriteLine(msg);
                System.Threading.Thread.Sleep(2500);
                io_EndGame = CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(i_Board, i_Player2, i_Player1);

            }

        }

    }
}