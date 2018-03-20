using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    public static class HeuristicFunctions
    {
        //On a square grid that allows 4 directions of movement, use Manhattan distance(L1).
        public static double ManhattanDistance(Coordinate nodeAValue, Coordinate nodeBValue, double D)
        {
            var dx = Math.Abs(nodeAValue.X - nodeBValue.X);
            var dy = Math.Abs(nodeAValue.Y - nodeBValue.Y);
            var dz = Math.Abs(nodeAValue.Z - nodeBValue.Z);

            return D * (dx + dy + dz);
        }

        //On a square grid that allows 8 directions of movement, use Diagonal distance(L∞).
        public static double DiagonalDistance(Coordinate nodeAValue, Coordinate nodeBValue, double D, double D2)
        {
            var dx = Math.Abs(nodeAValue.X - nodeBValue.X);
            var dy = Math.Abs(nodeAValue.Y - nodeBValue.Y);
            var dz = Math.Abs(nodeAValue.Z - nodeBValue.Z);

            return D * (dx + dy + dz) + (D2 - 2 * D) * Math.Min(Math.Min(dx, dy), Math.Min(dy, dz));
        }

        //On a square grid that allows any direction of movement, you want Euclidean distance(L2). 
        public static double EuclideanDistance(Coordinate nodeAValue, Coordinate nodeBValue, double D)
        {
            var dx = Math.Abs(nodeAValue.X - nodeBValue.X);
            var dy = Math.Abs(nodeAValue.Y - nodeBValue.Y);
            var dz = Math.Abs(nodeAValue.Z - nodeBValue.Z);

            return D * Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2));
        }
    }
}
