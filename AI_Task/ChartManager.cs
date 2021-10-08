using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace AI_Task
{
    class ChartManager
    {
        Chart _chart;
        Fff _fff;
        readonly Dictionary<string, Action> _series;

        public ChartManager(Fff fff, Chart chart)
        {
            _fff = fff;
            _chart = chart;

            _series = new Dictionary<string, Action>
            {
                {"X", delegate {
                    DrawX(_fff.XToCheck);
                }},
                {"funcsPoints", delegate {
                    Console.WriteLine("Только точки функций: ");
                    foreach (var func in _fff.GetFuncs())
                        DrawPoints("funcsPoints", func.Name, func.Points.ToArray());
                }},
                {"intersectionPoints", delegate {
                    DrawPoints("intersectionPoints", "Точки всех пересечений", _fff.GetIntersectionPoints());
                }},
                {"union", delegate {
                    DrawFunc("union", "Все точки объединения: ", _fff.GetUnion());
                }},
                {"diff", delegate {
                    DrawFunc("diff", "Все точки, участвующие в пересечении: ", _fff.GetDiff());
                }},
                {"maxOfUnion", delegate {
                }},
                {"unionMaxPoints", delegate {
                }},
            };

            foreach (var func in _fff.GetFuncs())
                _chart.Series.Add(func.Name);
            foreach (var label in GetSeriesLabels())
                _chart.Series.Add(label);

            foreach (var func in _fff.GetFuncs())
                DrawFunc(func.Name, func.Name, func);

            //_chart.Series.Add("парабола");
            //_chart.Series["парабола"].ChartType = SeriesChartType.Spline;
            //double x = 0.001;
            //const int N = 1000;
            //for (int i = 1; i < N; i++)
            //{
            //    double y = x * x;
            //    _chart.Series[0].Points.AddXY(x, y);
            //    x += 0.001;
            //}
        }

        public void Draw(string series)
        {
            if (_series.ContainsKey(series))
                _series[series]();           
        }

        public void DrawX(double x)
        {
            _chart.Series.Remove(_chart.Series["X"]);
            _chart.Series.Add("X");

            _chart.Series["X"].ChartType = SeriesChartType.FastLine;
            _chart.Series["X"].Points.AddXY(x, 0);
            _chart.Series["X"].Points.AddXY(x, 1);
            _chart.Series["X"].BorderWidth = 4;
        }

        public void DrawFunc(string seriesName, string text, Func func)
        {
            _chart.Series[seriesName].ToolTip = seriesName;
            _chart.Series[seriesName].LegendText = seriesName;
            _chart.Series[seriesName].ChartType = SeriesChartType.FastLine;
            _chart.Series[seriesName].BorderWidth = 3;

            foreach (var p in func.Points)
                _chart.Series[seriesName].Points.AddXY(p.X, p.Y);

            Console.WriteLine(text + ": ");
            foreach (var p in func.Points)
                Console.WriteLine(p);
        }

        public void DrawPoints(string seriesName, string text, Point[] points)
        {
            _chart.Series[seriesName].LegendText = seriesName;
            _chart.Series[seriesName].ChartType = SeriesChartType.Point;
            _chart.Series[seriesName].BorderWidth = 4;

            foreach (var p in points)
                _chart.Series[seriesName].Points.AddXY(p.X, p.Y);

            Console.WriteLine(text + ": ");
            foreach (var p in points)
                Console.WriteLine(p);
        }

        public void HideAllButFuncs()
        {
            foreach (string serName in _series.Keys)
            {
                _chart.Series.Remove(_chart.Series[serName]);
                _chart.Series.Add(serName);
            }
        }

        public string[] GetSeriesLabels()
        {
            return _series.Select(s => s.Key).ToArray();
        }
    }
}