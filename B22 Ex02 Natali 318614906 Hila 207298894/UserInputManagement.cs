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
                Console.WriteLine("Please enter your name:");
            }
            else
            {
                Console.WriteLine("Please enter the name of the other player:");
            }
            userName = Console.ReadLine();
            return userName;
        }
        public static int GetAndCheckValidBoardSize()
        {
            int userBoardSize = 0;
            bool validBoardSize = false;

            while (validBoardSize != true)
            {
                Console.WriteLine("Please select the desired board size (6/8/10)");
                userBoardSize = int.Parse(Console.ReadLine());

                if (userBoardSize == 6 || userBoardSize == 8 || userBoardSize == 10)
                {
                    validBoardSize = true;
                }
            }

            return userBoardSize;
        }

        public static bool PlayAgainstComputer()
        {
            int playerChoice = 0;
            bool validChoice = false;
            bool againstComputer = false;

            while (validChoice != true)
            {
                Console.WriteLine("If you want to play against the computer press 1, if you want to play against another player press 2.");
                playerChoice = int.Parse(Console.ReadLine());
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
                    Console.WriteLine("Please enter valid number!");
                }
            }

            return againstComputer;
        }

        public static string PartOfTheBoardSquares(int i_BoardSize)
        {
            bool validPointOnBoard = false;
            string userMove;
            Console.WriteLine("Please enter your next movment");//Ab>Ad
            do
            {

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
    }
}
