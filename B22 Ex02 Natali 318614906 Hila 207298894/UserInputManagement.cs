using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class UserInputManagement
    {
        public static string GetUserName(bool i_FirstPlayer)
        {
            string userName;
            if (i_FirstPlayer == true)
            {
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Please enter your name:"));
                
            }
            else
            {
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Please enter the name of the other player:"));//AB>BD
            }
            userName = Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
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
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Please select the desired board size (6/8/10)"));//AB>BD
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
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "If you want to play against the computer press 1, if you want to play against another player press 2."));//AB>BD
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
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Please enter valid number!"));//AB>BD

                }
            }
            Ex02.ConsoleUtils.Screen.Clear();

            return againstComputer;
        }

        public static string PartOfTheBoardSquares(int i_BoardSize)
        {
            bool validPointOnBoard = false;
            string userMove;
            do
            {
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Please enter valid movment"));//AB>BD

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
            } while (!validPointOnBoard);
            return userMove;
        }

        public static List<int> ChangedStringToListInt(string i_UserMove)//AB AC >>> [0][1][0][2]
        {
            int currColumnPos = i_UserMove[0] - 'A', currRowPos = i_UserMove[1] - 'a', newColumnPos = i_UserMove[3] - 'A', newRowPos = i_UserMove[4] - 'a';
            List<int> returnCurrPositionAndNewPosition = new List<int>() { currColumnPos, currRowPos, newColumnPos, newRowPos };

            return returnCurrPositionAndNewPosition;
        }

    }
}
