using UnityEngine;

using Zenject;

namespace Utilities.Zenject
{
    public static class ZenjectUtilities
    {
        #region FIELDS

        private const string ZenjectSceneContextName = "SceneContext";

        private static DiContainer container;

        #endregion

        #region PROPERTIES

        public static DiContainer Container
        {
            get
            {
                if (container == null)
                {
                    if (GameObject.Find(ZenjectSceneContextName))
                        return container = GameObject.Find(ZenjectSceneContextName).GetComponent<SceneContext>().Container;

                    GameObject newZenjectGameObject = new GameObject();
                    newZenjectGameObject.name = ZenjectSceneContextName;
                    return newZenjectGameObject.AddComponent<SceneContext>().Container;
                }
                else
                {
                    return container = GameObject.Find(ZenjectSceneContextName).GetComponent<SceneContext>().Container;
                }
            }
        }

        #endregion

        #region BEHAVIORS

        public static T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : UnityEngine.Object
        {
            GameObject newGameObject = Container.InstantiatePrefab(prefab, position, rotation, parent);
            newGameObject.transform.localScale = Vector3.one;
            return newGameObject.GetComponent<T>();
        }

        #endregion
    }
}
