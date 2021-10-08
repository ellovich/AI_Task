using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Task
{
    class Func
    {
        public List<Line> Lines { get; private set; }
        public List<Point> Points { get; private set; }
        public string Name { private set; get; }

        static int s_funcsCounter = 0;
        static double s_maxX = double.MinValue;

        public Func (string name, params Point[] points)
        {
            s_funcsCounter++;

            Name = name;
            Points = ConstructProperPoints(points.ToList());
            Lines = ConstructLinesFromPoints(Points);

            s_maxX = SetMaxX();
        }

        public Func(string name, List<Line> lines)
        {
            s_funcsCounter++;

            Name = name;
            Points = ConstructPointsFromLines(lines);
            Lines = ConstructLinesFromPoints(Points);

            s_maxX = SetMaxX();
        }

        public Func(List<Line> lines)
        {
            s_funcsCounter++;

            Name = "func" + (s_funcsCounter);
            Points = ConstructProperPoints(ConstructPointsFromLines(lines));
            Lines = ConstructLinesFromPoints(Points);

            s_maxX = SetMaxX();
        }

        public bool ExistsIn (double x)
        {
            foreach (Line line in Lines)
                if (line.XProjContains(x))
                    return true;
            return false;
        }

        public double FindValueIn(double x)
        {
            double y = 0;

            foreach (var line in Lines) 
                if (line.XProjContains(x))
                    y = line.FindValueIn(x);

            return y;
        }

        public List<Point> FindPointsOfMax()
        {
            return null;
        }

        public List<Point> GetPointsByInterval(double interval)
        {
            List<Point> points = new List<Point>();

            foreach (var line in Lines)
                points.AddRange(line.GetPoints());

            return points;
        } //TODO
      
        private List<Point> ConstructProperPoints(List<Point> points)
        {
            List<Point> properPoints = new List<Point>(points);

            Point firstPoint = points[0];
            Point lastPoint = points[points.Count - 1];

            // если линия идет сверху вниз
            if (!D.Eq(firstPoint.Y, 0))
                properPoints.Insert(0, (new Point(0, firstPoint.Y)));

            // если линия идет снизу вверх
            if (!D.Eq(lastPoint.Y, 0))
                properPoints.Add(new Point(s_maxX, lastPoint.Y));

            return properPoints;
        }

        private List<Line> ConstructLinesFromPoints(List<Point> points)
        {
            List<Line> lines = new List<Line>();

            for (int i = 0; i < points.Count - 1; i++)
                lines.Add(new Line(points[i], points[i + 1]));

            return lines;
        }

        private List<Point> ConstructPointsFromLines(List<Line> lines)
        { // ? 
            return lines.SelectMany(line => line.GetPoints())
             .Distinct().ToList();
        }

        private double SetMaxX()
        {
            double x = Points.Select(p => p.X).Max();

            if (x > s_maxX)
                s_maxX = x;

            return s_maxX;
        }

        #region OVERRIDINGS

        public static bool operator == (Func f1, Func f2)
        {
            return f1.Equals(f2);
        }

        public static bool operator != (Func f1, Func f2)
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