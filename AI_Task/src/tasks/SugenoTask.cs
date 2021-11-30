using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Task
{
    class SugenoTask
    {
        static double MyFunction(double x)
        {
            return Math.Sin(x) * Math.Log(x);
        }

        class Triangle
        {
            public static double s_width;
            public static double s_step;

            public Func Func { get; private set; }
            double _center;
            double _alpha;
            double _A;

            public Triangle(double center)
            {
                _center = center;
                Func = new Func("tr" + _center, new Point(_center - s_width, 0), new Point(_center, 1), new Point(_center + s_width, 0));
                _alpha = Func.FindValueIn(center);
                _A = MyFunction(center) / center; // должно быть ВНЕ
            }

            public void CalcAlpha()
            {
                _A = 0;
            }
        }

        public SugenoTask()
        {
            Triangle.s_width = 2;
            Triangle.s_step = 1;
            double x1 = 3;
            double x2 = 20;

            List<Triangle> triangles = new List<Triangle>();
            for (double x = x1; x < x2; x += Triangle.s_step)
            {
                Triangle tr = new Triangle(x);
                triangles.Add(tr);
            }

#pragma warning disable CS0219 // The variable 'sum' is assigned but its value is never used
            double sum = 0;
#pragma warning restore CS0219 // The variable 'sum' is assigned but its value is never used

            // пройтись по иксам и посчитать A = alpha * X
            // сложить альфы 







            // создание исходной функции
            List<Point> points = new List<Point>();
            for (double x = x1; x < x2; x += Triangle.s_step)
                points.Add(new Point(x, MyFunction(x)));
            Func originFunc = new Func("origin", true, points.ToArray());

            // добавление функций для отображения
            List<Func> funcs = new List<Func>(triangles.Select(t => t.Func));
            funcs.Add(originFunc);

            FuncsManager sugenoFM = new FuncsManager(funcs.ToArray());
            (new ChartForm("Sugeno", sugenoFM)).Show();
        }
    }
}