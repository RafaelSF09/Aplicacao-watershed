using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Text;
using tudoAteAgora.Classes.Pilhas;
using watershedAPP.classes.EDs.Fila;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace watershedAPP.classes
{
    internal class Watershed
    {
        public static int INDEFINIDO = 0;// não é parte do fundo nem de algum objeto

        public static int FUNDO = -1;

        public static int MARGEM = -2;//margem entre objetos

        public static int VISITADO = -3;//marca para não inserir repetido na fila

        //substitui os objetos da matriz com suas cores respectivas
        //e escreve na imagem
        private static void exibe(Bitmap img, int[,] matriz, int total)
        {
            int heigth = img.Height;
            int width = img.Width;

            Color[] cores = coresAleatorias(total);

            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (matriz[x, y] == FUNDO)
                        img.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    else
                        if (matriz[x, y] == INDEFINIDO)
                            img.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                        else
                            if (matriz[x, y] == MARGEM)
                                img.SetPixel(x, y, Color.FromArgb(0, 0, 255));
                            else//objeto
                                img.SetPixel(x, y, cores[matriz[x, y] - 1]);
                }
            }
        }

        private static Fila inicializaFila(int[,] marcadores, double[,] distancias, int width, int height)
        {
            //inicializa a fila
            Fila fila = new Fila();

            //percorre a matriz/imagem inteira
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // É um marcador de objeto
                    if (marcadores[x, y] > 0)
                    {
                        // Máscara 3x3
                        for (int i = -1; i <= 1; i++)
                        {
                            int auxX = x + i;

                            //verifica se auxX esta dentro dos limites
                            if (auxX >= 0 && auxX < width)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    int auxY = y + j;

                                    //verifica se auxY esta dentro dos limites
                                    if (auxY >= 0 && auxY < height)
                                    {
                                        //atual não pertence ao fundo ou algum obj
                                        if (marcadores[auxX, auxY] == INDEFINIDO)
                                        {
                                            //marca como visitado para não inserir repetido na fila
                                            marcadores[auxX, auxY] = VISITADO;

                                            //insere na fila
                                            fila.insere(distancias[auxX, auxY],auxX,auxY);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return fila;
        }

        private static int[] objsAoRedor(int[,] marcadores, int x, int y, int width, int height, out int tl)
        {
            tl = 0;
            int[] objs = new int[8];

            // Descobre quais objetos estão ao redor
            // mascara 3x3
            for (int i = -1; i <= 1; i++)
            {
                int auxX = x + i;

                //verifica auxX se está dentro das limitacoes
                if (auxX >= 0 && auxX < width)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int auxY = y + j;

                        //verifica auxY se está dentro das limitacoes
                        if (auxY >= 0 && auxY < height)
                        {
                            //é obj
                            if (marcadores[auxX, auxY] > 0 && !(auxX == x && auxY == y))
                            {
                                //pega o numero do obj na posicao atual
                                int objeto = marcadores[auxX, auxY];

                                //verifica se este obj ja esta na fila
                                int aux = 0;
                                while (aux < tl && objs[aux] != objeto)
                                    aux++;

                                //nao achou na fila
                                if (aux == tl){
                                    //insere na fila
                                    objs[tl++] = objeto;
                                }
                            }
                        }
                    }
                }
            }
            //retorna o vetor de objs
            return objs;
        }

        private static void expandeFila(Fila fila, int[,] marcadores, double[,] distancias, int x, int y, int width, int height)
        {
            // Só expande se pertenceu a algum objeto
            if (marcadores[x, y] > 0)
            {
                //mascara 3x3
                for (int i = -1; i <= 1; i++)
                {
                    int auxX = x + i;

                    if (auxX >= 0 && auxX < width)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int auxY = y + j;

                            if (auxY >= 0 && auxY < height)
                            {
                                //não pertence ao fundo nem a algum objeto
                                if (marcadores[auxX, auxY] == INDEFINIDO)
                                {
                                    //marca como visitado
                                    marcadores[auxX, auxY] = VISITADO;

                                    //insere na fila
                                    fila.insere(distancias[auxX, auxY], auxX, auxY);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static Color[] coresAleatorias(int total)
        {
            Color[] cores = new Color[total];//vetor de cores para todos os objetos da imagem

            Random random = new Random();

            for (int i = 0; i < total; i++)
            {
                //randomiza os 3 canais de cor
                int r = random.Next(50, 256);
                int g = random.Next(50, 256);
                int b = random.Next(50, 256);

                cores[i] = Color.FromArgb(r, g, b);//coloca nova cor no vetor
            }

            return cores;
        }

        public static void watershed(Bitmap img, int[,] mascaraFundo, double[,] distancias)
        {
            int width = img.Width;
            int height = img.Height;

            //fundo = -1; indefinido = 0; objs = 1...int.MaxValue
            int total;
            int[,] marcadores = Marcadores.separaComponentes(img, mascaraFundo, out total);
            Fila fila = inicializaFila(marcadores, distancias, width, height);
            Fila filaIndefinidos = new Fila();

            while (!fila.isEmpty())
            {
                Info info = fila.retira();

                int x = info.x;
                int y = info.y;

                //oito conectado
                int[] objs = objsAoRedor(marcadores, x, y, width, height, out int tl);

                //nenhum objeto em volta, não tem como saber a qual pertence
                if (tl == 0)
                    filaIndefinidos.insereNaoOrdenado(distancias[x, y], x, y);
                else
                    //tem somente 1 objeto em volta
                    if (tl == 1)
                    {
                        //pixel agora pertence a este objeto
                        marcadores[x, y] = objs[0];
                        //insere os pixeis não visitados na fila
                        expandeFila(fila, marcadores, distancias, x, y, width, height);
                    }
                    else
                        //mais de um objeto em volta
                        marcadores[x, y] = MARGEM;
            }

            bool loopInfinito = false;

            //percorre a fila de indefinidos(pixeis sem objeto em volta)
            while (!filaIndefinidos.isEmpty() && !loopInfinito)
            {
                Info info = filaIndefinidos.retira();

                int x = info.x;
                int y = info.y;

                //oito conectado
                int[] objs = objsAoRedor(marcadores, x, y, width, height, out int tl);

                //contador de vezes que não houve alteração na fila
                int semAlteracao = 0;

                //continua sem objetos em volta
                if (tl == 0)
                {
                    //retorna para o final da fila
                    filaIndefinidos.insereNaoOrdenado(distancias[x, y], x, y);
                    //aumenta o contador
                    semAlteracao++;
                }
                else
                {
                    //possui somente 1 objeto ao redor
                    if (tl == 1)
                        //pixel agora pertence a este objeto
                        marcadores[x, y] = objs[0];
                    else
                        //mais de 1 objeto ao redor
                        marcadores[x, y] = MARGEM;

                    //zera o contador de repetição
                    semAlteracao = 0;
                }
                //percorreu a fila inteira sem qualquer alteração
                //não tem como saber aode pertencem os pixeis da fila
                if (semAlteracao >= filaIndefinidos.tamanho)
                    loopInfinito = true;
            }
            //faz a imagem com os resultados do watershed
            exibe(img, marcadores, total);
        }

        private static Bitmap aplicaFiltroMorfologico(Bitmap img)
        {
            //cria uma imagem temporaria
            using (Bitmap imgTemp = new Bitmap(img.Width, img.Height))
            {
                //coloca a cor de fundo na imagem
                using (Graphics g = Graphics.FromImage(imgTemp))
                {
                    g.Clear(Pixel.corFundo);
                }

                //aplica o filtro morfológico na imagem
                FiltroMorfologico.run(img, imgTemp);

                //retorna o resultado
                return (Bitmap)imgTemp.Clone();
            }
        }

        private static Bitmap aplicaDilatacao(Bitmap img)
        {
            //cria uma imagem temporaria
            using (Bitmap imgTemp = new Bitmap(img.Width, img.Height))
            {
                //coloca a cor de fundo na imagem
                using (Graphics g = Graphics.FromImage(imgTemp))
                {
                    g.Clear(Pixel.corFundo);
                }

                //aplica a dilatação na imagem
                FiltroMorfologico.dilatacao(img, imgTemp);

                //retorna o resultado
                return (Bitmap)imgTemp.Clone();
            }
        }

        public static Bitmap run(Bitmap imgSrc)
        {
            int[,] mascaraFundo = PreProcessamento.trocaFundo(imgSrc);
            Bitmap imgAux = new Bitmap(imgSrc);
            PreProcessamento.laplaceESharp(imgSrc, imgAux);
            PreProcessamento.grayScale(imgAux);
            PreProcessamento.binarizacao(imgAux);
            double[,] distancias = TransformadaDistancia.run(imgAux);
            PreProcessamento.threshold(imgAux, 0.4);
            imgAux = aplicaFiltroMorfologico(imgAux);
            watershed(imgAux, mascaraFundo, distancias);
            return imgAux;
        }

        public static Bitmap[] runPassoAPasso(Bitmap imgSrc, bool dilatacao, double pesoThreshold)
        {
            Bitmap[] passos = new Bitmap[10];

            int i = 0;

            passos[i++] = new Bitmap(imgSrc);

            int[,] mascaraFundo = PreProcessamento.trocaFundo(imgSrc);
            passos[i++] = new Bitmap(imgSrc);

            Bitmap imgAux = new Bitmap(imgSrc);
            PreProcessamento.laplaceESharp(imgSrc, imgAux);
            passos[i++] = new Bitmap(imgAux);

            PreProcessamento.grayScale(imgAux);
            passos[i++] = new Bitmap(imgAux);

            PreProcessamento.binarizacao(imgAux);
            passos[i++] = new Bitmap(imgAux);

            if(dilatacao)
            {
                imgAux = aplicaDilatacao(imgAux);
                passos[i++] = new Bitmap(imgAux);
            }

            double[,] distancias = TransformadaDistancia.run(imgAux);
            passos[i++] = new Bitmap(imgAux);

            PreProcessamento.threshold(imgAux, pesoThreshold);
            passos[i++] = new Bitmap(imgAux);

            imgAux = aplicaFiltroMorfologico(imgAux);
            passos[i++] = new Bitmap(imgAux);

            watershed(imgAux, mascaraFundo, distancias);
            passos[i] = new Bitmap(imgAux);

            return passos;
        }
    }
}
