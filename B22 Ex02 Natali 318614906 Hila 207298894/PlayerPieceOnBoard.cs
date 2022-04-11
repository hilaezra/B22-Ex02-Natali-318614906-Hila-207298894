using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class PlayerPieceOnBoard
    {
        private bool m_IsPieceIsKing;
        private char m_Sign;

        private static bool validMove(Point i_NextPosition)
        {
            bool validMove = true;

            //TODO.....

            return validMove;
        }
        public PlayerPieceOnBoard(char i_Sign)
        {
            m_IsPieceIsKing = false;
            m_Sign = i_Sign;  
        }

        public bool isKing
        {
            get { return m_IsPieceIsKing; }
            set { m_IsPieceIsKing = value; }
        }

        public char signOfPlayerInBoard
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }
    }
}
