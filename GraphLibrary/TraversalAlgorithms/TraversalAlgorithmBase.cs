using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLibrary
{
    //Base abstract class for traversal algorithms
    internal abstract class TraversalAlgorithmBase<TEdge, TNode>
    {
        //Properties to store references for the starting and the goal node for traversal
        protected Node<TEdge, TNode> nodeA { get; set; }
        protected Node<TEdge, TNode> nodeB { get; set; }

        //Property to hold reference to route sorting algorithm
        private RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm { get; set; }

        //Base constructor to save initialisation parameters
        public TraversalAlgorithmBase(RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm)
        {
            this.routeSortingAlgorithm = routeSortingAlgorithm;
        }

        //Method to pass the route to the sorting algorithm
        protected void CommitRouteToSorting(List<Node<TEdge, TNode>> route)
        {
            routeSortingAlgorithm.CalculateValue(route);
        }

        //Abstract method to initiate traversal of graph
        internal abstract void TraverseGraph(Node<TEdge, TNode> nodeA, Node<TEdge, TNode> nodeB);
    }
}
