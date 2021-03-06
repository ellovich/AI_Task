using System;
using System.Collections.Generic;

namespace AI_Task
{
    public class Line
    {
        public Point Begin { get; private set; }
        public Point End { get; private set; }
        public double Length { get; private set; }

        private readonly double _a, _b;


        public Line(Point begin, Point end)
        {
            Begin = begin;
            End = end;
            Length = Begin.DistanceTo(End);

            _a = (Begin.Y - End.Y) / (Begin.X - End.X);
            _b = Begin.Y - _a * Begin.X;
        }

        public Point CalcIntersectionPointWith(Line line)
        { // !IsParallelTo

            if ((line.Begin.X - line.End.X).Eq(0)) // случай с вертикальной прямой
                return new Point(line.Begin.X, FindValueIn(line.Begin.X));

            double X = (line._b - this._b) / (this._a - line._a);
            double Y = _a * X + _b;

            return new Point(X, Y);
        }

        public double FindValueIn(double x)
        {
            return _a * x + _b;
        }

        public static List<Line> GetLinesFromPoints(List<Point> points) // TODO
        {
            List<Line> lines = new List<Line>();

            if ((lines.Count % 2) == 0)
                for (int i = 0; i < points.Count - 1; i += 2)
                    lines.Add(new Line(points[i], points[i + 1]));
            else
                throw new Exception("Нечётное количество точек!");

            return lines;
        }

        public List<Point> GetPointsByInterval(double interval = 0.005)
        {
            List<Point> points = new List<Point>();

            for (double i = Begin.X; i <= End.X; i += interval)
                points.Add(new Point(i, Begin.Y));

            return points;
        }

        public bool Contains(Point point)
        {
            return (point.DistanceTo(Begin) + point.DistanceTo(End)).Eq(Length);
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

        public List<Point> GetPoints()
        {
            return new List<Point>() { Begin, End };
        }

        #region OVERRIDINGS

        public static bool operator ==(Line line1, Line line2)
        {
            return line1.Equals(line2);
        }

        public static bool operator !=(Line line1, Line line2)
        {
            return !line1.Equals(line2);
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