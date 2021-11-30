using ImagerLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AI_Task
{
    public class TemplatesTask
    {
        public List<Template> Templates { get; private set; }
        public static FuncsManager s_FuncsManager;

        public TemplatesTask()
        {
            s_FuncsManager = new FuncsManager(
                new Func("black", new Point(0, 1), new Point(70, 1), new Point(200, 0)),
                new Func("white", new Point(180, 0), new Point(230, 1), new Point(255, 1))
                );
            ChartForm chartForm = new ChartForm("b&w", s_FuncsManager);
            chartForm.SetChartSizes(-10, 265, -0.1, 1.1);
            chartForm.Show();

            Template.s_ImageRez = 16;
            InitTemplates(AppDomain.CurrentDomain.BaseDirectory + @"res\signs");
            (new TemplatesForm(this)).Show();
        }

        public void InitTemplates(string pathToTemplates)
        {
            FileInfo[] signTemplates = new DirectoryInfo(pathToTemplates)
                .GetFilesByExtensions(".jpg", ".png");

            if (signTemplates.Length > 0)
            {
                Templates = new List<Template>();
                foreach (var item in signTemplates)
                    Templates.Add(new Template(item.FullName));
            }
        }


        //public static long s_time = 0;
        //public static long s_counter = 0;
        public void UpdatePossibilities(Bitmap image)
        {
            if (image != null && Templates.Count > 0)
            {
                image = (Bitmap)Imager.Resize(image, Template.s_ImageRez, Template.s_ImageRez);
                byte[] myImageGrayPixels = Imager.ImageGetGrayPixelsArray(image);

                int pixelsArrSize = image.Width * image.Height;
                double[] bs = new double[pixelsArrSize];
                double[] ws = new double[pixelsArrSize];

                for (int i = 0; i < pixelsArrSize; i++)
                {
                    bs[i] = s_FuncsManager["black"].FindValueIn(myImageGrayPixels[i]);
                    ws[i] = s_FuncsManager["white"].FindValueIn(myImageGrayPixels[i]);
                }

                //   var watch = System.Diagnostics.Stopwatch.StartNew();
                Templates.ForEach(t => t.CalcPossibility(bs, ws));
                //   // Parallel.ForEach(Templates, t => t.CalcPossibility(bs, ws));
                //   watch.Stop();
                //   var elapsedMs = watch.ElapsedTicks;
                //
                //   s_counter++;
                //   s_time += elapsedMs;
            }
        }

        public Image GetWinnerImage(int srcSize)
        {
            Template winner = GetWinner();
            return Imager.Resize(winner.Image, srcSize, srcSize);
        }

        public Template GetWinner()
        {
            return Templates.OrderByDescending(item => item.Possibility).First();
        }
    }
}