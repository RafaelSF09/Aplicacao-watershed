using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tudoAteAgora.Classes.Pilhas.PilhaComDma
{
    internal class NoPilha
    {
        private int num;

        private NoPilha prox;

        public int Num { 
            get { return num; }
            set { num = value; }
        }

        public NoPilha Prox
        {
            get { return prox; }
            set { prox = value; }
        }

        public NoPilha(NoPilha prox, int num)
        {
            this.num = num;
            this.prox = prox;
        }
    }
}
