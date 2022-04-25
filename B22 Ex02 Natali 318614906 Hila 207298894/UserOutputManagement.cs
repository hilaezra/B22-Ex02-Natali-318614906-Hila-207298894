using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class UserOutputManagement
    {
        public static string UpdateThePlayerThatWeHaveGameOverAndAskWhatToDoNext(Player i_PlayerQuit)
        {
            string returnAnswer;
            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                StringBuilder checkIfContinueGame = new StringBuilder();
                checkIfContinueGame.Insert(0, "GAME OVER!!" + Environment.NewLine + i_PlayerQuit.NameOfPlayer + ", Do you want to play another round??" + Environment.NewLine + "Prass 1)Yes  2)No");
                Console.WriteLine(checkIfContinueGame);
                returnAnswer = Console.ReadLine();
            }
            while (returnAnswer != "1" && returnAnswer != "2");

            return returnAnswer;
        }

        public static void PrintToTheScreenWhoWinAndAskHowToContinue(Player i_Winner, Player i_Loser,ref bool io_EndGame,Board i_Board, ref int io_IndexOfFirstPlayer)
        {
            string msg = string.Format("Congratulations {0} !!!! You are the    W I N N E R    !", i_Winner.NameOfPlayer);
            Console.WriteLine(msg);
            System.Threading.Thread.Sleep(2500);
            io_EndGame = CheckersGame.CheckIfThePlayerWantToQuitAfterWinOrLoseOrQ(i_Board, i_Winner, i_Loser, ref io_IndexOfFirstPlayer);
        }
    }
}
