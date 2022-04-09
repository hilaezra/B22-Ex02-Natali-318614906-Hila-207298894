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
    }
}
