using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    //Algorithm to sort routes by summarising the number of nodes they contain (hop). Has no specific type requirements.
    class SortRoutesByNumberOfNodes<TEdge, TNode> : RouteSortingAlgorithmBase<TEdge, TNode>
    {
        //Constructor to set error messages, initialise result properties and run arithmetic test
        public SortRoutesByNumberOfNodes(string noRouteExistString, string invalidDataTypeString) : base(noRouteExistString, invalidDataTypeString)
        {
            minValue = maxValue = new KeyValuePair<string, int>(noRouteExistString, default(int));

            IsValidType = TypeArithmeticTest<int>();
        }

        //Method to summarise the number of nodes in the route passed to it as a parameter and call SetMinAndMaxValue with the result
        internal override void CalculateValue(List<Node<TEdge, TNode>> currentRoute)
        {
            if (IsValidType)
            {
                StringBuilder currentRouteNodes = new StringBuilder();

                for (int i = 0; i < currentRoute.Count; i++)
                {
                    Node<TEdge, TNode> currentNode = currentRoute[i];

                    currentRouteNodes.Append(currentNode.nameOfNode);

                    if (i < currentRoute.Count - 1)
                    {
                        currentRouteNodes.Append('-');
                    }
                }

                SetMinAndMaxValue<int>(currentRouteNodes.ToString(), currentRoute.Count);
            }
        }
    }
}
