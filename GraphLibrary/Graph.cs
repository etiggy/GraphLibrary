using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GraphLibrary
{
    //Implementation of a graph using generics
    public class Graph<TEdge, TNode>
    {
        //Constants to store error messages
        private const string noRouteExistString = "No route exist";
        private const string invalidDataTypeString = "Invalid data type";
        private const string noDataTypeString = "The following nodes don't exist in the current graph structure: ";

        //Collection of all the nodes in the graph
        private List<Node<TEdge, TNode>> listOfNodes { get; set; }

        //Default constructor to create graph
        public Graph()
        {
            listOfNodes = new List<Node<TEdge, TNode>>();
        }

        //Method to create a node in the graph with node name and node value parameter. Checks before creation for node name clashes to 
        //circumvent duplication.
        //Returns boolean true on success and false on failure.
        public bool CreateNode(string nameOfNode, TNode valueOfNode)
        {
            bool createdOK = false;

            if (GetNode(nameOfNode) == null)
            {
                listOfNodes.Add(new Node<TEdge, TNode>(nameOfNode, valueOfNode));
                createdOK = true;
            }

            return createdOK;
        }

        //Overload for method to create a node in the graph with node name parameter. Checks before creation for node name clashes to circumvent 
        //duplication.
        //Returns boolean true on success and false on failure.
        public bool CreateNode(string nameOfNode)
        {
            return CreateNode(nameOfNode, default(TNode));
        }

        //Overload for method to create a node in the graph. Extra parameters to add name and value to nodes in an array of object format.
        //Returns boolean true on success and false on failure.
        public bool CreateNode(object[] nodeData)
        {
            try
            {
                switch (nodeData.Count())
                {
                    case 1:
                        return CreateNode((string)nodeData[0]);
                    case 2:
                        return CreateNode((string)nodeData[0], (TNode)nodeData[1]);
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Internal method to return reference of node from a custom list of nodes based on node name parameter 
        internal Node<TEdge, TNode> GetNode(string nameOfNode, List<Node<TEdge, TNode>> listOfNode)
        {
            return listOfNode.Find(m => m.nameOfNode.Equals(nameOfNode));
        }

        //Overload for internal method to return reference of node from the graph's listofnodes collection based on a node name parameter
        internal Node<TEdge, TNode> GetNode(string nameOfNode)
        {
            return GetNode(nameOfNode, listOfNodes);
        }

        //Method to connect two nodes with an edge. Extra parameters to add value to nodes, weight to edge and to set up bi-directional edges.
        //Returns boolean true on success and false on failure.
        public bool ConnectNodes(string nameOfNodeA, TNode valueOfNodeA, string nameOfNodeB, TNode valueOfNodeB, TEdge weightOfEdge, bool biDirectionalEdge)
        {
            bool connectionSuccessful = false;

            CreateNode(nameOfNodeA, valueOfNodeA);
            Node<TEdge, TNode> nodeA = GetNode(nameOfNodeA);

            CreateNode(nameOfNodeB, valueOfNodeB);
            Node<TEdge, TNode> nodeB = GetNode(nameOfNodeB);

            if (biDirectionalEdge)
            {
                if (!nodeA.IsConnectedTo(nodeB) && !nodeB.IsConnectedTo(nodeB))
                {
                    nodeA.ConnectTo(nodeB, weightOfEdge);
                    nodeB.ConnectTo(nodeA, weightOfEdge);
                    connectionSuccessful = true;
                }
            }
            else
            {
                if (!nodeA.IsConnectedTo(nodeB))
                {
                    nodeA.ConnectTo(nodeB, weightOfEdge);
                    connectionSuccessful = true;
                }
            }

            return connectionSuccessful;
        }

        //Overload for method to connect two nodes with a directional edge. Extra parameter to add weight to edge.
        //Returns boolean true on success and false on failure.
        public bool ConnectNodes(string nameOfNodeA, string nameOfNodeB, TEdge weightOfEdge)
        {
            return ConnectNodes(nameOfNodeA, default(TNode), nameOfNodeB, default(TNode), weightOfEdge, false);
        }

        //Overload for method to connect two nodes with a bi-directional edge. Extra parameter to add weight to edge and set edge mode to bi-directional.
        //Returns boolean true on success and false on failure.
        public bool ConnectNodes(string nameOfNodeA, string nameOfNodeB, TEdge weightOfEdge, bool biDirectionalEdge)
        {
            return ConnectNodes(nameOfNodeA, default(TNode), nameOfNodeB, default(TNode), weightOfEdge, biDirectionalEdge);
        }

        //Overload for method to connect two nodes with a directional edge. Extra parameter to add value to nodes and weight to edge.
        //Returns boolean true on success and false on failure.
        public bool ConnectNodes(string nameOfNodeA, TNode valueOfNodeA, string nameOfNodeB, TNode valueOfNodeB, TEdge weightOfEdge)
        {
            return ConnectNodes(nameOfNodeA, valueOfNodeA, nameOfNodeB, valueOfNodeB, weightOfEdge, false);
        }

        //Overload for method to connect two nodes with an edge. Extra parameters to add value to nodes and weight to an optionally bidirectional edge 
        //in an array of object format.
        //Returns boolean true on success and false on failure.
        public bool ConnectNodes(object[] nodeData)
        {
            try
            {
                switch (nodeData.Count())
                {
                    case 3:
                        return ConnectNodes((string)nodeData[0], (string)nodeData[1], (TEdge)nodeData[2]);
                    case 4:
                        return ConnectNodes((string)nodeData[0], (string)nodeData[1], (TEdge)nodeData[2], (bool)nodeData[3]);
                    case 5:
                        return ConnectNodes((string)nodeData[0], (TNode)nodeData[1], (string)nodeData[2], (TNode)nodeData[3], (TEdge)nodeData[4]);
                    case 6:
                        return ConnectNodes((string)nodeData[0], (TNode)nodeData[1], (string)nodeData[2], (TNode)nodeData[3], (TEdge)nodeData[4], (bool)nodeData[5]);
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Method to populate graph with nodes and edges through an edge list. Accepts a list of object arrays as edges and batch processes them
        //through the ConnectNodes(object[] nodeData) method.
        //Returns boolean true on success and false on failure.
        public bool LoadEdgeList(List<object[]> edgeList)
        {
            bool loadSuccessful = false;

            foreach (var edge in edgeList)
            {
                loadSuccessful = ConnectNodes(edge);

                if (!loadSuccessful)
                {
                    listOfNodes.Clear();
                    break;
                }
            }

            return loadSuccessful;
        }

        //Method to check if all nodes passed by a string array parameter exist in the current graph
        private bool NodesExist(ref string[] nodeNameArray)
        {
            List<string> missingNodes = new List<string>();

            foreach (var nodeName in nodeNameArray)
            {
                if (GetNode(nodeName) == null)
                {
                    missingNodes.Add(nodeName);
                }
            }

            if (missingNodes.Count > 0)
            {
                nodeNameArray = missingNodes.ToArray<string>();
                return false;
            }
            else
            {
                return true;
            }
        }

        //Method to initiate the traversal of the graph. Accepts string array as parameter with list of node names to include in the path search, 
        //boolean variable to note if sorting should be ascending or descending, a SortRoutesBy enum value to decide on the specific route 
        //weighting to be used and a TraverseGraphUsing enum value to select the specific traversal algorithm to be used.
        //Returns a string array with the results.
        public string[] GetRoute(string[] nodeNameArray, bool orderByAscending, SortRoutesBy currentSortingAlgorithm,
            TraverseGraphUsing currentTraversalAlgorithm)
        {
            try
            {
                if (NodesExist(ref nodeNameArray))
                {
                    RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm;
                    TraversalAlgorithmBase<TEdge, TNode> traversalAlgorithm;

                    CreateAlgorithmInstances(currentSortingAlgorithm, currentTraversalAlgorithm, out routeSortingAlgorithm, out traversalAlgorithm);

                    Dictionary<string, dynamic> resultDictionary = IterateThroughInputArray(nodeNameArray, orderByAscending, routeSortingAlgorithm,
                        traversalAlgorithm);

                    return CreateStringArrayFromDictionary(resultDictionary);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < nodeNameArray.Count(); i++)
                    {
                        sb.Append(nodeNameArray[i]);
                        if (i < nodeNameArray.Count() - 1)
                        {
                            sb.Append(", ");
                        }
                    }

                    return new string[] { noDataTypeString + sb.ToString() };
                }
            }
            catch (Exception excp)
            {
                return new string[] { excp.Message };
            }
        }

        //Method to create instances of the route sorting and traversal algorithms using reflection
        private void CreateAlgorithmInstances(SortRoutesBy currentSortingAlgorithm, TraverseGraphUsing currentTraversalAlgorithm,
            out RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm, out TraversalAlgorithmBase<TEdge, TNode> traversalAlgorithm)
        {
            Type[] genericTypeArgs = new Type[] { typeof(TEdge), typeof(TNode) };

            Type routeSortingAlgorithmType = Assembly.GetExecutingAssembly().GetTypes()
                .SingleOrDefault(t => t.Name.Contains("SortRoutesBy" + currentSortingAlgorithm.ToString()));
            Object[] routeSortingAlgorithmArgs = { noRouteExistString, invalidDataTypeString };
            Type routeSortingAlgorithmGenericType = routeSortingAlgorithmType.MakeGenericType(genericTypeArgs);

            routeSortingAlgorithm = (RouteSortingAlgorithmBase<TEdge, TNode>)Activator
                .CreateInstance(routeSortingAlgorithmGenericType, routeSortingAlgorithmArgs);
            Type traversalAlgorithmType = Assembly.GetExecutingAssembly().GetTypes()
                .SingleOrDefault(t => t.Name.Contains(currentTraversalAlgorithm.ToString() + "Algorithm"));
            Object[] traversalAlgorithmArgs = { routeSortingAlgorithm };
            Type traversalAlgorithmGenericType = traversalAlgorithmType.MakeGenericType(genericTypeArgs);

            traversalAlgorithm = (TraversalAlgorithmBase<TEdge, TNode>)Activator
                .CreateInstance(traversalAlgorithmGenericType, traversalAlgorithmArgs);
        }

        //Method to iterate through the input array of nodes and run the traversal for every node pair in consecutive order. 
        //Saves the result of every traversal in a dictionary.
        private Dictionary<string, dynamic> IterateThroughInputArray(string[] nodeNameArray, bool orderByAscending,
            RouteSortingAlgorithmBase<TEdge, TNode> routeSortingAlgorithm, TraversalAlgorithmBase<TEdge, TNode> traversalAlgorithm)
        {
            Node<TEdge, TNode> nodeA;
            Node<TEdge, TNode> nodeB;
            Dictionary<string, dynamic> resultDictionary = new Dictionary<string, dynamic>();

            for (int i = 0; i < nodeNameArray.Count() - 1; i++)
            {
                nodeA = GetNode(nodeNameArray[i]);
                nodeB = GetNode(nodeNameArray[i + 1]);

                traversalAlgorithm.TraverseGraph(nodeA, nodeB);

                dynamic currentResult = routeSortingAlgorithm.GetResult(orderByAscending);

                if (currentResult.Value == null)
                {
                    resultDictionary.Clear();
                    resultDictionary.Add(currentResult.Key, currentResult.Value);
                    break;
                }
                else
                {
                    resultDictionary.Add(currentResult.Key, currentResult.Value);
                }
            }

            return resultDictionary;
        }

        //Method to integrate the results stored in the dictionary into one big path. Returns the result as a string array.
        private string[] CreateStringArrayFromDictionary(Dictionary<string, dynamic> resultDictionary)
        {
            if (resultDictionary.FirstOrDefault().Value == null)
            {
                return new string[] { resultDictionary.FirstOrDefault().Key };
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                dynamic value = 0;

                foreach (var result in resultDictionary)
                {
                    if (sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                    }
                    sb.Append(result.Key);
                    value += result.Value;
                }

                return new string[] { sb.ToString(), value.ToString() };
            }
        }
    }
}
