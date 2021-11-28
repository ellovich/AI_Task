using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI_Task
{
    public class Func
    {
        public List<Line> Lines { get; private set; }
        public List<Point> Points { get; private set; }
        public string Name { private set; get; }
        public FuncType Type { private set; get; }


        public enum FuncType
        {
            linear,
            triangular,
            trapezoidal,
            spline,
            undefined,
        }

        public Func(string name, bool isSpline, params Point[] points)
        {
            Name = name;

            if (isSpline)
            {
                Points = points.ToList();
                Lines = new List<Line>();
                Type = Func.FuncType.spline;
            }
            else
            {
                if (points.ToList().Count != 0)
                {
                    Points = ConstructProperPoints(points.ToList());
                    Lines = ConstructLinesFromPoints(Points);
                    Type = DefineType();
                    s_maxX = SetMaxX();
                }
                else
                {
                    Points = new List<Point>();
                    Lines = new List<Line>();
                    Type = FuncType.undefined;
                }
            }
        }

        public Func(string name, params Point[] points)
        {
            Name = name;

            if (points.ToList().Count != 0)
            {
                //       Points = ConstructProperPoints(points.ToList());
                Points = points.ToList();
                Lines = ConstructLinesFromPoints(Points);
                Type = DefineType();
                s_maxX = SetMaxX();
            }
            else
            {
                Points = new List<Point>();
                Lines = new List<Line>();
                Type = FuncType.undefined;
            }
        }

        public Func(string name, List<Line> lines)
        {
            Name = name;
            Points = ConstructPointsFromLines(lines); // check
            Lines = ConstructLinesFromPoints(Points);
            Type = DefineType();

            s_maxX = SetMaxX();
        }


        public static Func ReadFuncsFromFile(StreamReader sr)
        {
            string line = sr.ReadLine();
            line = line.Replace(")", "");
            line = line.Replace("(", "");
            line = line.Replace(",", "");
            line = line.Replace(".", ",");
            string[] data = line.Split(':');
            data[1] = data[1].Trim(' ');

            List<string> points = data[1].Split(' ').ToList();
            List<Point> pointsList = new List<Point>();

            for (int i = 0; i < points.Count; i += 2)
            {
                double x = Convert.ToDouble(points[i]);
                double y = Convert.ToDouble(points[i + 1]);
                pointsList.Add(new Point(x, y));
            }

            return new Func(data[0], pointsList.ToArray());
        }

        public bool ExistsIn(double x)
        {
            return Lines.Any(line => line.XProjContains(x));
        }

        public double FindValueIn(double x)
        {
            return ExistsIn(x) ? Lines.Find(line => line.XProjContains(x)).FindValueIn(x) : 0;
        }

        public List<Point> GetPointsOfMax()
        {
            return Points.Where(p => p.Y == (Points.Select(mP => mP.Y).Max())).ToList();
        }

        public FuncType DefineType()
        {
            if (Lines.Count == 3)
                return FuncType.trapezoidal;
            else if (Points.First().Y != Points.Last().Y)
                return FuncType.linear;
            else if (Points.Count == 3)
                return FuncType.triangular;

            return FuncType.undefined;
        }

        private List<Point> ConstructProperPoints(List<Point> points)
        {
            List<Point> properPoints = new List<Point>(points);

            Point firstPoint = points.First();
            // если линия идет сверху вниз
            if (!(firstPoint.Y.Eq(0)))
                properPoints.Insert(0, (new Point(0, firstPoint.Y)));

            Point lastPoint = points.Last();
            // если линия идет снизу вверх и это линейая функция
            if (!lastPoint.Y.Eq(0) && points.Count == 2)
                properPoints.Add(new Point(s_maxX, lastPoint.Y));

            if (!lastPoint.Y.Eq(0) || !firstPoint.Y.Eq(0)) // уже не первая и последняя
                Type = FuncType.linear; // move somewhere

            return properPoints;
        }

        private List<Line> ConstructLinesFromPoints(List<Point> points)
        { // check
            List<Line> lines = new List<Line>();

            for (int i = 0; i < points.Count - 1; i++)
                lines.Add(new Line(points[i], points[i + 1]));

            return lines;
        }

        private List<Point> ConstructPointsFromLines(List<Line> lines)
        { // check
            return lines.SelectMany(line => line.GetPoints())
             .Distinct().ToList();
        }


        public Func ReplacePoint(Point oldPoint, Point newPoint)
        {
            int oldIndex = Points.IndexOf(oldPoint);
            Points.Remove(oldPoint);
            Points.Insert(oldIndex, newPoint);
            Points = Points.OrderBy(p => p.X).ToList();

            return new Func(Name, Points.ToArray());
        }

        private static double s_maxX = double.MinValue;
        /// <summary>
        /// для продления односторонних функций до макс. X
        /// </summary>
        private double SetMaxX()
        {
            double x = Points.Select(p => p.X).Max();
            return (x > s_maxX) ? x : s_maxX;
        }

        #region OVERRIDINGS

        public static bool operator ==(Func f1, Func f2)
        {
            return f1.Equals(f2);
        }

        public static bool operator !=(Func f1, Func f2)
        {
            return !f1.Equals(f2);
        }

        public override bool Equals(object obj)
        {
            Func f = (Func)obj;
            return Name.Equals(f.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            string output = "";

            foreach (var line in Lines)
                output += line.ToString();

            return output;
        }

        #endregion
    }
}