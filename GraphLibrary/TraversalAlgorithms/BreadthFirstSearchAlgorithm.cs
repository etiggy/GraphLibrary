using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    //The breadth first iterative implementation is a quick algorithm, particulalry useful to build trees from graphs
    class BreadthFirstSearchAlgorithm<TEdge, TNode> : TraversalAlgorithmBase<TEdge, TNode>
    {
        //Constructor 
        public BreadthFirstSearchAlgorithm(RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm) : base(routeSortingAlgorithm) { }

        //Method to initiate traversal of graph using the breadth-first search algorithm
        internal override void TraverseGraph(Node<TEdge, TNode> nodeA, Node<TEdge, TNode> nodeB)
        {
            this.nodeA = nodeA;
            this.nodeB = nodeB;

            Queue<Node<TEdge, TNode>> nodesToVisit = new Queue<Node<TEdge, TNode>>();
            List<Node<TEdge, TNode>> visitedNodes = new List<Node<TEdge, TNode>>();
            Dictionary<Node<TEdge, TNode>, Node<TEdge, TNode>> nodeRouteDictionary = new Dictionary<Node<TEdge, TNode>, Node<TEdge, TNode>>();

            nodeRouteDictionary[nodeA] = null;
            nodesToVisit.Enqueue(nodeA);

            while (nodesToVisit.Count > 0)
            {
                Node<TEdge, TNode> currentNode = nodesToVisit.Dequeue();

                if (currentNode == nodeB)
                {
                    ConstructPath(currentNode, nodeRouteDictionary);
                }
                else
                {
                    foreach (var edge in currentNode.listOfEdges)
                    {
                        Node<TEdge, TNode> nextNode = edge.nodeB;

                        if (!visitedNodes.Contains(nextNode))
                        {
                            if (!nodesToVisit.Contains(nextNode))
                            {
                                nodeRouteDictionary[nextNode] = currentNode;
                                nodesToVisit.Enqueue(nextNode);
                            }
                        }
                    }

                    visitedNodes.Add(currentNode);
                }
            }
        }

        //Method to construct route from the node route meta dictionary
        private void ConstructPath(Node<TEdge, TNode> currentNode, Dictionary<Node<TEdge, TNode>, Node<TEdge, TNode>> nodeRouteDictionary)
        {
            List<Node<TEdge, TNode>> nodesOnRoute = new List<Node<TEdge, TNode>>();
            nodesOnRoute.Add(currentNode);
            Node<TEdge, TNode> nextNode = nodeRouteDictionary[currentNode];

            while (nextNode != null)
            {
                currentNode = nextNode;
                nodesOnRoute.Add(nextNode);
                nextNode = nodeRouteDictionary[currentNode];
            }

            nodesOnRoute.Reverse();
            CommitRouteToSorting(nodesOnRoute);
        }
    }
}
