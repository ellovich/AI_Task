using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI_Task
{
    class FuncsManager
    {
        public double XToCheck { get; set; }

        private readonly List<Func> _funcs;

        private List<Point> _allPoints;
        private List<Point> _funcPoints;
        private List<Point> _intersectionPoints;

        private Func _diffFunc, _unionFunc;

        private List<Point> _unionMaxPoints;
        private double _unionMaxAverage;


        public FuncsManager()
        {
            _funcs = ReadFuncsFromFile("funcs.txt");

            _funcPoints = CalcFuncsPoints();
            _intersectionPoints = CalcAllIntersectionPoints(); 
            _allPoints = CalcAllPoints();
            
            _diffFunc = CalcDiff();
            _unionFunc = CalcUnion();
            
            _unionMaxPoints = CalcUnionMaxPoints();
            _unionMaxAverage = CalcUnionMaxAverage();
        }

        private List<Func> ReadFuncsFromFile(string source)
        {
            List<Func> funcs = new List<Func>();

            using (StreamReader file = new StreamReader(source))
                while (!file.EndOfStream)
                    funcs.Add(Func.ReadFuncsFromFile(file));

            return funcs;
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

            return new Func("diff", diffPoints.Distinct().ToArray());
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

            //
            // TODO: узнать, надо ли замыкать к нулю 
            unionPoints = unionPoints.OrderBy(p => p.X).ToList(); //
            unionPoints.Insert(0, new Point (unionPoints[0].X, 0)); //
            unionPoints.Add(new Point(unionPoints[unionPoints.Count-1].X, 0)); //
            //

            return new Func("union", unionPoints.Distinct().ToArray());
        }

        private List<Point> CalcUnionMaxPoints()
        {
            double maxY = _unionFunc.Points.Max(pMax => pMax.Y);

            return _unionFunc.Points           
                .Where(p => D.Eq(p.Y, maxY))
                .Where(p => _funcPoints.Contains(p))
                .OrderBy(p => p.X).ToList();
        }

        private double CalcUnionMaxAverage()
        {
            List<Line> lines = new List<Line>();

            // TODO
            if (_unionMaxPoints.Count == 1)
                return _unionMaxPoints[0].X;
            else
                lines = Line.GetLinesFromPoints(_unionMaxPoints);

            double sum = 0;
            double count = 0;

            foreach (var line in lines)
            {
                List<Point> points = line.GetPointsByInterval();
                sum += points.Sum(p => p.X);
                count += points.Count;
            }

            // прибавлять к сумме значения точек и единичку за каждую точку в количество

            return sum / count;
        }
        #endregion

        #region GETTERS
        public List<Func> GetFuncs() { return _funcs; }
        public Point[] GetFuncPoints() { return _funcPoints.ToArray(); }
        public Point[] GetAllPoints() { return _allPoints.ToArray(); }
        public Point[] GetIntersectionPoints() { return _intersectionPoints.ToArray(); }
        public Func GetDiff() { return _diffFunc; }
        public Func GetUnion() { return _unionFunc; }
        public Point[] GetUnionMaxPoints() { return _unionMaxPoints.ToArray(); }
        public double GetUnionMaxAverage() { return _unionMaxAverage; }
        #endregion
    }
}