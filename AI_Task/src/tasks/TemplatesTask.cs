using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AI_Task
{
    public class TemplatesTask
    {
        public static FuncsManager s_FuncsManager;

        public List<Template> Templates { get; private set; }

        public TemplatesTask()
        {
            Template.s_ImageRez = 9;

            Init(AppDomain.CurrentDomain.BaseDirectory + @"res\signs");

            s_FuncsManager = new FuncsManager(
                new Func("black", new Point(0, 1), new Point(70, 1), new Point(170, 0)),
                new Func("white", new Point(160, 0), new Point(220, 1), new Point(255, 1))
                );

            ChartForm chartForm = new ChartForm("b&w", s_FuncsManager);
            chartForm.SetChartSizes(-10, 265, -0.1, 1.1);
            chartForm.Show();

            TemplatesForm form = new TemplatesForm(this);
            form.Show();
        }

        public void Init(string pathToTemplates)
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
                Templates.ForEach(i => i.CalcPossibility(image));
            // Items.AsParallel().ForAll(i => i.CalcPossibility(image));
            // Parallel.ForEach(Items, i => i.CalcPossibility(image));
        }


        public Image CreateScaledDebugPicture(Image image, int srcSize)
        {
            Image debugImage = Imager.Resize(image, Template.s_ImageRez, Template.s_ImageRez);
            debugImage = Imager.GrayScaleFilter((Bitmap)debugImage);
            debugImage = Imager.Resize(debugImage, srcSize, srcSize);
            return debugImage;
        }

        int res = Template.s_ImageRez * Template.s_ImageRez;
        public Image CreateColoredDebugWinnerPicture(int srcSize)
        {
            Template winner = GetWinner();
            return Imager.Resize(winner.Image, srcSize, srcSize);

            //Bitmap debugImage = new Bitmap(Template.s_ImageRez, Template.s_ImageRez);

            //for (int i = 0; i < Template.s_ImageRez; i++)
            //    for (int j = 0; j < Template.s_ImageRez; j++)
            //        debugImage.SetPixel(i, j, Color.FromArgb(
            //                                            (int)(winner.PixelsPossibilities[i] * 255 / res),
            //                                            255 - (int)(winner.PixelsPossibilities[i] * 255 / res),
            //                                            0));
            //return Imager.Resize((Bitmap)debugImage, srcSize, srcSize);
        }

        public Template GetWinner()
        {
            return Templates.OrderByDescending(item => item.Possibility).First();
        }
    }
}