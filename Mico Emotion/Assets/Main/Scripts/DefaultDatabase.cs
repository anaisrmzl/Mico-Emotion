using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using Utilities.Data;

namespace Emotion.Data
{
    public class DefaultDatabase : MonoBehaviour
    {
        #region FIELDS

        private const string Path = "defaultbadges";

        #endregion

        #region PROPERTIES

        public Dictionary<string, object> DefaultBadges { get => LoadDictionaryFromStreamingAssets(APIKeys.BadgesKey); }

        #endregion

        #region  BEHAVIORS

        private Dictionary<string, object> LoadDictionaryFromStreamingAssets(string key)
        {
            TextAsset json = Resources.Load<TextAsset>(Path);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json.text, new GenericConverter());
        }
    }

    #endregion
}
