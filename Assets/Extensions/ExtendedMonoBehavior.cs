using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Extensions
{
    public class ExtendedMonoBehavior : MonoBehaviour
    {
        protected T[] GetComponentsInChildrenOnly<T>()
        {
            List<T> list = new List<T>();
            GetComponentsInChildren(list);
            foreach (T t in list)
            {
                if (t.GetHashCode() == GetHashCode())
                {
                    list.Remove(t);
                    break;
                }
            }

            return list.ToArray();
        }
        protected void Wait(float seconds, Action action)
        {
            StartCoroutine(_wait(seconds, action));
        }

        private static IEnumerator _wait(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback.Invoke();
        }
    }
}