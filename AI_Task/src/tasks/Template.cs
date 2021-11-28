using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AI_Task
{
    public class Template : IComparer<Template>, IComparable<Template>
    {
        public static int s_ImageRez = 7;  // change in Main

        public string Name { get; private set; }
        public Bitmap Image { get; private set; }
        public byte[] PixelsGray { get; private set; }
        public byte[] PixelsInvertedGray { get; private set; }
        public double Possibility { get; private set; }


        public Template(string imagePath)
        {
            Name = Path.GetFileNameWithoutExtension(imagePath);
            Image = new Bitmap(imagePath);
            Image = (Bitmap)Imager.Resize(Image, s_ImageRez, s_ImageRez, false);
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
                double myB = TemplatesTask.s_FuncsManager["black"].FindValueIn(myImagePixelsGrey[i]);
                double myW = TemplatesTask.s_FuncsManager["white"].FindValueIn(myImagePixelsGrey[i]);

                if (PixelsGray[i] == 0)
                    counter += myB;
                else if (PixelsGray[i] == 255)
                    counter += myW;

                if (PixelsInvertedGray[i] == 0)
                    counterForInv += myB;
                else if (PixelsInvertedGray[i] == 255)
                    counterForInv += myW;

                //    Console.WriteLine("\ttempl=" + PixelsGray[i] + "  \t\tmy=" + myImagePixelsGrey[i] +
                //        "\tb=" + Math.Round(myB, 2) + "\tw=" + Math.Round(myW, 2));
            }

            Possibility = !TemplatesTask.s_ConsiderInverted ? (counter / PixelsGray.Length) :
                Math.Max(counter / PixelsGray.Length, counterForInv / PixelsInvertedGray.Length);

            //    DebugImages(Image, myImage, myImagePixelsGrey);
        }


        public byte[] GetGrayPixels(Bitmap image)
        {
            image = (Bitmap)Imager.Resize(image, s_ImageRez, s_ImageRez, false);

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
            image2 = (Bitmap)Imager.Resize(image2, s_ImageRez, s_ImageRez, false);


            Console.WriteLine(new string(' ', 4) + Name + new string(' ', 12) + "Image2");

            for (int x = 0; x < t.Width; x++)
            {
                for (int y = 0; y < t.Height; y++)
                    Console.Write(PixelsGray[y * s_ImageRez + x] + " ");

                Console.Write(new string(' ', 4));

                for (int y = 0; y < image2.Height; y++)
                    Console.Write(image2Pixels[y * s_ImageRez + x] + " ");

                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 16));
        }

        public int Compare(Template t1, Template t2)
        {
            if (t1.Possibility < t2.Possibility) return -1;
            else if (t1.Possibility > t2.Possibility) return 1;
            else return 0;
        }

        public int CompareTo(Template t1)
        {
            return Compare(this, t1);
        }
    }
}