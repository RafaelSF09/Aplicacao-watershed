using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using tudoAteAgora.Classes.Pilhas;

namespace watershedAPP.classes
{
    internal class Marcadores
    {
        //imagem já binarizada depois do distance transform
        private static int[,] inicializaMarcadores(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            int pixelSize = 3;

            //matriz de marcadores
            int[,] marcadores = new int[width, height];

            BitmapData bitmapDataImg = img.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataImg.Stride - (width * pixelSize);

            unsafe
            {
                byte* pont = (byte*)bitmapDataImg.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //coloca 0 caso for fundo
                        //coloca 255 caso for objeto
                        marcadores[x, y] = *(pont);
                        pont += pixelSize;
                    }
                    pont += padding;
                }
                img.UnlockBits(bitmapDataImg);
            }
            return marcadores;
        }

        //flod fill para marcar todo o objeto encontrado
        private static void marcaComponente(int[,] marcadores, int width, int height, int x, int y, int atual)
        {
            //inicia as pilhas
            Pilha pilhaX = new Pilha(x);
            Pilha pilhaY = new Pilha(y);

            while (!pilhaX.isEmpty())
            {
                x = pilhaX.Pop();
                y = pilhaY.Pop();

                //algoritmo 4 conectado para marcar todo o objeto
                if(x - 1 >= 0 && marcadores[x - 1, y] == Pixel.corObj.R)
                {
                    //marca o pixel com um numero entre 1 e n
                    marcadores[x - 1, y] = atual;
                    //insere na pilha
                    pilhaX.Push(x - 1);
                    pilhaY.Push(y);
                }
                if(y - 1 >= 0 && marcadores[x, y - 1] == Pixel.corObj.R)
                {
                    marcadores[x, y - 1] = atual;
                    pilhaX.Push(x);
                    pilhaY.Push(y - 1);
                }
                if(y + 1 < height && marcadores[x, y + 1] == Pixel.corObj.R)
                {
                    marcadores[x, y + 1] = atual;
                    pilhaX.Push(x);
                    pilhaY.Push(y + 1);
                }
                if (x + 1< height && marcadores[x + 1, y] == Pixel.corObj.R)
                {
                    marcadores[x + 1, y] = atual;
                    pilhaX.Push(x + 1);
                    pilhaY.Push(y);
                }
            }
        }

        //imagem de distancias já binarizada
        public static int[,] separaComponentes(Bitmap img, int[,] mascarafundo, out int totalComponentes)
        {
            int heigth = img.Height;
            int width = img.Width;

            //inicializa o matriz com os dados da imagem binarizada
            int[,] marcadores = inicializaMarcadores(img);

            //inicia o contador de objetos
            totalComponentes = 1;

            //percorre o matriz inteiro
            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //se for fundo na mascara, marca como fundo
                    if (mascarafundo[x, y] == Watershed.FUNDO)
                        marcadores[x, y] = Watershed.FUNDO;
                    else
                        //verifica se é objeto
                        if (marcadores[x, y] == Pixel.corObj.R)
                        {
                            //achou um objeto
                            //manda para marcar todo o objeto com o numero total atual
                            marcaComponente(marcadores, width, heigth, x, y, totalComponentes);
                            totalComponentes++;
                        }
                    //caso não seja fundo nem objeto fica 0 que representa indefinido.
                }
            }
            //retira 1 do total pois inicia em 1
            totalComponentes -= 1;
            //retorna a matriz de marcadores
            return marcadores;
        }
    }
}
