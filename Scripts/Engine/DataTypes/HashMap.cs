using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.game
{
    public class HashMap<TKey, TValue>
    {
        //Check this out! http://javaexplorer03.blogspot.com/2015/10/create-own-hashmap.html
        // A class to store a key-value pair
        private class KeyValuePair
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
        }

        // A list to store the key-value pairs
        private List<KeyValuePair> _data = new List<KeyValuePair>();

        // Add a key-value pair to the hashmap
        public void Put(TKey key, TValue value)
        {
            // Check if the key already exists in the hashmap
            foreach (var item in _data)
            {
                if (item.Key.Equals(key))
                {
                    // The key already exists, so update the value
                    item.Value = value;
                    return;
                }
            }

            // The key does not exist, so add it to the hashmap
            _data.Add(new KeyValuePair { Key = key, Value = value });
        }

        // Get a value from the hashmap using its key
        public TValue Get(TKey key)
        {
            // Check if the key exists in the hashmap
            foreach (var item in _data)
            {
                if (item.Key.Equals(key))
                {
                    // The key exists, so return the corresponding value
                    return item.Value;
                }
            }

            // The key does not exist, so return the default value for the type
            return default(TValue);
        }

        // Remove a key-value pair from the hashmap using its key
        public void Remove(TKey key)
        {
            // Check if the key exists in the hashmap
            for (int i = 0; i < _data.Count; i++)
            {
                if (_data[i].Key.Equals(key))
                {
                    // The key exists, so remove it from the hashmap
                    _data.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
