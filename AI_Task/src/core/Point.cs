using System;

namespace AI_Task
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }


        public Point(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public double DistanceTo(Point p)
        {
            return Math.Sqrt((p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y));
        }

        public bool IsOnLine(Line line)
        {
            return line.Contains(this);
        }

        public bool IsOn_X_ProjectionOf(Line line)
        {
            return line.Begin.X < X && X < line.End.X ||
                   line.Begin.X.Eq(X) || line.End.X.Eq(X);
        }

        public bool IsOn_Y_ProjectionOf(Line line)
        {
            return line.Begin.Y < Y && Y < line.End.Y ||
                   line.Begin.Y.Eq(Y) || line.End.Y.Eq(Y);
        }

        public bool IsValid
        { // только для данной задачи
            get { return Y >= 0 && Y <= 1 && X >= 0; }
        }

        #region OVERRIDINGS
        public static bool operator ==(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !point1.Equals(point2);
        }

        public override bool Equals(object obj)
        {
            Point p = (Point)obj;
            return X.Eq(p.X) && Y.Eq(p.Y);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(X, Y).GetHashCode();
        }

        public override string ToString()
        {
            return "( " + Math.Round(X, 2) + "; " + Math.Round(Y, 2) + " )";
        }
        #endregion
    }
}