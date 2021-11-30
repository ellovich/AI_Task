using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI_Task
{
    public class FuncsManager
    {
        public double XToCheck { get; set; }

        private List<Func> _funcs;
        private List<Point> _allPoints;
        private List<Point> _funcPoints;
        private List<Point> _intersectionPoints;

        private Func _diffFunc, _unionFunc;

        private List<Point> _bordersOfMaxLines;
        private List<Point> _trainglesMaxPoints;
        private List<Point> _unionMaxPoints;
        private List<Point> _unionMaxPointsAll;
        private double _unionMaxAverage;


        public FuncsManager(string fileName)
        {
            _funcs = ReadFuncsFromFile(fileName);

            _funcPoints = CalcFuncsPoints();
            _intersectionPoints = CalcAllIntersectionPoints();
            _allPoints = CalcAllPoints();

            _diffFunc = CalcDiff();
            _unionFunc = CalcUnion();

            // order is important!
            _trainglesMaxPoints = CalcTrainglesMaxPoints();
            _bordersOfMaxLines = CalcBordersOfMaxLines();
            _unionMaxPoints = CalcUnionMaxPoints();
            _unionMaxAverage = CalcUnionMaxAverage(0.5);
        }

        public FuncsManager(params Func[] funcs)
        {
            _funcs = funcs.ToList();

            _funcPoints = CalcFuncsPoints();
            _intersectionPoints = CalcAllIntersectionPoints();
            _allPoints = CalcAllPoints();

            _diffFunc = CalcDiff();
            _unionFunc = CalcUnion();

            //order is important!
            //   _trainglesMaxPoints = CalcTrainglesMaxPoints();
            //   _bordersOfMaxLines = CalcBordersOfMaxLines();
            //   _unionMaxPoints = CalcUnionMaxPoints();
            //   _unionMaxAverage = CalcUnionMaxAverage(15);
        }

        public void RecalculateAll() // works only with FuncsManager constructed from points
        {
            _funcPoints = CalcFuncsPoints();
            _intersectionPoints = CalcAllIntersectionPoints();
            _allPoints = CalcAllPoints();

            _diffFunc = CalcDiff();
            _unionFunc = CalcUnion();
        }


        private List<Func> ReadFuncsFromFile(string source)
        {
            List<Func> funcs = new List<Func>();

            using (StreamReader file = new StreamReader(source))
                while (!file.EndOfStream)
                    funcs.Add(Func.ReadFuncsFromFile(file));

            return funcs;
        }


        public List<double> GetAnswer(double x)
        {
            XToCheck = x;

            List<double> ans = new List<double>();

            foreach (var func in _funcs)
                if (func.ExistsIn(XToCheck))
                    ans.Add(func.FindValueIn(XToCheck));
                else
                    ans.Add(0);

            return ans;
        }

        public Func this[string funcName]
        {
            get { return _funcs.Where(f => f.Name == funcName).First(); }
        }

        public void CutFuncWith(string funcName, Line line) // CHECK
        {
            Func func = _funcs.Where(f => f.Name == funcName).First();
            var funcPoints = func.Points;
            var pointsToRemove = func.Points.Where(p => p.Y < line.Begin.Y);

            foreach (var p in pointsToRemove)
                funcPoints.Remove(p);

            foreach (var funcLine in func.Lines)
                funcPoints.Add(funcLine.CalcIntersectionPointWith(line));

            func = new Func(funcName, funcPoints.ToArray());
        }

        public void ReplaceFunc(Func oldFunc, Func newFunc)
        {
            int ind = _funcs.IndexOf(oldFunc);
            _funcs.Remove(oldFunc);
            _funcs.Insert(ind, newFunc);
        }


        #region CALCS
        private List<Point> CalcFuncsPoints()
        {
            return _funcs.SelectMany(f => f.Points).ToList();
        }

        private List<Point> CalcAllIntersectionPoints()
        {
            List<Point> intersectionPoints = new List<Point>();

            foreach (Func func1 in _funcs)
                foreach (Func func2 in _funcs)
                    if (func1 != func2)
                        foreach (Line line1 in func1.Lines)
                            foreach (Line line2 in func2.Lines)
                                if (!line1.IsParallelTo(line2))
                                {
                                    Point point = line1.CalcIntersectionPointWith(line2);

                                    // если точка пересечения принадлежит обеим отрезкам
                                    if (point.IsOnLine(line1) && point.IsOnLine(line2))
                                        intersectionPoints.Add(point);
                                }

            return intersectionPoints.Distinct().ToList();
        }

        private List<Point> CalcAllPoints()
        {
            List<Point> allPoints = new List<Point>();

            allPoints.AddRange(_funcPoints);
            allPoints.AddRange(_intersectionPoints);
            allPoints = allPoints.OrderBy(p => p.X).ToList();

            return allPoints;
        }

        private Func CalcDiff()
        {
            List<Point> diffPoints = new List<Point>();

            foreach (double x in _allPoints.Select(x => x.X))
            {
                List<Line> linesWithX = _funcs
                    .SelectMany(f => f.Lines.Where(line => line.XProjContains(x)))
                    .ToList();

                // чтобы не рассматривать стыковые точки в функции дважды
                for (int i = 0; i < linesWithX.Count - 1; i++)
                    if (linesWithX[i].End == linesWithX[i + 1].Begin)
                        linesWithX.RemoveAt(i);

                // если все функции существуют в данном X
                if (linesWithX.Count == _funcs.Count)
                    // добавляется та точка, у которой Y мин.
                    diffPoints.Add(new Point(x, linesWithX.Select(line => line.FindValueIn(x)).Min()));
            }

            HashSet<Point> diffPointsSet = new HashSet<Point>(diffPoints);

            // удаление лишних точек
            for (int i = 0; i < diffPoints.Count; i++)
            {
                Point point = diffPoints[i];
                if (!_funcPoints.Contains(point) && !_intersectionPoints.Contains(point))
                    diffPoints.RemoveAt(i--);
            }

            return new Func("diff", diffPoints.ToArray());
        }

        private Func CalcUnion()
        {
            List<Point> unionPoints = new List<Point>();

            foreach (double x in _allPoints.Select(p => p.X))
            {
                List<Line> linesWithX = _funcs
                    .SelectMany(f => f.Lines.Where(line => line.XProjContains(x)))
                    .ToList();

                // добавляется та точка, у которой Y макс.
                unionPoints.Add(new Point(x, linesWithX.Select(line => line.FindValueIn(x)).Max()));
            }

            unionPoints = unionPoints.Distinct().ToList();

            // удаление лишних точек
            for (int i = 0; i < unionPoints.Count; i++)
            {
                Point point = unionPoints[i];
                if (!_funcPoints.Contains(point) && !_intersectionPoints.Contains(point))
                    unionPoints.RemoveAt(i--);
            }

            // TODO: узнать, надо ли замыкать к нулю 
            unionPoints = unionPoints.OrderBy(p => p.X).ToList(); //
            unionPoints.Insert(0, new Point(unionPoints[0].X, 0)); //
            unionPoints.Add(new Point(unionPoints[unionPoints.Count - 1].X, 0)); //
            /////////////////////////////////////////////////////////////////////

            return new Func("union", unionPoints.ToArray());
        }

        #region FOR UNION MAX AVERAGE
        private List<Point> CalcTrainglesMaxPoints()
        {
            double maxY = _unionFunc.Points.Max(pMax => pMax.Y);

            return _funcs
                .Where(f => f.Type == Func.FuncType.triangular)
                .SelectMany(f => f.Points)
                .Where(p => p.Y.Eq(maxY))
                .OrderBy(p => p.X).ToList();
        }

        private List<Point> CalcBordersOfMaxLines()
        {
            double maxY = _unionFunc.Points.Max(pMax => pMax.Y);
            List<Point> borders = _funcs
                .Where(f => f.Type != Func.FuncType.triangular)
                .SelectMany(f => f.Points)
                .Where(p => p.Y.Eq(maxY)).ToList();
            HashSet<Point> hs = new HashSet<Point>(borders);
            return hs.Except(_trainglesMaxPoints).OrderBy(p => p.X).ToList();
        }

        private List<Point> CalcUnionMaxPoints()
        {
            List<Point> unionMaxPoints = new List<Point>(_bordersOfMaxLines);
            unionMaxPoints.AddRange(_trainglesMaxPoints);
            return unionMaxPoints;
        }

        private double CalcUnionMaxAverage(double interval = 0.001)
        {
            double sum = 0;
            double count = 0;

            _unionMaxPointsAll = new List<Point>(); // to CalcUnionMaxPoints()

            if (_bordersOfMaxLines.Count != 0)
            {
                List<Line> lines = Line.GetLinesFromPoints(_bordersOfMaxLines);

                foreach (var line in lines)
                {
                    List<Point> points = line.GetPointsByInterval(interval);
                    _unionMaxPointsAll.AddRange(points);
                    sum += points.Sum(p => p.X);
                    count += points.Count;
                }
            }

            if (_trainglesMaxPoints.Count != 0)
            {
                foreach (var point in _trainglesMaxPoints)
                {
                    _unionMaxPointsAll.Add(point);
                    sum += point.X;
                    count += 1;
                }
            }

            return sum / count;
        }
        #endregion

        #endregion


        #region GETTERS
        public List<Func> GetFuncs() { return _funcs; }
        public Point[] GetFuncPoints() { return _funcPoints.ToArray(); }
        public Point[] GetAllPoints() { return _allPoints.ToArray(); }
        public Point[] GetIntersectionPoints() { return _intersectionPoints.ToArray(); }
        public Func GetDiff() { return _diffFunc; }
        public Func GetUnion() { return _unionFunc; }
        public Point[] GetUnionMaxPoints() { return _unionMaxPoints.ToArray(); }
        public Point[] GetUnionMaxPointsAll() { return _unionMaxPointsAll.ToArray(); }
        public double GetUnionMaxAverage() { return _unionMaxAverage; }
        #endregion
    }
}