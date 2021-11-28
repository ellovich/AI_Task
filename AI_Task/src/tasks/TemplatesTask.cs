using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AI_Task
{
    public class TemplatesTask
    {
        public List<Template> Templates { get; private set; }
        public static FuncsManager s_FuncsManager;
        public static bool s_ConsiderInverted = true;

        public TemplatesTask()
        {
            Template.s_ImageRez = 7;
            Init(AppDomain.CurrentDomain.BaseDirectory + @"res\letters");

            s_FuncsManager = new FuncsManager(
                new Func("black", new Point(0, 1), new Point(70, 1), new Point(170, 0)),
                new Func("white", new Point(160, 0), new Point(220, 1), new Point(255, 1))
                );

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
    }
}