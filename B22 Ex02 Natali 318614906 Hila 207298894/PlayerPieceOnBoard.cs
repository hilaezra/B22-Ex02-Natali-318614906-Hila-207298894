using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class PlayerPieceOnBoard
    {
        private bool m_IsPieceIsKing;
        private char m_Sign;

        public PlayerPieceOnBoard(char i_Sign)
        {
            m_IsPieceIsKing = false;
            m_Sign = i_Sign;
        }

        public bool IsKing
        {
            get { return m_IsPieceIsKing; }
            set { m_IsPieceIsKing = value; }
        }

        public char SignOfPlayerInBoard
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public static bool CheckAndReturnIfThePieceIsKing(Board i_Board, int i_Row, int i_Col)
        {
            bool returnAnswer = false;
            if (i_Board.GameBoard[i_Row, i_Col].PlayerInBoard.IsKing == true)
            {
                returnAnswer = true;
            }

            return returnAnswer;
        }
    }
}
