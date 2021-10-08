using System.Collections.Generic;
using System.Linq;

namespace AI_Task
{
    class Fff
    {
        public double XToCheck { get; set; }

        List<Point> _allPoints;
        List<Point> _funcPoints;
        List<Point> _intersectionPoints;

        Func _diffFunc, _unionFunc;

        List<Point> _unionMaxPoints;

        List<Func> _funcs = new List<Func>()
        {
            new Func("функция0", new Point(20, 0.5),  new Point(45, 0)),
            new Func("функция1", new Point(0, 0),    new Point(10, 0.8), new Point(30, 0.8), new Point(50, 0)),
            new Func("функция2", new Point(20, 0),   new Point(30, 0.5), new Point(40, 0.5), new Point(70, 0)),
            new Func("функция3", new Point(30, 0),   new Point(35, 0.8), new Point(65, 0.8), new Point(90, 0)),
            new Func("функция4", new Point(20, 0),   new Point(30, 0.5)),
            new Func("функция5", new Point(30, 0),   new Point(40, 0.7)),
            new Func("функция6", new Point(10, 0),   new Point(25, 1),   new Point(50, 0)),
            new Func("функция7", new Point(0, 0),    new Point(55, 0.9)),
        };

        public Fff()
        {
            _funcPoints = CalcFuncsPoints();
            _intersectionPoints = CalcAllIntersectionPoints(); 
            _allPoints = CalcAllPoints();

            _diffFunc = Diff();
            _unionFunc = Union();

            _unionMaxPoints = CalcUnionMaxPoints();
        }

        Func Diff()
        {
            List<Point> diffPoints = new List<Point>();

            foreach (double x in _allPoints.Select(x => x.X))
            {
                List<Line> linesWithX = new List<Line>();

                foreach (Func func in _funcs)
                    linesWithX.AddRange(func.Lines.Where(line => line.XProjContains(x)));

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

        Func Union()
        {
            List<Point> unionPoints = new List<Point>();

            foreach (double x in _allPoints.Select(p => p.X))
            {
                List<Line> linesWithX = new List<Line>();

                foreach (Func func in _funcs)
                    linesWithX.AddRange(func.Lines.Where(line => line.XProjContains(x)));

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

        List<Point> CalcFuncsPoints()
        {
            List<Point> funcPoints = new List<Point>();

            foreach (Func func in _funcs)
                funcPoints.AddRange(func.Points);

            return funcPoints;
        }

        List<Point> CalcAllIntersectionPoints()
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

        List<Point> CalcAllPoints()
        {
            List<Point> allPoints = new List<Point>();

            allPoints.AddRange(_funcPoints);
            allPoints.AddRange(_intersectionPoints);
            allPoints = allPoints.OrderBy(p => p.X).ToList();

            return allPoints;
        }

        List<Point> CalcUnionMaxPoints()
        {
            List<Point> unionMaxPoints = new List<Point>();

            double maxY = _unionFunc.Points.Max(pMax => pMax.Y);

            unionMaxPoints = _unionFunc.Points.Where(p => D.Eq(p.Y, maxY)).ToList();

            return unionMaxPoints.Distinct().ToList();
        }

        #region GETTERS
        public List<Func> GetFuncs() { return _funcs; }
        public Point[] GetFuncPoints() { return _funcPoints.ToArray(); }
        public Point[] GetAllPoints() { return _allPoints.ToArray(); }
        public Point[] GetIntersectionPoints() { return _intersectionPoints.ToArray(); }
        public Func GetDiff() { return _diffFunc; }
        public Func GetUnion() { return _unionFunc; }
        public Point[] GetUnionMaxPoints() { return _unionMaxPoints.ToArray(); }
    #endregion
    }
}

/*
 
найти максимум общей функции, несколько точек на одной прямой
найти среднее значение, используя суммы (в числителе и в знаменателе)

сумма xj (j - max)
--------
   n

в недискретном случае:
сумма интегралов в числителе / сумма интегралов в знаменателе
 
 */