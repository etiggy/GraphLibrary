using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    //Algorithm to sort routes by summarising the values of nodes and the weight of edges. Requires both types to be value types.
    class SortRoutesByCombinedValue<TEdge, TNode> : RouteSortingAlgorithmBase<TEdge, TNode> where TEdge : struct where TNode : struct
    {
        //Constructor to initialise result properties and run arithmetic test
        public SortRoutesByCombinedValue(string noRouteExistString, string invalidDataTypeString) : base(noRouteExistString, invalidDataTypeString)
        {
            minValue = maxValue = new KeyValuePair<string, TNode>(noRouteExistString, default(TNode));

            IsValidType = TypeArithmeticTest<TEdge>() && TypeArithmeticTest<TNode>();
        }

        //Method to summarise the weight of edges and the value of nodes in the route passed to it as a parameter and call SetMinAndMaxValue 
        //with the result
        internal override void CalculateValue(List<Node<TEdge, TNode>> currentRoute)
        {
            if (IsValidType)
            {
                StringBuilder currentRouteNodes = new StringBuilder();
                dynamic currentRouteEdgeWeights = default(TEdge);
                dynamic currentRouteNodeValues = default(TNode);

                for (int i = 0; i < currentRoute.Count; i++)
                {
                    Node<TEdge, TNode> currentNode = currentRoute[i];

                    currentRouteNodes.Append(currentNode.nameOfNode);
                    currentRouteNodeValues += currentNode.valueOfNode;

                    if (i < currentRoute.Count - 1)
                    {
                        Node<TEdge, TNode> nextNode = currentRoute[i + 1];

                        currentRouteNodes.Append('-');
                        currentRouteEdgeWeights += currentNode.listOfEdges.Find(m => m.nodeB == nextNode).weightOfEdge;
                    }
                }

                SetMinAndMaxValue<TNode>(currentRouteNodes.ToString(), currentRouteNodeValues);
            }
        }
    }
}
