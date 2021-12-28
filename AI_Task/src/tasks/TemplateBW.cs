//using ImagerLib;
//using System;
//using System.Drawing;
//using System.IO;

//namespace AI_Task
//{
//    public class TemplateBW
//    {
//        public static int s_ImageRez; // to be changed in Main
//        public static bool s_ConsiderInverted = true;

//        public string Name { get; private set; }
//        public Bitmap Image { get; private set; }
//        public double Possibility { get; private set; }

//        byte[] _pixels;
//        byte[] _pixelsInverted;
//        double[] _blacks;
//        double[] _whites;

//        public TemplateBW(string imagePath)
//        {
//            Name = Path.GetFileNameWithoutExtension(imagePath);
//            Image = (Bitmap)Imager.Resize(new Bitmap(imagePath), s_ImageRez, s_ImageRez);

//            _pixels = Imager.ImageGetGrayPixelsArray(Image);

//            _pixelsInverted = new byte[_pixels.Length];
//            for (int i = 0; i < _pixels.Length; i++)
//                _pixelsInverted[i] = (byte)(1 - _pixels[i]);

//            UpdateBlacksAndWhites();
//        }

//        public void UpdateBlacksAndWhites()
//        {
//            _whites = new double[_pixels.Length];
//            for (int i = 0; i < _pixels.Length; i++)
//                _whites[i] = TemplatesTask.s_FuncsManager["white"].FindValueIn(_pixels[i]);
//            _whites[i] = _pixels[i] / 255.0;

//            _blacks = new double[_pixels.Length];
//            for (int i = 0; i < _pixels.Length; i++)
//                _blacks[i] = TemplatesTask.s_FuncsManager["black"].FindValueIn(_pixels[i]);
//            _blacks[i] = 1.0 - _whites[i];
//        }

//        / <summary>
//        / Подсчет вероятности совпадения шаблона с текущей картинкой
//        / </summary>
//        / <param name = "bs" > Значения пикселя текущей картинки в функции 'black' </param>
//        / <param name = "ws" > Значения пикселя текущей картинки в функции 'white' </param>
//        public void CalcPossibility(double[] bs, double[] ws)
//        {
//            double counter = 0;
//            double counterForInv = 0;

//            for (int i = 0; i < _pixels.Length; i++)
//            {
//                counter += bs[i] * _blacks[i] + ws[i] * _whites[i];
//                counterForInv += bs[i] * _whites[i] + ws[i] * _blacks[i];
//            }

//            Possibility = (!s_ConsiderInverted) ? (counter / _pixels.Length) :
//                Math.Max(counter / _pixels.Length, counterForInv / _pixelsInverted.Length);
//        }
//    }
//}

///*
//==9===========M============================
//T:255    	myImg:255    	b:0    	w:1
//T:255    	myImg:151    	b:0,26  w:0
//T:255    	myImg:93    	b:0,79  w:0
//T:255    	myImg:255    	b:0    	w:1
//T:255    	myImg:255    	b:0    	w:1
//T:255    	myImg:255    	b:0    	w:1
//T:255    	myImg:247    	b:0    	w:1
//T:255    	myImg:31    	b:1    	w:0
//T:255    	myImg:159    	b:0,19  w:0
//T:255    	myImg:213    	b:0    	w:0,88
//T:255    	myImg:156    	b:0,22  w:0
//T:135    	myImg:129    	b:0,46  w:0
//T:0    	myImg:174    	b:0,05  w:0,23
//T:0    	myImg:255    	b:0    	w:1
//T:0    	myImg:255    	b:0    	w:1
//T:0    	myImg:180    	b:0    	w:0,33
//T:235    	myImg:132    	b:0,44  w:0
//T:255    	myImg:169    	b:0,1   w:0,15
//T:255    	myImg:98    	b:0,75  w:0
//T:0    	myImg:131    	b:0,45  w:0
//T:0    	myImg:174    	b:0,05  w:0,23
//T:60    	myImg:131    	b:0,45  w:0
//T:255    	myImg:255    	b:0    	w:1
//T:247    	myImg:170    	b:0,09  w:0,17
//T:0    	myImg:181    	b:0    	w:0,35
//T:0    	myImg:126    	b:0,49  w:0
//T:146    	myImg:170    	b:0,09  w:0,17
//T:255    	myImg:91    	b:0,81  w:0
//T:0    	myImg:152    	b:0,25  w:0
//T:0    	myImg:179    	b:0,01  w:0,32
//T:255    	myImg:96    	b:0,76  w:0
//T:255    	myImg:66    	b:1    	w:0
//T:255    	myImg:147    	b:0,3   w:0
//T:153    	myImg:107    	b:0,66  w:0
//T:0    	myImg:179    	b:0,01  w:0,32
//T:0    	myImg:125    	b:0,5   w:0
//T:255    	myImg:93    	b:0,79  w:0
//T:0    	myImg:120    	b:0,55  w:0
//T:0    	myImg:76    	b:0,95  w:0
//T:125    	myImg:178    	b:0,02  w:0,3
//T:255    	myImg:98    	b:0,75  w:0
//T:255    	myImg:175    	b:0,05  w:0,25
//T:50    	myImg:156    	b:0,22  w:0
//T:0    	myImg:157    	b:0,21  w:0
//T:0    	myImg:98    	b:0,75  w:0
//T:255    	myImg:79    	b:0,92  w:0
//T:243    	myImg:170    	b:0,09  w:0,17
//T:0    	myImg:92    	b:0,8   w:0
//T:0    	myImg:153    	b:0,25  w:0
//T:0    	myImg:148    	b:0,29  w:0
//T:0    	myImg:159    	b:0,19  w:0
//T:0    	myImg:53    	b:1    	w:0
//T:0    	myImg:183    	b:0    	w:0,38
//T:0    	myImg:154    	b:0,24  w:0
//T:255    	myImg:84    	b:0,87  w:0
//T:255    	myImg:178    	b:0,02  w:0,3
//T:255    	myImg:36    	b:1    	w:0
//T:255    	myImg:142    	b:0,35  w:0
//T:255    	myImg:181    	b:0    	w:0,35
//T:255    	myImg:150    	b:0,27  w:0
//T:145    	myImg:20    	b:1    	w:0
//T:0    	myImg:163    	b:0,15  w:0,05
//T:0    	myImg:164    	b:0,15  w:0,07
//T:255    	myImg:107    	b:0,66  w:0
//T:211    	myImg:158    	b:0,2   w:0
//T:222    	myImg:89    	b:0,83  w:0
//T:255    	myImg:94    	b:0,78  w:0
//T:255    	myImg:133    	b:0,43  w:0
//T:111    	myImg:158    	b:0,2   w:0
//T:0    	myImg:255    	b:0    	w:1
//T:0    	myImg:137    	b:0,39  w:0
//T:251    	myImg:174    	b:0,05  w:0,23
//T:255    	myImg:207    	b:0    	w:0,78
//T:41    	myImg:108    	b:0,65  w:0
//T:0    	myImg:41    	b:1    	w:0
//T:0    	myImg:255    	b:0    	w:1
//T:0    	myImg:86    	b:0,85  w:0
//T:0    	myImg:155    	b:0,23  w:0
//T:60    	myImg:255    	b:0    	w:1
//T:255    	myImg:167    	b:0,12  w:0,12
//T:255    	myImg:91    	b:0,81  w:0
//*/