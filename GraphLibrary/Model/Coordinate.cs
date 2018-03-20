using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    //Class to implement a simple coordinate type up to three dimensions.
    public class Coordinate
    {
        //Internal properties with private setters
        internal double X { get; private set; }
        internal double Y { get; private set; }
        internal double Z { get; private set; }

        //Default constructor
        public Coordinate(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        //Chained constructor for two dimensional coordinates
        public Coordinate(double X, double Y) : this(X, Y, 0) { }

        //Chained constructor for one dimensional coordinates
        public Coordinate(double X) : this(X, 0, 0) { }

        //Method to return the distance between two coordinates
        public double GetDistance(Coordinate otherCoordinate)
        {
            return Math.Sqrt(Math.Pow((this.X - otherCoordinate.X), 2) + Math.Pow((this.Y - otherCoordinate.Y), 2) + 
                Math.Pow((this.Z - otherCoordinate.Z), 2));
        }
    }
}
