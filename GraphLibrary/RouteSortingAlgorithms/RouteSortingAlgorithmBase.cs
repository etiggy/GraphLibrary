using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    //Base abstract class for sorting algorithms
    public abstract class RouteSortingAlgorithmBase<TEdge, TNode>
    {
        //Properties to store error messages
        protected string noRouteExistString { get; set; }
        protected string invalidDataTypeString { get; set; }

        //KeyValue pair properties declared as dynamic to store minimum and maximum result for route weighting methods  
        protected dynamic minValue { get; set; }
        protected dynamic maxValue { get; set; }

        //Boolean property to store result of arithmetic test
        protected bool IsValidType { get; set; }

        //Base constructor to set error messages
        public RouteSortingAlgorithmBase(string noRouteExistString, string invalidDataTypeString)
        {
            this.noRouteExistString = noRouteExistString;
            this.invalidDataTypeString = invalidDataTypeString;
        }

        //Method to run basic arithmetics tests on generics to see if operators <, > and += are defined
        protected bool TypeArithmeticTest<T>()
        {
            try
            {
                T testValue1 = default(T);
                dynamic testValue2 = default(T);

                bool comparison = (testValue1 < testValue2) && (testValue1 > testValue2);
                testValue1 += testValue2;

                return true;
            }
            catch (Exception)
            {
                minValue = maxValue = new KeyValuePair<string, object>(invalidDataTypeString, null);
                return false;
            }
        }

        //Helper method to compare a value in a KeyValue pair to previous min and max results and save it in case it is smaller than min or 
        //bigger than max 
        protected void SetMinAndMaxValue<T>(string route, dynamic value)
        {
            if (value < minValue.Value || minValue.Value == default(T))
            {
                minValue = new KeyValuePair<string, T>(route, value);
            }

            if (value > maxValue.Value || maxValue.Value == default(T))
            {
                maxValue = new KeyValuePair<string, T>(route, value);
            }
        }

        //Method to return the result of the sorting based on a boolean value
        internal dynamic GetResult(bool orderByAscending)
        {
            return orderByAscending ? minValue : maxValue;
        }

        //Abstract method to calculate the value of the route passed as a parameter
        internal abstract void CalculateValue(List<Node<TEdge, TNode>> currentRoute);
    }
}
