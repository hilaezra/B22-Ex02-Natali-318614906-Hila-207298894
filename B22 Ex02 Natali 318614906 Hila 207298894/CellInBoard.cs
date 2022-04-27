using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class CellInBoard
    {
        private bool m_IsEmpty;
        private PlayerPieceOnBoard m_Cell;

        public CellInBoard(bool i_IsThereAPlayer, int i_NumberOfPlayer)
        {
            m_IsEmpty = i_IsThereAPlayer;
            PlayerPieceOnBoard piece;
            if (i_NumberOfPlayer == (int)Player.PlayerNumber.One)
            {
                piece = new PlayerPieceOnBoard('O');
            }
            else if (i_NumberOfPlayer == (int)Player.PlayerNumber.Two)
            {
                piece = new PlayerPieceOnBoard('X');
            }
            else
            {
                piece = new PlayerPieceOnBoard(' '); ////Empty cell
            }

            m_Cell = piece;
        }

        public bool IsEmpty
        {
            get { return m_IsEmpty; }
            set { m_IsEmpty = value; } 
        }

        public PlayerPieceOnBoard PlayerInBoard
        {
            get { return m_Cell; }
            set { m_Cell = value; }
        }
    }
}
