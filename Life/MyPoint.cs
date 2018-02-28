using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class MyPoint
    {
        private int x, y;
        private bool live = false;

        public MyPoint()
        {
            x = 0;
            y = 0;
        }

        public MyPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.live = true;
        }

        public bool Live
        {
            get { return live; }
            set { live = value;}
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
