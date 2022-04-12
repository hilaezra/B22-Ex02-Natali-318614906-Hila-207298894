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
        
        public bool CheckIfTheCurrPositionIsMine(Board i_Board,List<int> i_Position)
        {
            bool returnAnswer = false,currPosition=false;
            if(!i_Board.GameBoard[i_Position[1], i_Position[0]].IsEmpty&& (i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard==this.m_SignPlayer))
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
            if (!CheckIfTheWantedPositionIsOk(i_Board, i_Position))
                returnAnswer = false;

            return returnAnswer;
        }
        public static bool CheckIfTheWantedPositionIsOk(Board i_Board, List<int> i_Position)//בדיקה האם לאן שהוא רוצה להזיז בכלל אפשרי מבחינת המקום שהוא נמצא עכשיו.
        {
            //צריך להבדיל האם זה מלך ואז יש לו עוד אופציות לאן ללכת
            bool returnAnswer = false;
            bool pieceIsKing = i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.IsKing;
            if (i_Board.GameBoard[i_Position[1], i_Position[0]].PlayerInBoard.SignOfPlayerInBoard == 'O')
            {
                if (i_Position[1] + 1 == i_Position[3] && (i_Position[0] + 1 == i_Position[2] || i_Position[0] - 1 == i_Position[2]))//בדיקה לגבי שינוי העמודה או אחד ימינה או אחד שמאלה ושורה אחד למעלה
                {
                    returnAnswer = true;
                }
            }
            else//'X'
            {
                if (i_Position[1] - 1 == i_Position[3] && (i_Position[0] + 1 == i_Position[2] || i_Position[0] - 1 == i_Position[2]))//בדיקה לגבי שינוי העמודה או אחד ימינה או אחד שמאלה ושורה אחד למעלה
                {
                    returnAnswer = true;
                }
            }
            return returnAnswer;
        }
    }
}
