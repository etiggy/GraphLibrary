using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLibrary
{
    //This specific implementation of the recursive depth-first is the most versatile of the algorithms, but the slowest to run.
    //Instead of marking nodes visited or using an open and a closed set, it explores every possible edges as long as the neighbouring node
    //is not present in the route tracking set.
    class DepthFirstSearchAlgorithm<TEdge, TNode> : TraversalAlgorithmBase<TEdge, TNode>
    {
        //Constructor 
        public DepthFirstSearchAlgorithm(RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm) : base(routeSortingAlgorithm) { }

        //Method to initiate traversal of graph
        internal override void TraverseGraph(Node<TEdge, TNode> nodeA, Node<TEdge, TNode> nodeB)
        {
            this.nodeA = nodeA;
            this.nodeB = nodeB;

            List<Node<TEdge, TNode>> nodesOnRoute = new List<Node<TEdge, TNode>>();
            RecursiveMethod(nodesOnRoute, nodeA, nodeB);
        }

        //Recursive method to traverse graph using a depth-first search algorithm. Takes a route tracing list, a starting and a destination 
        //node as parameters. Upon finding a valid route it passes it to the sorting algorithm.
        private void RecursiveMethod(List<Node<TEdge, TNode>> nodesOnRoute, Node<TEdge, TNode> nodeA, Node<TEdge, TNode> nodeB)
        {
            nodesOnRoute.Add(nodeA);

            foreach (var edge in nodeA.listOfEdges)
            {
                Node<TEdge, TNode> nextNode = edge.nodeB;

                if (!nodesOnRoute.Contains(nextNode))
                {
                    if (nextNode == nodeB)
                    {
                        List<Node<TEdge, TNode>> tempNodesOnRoute = nodesOnRoute.ToList();

                        tempNodesOnRoute.Add(nextNode);

                        CommitRouteToSorting(tempNodesOnRoute);
                    }
                    else
                    {
                        RecursiveMethod(nodesOnRoute, nextNode, nodeB);
                    }
                }
            }

            nodesOnRoute.Remove(nodesOnRoute.Last());
        }
    }
}
