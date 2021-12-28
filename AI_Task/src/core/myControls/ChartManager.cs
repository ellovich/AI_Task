using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AI_Task
{
    public class ChartManager
    {
        Chart _chart;
        FuncsManager _funcsManager;
        Dictionary<string, Action> _seriesActions;

        public ChartManager(FuncsManager funcsManager, Chart chart)
        {
            _funcsManager = funcsManager;
            _chart = chart;

            _seriesActions = BindActionsToSeries();
            ChartSetup();
            InitChartWithAllFuncs();
        }

        public void SetSizes(double xMin, double xMax, double yMin, double yMax)
        {
            _chart.ChartAreas[0].AxisX.Minimum = xMin;
            _chart.ChartAreas[0].AxisX.Maximum = xMax;
            _chart.ChartAreas[0].AxisY.Minimum = yMin;
            _chart.ChartAreas[0].AxisY.Maximum = yMax;
        }


        public void Draw(string series)
        {
            if (_seriesActions.ContainsKey(series))
                _seriesActions[series]();
            else
                throw new Exception("No such series: " + series);
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
        }

        private void DrawFunc(string seriesName, string text, Func func)
        {
            _chart.Series[seriesName].ToolTip = seriesName;
            _chart.Series[seriesName].LegendText = seriesName;
            _chart.Series[seriesName].ChartType = (func.Type == Func.FuncType.spline) ?
                SeriesChartType.Spline : SeriesChartType.FastLine;
            _chart.Series[seriesName].BorderWidth = 3;

            foreach (var p in func.Points)
                _chart.Series[seriesName].Points.AddXY(p.X, p.Y);
        }

        private void DrawPoints(string seriesName, string text, Point[] points)
        {
            _chart.Series[seriesName].ToolTip = "X=#VALX, Y=#VALY";
            _chart.Series[seriesName].LegendText = seriesName;
            _chart.Series[seriesName].ChartType = SeriesChartType.Point;
            _chart.Series[seriesName].BorderWidth = 8;

            foreach (var p in points)
                _chart.Series[seriesName].Points.AddXY(p.X, p.Y);
        }


        #endregion


        #region FUNCS EDITING

        public void EnterEditingMode()
        {
            HideAllButFuncs();
            Draw("funcsPoints");
            _chart.MouseMove += new MouseEventHandler(MouseMove);
            _chart.MouseUp += new MouseEventHandler(MouseUp);
            _chart.Cursor = Cursors.Cross;
        }

        DataPoint _curPoint = null;
        Point _curPointTemp = null;
        Point _selectedPoint = null;
        bool _isSelected = false;

        void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                HitTestResult hit = _chart.HitTest(e.X, e.Y);
                Series s = hit.Series;

                if (s != null)
                    if (s.Points.Count == _funcsManager.GetFuncPoints().Length)
                        if (hit.PointIndex >= 0)
                            _curPoint = s.Points[hit.PointIndex];

                if (_curPoint != null)
                {
                    ChartArea ca = _chart.ChartAreas[0];
                    Axis ax = ca.AxisX;
                    Axis ay = ca.AxisY;

                    double dx = ax.PixelPositionToValue(e.X);
                    double dy = ay.PixelPositionToValue(e.Y);
                    _curPointTemp = new Point(dx, dy);

                    if (!_isSelected)
                    {
                        List<Point> similarPoints = _funcsManager.GetFuncs()
                            .SelectMany(f => f.Points)
                            .Where(p => p.X.Eq(dx, 2) && p.Y.Eq(dy, 0.01)).ToList();

                        if (similarPoints.Count > 0)
                        {
                            _selectedPoint = similarPoints[0];
                            _isSelected = true;
                        }
                    }

                    _curPoint.XValue = dx;
                    _curPoint.YValues[0] = dy;
                }
            }
        }

        void MouseUp(object sender, MouseEventArgs e)
        {
           if (!(ReferenceEquals(_selectedPoint, null)))
            {
                Func selectedFunc = _funcsManager.GetFuncs().Where(f => f.Points.Contains(_selectedPoint)).First();
                Func newFunc = selectedFunc.ReplacePoint(_selectedPoint, _curPointTemp);
                _funcsManager.ReplaceFunc(selectedFunc, newFunc);
            }
            _curPoint = null;
            _selectedPoint = null;
            _isSelected = false;

            _chart.Series.Clear();
            InitChartWithAllFuncs();
            Draw("funcsPoints");
        }

        public void EndEditingMode()
        {
            HideAllButFuncs();
            _chart.MouseMove -= new MouseEventHandler(MouseMove);
            _chart.MouseUp -= new MouseEventHandler(MouseUp);
            _chart.Cursor = Cursors.Default;
        }

        #endregion


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


        Dictionary<string, Action> BindActionsToSeries()
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

        void ChartSetup()
        {
            _chart.ChartAreas[0].AxisX.RoundAxisValues();
        }

        void InitChartWithAllFuncs()
        {
            foreach (Func func in _funcsManager.GetFuncs())
                _chart.Series.Add(func.Name);
            foreach (string label in GetSeriesLabels())
                _chart.Series.Add(label);

            foreach (var func in _funcsManager.GetFuncs())
                DrawFunc(func.Name, func.Name, func);
        }
    }
}