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
                new Func("red", new Point(0, 0), new Point(255, 1)),
                new Func("green", new Point(0, 0), new Point(255, 1)),
                new Func("blue", new Point(0, 0), new Point(255, 1))
            );
            ChartForm chartForm = new ChartForm("RGB", s_FuncsManager);
            chartForm.SetChartSizes(-10, 265, -0.1, 1.1);
            chartForm.Show();

            Template.s_ImageRez = 9;
            InitTemplates(AppDomain.CurrentDomain.BaseDirectory + @"res\alphabet");
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


        public void UpdatePossibilities(Bitmap image)
        {
            if (image != null && Templates.Count > 0)
            {
                image = (Bitmap)Imager.Resize(image, Template.s_ImageRez, Template.s_ImageRez);
                Color[] pixels = Imager.ImageGetPixelsArray(image);

                int pixelsArrSize = image.Width * image.Height;

                double[] rs = new double[pixelsArrSize];
                double[] gs = new double[pixelsArrSize];
                double[] bs = new double[pixelsArrSize];

                for (int i = 0; i < pixelsArrSize; i++)
                {
                    rs[i] = s_FuncsManager["red"].FindValueIn(pixels[i].R);
                    gs[i] = s_FuncsManager["green"].FindValueIn(pixels[i].G);
                    bs[i] = s_FuncsManager["blue"].FindValueIn(pixels[i].B);
                }

                Templates.ForEach(t => t.CalcPossibility(rs, gs, bs));
                //   // Parallel.ForEach(Templates, t => t.CalcPossibility(bs, ws));
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