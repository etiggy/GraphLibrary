using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    //Enum to store the names of the implemented sorting and traversal algorithms

    public enum SortRoutesBy { EdgeWeight, NodeValue, CombinedValue, NumberOfNodes }

    public enum TraverseGraphUsing { DepthFirstSearch, BreadthFirstSearch, AStarSearch }
}
