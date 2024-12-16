using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static private T instance;
    static public T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
        }
    }
}
