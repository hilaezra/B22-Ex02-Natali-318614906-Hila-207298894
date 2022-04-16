using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B22_Ex02_Natali_318614906_Hila_207298894
{
    public class Point
    {
        private int m_X;
        private int m_Y;

        public Point(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }
        public int X
        {
            set { m_X = value; }
            get { return m_X; }
        }

        public int Y
        {
            set { m_Y = value; }
            get { return m_Y; }
        }
    }
}
