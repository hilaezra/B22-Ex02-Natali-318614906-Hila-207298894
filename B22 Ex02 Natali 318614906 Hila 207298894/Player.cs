using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class Player
    {
        private string m_NameOfPlayer;
        private int m_PointsOfPlayer;
        private int m_RemainPieces;
        private char m_SignPlayer;

        public Player(string i_Name, int i_SizeOfBorad, char i_Sign)
        {
            m_NameOfPlayer = i_Name;
            m_PointsOfPlayer = 0;
            m_RemainPieces = ((i_SizeOfBorad * i_SizeOfBorad) - 2 * i_SizeOfBorad) / 4;
            m_SignPlayer = i_Sign;
        }
        public string NameOfPlayer
        {
            get { return m_NameOfPlayer; }
            set { m_NameOfPlayer = value; }
        }
        public int PointsOfPlayer
        {
            get { return m_PointsOfPlayer; }
            set { m_PointsOfPlayer = value; }
        }
        public int RemainPieces
        {
            get { return m_RemainPieces; }
            set { m_RemainPieces = value; }
        }

        public bool CheckIfTheCurrPositionIsMine(Board i_Board, List<int> i_Position, ref bool io_IsEaten)
        {
            bool returnAnswer = false, currPosition = false;
            if (!i_Board.GameBoard[i_Position[1], i_Position[0]].IsEmpty && (i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == this.m_SignPlayer))
            {
                currPosition = true;//הקורדינטה שהוא רוצה להזיז היא באמת שלו
            }
            if (currPosition)
            {
                if (i_Board.GameBoard[i_Position[3], i_Position[2]].IsEmpty)
                {
                    returnAnswer = true;//המקום שהוא רוצה להזיז אליו ריק
                }
            }
            if (!this.CheckIfTheWantedPositionIsOk(i_Board, i_Position, ref io_IsEaten))
            {
                returnAnswer = false;
            }

            return returnAnswer;
        }
        public bool CheckIfTheWantedPositionIsOk(Board i_Board, List<int> i_Position, ref bool io_IsEaten)//בדיקה האם לאן שהוא רוצה להזיז בכלל אפשרי מבחינת המקום שהוא נמצא עכשיו.
        {
            //צריך להבדיל האם זה מלך ואז יש לו עוד אופציות לאן ללכת
            bool returnAnswer = false;
            bool pieceIsKing = i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.IsKing;
            int addOrSub = 0, indexMiddle = 0, intMoveCol = 0;
            if (i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == 'O')
            {
                returnAnswer = CheckWantedPosition(i_Position, ref intMoveCol, ref addOrSub, ref indexMiddle, 1, i_Board, ref io_IsEaten);

            }
            else//'X'
            {
                returnAnswer = CheckWantedPosition(i_Position, ref intMoveCol, ref addOrSub, ref indexMiddle, 2, i_Board,ref io_IsEaten);
                
            }
            return returnAnswer;
        }
        public bool CheckWantedPosition(List<int> i_Position, ref int io_MoveCol, ref int io_AddOrSub, ref int io_IndexMiddle, int i_NumberOfPlayer, Board i_Board, ref bool io_IsEaten)
        {
            bool returnAnswer = false;
            FindMiddlePosition(i_Position, ref io_MoveCol, ref io_AddOrSub, ref io_IndexMiddle, i_NumberOfPlayer);
            if (i_Position[1] + io_IndexMiddle == i_Position[3] && (i_Position[0] + io_MoveCol == i_Position[2]))//בדיקה לגבי שינוי העמודה או אחד ימינה או אחד שמאלה ושורה אחד למעלה
            {
                returnAnswer = true;
            }
            else
            {
                returnAnswer = this.CheckIfCanEaten(i_Board, i_Position, 1);
                io_IsEaten = true;
            }
            return returnAnswer;
        }
        public bool CheckIfCanEaten(Board i_Board, List<int> i_Position, int i_NumberOfPlayer)
        {
            bool returnAns = false;
            int addOrSub = 0, indexMiddle = 0, intMoveCol = 0;
            FindMiddlePosition(i_Position, ref intMoveCol, ref addOrSub, ref indexMiddle, i_NumberOfPlayer);

            if (i_Board.GameBoard[i_Position[3], i_Position[2]].IsEmpty && i_Board.GameBoard[i_Position[1] + indexMiddle, i_Position[0] + intMoveCol].PlayerInBoard.SignOfPlayerInBoard != this.m_SignPlayer)//רוצה לאכול את היריב!
            {
                if (i_Position[1] + addOrSub == i_Position[3] && (i_Position[0] + addOrSub == i_Position[2]))
                {
                    returnAns = true;
                }
            }
            return returnAns;
        }
        public void MovePlayerOnBoard(Board i_Board, List<int> i_Positions, bool i_IsEaten, int i_NumberOfPlayer)
        {
            int intMoveCol = 0, addOrSub = 0, indexMiddle = 0;
            FindMiddlePosition(i_Positions, ref intMoveCol, ref addOrSub, ref indexMiddle, i_NumberOfPlayer);
            i_Board.GameBoard[i_Positions[1], i_Positions[0]].IsEmpty = true;
            i_Board.GameBoard[i_Positions[1], i_Positions[0]].PlayerInBoard.SignOfPlayerInBoard = ' ';
            i_Board.GameBoard[i_Positions[3], i_Positions[2]].IsEmpty = false;
            i_Board.GameBoard[i_Positions[3], i_Positions[2]].PlayerInBoard.SignOfPlayerInBoard = this.m_SignPlayer;
            if (i_IsEaten)
            {
                i_Board.GameBoard[i_Positions[1] + indexMiddle, i_Positions[0] + intMoveCol].PlayerInBoard.SignOfPlayerInBoard = ' ';
                i_Board.GameBoard[i_Positions[1] + indexMiddle, i_Positions[0] + intMoveCol].IsEmpty = true;
            }

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
    }
}
