using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace AI_Task
{
    class ChartManager
    {
        private readonly Chart _chart;
        private readonly FuncsManager _funcsManager;
        private readonly Dictionary<string, Action> _seriesActions;

        public ChartManager(FuncsManager funcsManager, Chart chart)
        {
            _funcsManager = funcsManager;
            _chart = chart;

            ChartSetup();
            _seriesActions = BindActionsToSeries();
            InitChartWithAllFuncs();
        }

        public void HideAllButFuncs()
        {
            foreach (string serName in _seriesActions.Keys)
            {
                _chart.Series.Remove(_chart.Series[serName]);
                _chart.Series.Add(serName);
            }
        }

        public string[] GetSeriesLabels()
        {
            return _seriesActions.Select(s => s.Key).ToArray();
        }

        public void Draw(string series)
        {
            if (_seriesActions.ContainsKey(series))
                _seriesActions[series]();
        }

        #region DRAWING FUNCS
        private void DrawX(string seriesName, double x)
        {
            _chart.Series.Remove(_chart.Series[seriesName]);
            _chart.Series.Add(seriesName);

            _chart.Series[seriesName].ChartType = SeriesChartType.FastLine;
            _chart.Series[seriesName].Points.AddXY(x, 0);
            _chart.Series[seriesName].Points.AddXY(x, 1);
            _chart.Series[seriesName].BorderWidth = 4;

            Debug.WriteLine(seriesName + " = " + x);
        }

        private void DrawFunc(string seriesName, string text, Func func)
        {
            _chart.Series[seriesName].ToolTip = seriesName;
            _chart.Series[seriesName].LegendText = seriesName;
            _chart.Series[seriesName].ChartType = SeriesChartType.FastLine;
            _chart.Series[seriesName].BorderWidth = 3;

            foreach (var p in func.Points)
                _chart.Series[seriesName].Points.AddXY(p.X, p.Y);

            Debug.WriteLine(text + ": ");
            foreach (var p in func.Points)
                Debug.WriteLine(p);
        }

        private void DrawPoints(string seriesName, string text, Point[] points)
        {
            _chart.Series[seriesName].ToolTip = "X=#VALX, Y=#VALY";
            _chart.Series[seriesName].LegendText = seriesName;
            _chart.Series[seriesName].ChartType = SeriesChartType.Point;
            _chart.Series[seriesName].BorderWidth = 4;

            foreach (var p in points)
                _chart.Series[seriesName].Points.AddXY(p.X, p.Y);

            Debug.WriteLine(text + ": ");
            foreach (var p in points)
                Debug.WriteLine(p);
        }
        #endregion

        private void ChartSetup()
        {
            _chart.ChartAreas[0].AxisX.RoundAxisValues();
            _chart.ChartAreas[0].AxisX.Minimum = 0;
            _chart.ChartAreas[0].AxisY.Maximum = 1;
        }

        private Dictionary<string, Action> BindActionsToSeries()
        {
            return new Dictionary<string, Action>
            {
                {"X", delegate {
                    DrawX("X", _funcsManager.XToCheck);
                }},
                {"funcsPoints", delegate {
                    foreach (var func in _funcsManager.GetFuncs())
                        DrawPoints("funcsPoints", func.Name, func.Points.ToArray());
                }},
                {"intersectionPoints", delegate {
                    DrawPoints("intersectionPoints", "Точки всех пересечений", _funcsManager.GetIntersectionPoints());
                }},
                {"union", delegate {
                    DrawFunc("union", "Все точки объединения", _funcsManager.GetUnion());
                }},
                {"unionPoints", delegate {
                    DrawPoints("unionPoints", "Все точки объединения", _funcsManager.GetUnion().Points.ToArray());
                }},
                {"diff", delegate {
                    DrawFunc("diff", "Все точки, участвующие в пересечении", _funcsManager.GetDiff());
                }},
                {"diffPoints", delegate {
                    DrawPoints("diffPoints", "Все точки, участвующие в пересечении", _funcsManager.GetDiff().Points.ToArray());
                }},
                {"unionMaxPoints", delegate {
                    DrawPoints("unionMaxPoints", "Точки максимумов", _funcsManager.GetUnionMaxPoints());
                }},
                {"unionMaxPointsAll", delegate {
                    DrawPoints("unionMaxPointsAll", "Все точки максимумов", _funcsManager.GetUnionMaxPointsAll());
                }},
                {"unionMaxAverage", delegate {
                    DrawX("unionMaxAverage", _funcsManager.GetUnionMaxAverage());
                }},
            };
        }
        
        private void InitChartWithAllFuncs()
        {
            foreach (var func in _funcsManager.GetFuncs())
                _chart.Series.Add(func.Name);
            foreach (var label in GetSeriesLabels())
                _chart.Series.Add(label);

            foreach (var func in _funcsManager.GetFuncs())
                DrawFunc(func.Name, func.Name, func);
        }
    }
}