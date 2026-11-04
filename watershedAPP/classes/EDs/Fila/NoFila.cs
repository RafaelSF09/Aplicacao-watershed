using System;
using System.Collections.Generic;
using System.Text;

namespace watershedAPP.classes.EDs.Fila
{
    internal class NoFila
    {
        public Info info {  get; private set; }

        public NoFila? prox {  get; set; }

        public NoFila(double num, int x, int y, NoFila? prox)
        {
            info = new Info(num, x, y);
            this.prox = prox;
        }
    }
}
