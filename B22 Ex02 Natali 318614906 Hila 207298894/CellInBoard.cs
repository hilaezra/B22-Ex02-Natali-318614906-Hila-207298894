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
        private PlayerPieceOnBoard m_Cell=null;
        
        public CellInBoard(Point i_Cordniate,bool i_IsThereAPlayer,int i_NumberOfPlayer)//constuctor
        {
            m_Point = i_Cordniate;
            m_IsEmpty = i_IsThereAPlayer;
            PlayerPieceOnBoard piece;
            if (i_NumberOfPlayer==1)
                piece = new PlayerPieceOnBoard('X');
            else
                piece = new PlayerPieceOnBoard('O');
            m_Cell = piece;
        }

    }
}
