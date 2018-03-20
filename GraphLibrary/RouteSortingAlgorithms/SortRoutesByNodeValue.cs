using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    //Algorithm to sort routes by summarising the values of nodes. Requires the type to be a value type.
    class SortRoutesByNodeValue<TEdge, TNode> : RouteSortingAlgorithmBase<TEdge, TNode> where TNode : struct
    {
        //Constructor to set error messages, initialise result properties and run arithmetic test
        public SortRoutesByNodeValue(string noRouteExistString, string invalidDataTypeString) : base(noRouteExistString, invalidDataTypeString)
        {
            minValue = maxValue = new KeyValuePair<string, TNode>(noRouteExistString, default(TNode));

            IsValidType = TypeArithmeticTest<TNode>();
        }

        //Method to summarise the value of nodes in the route passed to it as a parameter and call SetMinAndMaxValue with the result
        internal override void CalculateValue(List<Node<TEdge, TNode>> currentRoute)
        {
            if (IsValidType)
            {
                StringBuilder currentRouteNodes = new StringBuilder();
                dynamic currentRouteNodeValues = default(TNode);

                for (int i = 0; i < currentRoute.Count; i++)
                {
                    Node<TEdge, TNode> currentNode = currentRoute[i];

                    currentRouteNodes.Append(currentNode.nameOfNode);
                    currentRouteNodeValues += currentNode.valueOfNode;

                    if (i < currentRoute.Count - 1)
                    {
                        currentRouteNodes.Append('-');
                    }
                }

                SetMinAndMaxValue<TNode>(currentRouteNodes.ToString(), currentRouteNodeValues);
            }
        }
    }
}
