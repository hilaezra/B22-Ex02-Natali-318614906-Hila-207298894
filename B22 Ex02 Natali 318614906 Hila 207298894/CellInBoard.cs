using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    class CellInBoard
    {
        private Point m_Point;//cordinate of cell
        private bool m_IsEmpty;
        private PlayerPieceOnBoard m_Cell = null;

        public CellInBoard(Point i_Cordniate, bool i_IsThereAPlayer, int i_NumberOfPlayer)//constuctor
        {
            m_Point = i_Cordniate;
            m_IsEmpty = i_IsThereAPlayer;
            PlayerPieceOnBoard piece;
            if (i_NumberOfPlayer == 1)
            {
                piece = new PlayerPieceOnBoard('X');
            }
            else if (i_NumberOfPlayer == 2)
            {
                piece = new PlayerPieceOnBoard('O');
            }
            else
            {
                piece = new PlayerPieceOnBoard(' '); //Empty cell
            }
            m_Cell = piece;
        }

        public Point _point
        {
            get { return m_Point; }
            set { m_Point = value; }
        }

        public bool isEmpty
        {
            get { return m_IsEmpty; }
            set { m_IsEmpty = value; } 
        }

        public PlayerPieceOnBoard playerInBoard
        {
            get { return m_Cell; }
            set { m_Cell = value; }
        }

    }
}
