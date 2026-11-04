using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace watershedAPP.classes
{
    internal class TransformadaDistancia
    {
        //imagem já binarizada
        private static double[,] inicializaDistancias(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            int pixelSize = 3;

            //matriz com mesmas dimensoes que img
            double[,] distancias = new double[width, height];

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
                        int b, g, r;

                        b = *(pont++);
                        g = *(pont++);
                        r = *(pont++);

                        if (Pixel.isFundo(Color.FromArgb(r, g, b)))
                            distancias[x, y] = 0;// 0 = fundo
                        else
                            //não é fundo
                            //coloca como infinidade para não dar
                            //problemas nos calculos das distancias
                            distancias[x, y] = double.PositiveInfinity;// infi = obj
                    }
                    pont += padding;
                }
                img.UnlockBits(bitmapDataImg);
            }
            return distancias;
        }

        private static void primeiraPassagem(double[,] distancias, int height, int width)
        {
            for (int y = 1; y < height; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (double.IsPositiveInfinity(distancias[x, y]))
                    {
                        //mascara:
                        //a b c
                        //d x
                        //min entre
                        //a + sqrt(2),
                        //b + 1,
                        //c + sqrt(2),
                        //d + 1

                        //calcula a, b, c e d
                        double a = distancias[x - 1, y - 1] + Math.Sqrt(2);
                        double b = distancias[x, y - 1] + 1;
                        double c = distancias[x + 1, y - 1] + Math.Sqrt(2);
                        double d = distancias[x - 1, y] + 1;

                        //distancia recebe o menor entre eles
                        distancias[x, y] =
                            Math.Min(
                                Math.Min(a, b),
                                Math.Min(c, d)
                            );
                    }
                }
            }
        }

        private static double segundaPassagem(double[,] distancias, int height, int width)
        {
            double maior = 0;

            for (int y = height - 2; y >= 0; y--)
            {
                for (int x = width - 2; x >= 1; x--)
                {
                    //mascara:
                    //  x e
                    //f g h
                    //min entre
                    //x,
                    //e + 1,
                    //f + sqrt(2),
                    //g + 1,
                    //h + sqrt(2)

                    double atual = distancias[x, y];

                    //calcula e, f, g e h
                    double e = distancias[x + 1, y] + 1;
                    double f = distancias[x - 1, y + 1] + Math.Sqrt(2);
                    double g = distancias[x, y + 1] + 1;
                    double h = distancias[x + 1, y + 1] + Math.Sqrt(2);

                    //acha o minimo entre eles e o atual
                    distancias[x, y] =
                        Math.Min(
                            atual,
                            Math.Min(
                                Math.Min(e, f),
                                Math.Min(g, h)
                            )
                        );

                    //calcula o maior
                    //usado na normalização
                    if (distancias[x, y] > maior)
                        maior = distancias[x, y];
                }
            }
            return maior;
        }

        private static void normalizaDistancias(double[,] distancias, int height, int width, double maior)
        {
            if(maior > 0 ){
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if(!double.IsPositiveInfinity(distancias[x, y]))
                            //normaliza a distancia para ficar entre 0 e 255
                            distancias[x, y] = (distancias[x, y] / maior) * 255.0;
                    }
                }
            }
        }

        //escreve as distancias normalizadas na imagem como niveis de cinza
        public static double[,] run(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            int pixelSize = 3;

            //recebe o mapa das distancias normalizadas
            double[,] dist = distancias(img);

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
                        int nivelCinza = (int)dist[x, y];

                        *(pont++) = (byte)nivelCinza;
                        *(pont++) = (byte)nivelCinza;
                        *(pont++) = (byte)nivelCinza;
                    }
                    pont += padding;
                }
                img.UnlockBits(bitmapDataImg);
            }
            return dist;
        }

        //realiza a os passos para o calculo das distancias
        public static double[,] distancias(Bitmap img)
        {
            double[,] distancias = inicializaDistancias(img);

            primeiraPassagem(distancias, img.Height, img.Width);
            double maior = segundaPassagem(distancias, img.Height, img.Width);
            normalizaDistancias(distancias, img.Height, img.Width, maior);
            return distancias;
        }
    }
}
