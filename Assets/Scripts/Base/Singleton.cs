using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    private static T _instance;

    private void Awake()
    {
        _instance = this as T;
    }
}
