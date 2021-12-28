using ImagerLib;
using System.Drawing;
using System.IO;

namespace AI_Task
{
    public class Template
    {
        public static int s_ImageRez; // to be changed in Main
        public static bool s_ConsiderInverted = true;

        public string Name { get; private set; }
        public Bitmap Image { get; private set; }
        public double Possibility { get; private set; }

        Color[] _pixels;
        double[] _reds;
        double[] _greens;
        double[] _blues;

        public Template(string imagePath)
        {
            Name = Path.GetFileNameWithoutExtension(imagePath);
            Image = (Bitmap)Imager.Resize(new Bitmap(imagePath), s_ImageRez, s_ImageRez);
            _pixels = Imager.ImageGetPixelsArray(Image);

            _reds = new double[_pixels.Length];
            _greens = new double[_pixels.Length];
            _blues = new double[_pixels.Length];
            UpdatePixelsDegreesOfTruth();
        }

        public void UpdatePixelsDegreesOfTruth()
        {
            for (int i = 0; i < _pixels.Length; i++)
            {
                _reds[i] = TemplatesTask.s_FuncsManager["red"].FindValueIn(_pixels[i].R);
                _greens[i] = TemplatesTask.s_FuncsManager["green"].FindValueIn(_pixels[i].G);
                _blues[i] = TemplatesTask.s_FuncsManager["blue"].FindValueIn(_pixels[i].B);
            }
        }

        /// <summary>
        /// Подсчет истинности совпадения шаблона с текущей картинкой
        /// </summary>
        /// <param name="rs"> Значения пикселя текущей картинки в функции 'red' </param>
        /// <param name="gs"> Значения пикселя текущей картинки в функции 'green' </param>
        /// <param name="bs"> Значения пикселя текущей картинки в функции 'blue' </param>
        public void CalcPossibility(double[] rs, double[] gs, double[] bs)
        {
            double counter = 0;

            for (int i = 0; i < _pixels.Length; i++)
            {
                counter += rs[i] * _reds[i] + gs[i] * _greens[i] + bs[i] * _blues[i];
                //if (Name == "y" || Name == "goldX")
                //System.Console.WriteLine($"N:  {i}\t" +
                //                         $"p:  {_pixels[i]}\t" +
                //                         $"rs: {rs[i]}::{_reds[i]}\t" +
                //                         $"gs: {gs[i]}::{_greens[i]}\t" +
                //                         $"bs: {bs[i]}::{_blues[i]}");
            }

            Possibility = counter / (_pixels.Length * 3);
        }
    }
}