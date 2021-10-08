using System;

namespace AI_Task
{
    class Point
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
            return Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2)); ;
        }

        public bool IsOnLine(Line line)
        {
            return line.Contains(this);
        }

        public bool IsOn_X_ProjectionOf(Line line)
        {
            return line.Begin.X <= X && X <= line.End.X || 
                   D.Eq(line.Begin.X, X) || D.Eq(line.End.X, X);
        }

        public bool IsOn_Y_ProjectionOf(Line line)
        {
            return line.Begin.Y <= Y && Y <= line.End.Y ||
                   D.Eq(line.Begin.Y, Y) || D.Eq(line.End.Y, Y);
        }

        public bool IsValid
        { // только для данной задачи
            get { return Y >= 0 && Y <= 1 && X >= 0; }
        } 

        #region OVERRIDINGS

        public static bool operator ==(Point point1, Point point2)
        {
            if (point1.Equals(point2))
                return true;
            return false;
        }

        public static bool operator !=(Point point1, Point point2)
        {
            if (!point1.Equals(point2))
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            Point p = (Point)obj;
            return D.Eq(X, p.X) && D.Eq(Y, p.Y);
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