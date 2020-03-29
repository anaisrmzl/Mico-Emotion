using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Utilities.Data
{
    public class UserData
    {
        #region FIELDS

        private const string DataKey = "data";

        #endregion

        #region PROPERTIES

        [JsonProperty(DataKey)] public Dictionary<string, object> Data { get; private set; } = null;

        #endregion

        #region CONSTRUCTORS

        public UserData() : this(new Dictionary<string, object>()) { }

        [JsonConstructor]
        public UserData(Dictionary<string, object> data)
        {
            Data = data;
        }

        #endregion

        #region BEHAVIORS

        internal void SetData<T>(string[] keys, T data)
        {
            SetValueOnDictionary(keys, data);
        }

        internal void MergeData(Dictionary<string, object> newDictionary)
        {
            MergeWithDataDictionary(newDictionary, new List<string>());
        }

        private void MergeWithDataDictionary(object newElement, List<string> keys)
        {
            if (newElement.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (KeyValuePair<string, object> element in (Dictionary<string, object>)newElement)
                {
                    var newKeys = new List<string>(keys);
                    newKeys.Add(element.Key);
                    MergeWithDataDictionary(element.Value, newKeys);
                }
            }
            else
            {
                SetValueOnDictionary(keys.ToArray(), newElement);
            }
        }

        private void SetValueOnDictionary(string[] keys, object newData)
        {
            var data = Data;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (!data.ContainsKey(keys[i]))
                    data.Add(keys[i], new Dictionary<string, object>());

                data = (Dictionary<string, object>)data[keys[i]];
            }

            if (data.ContainsKey(keys.Last()))
                data[keys.Last()] = newData;
            else
                data.Add(keys.Last(), newData);
        }

        internal List<T> GetDataList<T>(string[] keys, object defaultValue)
        {
            var list = GetData<object>(keys, defaultValue);
            if (list.GetType() == typeof(List<T>))
                return (List<T>)list;
            else
                return ConvertToList<T>(list);
        }

        internal T GetData<T>(string[] keys, object defaultValue)
        {
            Dictionary<string, object> dictionary = Data;
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

        private List<T> ConvertToList<T>(object originalList)
        {
            var newList = new List<T>();
            foreach (object element in (List<object>)originalList)
                newList.Add((T)Convert.ChangeType(element, typeof(T)));

            return newList;
        }

        private object CastObject(object data)
        {
            if (data.GetType() == typeof(double))
                data = Convert.ToSingle(data);

            if (data.GetType() == typeof(Int64))
                data = Convert.ToInt32(data);

            return data;
        }

        #endregion
    }
}
