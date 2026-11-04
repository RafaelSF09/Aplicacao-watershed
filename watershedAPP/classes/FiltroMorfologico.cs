using System;
using System.Collections.Generic;
using System.Text;

namespace watershedAPP.classes
{
    internal class FiltroMorfologico
    {
        public static void dilatacao(Bitmap imgSrc, Bitmap imgDest)
        {
            int heigth = imgDest.Height;
            int width = imgDest.Width;

            bool[][] mascara = new bool[][]{
                new bool[] { true, true, true},
                new bool[] { true, true, true},
                new bool[] { true, true, true}
            };

            //x,y do pixel 
            int posX = 1, posY = 1;

            int length = mascara.Length;

            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Pixel.isBranco(imgSrc.GetPixel(x, y)))
                    {
                        for (int i = 0; i < length; i++)
                        {
                            for (int j = 0; j < length; j++)
                            {
                                int auxX = x + j - posX;
                                int auxY = y + i - posY;

                                if (mascara[i][j] && auxX >= 0 && auxX < width && auxY >= 0 && auxY < heigth)
                                    imgDest.SetPixel(auxX, auxY, Color.FromArgb(255, 255, 255));
                            }
                        }
                    }
                }
            }
        }

        public static void erosao(Bitmap imgSrc, Bitmap imgDest)
        {
            int heigth = imgDest.Height;
            int width = imgDest.Width;

            bool[][] mascara = new bool[][]{
                new bool[] { true, true, true},
                new bool[] { true, true, true},
                new bool[] { true, true, true}
            };

            int posX = 1, posY = 1;

            int length = mascara.Length;

            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Pixel.isObj(imgSrc.GetPixel(x, y))
                        && x - posX >= 0 &&
                        y - posY >= 0 &&
                        x + (length - 1 - posX) < width &&
                        y + (length - 1 - posY) < heigth)
                    {
                        bool flag = true;
                        for (int i = 0; i < length; i++)
                        {
                            for (int j = 0; j < length; j++)
                            {
                                int auxX = x + j - posX;
                                int auxY = y + i - posY;

                                if (mascara[i][j] && Pixel.isFundo(imgSrc.GetPixel(auxX, auxY)))
                                    flag = false;
                            }
                        }
                        if (flag)
                        {
                            imgDest.SetPixel(x, y, Pixel.corObj);
                        }
                    }
                }
            }
        }

        public static void abertura(Bitmap imgSrc, Bitmap imgDest)
        {
            using (Bitmap imgAux = new Bitmap(imgSrc.Width, imgSrc.Height))
            {
                using (Graphics g = Graphics.FromImage(imgAux))
                {
                    g.Clear(Pixel.corFundo);
                }
                erosao(imgSrc, imgAux);
                dilatacao(imgAux, imgDest);
            }
        }

        public static void fechamento(Bitmap imgSrc, Bitmap imgDest)
        {
            using (Bitmap imgAux = new Bitmap(imgSrc.Width, imgSrc.Height))
            {
                using (Graphics g = Graphics.FromImage(imgAux))
                {
                    g.Clear(Pixel.corFundo);
                }
                dilatacao(imgSrc, imgAux);
                erosao(imgAux, imgDest);
            }
        }

        public static void run(Bitmap imgSrc, Bitmap imgDest)
        {
            using (Bitmap imgAux = new Bitmap(imgSrc.Width, imgSrc.Height))
            {
                using (Graphics g = Graphics.FromImage(imgAux))
                {
                    g.Clear(Pixel.corFundo);
                }
                abertura(imgSrc, imgAux);
                fechamento(imgAux, imgDest);
            }
        }
    }
}
