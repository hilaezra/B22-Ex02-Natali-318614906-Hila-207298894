using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class UserInputManagement
    {
        public static string GetUserName(bool i_FirstPlayer)
        {
            string userName = "";
            bool validName = false;
            while(validName != true)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop);
                if (i_FirstPlayer == true)
                {
                    Console.WriteLine("Please enter your name:");
                }
                else
                {
                    Console.WriteLine("Please enter the name of the other player:"); 
                }

                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop);
                userName = Console.ReadLine();
                if(userName.Length <= 20 && !userName.Contains(" "))
                {
                    validName = true;
                }

                Ex02.ConsoleUtils.Screen.Clear();
            }

            return userName;
        }

        public static int GetAndCheckValidBoardSize()
        {
            int userBoardSize = 0;
            bool validBoardSize = false;
            bool isNumber = false;

            while (validBoardSize != true || isNumber != true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop);
                Console.WriteLine("Please select the desired board size (6/8/10)");  
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop);
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

        public static bool PlayAgainstComputer()
        {
            int playerChoice = 0;
            bool validChoice = false;
            bool againstComputer = false;
            bool isString = true;
            while (validChoice != true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.SetCursorPosition((Console.WindowWidth / 2) - 50, Console.CursorTop);
                Console.WriteLine("If you want to play against the computer press 1, if you want to play against another player press 2."); ////AB>BD
                Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop);
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
                    Console.SetCursorPosition((Console.WindowWidth / 2) - 23, Console.CursorTop);
                    Console.WriteLine("Please enter valid number!");  
                }
            }

            Ex02.ConsoleUtils.Screen.Clear();

            return againstComputer;
        }

        public static string PartOfTheBoardSquares(int i_BoardSize, int i_WhichPlayer, Player i_Player1, Player i_Player2,ref bool io_Quit)
        {
            bool validPointOnBoard = false;
            string userMove;
            do
            {
                Console.SetCursorPosition(85, 0);
                StringBuilder requestStep = new StringBuilder();
                if (i_WhichPlayer % 2 == 0)
                {
                    requestStep.Insert(0, i_Player1.NameOfPlayer + ", Please enter valid movment");
                }
                else
                {
                    requestStep.Insert(0, i_Player2.NameOfPlayer + ", Please enter valid movment");
                }

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
                else if(userMove.Length == 1)
                {
                    if(userMove[0]=='Q')
                    {
                        validPointOnBoard = true;
                        io_Quit = true;
                    }
                }
            } 
            while (!validPointOnBoard);

            return userMove;
        }

        public static List<int> ChangedStringToListInt(string i_UserMove) ////AB AC >>> [0][1][0][2]
        {
            int currColumnPos = i_UserMove[0] - 'A', currRowPos = i_UserMove[1] - 'a', newColumnPos = i_UserMove[3] - 'A', newRowPos = i_UserMove[4] - 'a';
            List<int> returnCurrPositionAndNewPosition = new List<int>() { currColumnPos, currRowPos, newColumnPos, newRowPos };

            return returnCurrPositionAndNewPosition;
        }
    }
}
