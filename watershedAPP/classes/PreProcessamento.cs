using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace watershedAPP.classes
{
    internal class PreProcessamento
    {
        public static int[,] trocaFundo(Bitmap img)
        {
            int width = img.Width;
            int heigth = img.Height;
            int pixelSize = 3;

            //cria a mascara do fundo para o watershed
            int[,] mascaraFundo = new int[width, heigth];

            BitmapData bitmapDataImg = img.LockBits(new Rectangle(0, 0, width, heigth),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataImg.Stride - (width * pixelSize);

            unsafe
            {
                byte* pont = (byte*)bitmapDataImg.Scan0.ToPointer();

                for (int y = 0; y < heigth; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int b, g, r;

                        b = *(pont);
                        g = *(pont + 1);
                        r = *(pont + 2);

                        //fundo é branco
                        int fundo = 255;

                        //verifica se é parte do fundo
                        if (r >= fundo && g >= fundo && b >= fundo)
                        {
                            //marca como fundo na matriz
                            mascaraFundo[x, y] = Watershed.FUNDO;

                            //troca para preto
                            *(pont) = (byte)0;
                            *(pont + 1) = (byte)0;
                            *(pont + 2) = (byte)0;
                        }
                        pont += pixelSize;
                    }
                    pont += padding;
                }
                img.UnlockBits(bitmapDataImg);
                //retorna a mascara do fundo
                return mascaraFundo;
            }
        }

        public static void laplaceESharp(Bitmap imgSrc, Bitmap imgDest)
        {
            int heigth = imgDest.Height;
            int width = imgDest.Width;

            for (int y = 1; y < heigth - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    Color centro = imgSrc.GetPixel(x, y);

                    //mascara laplaciana usada
                    //  1   1   1
                    //  1  -8   1
                    //  1   1   1
                    int lapB = -8 * centro.B;
                    int lapG = -8 * centro.G;
                    int lapR = -8 * centro.R;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (!(i == 0 && j == 0)){
                                Color pixel = imgSrc.GetPixel(x + j, y + i);

                                lapB += pixel.B;
                                lapG += pixel.G;
                                lapR += pixel.R;
                            }
                        }
                    }

                    // sharp = original - Laplaciano
                    int b = centro.B - lapB;
                    int g = centro.G - lapG;
                    int r = centro.R - lapR;

                    //Clamp para limitar o max e min
                    b = Math.Clamp(b, 0, 255);
                    g = Math.Clamp(g, 0, 255);
                    r = Math.Clamp(r, 0, 255);

                    imgDest.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
        }


        public static void grayScale(Bitmap img)
        {
            int width = img.Width;
            int heigth = img.Height;
            int pixelSize = 3;

            BitmapData bitmapDataImg = img.LockBits(new Rectangle(0, 0, width, heigth),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataImg.Stride - (width * pixelSize);

            unsafe
            {
                byte* pont = (byte*)bitmapDataImg.Scan0.ToPointer();

                for (int y = 0; y < heigth; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int b, g, r;

                        b = *(pont);
                        g = *(pont + 1);
                        r = *(pont + 2);

                        int cinza = (int)(0.299 * r + 0.587 * g + 0.114 * b);

                        *(pont++) = (byte)cinza;
                        *(pont++) = (byte)cinza;
                        *(pont++) = (byte)cinza;
                    }
                    pont += padding;
                }
                img.UnlockBits(bitmapDataImg);
            }
        }

        private static int[] histograma(Bitmap img)
        {
            int width = img.Width;
            int heigth = img.Height;
            int pixelSize = 3;

            int[] histograma = new int[256];

            BitmapData bitmapDataImg = img.LockBits(new Rectangle(0, 0, width, heigth),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataImg.Stride - (width * pixelSize);

            unsafe
            {
                byte* pont = (byte*)bitmapDataImg.Scan0.ToPointer();

                for (int y = 0; y < heigth; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int cinza = *(pont);

                        histograma[cinza]++;

                        pont += pixelSize;
                    }
                    pont += padding;
                }
                img.UnlockBits(bitmapDataImg);
            }

            return histograma;
        }

        //Binarização de Otsu com histograma
        public static void binarizacao(Bitmap imgDest)
        {
            int height = imgDest.Height;
            int width = imgDest.Width;

            grayScale(imgDest);

            int[] hist= histograma(imgDest);

            int totalPixel = height * width;

            double maiorVariancia = 0;

            int melhorThreshold = 0;

            for(int t = 0; t < 255; t++)
            {
                int pesoC1 = 0;
                int pesoC2 = 0;
                int somaCinzaC1 = 0;
                int somaCinzaC2 = 0;

                int i;

                //classe 1: 0 - t
                for (i = 0; i < t; i++){
                        pesoC1 += hist[i];
                        somaCinzaC1 += i * hist[i];
                }
                //classe 2: t+1 - 255
                for (i = t + 1; i < 255; i++){
                        pesoC2 += hist[i];
                        somaCinzaC2 += i * hist[i];
                }

                if(pesoC1 > 0 && pesoC2 > 0){
                    // pesos formalizados do Otsu
                    // necessario pois o int no calculo da variancia
                    // pode ultrapassar o limite maximo de inteiro
                    double w1 = (double)pesoC1 / totalPixel;
                    double w2 = (double)pesoC2 / totalPixel;

                    int mediaC1 = somaCinzaC1 / pesoC1;
                    int mediaC2 = somaCinzaC2 / pesoC2;

                    double variancia = w1 * w2 * Math.Pow(mediaC1 - mediaC2, 2);

                    if (variancia > maiorVariancia)
                    {
                        maiorVariancia = variancia;
                        melhorThreshold = t;
                    }
                }
            }

            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Color cinza = imgDest.GetPixel(x, y);

                    if(cinza.R <= melhorThreshold)
                        imgDest.SetPixel(x, y, Color.FromArgb(0,0,0));
                    else
                        imgDest.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                }
            }
        }


        public static void threshold(Bitmap img, double peso)
        {
            int width = img.Width;
            int heigth = img.Height;
            int pixelSize = 3;

            int threshold = (int)(255 * peso);

            BitmapData bitmapDataImg = img.LockBits(new Rectangle(0, 0, width, heigth),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataImg.Stride - (width * pixelSize);

            unsafe
            {
                byte* pont = (byte*)bitmapDataImg.Scan0.ToPointer();

                for (int y = 0; y < heigth; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int cinza = *(pont);

                        int novaCor;

                        if (cinza >= threshold)
                            novaCor = 255;
                        else
                            novaCor = 0;

                        *(pont++) = (byte)novaCor;
                        *(pont++) = (byte)novaCor;
                        *(pont++) = (byte)novaCor;
                    }
                    pont += padding;
                }
                img.UnlockBits(bitmapDataImg);
            }
        }
    }
}
