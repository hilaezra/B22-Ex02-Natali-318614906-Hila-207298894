using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class UserInputManagement
    {
        public static string GetUserName()
        {
            string userName; 
            Console.WriteLine("Please enter your name:");
            userName = Console.ReadLine();
            return userName;
        }
        public static int GetAndCheckValidBoardSize()
        {
            int userBoardSize = 0;
            bool validBoardSize = false;
           
            while(validBoardSize != true)
            {
                Console.WriteLine("Please select the desired board size (6/8/10)");
                userBoardSize = int.Parse(Console.ReadLine());

                if(userBoardSize == 6 || userBoardSize == 8 || userBoardSize == 10)
                {
                    validBoardSize = true;
                }
            }

            return userBoardSize;
        }
    }
}
