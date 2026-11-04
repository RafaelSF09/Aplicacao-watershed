using System;
using System.Collections.Generic;
using System.Text;

namespace watershedAPP.classes
{
    internal class Pixel
    {
        public static Color corObj = Color.FromArgb(255, 255, 255);

        public static Color corFundo = Color.FromArgb(0, 0, 0);

        public static bool isPreto(Color cor)
        {
            return cor.R == 0 && cor.G == 0 && cor.B == 0;
        }

        public static bool isBranco(Color cor)
        {
            return cor.R == 255 && cor.G == 255 && cor.B == 255;
        }

        public static bool isFundo(Color cor)
        {
            return cor == corFundo;
        }

        public static bool isObj(Color cor)
        {
            return cor == corObj;
        }
    }
}
