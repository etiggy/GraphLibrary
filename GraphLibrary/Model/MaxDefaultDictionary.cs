using System.Collections.Generic;
using System.Reflection;

namespace GraphLibrary
{
    //Custom implementation of a dictionary where the default value is set to the MaxValue property of the Value's type instead of null
    internal class MaxDefaultDictionary<TKey, TValue>
    {
        //Reference variable for the inner dictionary
        internal Dictionary<TKey, TValue> dictionary;

        //Default constructor
        public MaxDefaultDictionary()
        {
            dictionary = new Dictionary<TKey, TValue>();
        }

        //Iterator property to implement the default null value override to MaxValue. Items in the dictionary need to be accessed through iterators
        //for the override to work.  
        public TValue this[TKey key]
        {
            get
            {
                if (dictionary.ContainsKey(key))
                {
                    return dictionary[key];
                }
                else
                {
                    FieldInfo field = typeof(TValue).GetField("MaxValue", BindingFlags.Public | BindingFlags.Static);
                    return (TValue)field.GetValue(null);
                }
            }
            set
            {
                dictionary[key] = value;
            }
        }
    }
}
