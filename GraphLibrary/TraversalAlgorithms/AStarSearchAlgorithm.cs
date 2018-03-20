using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLibrary
{
    //The A* algorithm is widely used for path-finding purposes in games. The below implementation only works for nodes with values as coordinates.
    //Uses a heuristic function to estimate the distance of the goal, for the list fo functions and their specific uses please see HeuristicFunctions.cs. 
    class AStarSearchAlgorithm<TEdge, TNode> : TraversalAlgorithmBase<TEdge, TNode> where TEdge: struct where TNode : Coordinate 
    {
        //Constructor 
        public AStarSearchAlgorithm(RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm) : base(routeSortingAlgorithm) { }

        //Method to initiate traversal of graph
        internal override void TraverseGraph(Node<TEdge, TNode> nodeA, Node<TEdge, TNode> nodeB)
        {
            this.nodeA = nodeA;
            this.nodeB = nodeB;

            List<Node<TEdge, TNode>> visitedNodes = new List<Node<TEdge, TNode>>();
            List<Node<TEdge, TNode>> nodesToVisit = new List<Node<TEdge, TNode>>();
            nodesToVisit.Add(nodeA);

            Dictionary<Node<TEdge, TNode>, Node<TEdge, TNode>> nodeRouteDictionary = new Dictionary<Node<TEdge, TNode>, Node<TEdge, TNode>>();

            //Default to infinity
            MaxDefaultDictionary<Node<TEdge, TNode>, TEdge> costFromStartToThisNode = new MaxDefaultDictionary<Node<TEdge, TNode>, TEdge>();
            costFromStartToThisNode[nodeA] = default(TEdge);

            //Default to infinity
            MaxDefaultDictionary<Node<TEdge, TNode>, TEdge> costFromStartToGoalThroughThisNode = new MaxDefaultDictionary<Node<TEdge, TNode>, TEdge>();
            costFromStartToGoalThroughThisNode[nodeA] = HeuristicCostEstimate(nodeA, nodeB);

            while (nodesToVisit.Count > 0)
            {
                Node<TEdge, TNode> currentNode = nodesToVisit.FirstOrDefault();

                foreach (var node in nodesToVisit)
                {
                    if ((dynamic)costFromStartToGoalThroughThisNode[node] < costFromStartToGoalThroughThisNode[currentNode])
                    {
                        currentNode = node;
                    }
                }

                if (currentNode == nodeB)
                {
                    ConstructPath(nodeRouteDictionary, currentNode);
                }

                nodesToVisit.Remove(currentNode);
                visitedNodes.Add(currentNode);

                foreach (var edge in currentNode.listOfEdges)
                {
                    Node<TEdge, TNode> nextNode = edge.nodeB;

                    if (visitedNodes.Contains(nextNode))
                    {
                        continue;
                    }

                    if (!nodesToVisit.Contains(nextNode))
                    {
                        nodesToVisit.Add(nextNode);
                    }

                    TEdge newCostFromStartToThisNode = (dynamic)costFromStartToThisNode[currentNode] + edge.weightOfEdge;

                    if ((dynamic)newCostFromStartToThisNode >= costFromStartToThisNode[nextNode])
                    {
                        continue;
                    }

                    nodeRouteDictionary[nextNode] = currentNode;
                    costFromStartToThisNode[nextNode] = newCostFromStartToThisNode;
                    costFromStartToGoalThroughThisNode[nextNode] = (dynamic)costFromStartToThisNode[nextNode] + HeuristicCostEstimate(nextNode, nodeB);
                }
            }
        }

        private TEdge HeuristicCostEstimate(Node<TEdge, TNode> nodeA, Node<TEdge, TNode> nodeB)
        {
            return (dynamic)HeuristicFunctions.ManhattanDistance(nodeA.valueOfNode, nodeB.valueOfNode, 1);
        }

        private void ConstructPath(Dictionary<Node<TEdge, TNode>, Node<TEdge, TNode>> nodeRouteDictionary, Node<TEdge, TNode> currentNode)
        {
            List<Node<TEdge, TNode>> nodesOnRoute = new List<Node<TEdge, TNode>>();
            nodesOnRoute.Add(currentNode);

            while (nodeRouteDictionary.ContainsKey(currentNode))
            {
                currentNode = nodeRouteDictionary[currentNode];
                nodesOnRoute.Add(currentNode);
            }

            nodesOnRoute.Reverse();
            CommitRouteToSorting(nodesOnRoute);
        }
    }
}
