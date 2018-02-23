using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public static class GameObjectUtil<T>
    {
        public static List<T> GetGameObjectsInScene()
        {
            return GetGameObjectsInScene(SceneManager.GetActiveScene());
        }

        public static List<T> GetGameObjectsInScene(Scene scene)
        {
            List<T> list = new List<T>();
            foreach (GameObject gameObject in scene.GetRootGameObjects())
            {
                list.AddRange(gameObject.GetComponentsInChildren<T>());
            }

            return list;
        }
        
    }
}