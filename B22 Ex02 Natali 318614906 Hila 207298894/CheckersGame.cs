﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class CheckersGame
    {
        private bool m_IsPlayingAgainstComputer;

        public static void InitializationGame()
        {
            bool firstPlayer = true;
            string nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
            int sizeOfBoard = UserInputManagement.GetAndCheckValidBoardSize();
            Player playerNumber1=new Player (nameOfPlayer,sizeOfBoard,'O');//PlayerNumber1IsReady            
            Player playerNumber2=new Player("computer", sizeOfBoard, 'X'); ;
            Board board = new Board(sizeOfBoard);
            if (!UserInputManagement.PlayAgainstComputer())
            {
                firstPlayer = false;
                nameOfPlayer = UserInputManagement.GetUserName(firstPlayer);
                playerNumber2.NameOfPlayer=nameOfPlayer;//SET
            }

            board.InitBoard();

            //string move = UserInputManagement.PartOfTheBoardSquares(sizeOfBoard);//כל זה יהיה בתוך לולאה כמובן. זה רק לשם הבדיקה :)
            List<int> move = UserInputManagement.ChangedStringToListInt("Bc>Ad");
            if(playerNumber1.CheckIfTheCurrPositionIsMine(board, move))
            {
                playerNumber1.MovePlayerOnBoard(board, move);
            }
            else
            {
                //ERROR
            }
            board.PrintBoard();
        }
        
    }
}
