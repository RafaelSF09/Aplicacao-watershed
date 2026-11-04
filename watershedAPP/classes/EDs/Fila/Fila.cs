using System;
using System.Collections.Generic;
using System.Text;

namespace watershedAPP.classes.EDs.Fila
{
    internal class Fila
    {
        public NoFila? inicio {  get; private set; }
        public NoFila? fim {  get; private set; }

        public int tamanho { get; private set; }

        public bool isEmpty()
        {
            return fim ==null;
        }

        public void insere(double num, int x, int y)
        {
            NoFila novo = new NoFila(num, x, y, null);
            tamanho++;

            if (isEmpty())
            {
                inicio = fim = novo;
            }
            else
            {
                // Maior prioridade entra no início
                if (inicio.info.num < num)
                {
                    novo.prox = inicio;
                    inicio = novo;
                }
                else
                {
                    NoFila atual = inicio;

                    while (atual.prox != null &&
                           atual.prox.info.num >= num)
                    {
                        atual = atual.prox;
                    }

                    novo.prox = atual.prox;
                    atual.prox = novo;

                    if (novo.prox == null)
                        fim = novo;
                }
            }
        }

        public void insereNaoOrdenado(double num, int x, int y)
        {
            NoFila novo = new NoFila(num, x, y, null);
            tamanho++;

            if (isEmpty())
            {
                inicio = fim = novo;
            }
            else
            {
                fim.prox = novo;
                fim = novo;
            }
        }

        public Info? retira()
        {
            if (isEmpty())
                return null;
            Info info = inicio.info;
            if (inicio == fim)
                inicio = fim = null;
            else
                inicio = inicio.prox;
            tamanho--;
            return info;
        }
    }
}
