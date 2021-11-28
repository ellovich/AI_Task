using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

            //  Sugeno sugeno = new Sugeno();
            TemplatesTask signs = new TemplatesTask();

            Application.Run();
        }
    }

    public static class Extensions
    {
        public static bool Eq(this double d1, double d2)
        {
            return Math.Abs(d1 - d2) <= 0.00001;
        }

        public static Color InvertColor(this Color ColourToInvert)
        {
            return Color.FromArgb(255 - ColourToInvert.R,
              255 - ColourToInvert.G, 255 - ColourToInvert.B);
        }

        public static FileInfo[] GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            return files.Where(f => extensions.Contains(f.Extension)).ToArray();
        }

        static void GetPixel2(this Image image)
        {
            using (var bmp = new Bitmap(100, 100))
            {
                // Фиксируем изображение в памяти
                var bd = bmp.LockBits(
                    new Rectangle(0, 0, 100, 100),
                    ImageLockMode.ReadWrite,
                    bmp.PixelFormat
                );
                // Буфер под размер изображения
                var buffer = new byte[bd.Stride * bd.Height];
                // Копируем байтовое представление изображения
                // в выделенный буфер
                Marshal.Copy(bd.Scan0, buffer, 0, buffer.Length);

                /*
                 * Выполнение некоторых модификаций над буфером
                 */

                // Копируем буфер обратно по адресу расположения
                // изображения в памяти
                Marshal.Copy(buffer, 0, bd.Scan0, buffer.Length);
                // Разблокируем изображение
                bmp.UnlockBits(bd);
            }
        }

        //static void GetPixel3(this Image image)
        //{
        //    Bitmap one = new Bitmap("sddd");
        //    Bitmap two = new Bitmap("sddd");
        //    Bitmap thr = new Bitmap(one.Width, one.Height);

        //    unsafe
        //    {
        //        var oneBits = one.LockBits(new Rectangle(0, 0, one.Width, one.Height), ImageLockMode.ReadOnly, one.PixelFormat);
        //        var twoBits = two.LockBits(new Rectangle(0, 0, two.Width, two.Height), ImageLockMode.ReadOnly, two.PixelFormat);
        //        var thrBits = thr.LockBits(new Rectangle(0, 0, thr.Width, thr.Height), ImageLockMode.WriteOnly, thr.PixelFormat);

        //        int padding = twoBits.Stride - (two.Width * sizeof(Pixel));

        //        int width = two.Width;
        //        int height = two.Height;

        //        Parallel.For(0, one.Width * one.Height, i => {
        //            Pixel* pxOne = (Pixel*)((byte*)oneBits.Scan0 + i * sizeof(Pixel));

        //            byte* ptr = (byte*)twoBits.Scan0;

        //            for (int j = 0; j < height; j++)
        //            {
        //                for (int k = 0; k < width; k++)
        //                {
        //                    Pixel* pxTwo = (Pixel*)ptr;
        //                    if (pxOne->Equals(*pxTwo))
        //                    {
        //                        Pixel* pxThr = (Pixel*)((byte*)thrBits.Scan0 + i * sizeof(Pixel));
        //                        pxThr->Alpha = pxThr->Red = pxThr->Green = pxThr->Blue = 0xFF;
        //                    }
        //                    ptr += sizeof(Pixel);
        //                }
        //                ptr += padding;
        //            }
        //        });

        //        one.UnlockBits(oneBits);
        //        two.UnlockBits(twoBits);
        //        thr.UnlockBits(thrBits);
        //    }
        //}
    }
}
