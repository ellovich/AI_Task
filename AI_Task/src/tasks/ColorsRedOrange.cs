using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Task.forms;

namespace AI_Task
{
    public class ColorsRedOrange
    {
        FuncsManager _redManager;
        FuncsManager _orangeManager;

        public ColorsRedOrange()
        {
            _redManager = new FuncsManager(
                new Func("Magenta", new Point(0, 0), new Point(100, 1)),
                new Func("Yellow", new Point(0, 0), new Point(100, 1))
                );
            ChartForm redForm = new ChartForm("Red", _redManager);
            redForm.SetChartSizes(-5, 105, -0.1, 1.1);
            redForm.SetFormColor(Color.Red);
            redForm.Show();

            _orangeManager = new FuncsManager(
                new Func("Magenta", new Point(18, 0), new Point(30, 1), new Point(50, 1), new Point(65, 0)),
                new Func("Yellow", new Point(0, 0), new Point(100, 1))
                );
            ChartForm orangeForm = new ChartForm("Orange", _orangeManager);
            orangeForm.SetChartSizes(-5, 105, -0.1, 1.1);
            orangeForm.SetFormColor(Color.Orange);
            orangeForm.Show();

            ColorPicker colorPicker = new ColorPicker(this);
            colorPicker.Show();
        }

        public double CalcPossibilityRed(Color rgbColor)
        {
            CmykColor cmykColor = CmykColor.ConvertRgbToCmyk(rgbColor);
            var m = _redManager["Magenta"].FindValueIn(cmykColor.m);
            var y = _redManager["Yellow"].FindValueIn(cmykColor.y);

            return Math.Min(m, y);
        }

        public double CalcPossibilityOrange(Color rgbColor)
        {
            CmykColor cmykColor = CmykColor.ConvertRgbToCmyk(rgbColor);
            var m = _orangeManager["Magenta"].FindValueIn(cmykColor.m);
            var y = _orangeManager["Yellow"].FindValueIn(cmykColor.y);

            return Math.Min(m,y);
        }
    }

    public class CmykColor
    {
        public float c, m, y, k;

        public CmykColor(float c, float m, float y, float k)
        {
            this.c = c;
            this.m = m;
            this.y = y;
            this.k = k;
        }

        public static CmykColor ConvertRgbToCmyk(Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;

            float c;
            float m;
            float y;
            float k;
            float rf;
            float gf;
            float bf;

            rf = r / 255F;
            gf = g / 255F;
            bf = b / 255F;

            k = ClampCmyk(1 - Math.Max(Math.Max(rf, gf), bf));
            c = ClampCmyk((1 - rf - k) / (1 - k)) * 100;
            m = ClampCmyk((1 - gf - k) / (1 - k)) * 100;
            y = ClampCmyk((1 - bf - k) / (1 - k)) * 100;


            return new CmykColor(c, m, y, k);
        }

        private static float ClampCmyk(float value)
        {
            if (value < 0 || float.IsNaN(value))
            {
                value = 0;
            }

            return value;
        }

        public override string ToString()
        {
            return $"C:{(int)c} M:{(int)m} Y:{(int)y} K:{(int)k}";
        }
    }
}
