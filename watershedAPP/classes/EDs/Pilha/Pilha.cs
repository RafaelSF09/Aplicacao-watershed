using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tudoAteAgora.Classes.Pilhas.PilhaComDma;

namespace tudoAteAgora.Classes.Pilhas
{
    internal class Pilha
    {
        private NoPilha cabeca;

        public Pilha(int num)
        {
            Push(num);
        }

        public Pilha()
        {

        }

        public void Push(int num)
        {
            cabeca = new NoPilha(cabeca, num);
        }

        public int Pop()
        {
            if(!isEmpty())
            {
                int num = cabeca.Num;
                cabeca = cabeca.Prox;
                return num;
            }
            return -1;
        }

        public bool isEmpty()
        {
            return cabeca == null;
        }
    }
}
