using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.DictionaryExtensions
{
    public static partial class DictionaryExtensions
    {
        public static int TotalCount(this Dictionary<string, object> dictionary)
        {
            int count = 0;
            foreach (KeyValuePair<string, object> element in dictionary)
                count += ((element.Value as Dictionary<string, object>).Count);

            return count;
        }

        public static int GetIndexInSubDictionary(this Dictionary<string, object> dictionary, string key, string subkey)
        {
            int count = 0;
            foreach (KeyValuePair<string, object> element in dictionary)
                foreach (KeyValuePair<string, object> subelement in element.Value as Dictionary<string, object>)
                {
                    if (element.Key == key && subelement.Key == subkey)
                        return count;

                    count++;
                }

            return count;
        }

        public static T GetData<T>(string[] keys, Dictionary<string, object> dictionary, object defaultValue)
        {
            defaultValue = defaultValue == null ? default(T) : defaultValue;

            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (!dictionary.ContainsKey(keys[i]))
                    return (T)CastObject(defaultValue);

                if (dictionary[keys[i]].GetType() != typeof(Dictionary<string, object>))
                    return (T)CastObject(defaultValue);

                dictionary = dictionary[keys[i]] as Dictionary<string, object>;
            }

            if (!dictionary.ContainsKey(keys.Last()))
                return (T)CastObject(defaultValue);

            return (T)CastObject(dictionary[keys.Last()]);
        }

        private static List<T> GetDataList<T>(string[] keys, Dictionary<string, object> dictionary, object defaultValue)
        {
            var list = GetData<object>(keys, dictionary, defaultValue);
            return list.GetType() == typeof(List<T>) ? (List<T>)list : ConvertToList<T>(list);
        }

        private static List<T> ConvertToList<T>(object originalList)
        {
            var newList = new List<T>();
            foreach (object element in (List<object>)originalList)
                newList.Add((T)Convert.ChangeType(element, typeof(T)));

            return newList;
        }

        public static Dictionary<string, object> GetDataFromDictionary(string[] keys, Dictionary<string, object> dictionary)
        {
            return GetData<Dictionary<string, object>>(keys, dictionary, new Dictionary<string, object>());
        }

        public static List<string> GetStringListFromDictionary(string[] keys, Dictionary<string, object> dictionary)
        {
            return GetDataList<string>(keys, dictionary, new Dictionary<string, object>());
        }

        public static object CastObject(object data)
        {
            if (data.GetType() == typeof(double))
                data = Convert.ToSingle(data);

            if (data.GetType() == typeof(Int64))
                data = Convert.ToInt32(data);

            return data;
        }
    }
}
