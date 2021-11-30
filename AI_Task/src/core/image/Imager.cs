using FastBitmapLib;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace ImagerLib
{
    public static class Imager
    {
        public static Image Resize(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider = false)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.PixelOffsetMode = PixelOffsetMode.Default;
                graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphic.SmoothingMode = SmoothingMode.None;
                graphic.CompositingQuality = CompositingQuality.Default;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }

        public static Bitmap GrayScaleFilter(Bitmap image)
        {
            Bitmap grayScale = new Bitmap(image.Width, image.Height);

            using (var fastBitmap = image.FastLock())
            using (var fastBitmapGray = grayScale.FastLock())
            {
                for (int y = 0; y < fastBitmap.Height; y++)
                    for (int x = 0; x < fastBitmap.Width; x++)
                    {
                        Color c = fastBitmap.GetPixel(x, y);
                        int gs = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                        fastBitmapGray.SetPixel(x, y, Color.FromArgb(gs, gs, gs));
                    }
            }

            return grayScale;
        }

        public static Image CreateTwiceScaledGrayImage(Image image, int fstSize, int sndSize)
        {
            Image debugImage = Resize(image, sndSize, sndSize);
            debugImage = GrayScaleFilter((Bitmap)debugImage);
            debugImage = Resize(debugImage, fstSize, fstSize);
            return debugImage;
        }

        public static Bitmap GetWhiteBitmap(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);

            using (var fastBmp = bmp.FastLock())
            {
                fastBmp.Clear(Color.White);
            }

            return bmp;
        }


        #region UNUSED

        public static Image Crop(Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static void SaveJpeg(string path, Image img)
        {
            var qualityParam = new EncoderParameter(Encoder.Quality, 100L);
            var jpegCodec = GetEncoderInfo("image/jpeg");

            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        public static void Save(string path, Image img, ImageCodecInfo imageCodecInfo)
        {
            var qualityParam = new EncoderParameter(Encoder.Quality, 100L);

            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, imageCodecInfo, encoderParams);
        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(t => t.MimeType == mimeType);
        }

        public static Image PutOnCanvas(Image image, int width, int height, Color canvasColor)
        {
            var res = new Bitmap(width, height);
            using (var g = Graphics.FromImage(res))
            {
                g.Clear(canvasColor);
                var x = (width - image.Width) / 2;
                var y = (height - image.Height) / 2;
                g.DrawImageUnscaled(image, x, y, image.Width, image.Height);
            }

            return res;
        }

        public static Image PutOnWhiteCanvas(Image image, int width, int height)
        {
            return PutOnCanvas(image, width, height, Color.White);
        }

        public static byte[] ImageGetGrayPixelsArray(Bitmap img)
        {
            byte[] grayPixels = new byte[img.Width * img.Height];

            using (var fastBitmap = img.FastLock())
                for (int y = 0; y < img.Height; y++)
                    for (int x = 0; x < img.Width; x++)
                    {
                        Color c = fastBitmap.GetPixel(x, y);
                        byte gs = (byte)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                        grayPixels[y * img.Width + x] = gs;
                    }

            return grayPixels;
        }

        public static string GetImage(object img)
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }

        #endregion
    }
}