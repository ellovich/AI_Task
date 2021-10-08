using System;
using System.Collections.Generic;

namespace AI_Task
{
    class Line
    {
        public Point Begin { get; private set; }
        public Point End { get; private set; }
        public double Length { get; private set; }

        double _a, _b;


        public Line(Point begin, Point end)
        {
            Begin = begin;
            End = end;
            Length = Begin.DistanceTo(End);
            _a = (Begin.Y - End.Y) / (Begin.X - End.X); // check Begin.X == End.X
            _b = Begin.Y - _a * Begin.X;
        }

        public List<Point> GetPoints()
        {
            return new List<Point>() { Begin, End };
        }    

        public Point CalcIntersectionPointWith(Line line)
        {
            double X = (line._b - this._b) / (this._a - line._a);
            double Y = _a * X + _b;

            return new Point(X, Y);
        }

        public double FindValueIn(double x)
        {
            return _a * x + _b;
        }

        public bool IsParallelTo(Line line)
        {
            return _a == line._a;
        }

        public bool XProjContains(Point p)
        {
            return p.IsOn_X_ProjectionOf(this);
        }

        public bool YProjContains(Point p)
        {
            return p.IsOn_Y_ProjectionOf(this);
        }

        public bool XProjContains(double x)
        {   // y не важен
            return XProjContains(new Point(x, 0));
        }

        public bool YProjContains(double y)
        {   // x не важен
            return XProjContains(new Point(0, y));
        }

        public bool Contains(Point point)
        {
            return D.Eq(point.DistanceTo(Begin) + point.DistanceTo(End), Length);
        }

        #region OVERRIDINGS

        public static bool operator == (Line line1, Line line2)
        {
            if (line1.Equals(line2))
                return true;
            return false;
        }

        public static bool operator != (Line line1, Line line2)
        {
            if (!line1.Equals(line2))
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            Line line = (Line)obj;
            return Begin == line.Begin && End == line.End;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Begin, End).GetHashCode();
        }

        public override string ToString()
        {
            return "{ " + Begin + "; " + End + " }";
        }

        #endregion
    }
}
