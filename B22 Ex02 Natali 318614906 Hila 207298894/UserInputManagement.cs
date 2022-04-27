using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class UserInputManagement
    {
        private static CheckersGame InitializationGame()
        {
            int sizeOfBoard = 0;
            List<Player> players = new List<Player>(2);
            UserInputManagement.InitializationDataForTheGame(players, ref sizeOfBoard);
            Board board = new Board(sizeOfBoard);
            CheckersGame checkersGame = new CheckersGame(players[0], players[1], board);

            checkersGame.CheckersBoard.InitBoard(checkersGame.CheckerPlayer1, checkersGame.CheckerPlayer2);

            return checkersGame;
        }

        public static void RunGame()
        {
            CheckersGame checkersGame = InitializationGame();
            bool endGame = false, isEaten = false, quit = false, draw = false, playerWithoutPieces = false;
            int i = 0, indexWhoEat;
            string userMoveInString = string.Empty;
            List<int> userMoverInInt = new List<int>(4);
            List<List<int>> listOfPositionOptionToEat = new List<List<int>>();
            while (!endGame)
            {
                playerWithoutPieces = false;
                isEaten = false;
                Ex02.ConsoleUtils.Screen.Clear();
                UserOutputManagement.PrintBoard(checkersGame.CheckerPlayer1, checkersGame.CheckerPlayer2, checkersGame.CheckersBoard.BoardSize, checkersGame.CheckersBoard);
                UserOutputManagement.PrintPlayersDetails(checkersGame.CheckerPlayer1);
                UserOutputManagement.PrintPlayersDetails(checkersGame.CheckerPlayer2);
                quit = false;
                if (i % 2 == 0)
                {
                    indexWhoEat = 1;
                    userMoverInInt = CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(checkersGame, ref quit, checkersGame.CheckerPlayer1, checkersGame.CheckerPlayer2, ref endGame, ref isEaten, ref i, ref userMoveInString);
                    checkersGame.IfThePlayerDosentWantToQuitContinueTheGame(quit, checkersGame.CheckerPlayer1, checkersGame.CheckerPlayer2, ref isEaten, ref i, ref indexWhoEat, ref endGame, ref draw, userMoverInInt, ref userMoveInString, listOfPositionOptionToEat);
                }
                else
                {
                    if (checkersGame.CheckerPlayer2.NameOfPlayer == "computer")
                    {
                        checkersGame.GetRandomPosition(userMoverInInt, listOfPositionOptionToEat, ref userMoveInString);
                    }
                    else
                    {
                        userMoverInInt = CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(checkersGame, ref quit, checkersGame.CheckerPlayer2, checkersGame.CheckerPlayer1, ref endGame, ref isEaten, ref i, ref userMoveInString);
                    }

                    indexWhoEat = 2;
                    checkersGame.IfThePlayerDosentWantToQuitContinueTheGame(quit, checkersGame.CheckerPlayer2, checkersGame.CheckerPlayer1, ref isEaten, ref i, ref indexWhoEat, ref endGame, ref draw, userMoverInInt, ref userMoveInString, listOfPositionOptionToEat);
                }

                if (isEaten)
                {
                    checkersGame.UpdatePlayersAfterOneOfThemGotEaten(ref endGame, indexWhoEat, ref playerWithoutPieces, ref i);
                    if (endGame)
                    {
                        UserOutputManagement.CheckWhoWinAndAnnounceToThePlayer(checkersGame, indexWhoEat, checkersGame.CheckerPlayer1, checkersGame.CheckerPlayer2, ref endGame, ref i);
                    }
                }

                if (endGame && !playerWithoutPieces && !quit)
                {
                    checkersGame.CheckAndHandlePlayersAfterOneOfThePlayersDosentHaveValidMove(indexWhoEat, draw, ref endGame, ref i);
                    UserOutputManagement.CheckWhoWinAndAnnounceToThePlayer(checkersGame, indexWhoEat, checkersGame.CheckerPlayer1, checkersGame.CheckerPlayer2, ref endGame, ref i);
                }

                i++;
                System.Threading.Thread.Sleep(800);
            }
        }

        private static List<int> CheckIfThePlayerWantToQuitAndIfNotContinueTheGame(CheckersGame io_CheckersGame, ref bool io_Quit, Player i_CurrPlayer, Player i_NextPlayer, ref bool io_EndGame, ref bool io_IsEaten, ref int i_Index, ref string io_UserMoveInString)
        {
            List<int> userMoverInInt = new List<int>(4);
            string userMoveInString = UserInputManagement.PartOfTheBoardSquares(io_CheckersGame.CheckersBoard.BoardSize, i_Index, i_CurrPlayer, ref io_Quit);
            if (io_Quit)
            {
                i_NextPlayer.UpdatePointsAfterEachGame(i_CurrPlayer, io_CheckersGame.CheckersBoard);
                if (CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(io_CheckersGame, i_CurrPlayer, i_NextPlayer, ref i_Index))
                {
                    io_EndGame = true;
                }
            }
            else
            {
                userMoverInInt = UserInputManagement.ChangedStringToListInt(userMoveInString);
                io_UserMoveInString = userMoveInString;
            }

            return userMoverInInt;
        }

        public static bool CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(CheckersGame io_CheckersGame, Player i_PlayerQuit, Player i_Player, ref int o_Index)
        {
            bool returnAnswer = false;
            string answer;
            answer = UserOutputManagement.UpdateThePlayerThatWeHaveGameOverAndAskWhatToDoNext(i_PlayerQuit);
            if (answer == "1")
            {
                o_Index = 1;
                if (i_PlayerQuit.NumberOfPlayer == (int)Player.PlayerNumber.One)
                {
                    io_CheckersGame.CheckersBoard.InitBoard(i_PlayerQuit, i_Player);
                }
                else
                {
                    io_CheckersGame.CheckersBoard.InitBoard(i_Player, i_PlayerQuit);
                }

                io_CheckersGame.InitNewGame();
            }
            else
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }

        private static string GetUserName(bool i_FirstPlayer)
        {
            string userName = string.Empty;
            bool validName = false;
            while (validName != true)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop + 1);
                if (i_FirstPlayer == true)
                {
                    Console.WriteLine("Please enter your name:");
                }
                else
                {
                    Console.WriteLine("Please enter the name of the other player:");
                }

                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop + 1);
                userName = Console.ReadLine();
                if (userName.Length <= 20 && !userName.Contains(" "))
                {
                    validName = true;
                }

                Ex02.ConsoleUtils.Screen.Clear();
            }

            return userName;
        }

        private static int GetAndCheckValidBoardSize()
        {
            int userBoardSize = 0;
            bool validBoardSize = false;
            bool isNumber = false;

            while (validBoardSize != true || isNumber != true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop + 1);
                Console.WriteLine("Please select the desired board size (6/8/10)");
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop + 1);
                isNumber = int.TryParse(Console.ReadLine(), out userBoardSize);

                if (isNumber == true)
                {
                    if (userBoardSize == 6 || userBoardSize == 8 || userBoardSize == 10)
                    {
                        validBoardSize = true;
                    }
                }
            }

            return userBoardSize;
        }

        private static bool PlayAgainstComputer()
        {
            int playerChoice = 0;
            bool validChoice = false;
            bool againstComputer = false;
            bool isString = true;
            while (validChoice != true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.SetCursorPosition((Console.WindowWidth / 2) - 50, Console.CursorTop + 1);
                Console.WriteLine("If you want to play against the computer press 1, if you want to play against another player press 2.");
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop + 1);
                isString = int.TryParse(Console.ReadLine(), out playerChoice);

                if (playerChoice == 1)
                {
                    againstComputer = true;
                    validChoice = true;
                }
                else if (playerChoice == 2)
                {
                    validChoice = true;
                }
                else
                {
                    Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop + 1);
                    Console.WriteLine("Please enter valid number!");
                }
            }

            Ex02.ConsoleUtils.Screen.Clear();

            return againstComputer;
        }

        private static string PartOfTheBoardSquares(int i_BoardSize, int i_WhichPlayer, Player i_Player1, ref bool io_Quit)
        {
            bool validPointOnBoard = false;
            string userMove;
            do
            {
                Console.SetCursorPosition(85, 0);
                StringBuilder requestStep = new StringBuilder();
                requestStep.Insert(0, i_Player1.NameOfPlayer + ", Please enter valid movment");
                Console.WriteLine(requestStep);
                Console.SetCursorPosition(85, 1);
                Console.WriteLine("                                       ");
                Console.SetCursorPosition(85, 1);

                userMove = Console.ReadLine();

                if (userMove.Length == 5)
                {
                    if (userMove[0] >= 'A' && userMove[0] <= (char)('A' + i_BoardSize - 1)
                        && userMove[3] >= 'A' && userMove[3] <= (char)('A' + i_BoardSize - 1)
                        && userMove[2] == '>'
                        && userMove[1] >= 'a' && userMove[1] <= (char)('a' + i_BoardSize - 1)
                        && userMove[4] >= 'a' && userMove[4] <= (char)('a' + i_BoardSize - 1))
                    {
                        validPointOnBoard = true;
                    }
                }
                else if (userMove.Length == 1)
                {
                    if (userMove[0] == 'Q')
                    {
                        validPointOnBoard = true;
                        io_Quit = true;
                    }
                }
            }
            while (!validPointOnBoard);

            return userMove;
        }

        private static List<int> ChangedStringToListInt(string i_UserMove) 
        {
            int currColumnPos = i_UserMove[0] - 'A', currRowPos = i_UserMove[1] - 'a', newColumnPos = i_UserMove[3] - 'A', newRowPos = i_UserMove[4] - 'a';
            List<int> returnCurrPositionAndNewPosition = new List<int>() { currColumnPos, currRowPos, newColumnPos, newRowPos };

            return returnCurrPositionAndNewPosition;
        }

        private static void InitializationDataForTheGame(List<Player> o_Players, ref int io_SizeOfBoard)
        {
            bool firstPlayer = true;
            string nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
            io_SizeOfBoard = UserInputManagement.GetAndCheckValidBoardSize();
            o_Players.Add(new Player(nameOfPlayer, io_SizeOfBoard, 'O', 1));
            Player playerNumber2 = new Player("computer", io_SizeOfBoard, 'X', 2);
            if (!UserInputManagement.PlayAgainstComputer())
            {
                firstPlayer = false;
                nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
                playerNumber2.NameOfPlayer = nameOfPlayer;
            }

            o_Players.Add(playerNumber2);
        }
    }
}
