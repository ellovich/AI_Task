using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AI_Task
{
    class SugenoTask
    {
        static double MyFunction(double x)
        {
            // return x / Math.Log(x);
            // return x / Math.Sin(x);               // (хрень!)
            // return Math.Sqrt(x) * Math.Log(x);
            // return Math.Cos(x) / Math.Log(x);
            // return Math.Sin(x) / Math.Sqrt(x);
            // return Math.Sin(x) *  Math.Sqrt(x);
            // return Math.Log(x) *  Math.Sqrt(x);
            // return Math.Cos(x) *  Math.Log(x);
            // return Math.Cos(x) * Math.Cos(x) * x; // (хрень!)
            return Math.Sin(x) * Math.Cos(x);        // (хрень!)
        }

        public class Triangle
        {
            public static List<Triangle> s_Triangles = new List<Triangle>();

            public static double s_width;
            public static double s_step;

            public Func Func { get; private set; }
            public double center;
            public double A;

            public double Y;

            public Triangle(double center)
            {
                this.center = center;
                Func = new Func("tr" + center, new Point(center - s_width, 0), new Point(center, 1), new Point(center + s_width, 0));
                A = MyFunction(center) / center;
            }

            public static List<Point> CalcNewPoints()
            {
                List<Point> NewPoints = new List<Point>();

                foreach (var curTr in s_Triangles)
                {
                    Func lineX = new Func("myX", new Point(curTr.center, -100), new Point(curTr.center, 100));

                    double sum_alpha = 0;
                    double new_y = 0;

                    foreach (var otherTr in s_Triangles)
                    {
                        foreach (var intersectionPoint in otherTr.Func.FindValuesWith(lineX))
                        {
                            new_y += intersectionPoint.Y * otherTr.A * curTr.center;
                            sum_alpha += intersectionPoint.Y;
                        }
                    }

                    curTr.Y = new_y / sum_alpha;
                    NewPoints.Add(new Point(curTr.center, new_y / sum_alpha));
                }
                return NewPoints;
            }

           //если апроксимация идет в прямую линию - меняй местами (originalY[i] / aproxY[i]) на (aproxY[i]/originalY[i] )
            public void UpdateA()
            {
                A = (A * (MyFunction(center) / Y)) * 0.5 + (0.5 * A);
          //      A = (A * (Y / MyFunction(center))) * 0.5 + (0.5 * A);
            }
        }

        public SugenoTask()
        {
            // отображаемые функции
            List<Func> funcs = new List<Func>();

            double x1 = 2, x2 = 10; // интервал апроксимации

            Triangle.s_width = 2; 
            Triangle.s_step = 0.5;
            List<Point> points = new List<Point>();
            for (double x = x1; x < x2; x += Triangle.s_step)
            {
                Triangle.s_Triangles.Add(new Triangle(x));
                points.Add(new Point(x, MyFunction(x)));
            }

            List<Point> newPoints = Triangle.CalcNewPoints();
            for (int i = 0; i < 100; i++) // количество корректировок
            {
                foreach (var tr in Triangle.s_Triangles)
                {
                    tr.UpdateA();
                    newPoints = Triangle.CalcNewPoints();
                }
            }
            Func newFunc = new Func("NEW", newPoints.ToArray());

            // построение исходной функции
            List<Point> originFuncPoints = new List<Point>();
            for (double x = x1; x < x2; x += 0.05)
                originFuncPoints.Add(new Point(x, MyFunction(x)));
            Func originFunc = new Func("origin", originFuncPoints.ToArray());

            // добавление функций для отображения
            funcs.Add(originFunc);
            funcs.Add(newFunc);
            funcs.AddRange(Triangle.s_Triangles.Select(t => t.Func));

            FuncsManager sugenoFM = new FuncsManager(funcs.ToArray());
            var form = new ChartForm("Sugeno", sugenoFM);
            form.SetChartSizes(x1, x2, 
                originFuncPoints.Select(p => p.Y).Min(),
                originFuncPoints.Select(p => p.Y).Max());
            form.Show();
        }
    }
}