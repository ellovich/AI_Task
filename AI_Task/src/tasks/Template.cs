using System;
using System.Drawing;
using System.IO;

namespace AI_Task
{
    public class Template
    {
        public static int s_ImageRez = 32;  // changing in Main
        public static bool s_ConsiderInverted = true;

        public string Name { get; private set; }
        public Bitmap Image { get; private set; }
        public byte[] PixelsGray { get; private set; }
        public byte[] PixelsInvertedGray { get; private set; }
        public double[] PixelsPossibilities { get; private set; }
        public double Possibility { get; private set; }


        public Template(string imagePath)
        {
            Name = Path.GetFileNameWithoutExtension(imagePath);
            Image = new Bitmap(imagePath);
            Image = (Bitmap)Imager.Resize(Image, s_ImageRez, s_ImageRez);
            PixelsPossibilities = new double[s_ImageRez * s_ImageRez];
            PixelsGray = GetGrayPixels(Image);

            PixelsInvertedGray = new byte[PixelsGray.Length];
            for (int i = 0; i < PixelsGray.Length; i++)
                PixelsInvertedGray[i] = (byte)(255 - PixelsGray[i]);
        }


        public void CalcPossibility(Bitmap myImage)
        {
            byte[] myImagePixelsGrey = GetGrayPixels(myImage);

            double counter = 0;
            double counterForInv = 0;
            for (int i = 0; i < PixelsGray.Length; i++)
            {
                // update s_FuncsManager
                double myB = TemplatesTask.s_FuncsManager["black"].FindValueIn(myImagePixelsGrey[i]);
                double myW = TemplatesTask.s_FuncsManager["white"].FindValueIn(myImagePixelsGrey[i]);

                if (PixelsGray[i] == 0)
                    counter += myB;
                else if (PixelsGray[i] == 255)
                    counter += myW;

                PixelsPossibilities[i] = counter;

                if (PixelsInvertedGray[i] == 0)
                    counterForInv += myB;
                else if (PixelsInvertedGray[i] == 255)
                    counterForInv += myW;

                //    Console.WriteLine("\ttempl=" + PixelsGray[i] + "  \t\tmy=" + myImagePixelsGrey[i] +
                //        "\tb=" + Math.Round(myB, 2) + "\tw=" + Math.Round(myW, 2));
            }

            Possibility = !s_ConsiderInverted ? (counter / PixelsGray.Length) :
                Math.Max(counter / PixelsGray.Length, counterForInv / PixelsInvertedGray.Length);

            // DebugImages(Image, myImage, myImagePixelsGrey);
        }

        public byte[] GetGrayPixels(Bitmap image)
        {
            image = (Bitmap)Imager.Resize(image, s_ImageRez, s_ImageRez);

            byte[] pixels = new byte[s_ImageRez * s_ImageRez];
            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                {
                    Color p = image.GetPixel(y, x); //CHANGE
                    pixels[y * image.Height + x] = (byte)(0.3 * p.R + 0.59 * p.G + 0.11 * p.B);
                }

            return pixels;
        }


        public void DebugImages(Image t, Image image2, byte[] image2Pixels)
        {
            string log = "";

            image2 = (Bitmap)Imager.Resize(image2, s_ImageRez, s_ImageRez);

            log += new string(' ', 4) + Name + new string(' ', 12) + "Image2" + Environment.NewLine;

            for (int x = 0; x < t.Width; x++)
            {
                for (int y = 0; y < t.Height; y++)
                    log += PixelsGray[y * s_ImageRez + x] + " ";

                log += new string(' ', 4);

                for (int y = 0; y < image2.Height; y++)
                    log += image2Pixels[y * s_ImageRez + x] + " ";

                log += Environment.NewLine;
            }
            log += new string('=', 16) + Environment.NewLine;

            Console.WriteLine(log);
        }
    }
}