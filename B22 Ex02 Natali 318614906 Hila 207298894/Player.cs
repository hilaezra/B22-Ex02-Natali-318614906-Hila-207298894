using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class Player
    {
        private string m_NameOfPlayer;
        private int m_PointsOfPlayer;
        private int m_RemainPieces;
        private char m_SignPlayer;
        private int m_NumberOfPlayer;
        private List<Point> m_ListOfPositionsOfRemainPieces;
        private char m_SignOfKing;
        private string m_LastMove;

        public Player(string i_Name, int i_SizeOfBoard, char i_Sign, int i_NumberOfPlayer)
        {
            m_LastMove = string.Empty;
            m_NameOfPlayer = i_Name;
            m_PointsOfPlayer = 0;
            m_RemainPieces = ((i_SizeOfBoard * i_SizeOfBoard) - (2 * i_SizeOfBoard)) / 4;
            m_SignPlayer = i_Sign;
            m_NumberOfPlayer = i_NumberOfPlayer;
            m_ListOfPositionsOfRemainPieces = new List<Point>(m_RemainPieces);
            if (i_NumberOfPlayer == 1)
            {
                m_SignOfKing = 'U';
            }
            else
            {
                m_SignOfKing = 'K';
            }
        }

        public static void UpdatePlayerAfterGotEaten(Board i_Board, List<int> i_Positions, int i_IndexMiddle, int i_IndexMoveCol, Player i_Player)
        {
            i_Board.GameBoard[i_Positions[1] + i_IndexMiddle, i_Positions[0] + i_IndexMoveCol].PlayerInBoard.SignOfPlayerInBoard = ' ';
            i_Board.GameBoard[i_Positions[1] + i_IndexMiddle, i_Positions[0] + i_IndexMoveCol].IsEmpty = true;
        }

        public static void UpdateCellInBoardWhileThePlayerMove(Board i_Board, List<int> i_Positions, Player i_Player, char i_KingSign)
        {
            if (i_Board.GameBoard[i_Positions[1], i_Positions[0]].PlayerInBoard.IsKing == true)
            {
                i_Board.GameBoard[i_Positions[1], i_Positions[0]].PlayerInBoard.IsKing = false;
                i_Board.GameBoard[i_Positions[3], i_Positions[2]].PlayerInBoard.IsKing = true;
                i_Board.GameBoard[i_Positions[3], i_Positions[2]].PlayerInBoard.SignOfPlayerInBoard = i_KingSign;
            }
            else
            {
                i_Board.GameBoard[i_Positions[3], i_Positions[2]].PlayerInBoard.SignOfPlayerInBoard = i_Player.m_SignPlayer;
            }

            i_Board.GameBoard[i_Positions[1], i_Positions[0]].IsEmpty = true;
            i_Board.GameBoard[i_Positions[1], i_Positions[0]].PlayerInBoard.SignOfPlayerInBoard = ' ';
            i_Board.GameBoard[i_Positions[3], i_Positions[2]].IsEmpty = false;
        }

        public static void FindMiddlePosition(List<int> i_Positions, ref int io_MoveCol, ref int io_AddOrSub, ref int io_IndexMiddle, int i_NumberOfPlayer)
        {
            if (i_Positions[0] < i_Positions[2])
            {
                io_MoveCol = 1;
            }
            else
            {
                io_MoveCol = -1;
            }

            if (i_NumberOfPlayer == 1)
            {
                io_AddOrSub = 2;
                io_IndexMiddle = 1;
            }
            else
            {
                io_AddOrSub = -2;
                io_IndexMiddle = -1;
            }
        }

        public string NameOfPlayer
        {
            get { return m_NameOfPlayer; }
            set { m_NameOfPlayer = value; }
        }

        public string LastMove
        {
            get { return m_LastMove; }
            set { m_LastMove = value; }
        }

        public int PointsOfPlayer
        {
            get { return m_PointsOfPlayer; }
            set { m_PointsOfPlayer = value; }
        }

        public char SignOfKing
        {
            get { return m_SignOfKing; }
            set { m_SignOfKing = value; }
        }

        public int NumberOfPlayer
        {
            get { return m_NumberOfPlayer; }
            set { m_NumberOfPlayer = value; }
        }

        public int RemainPieces
        {
            get { return m_RemainPieces; }
            set { m_RemainPieces = value; }
        }

        public char SignOfPlayer
        {
            get { return m_SignPlayer; }
            set { m_SignPlayer = value; }
        }

        public List<Point> Positions
        {
            get { return m_ListOfPositionsOfRemainPieces; }
            set { m_ListOfPositionsOfRemainPieces = value; }
        }

        public bool CheckIfTheCurrPositionIsMine(Board i_Board, List<int> i_Position, ref bool io_IsEaten, ref bool io_EatBackWord)///צריך להחליף שם כי היא לא רק בודקת האם זה שלו היא גם בודקת את התזוזה
        {
            bool returnAnswer = false;
            char sign = this.m_SignPlayer, kingSign = this.m_SignOfKing;
            if (!i_Board.GameBoard[i_Position[1], i_Position[0]].IsEmpty && (i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == this.m_SignPlayer || i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == kingSign))
            {
                if (i_Board.GameBoard[i_Position[3], i_Position[2]].IsEmpty)
                {
                    returnAnswer = true;
                }
            }

            if (!this.CheckIfTheWantedPositionIsOk(i_Board, i_Position, ref io_IsEaten, ref io_EatBackWord))
            {
                returnAnswer = false;
            }

            return returnAnswer;
        }

        public bool CheckIfTheWantedPositionIsOk(Board i_Board, List<int> i_Position, ref bool io_IsEaten, ref bool io_EatBackWord)
        {
            bool returnAnswer = false;
            bool pieceIsKing = i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.IsKing;
            int addOrSub = 0, indexMiddle = 0, intMoveCol = 0;
            if (i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == 'O' || i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == 'K')
            {
                returnAnswer = CheckWantedPosition(i_Position, ref intMoveCol, ref addOrSub, ref indexMiddle, this.m_NumberOfPlayer, i_Board, ref io_IsEaten);
            }
            else if (i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == 'X' || i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == 'U')
            {
                returnAnswer = CheckWantedPosition(i_Position, ref intMoveCol, ref addOrSub, ref indexMiddle, this.m_NumberOfPlayer, i_Board, ref io_IsEaten);
            }

            if (pieceIsKing && !returnAnswer)
            {
                returnAnswer = CheckIfTheWantedPositionIsOkForKing(i_Position, ref intMoveCol, ref addOrSub, ref indexMiddle, this.m_NumberOfPlayer, i_Board, ref io_IsEaten, ref io_EatBackWord);
                if (returnAnswer)
                {
                    io_EatBackWord = true;
                }
            }

            return returnAnswer;
        }

        public bool CheckWantedPosition(List<int> i_Position, ref int io_MoveCol, ref int io_AddOrSub, ref int io_IndexMiddle, int i_NumberOfPlayer, Board i_Board, ref bool io_IsEaten)
        {
            bool returnAnswer = false;
            FindMiddlePosition(i_Position, ref io_MoveCol, ref io_AddOrSub, ref io_IndexMiddle, i_NumberOfPlayer);
            if (i_Position[1] + io_IndexMiddle == i_Position[3] && (i_Position[0] + io_MoveCol == i_Position[2]))
            {
                returnAnswer = true;
            }
            else
            {
                returnAnswer = this.CheckIfCanEaten(i_Board, i_Position, i_NumberOfPlayer, ref io_IsEaten);
            }

            return returnAnswer;
        }

        public bool CheckIfCanEaten(Board i_Board, List<int> i_Position, int i_NumberOfPlayer, ref bool io_IsEaten)
        {
            bool returnAns = false;
            int addOrSub = 0, indexMiddle = 0, intMoveCol = 0;
            FindMiddlePosition(i_Position, ref intMoveCol, ref addOrSub, ref indexMiddle, i_NumberOfPlayer);
            if (CheckersGame.CheckThatWeDontGoBeyondBoundaries(i_Board.BoardSize, i_Position[1] + indexMiddle, i_Position[0] + intMoveCol))
            {
                if (i_Board.GameBoard[i_Position[3], i_Position[2]].IsEmpty && i_Board.GameBoard[i_Position[1] + indexMiddle, i_Position[0] + intMoveCol].PlayerInBoard.SignOfPlayerInBoard != this.m_SignPlayer && i_Board.GameBoard[i_Position[1] + indexMiddle, i_Position[0] + intMoveCol].PlayerInBoard.SignOfPlayerInBoard != ' ' &&
                    i_Board.GameBoard[i_Position[1] + indexMiddle, i_Position[0] + intMoveCol].PlayerInBoard.SignOfPlayerInBoard != this.m_SignOfKing)
                {
                    if (i_Position[1] + addOrSub == i_Position[3] && (i_Position[0] + (intMoveCol * 2) == i_Position[2]))
                    {
                        io_IsEaten = true;
                        returnAns = true;
                    }
                }
            }

            return returnAns;
        }

        public void MovePlayerOnBoard(Board i_Board, List<int> i_Positions, bool i_IsEaten, int i_NumberOfPlayer, ref bool io_EatBackWord) ////צריך לשים את כל החרא פה בפונקציה שמעדכנת את השחקנים
        {
            int intMoveCol = 0, addOrSub = 0, indexMiddle = 0;
            int backWard = i_NumberOfPlayer;
            char kingSign = this.m_SignOfKing;
            if (io_EatBackWord)
            {
                if (this.m_NumberOfPlayer == 1)
                {
                    backWard = 2;
                }
                else
                {
                    backWard = 1;
                }
            }

            FindMiddlePosition(i_Positions, ref intMoveCol, ref addOrSub, ref indexMiddle, backWard);
            UpdateCellInBoardWhileThePlayerMove(i_Board, i_Positions, this, kingSign);
            if (i_IsEaten)
            {
                UpdatePlayerAfterGotEaten(i_Board, i_Positions, indexMiddle, intMoveCol, this);
            }
        }

        public bool CheckIfThereIsNoValidMoveForPlayer(Board i_Board)
        {
            bool returnAns = false;
            List<List<int>> listOfPositionOptionToMove = new List<List<int>>();
            CheckersGame.UpdateOptionsOfValidMovements(this, i_Board, listOfPositionOptionToMove, this.NumberOfPlayer);
            if (listOfPositionOptionToMove.Count == 0)
            {
                returnAns = true;
            }

            return returnAns;
        }

        public void UpdatePointsAfterEachGame(Player i_LosePlayer, Board i_Board)
        {
            int currNumberOfDamka = 0, loseNumberOfDamka = 0;
            this.GoOverAllPlayerPositionAfterGameOverAndSumUpPoints(i_Board, ref currNumberOfDamka);
            if (i_LosePlayer.RemainPieces == 0)
            {
                this.PointsOfPlayer += currNumberOfDamka;
            }
            else
            {
                i_LosePlayer.GoOverAllPlayerPositionAfterGameOverAndSumUpPoints(i_Board, ref loseNumberOfDamka);
                this.PointsOfPlayer += currNumberOfDamka - loseNumberOfDamka;
            }
        }

        public void GoOverAllPlayerPositionAfterGameOverAndSumUpPoints(Board i_Board, ref int currNumberOfDamka)
        {
            for (int i = 0; i < this.RemainPieces; i++)
            {
                if (i_Board.GameBoard[this.Positions[i].X, this.Positions[i].Y].PlayerInBoard.IsKing)
                {
                    currNumberOfDamka += 4;
                }
                else
                {
                    currNumberOfDamka++;
                }
            }
        }

        public void PrintPlayersDetails()
        {
            StringBuilder playerDetails = new StringBuilder();
            if (m_SignPlayer == 'O')
            {
                Console.SetCursorPosition(0, 0);
                playerDetails.Insert(0, "First player name: " + m_NameOfPlayer);
            }
            else
            {
                Console.SetCursorPosition(0, 3);
                playerDetails.Insert(0, Environment.NewLine + "Second player name: " + m_NameOfPlayer);
            }

            playerDetails.Insert(playerDetails.Length, Environment.NewLine + "    Score in totall = " + m_PointsOfPlayer);
            playerDetails.Insert(playerDetails.Length, Environment.NewLine + "    Last move was: = " + m_LastMove);
            Console.WriteLine(playerDetails);
        }

        public bool CheckIfTheWantedPositionIsOkForKing(List<int> i_Position, ref int io_MoveCol, ref int io_AddOrSub, ref int io_IndexMiddle, int i_NumberOfPlayer, Board i_Board, ref bool io_IsEaten, ref bool io_EatBackWord)
        {
            bool returnAnswer = false;
            int backWard = 1;
            if (this.m_NumberOfPlayer == 1)
            {
                backWard = 2;
            }

            FindMiddlePosition(i_Position, ref io_MoveCol, ref io_AddOrSub, ref io_IndexMiddle, backWard);
            if (i_Position[1] + io_IndexMiddle == i_Position[3] && (i_Position[0] + io_MoveCol == i_Position[2]))
            {
                returnAnswer = true;
            }
            else
            {
                returnAnswer = this.CheckIfCanEaten(i_Board, i_Position, backWard, ref io_IsEaten);
            }

            if (returnAnswer == true)
            {
                io_EatBackWord = true;
            }

            return returnAnswer;
        }
    }
}