using System;
using System.Collections.Generic;
using System.Text;

namespace watershedAPP.classes.EDs.Fila
{
    internal class Info
    {
        public double num { get; private set; }

        public int x { get; private set; }

        public int y { get; private set; }

        public Info(double num, int x, int y)
        { 
            this.num = num;
            this.x = x;
            this.y = y;
        }
    }
}
