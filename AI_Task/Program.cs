using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AI_Task
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TemplatesTask tt = new TemplatesTask();
             SugenoTask st = new SugenoTask();
            // TemplatesTask signs = new TemplatesTask();
            // Console.WriteLine(TemplatesTask.s_time / TemplatesTask.s_counter);
            // BellmanZadde bz = new BellmanZadde();

            // ColorsRedOrange colorsRedOrange = new ColorsRedOrange();

            Application.Run();
        }
    }

    public static class Extensions
    {
        public static bool Eq(this double d1, double d2, double e = 0.0001)
        {
            return Math.Abs(d1 - d2) <= e;
        }

        public static FileInfo[] GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            return files.Where(f => extensions.Contains(f.Extension)).ToArray();
        }

        public static Color InvertColor(this Color ColourToInvert)
        {
            return Color.FromArgb(255 - ColourToInvert.R,
              255 - ColourToInvert.G, 255 - ColourToInvert.B);
        }
    }
}

