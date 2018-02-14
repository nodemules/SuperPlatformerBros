using UnityEngine;

public class Singleton<TInstance> : MonoBehaviour where TInstance : Singleton<TInstance>
{
    private static TInstance _instance;
    public bool IsPersistant;

    public void Awake()
    {
        if (IsPersistant)
        {
            if (!_instance)
            {
                _instance = this as TInstance;
            }
            else
            {
                DestroyObject(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            _instance = this as TInstance;
        }
    }
}