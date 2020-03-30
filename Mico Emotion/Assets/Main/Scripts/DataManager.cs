using UnityEngine;
using System.Collections.Generic;

using Newtonsoft.Json;
using Zenject;

using Emotion;
using Emotion.Data;

namespace Utilities.Data
{
    public class DataManager : MonoBehaviour
    {
        #region FIELDS

        public const string UserDataKey = "UserData";

        [Inject] private DefaultDatabase defaultDatabase;

        #endregion

        #region PROPERTIES

        public UserData User { get; private set; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            LoadLocalData();
        }

        private void LoadLocalData()
        {
            User = JsonConvert.DeserializeObject<UserData>(PlayerPrefs.GetString(UserDataKey), new GenericConverter());

            if (User == null)
                InitializeNewUser();

            SaveLocalData();
        }

        private void InitializeNewUser()
        {
            User = new UserData();
            SetData<Dictionary<string, object>>(GenerateKeys(APIKeys.Badges), defaultDatabase.DefaultBadges);
        }

        public void SaveLocalData()
        {
            PlayerPrefs.SetString(UserDataKey, SerializeUser(User));
        }

        public void DeleteUserData()
        {
            PlayerPrefs.DeleteKey(UserDataKey);
        }

        private string SerializeUser(object user)
        {
            return JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public void SetData<T>(string[] keys, T data)
        {
            User.SetData(keys, data);
            SaveLocalData();
        }

        public void MergeData(Dictionary<string, object> dataDictionary)
        {
            User.MergeData(dataDictionary);
            SaveLocalData();
        }

        public List<T> GetDataList<T>(string[] keys, object defaultValue)
        {
            return User.GetDataList<T>(keys, defaultValue);
        }

        public T GetData<T>(string[] keys, object defaultValue = null)
        {
            return User.GetData<T>(keys, defaultValue);
        }

        public static void ResetKey(string[] keys, object newValue)
        {
            DataManager dataManager = new DataManager();
            dataManager.LoadLocalData();
            dataManager.SetData<object>(keys, newValue);
        }

        public static string[] GenerateKeys(params string[] keys)
        {
            return keys;
        }

        public static string[] GenerateKeys(string[] prefixKeys, params string[] keys)
        {
            List<string> keyList = new List<string>(prefixKeys);
            keyList.AddRange(keys);
            return keyList.ToArray();
        }

        #endregion
    }
}
