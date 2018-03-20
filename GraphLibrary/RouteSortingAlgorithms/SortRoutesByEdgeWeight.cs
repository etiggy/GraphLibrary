using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLibrary
{
    //Algorithm to sort routes by summarising the weight of edges. Requires the type to be a value type.
    public class SortRoutesByEdgeWeight<TEdge, TNode> : RouteSortingAlgorithmBase<TEdge, TNode> where TEdge : struct
    {
        //Constructor to set error messages, initialise result properties and run arithmetic test
        public SortRoutesByEdgeWeight(string noRouteExistString, string invalidDataTypeString) : base(noRouteExistString, invalidDataTypeString)
        {
            minValue = maxValue = new KeyValuePair<string, TEdge>(noRouteExistString, default(TEdge));

            IsValidType = TypeArithmeticTest<TEdge>();
        }

        //Method to summarise the weight of edges in the route passed to it as a parameter and call SetMinAndMaxValue with the result
        internal override void CalculateValue(List<Node<TEdge, TNode>> currentRoute)
        {
            if (IsValidType)
            {
                StringBuilder currentRouteNodes = new StringBuilder();
                dynamic currentRouteEdgeWeights = default(TEdge);

                for (int i = 0; i < currentRoute.Count - 1; i++)
                {
                    Node<TEdge, TNode> currentNode = currentRoute[i];
                    Node<TEdge, TNode> nextNode = currentRoute[i + 1];

                    currentRouteNodes.Append(currentNode.nameOfNode);
                    currentRouteNodes.Append('-');
                    currentRouteEdgeWeights += currentNode.listOfEdges.Find(m => m.nodeB == nextNode).weightOfEdge;
                }

                currentRouteNodes.Append(currentRoute.Last().nameOfNode);

                SetMinAndMaxValue<TEdge>(currentRouteNodes.ToString(), currentRouteEdgeWeights);
            }
        }
    }
}
