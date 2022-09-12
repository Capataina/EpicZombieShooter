using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            T[] instances = Resources.FindObjectsOfTypeAll<T>();
            if (instances == null || instances.Length == 0)
            {
                Debug.LogException(new System.Exception("No instance of singleton " + typeof(T).ToString() + " found but it's required."));
                UnityEditor.EditorApplication.isPlaying = false;
                return null;
            }
            if (instances.Length > 1)
            {
                Debug.LogException(new System.Exception("Multiple instances of singleton " + typeof(T).ToString() + " created."));
                UnityEditor.EditorApplication.isPlaying = false;
                return null;
            }
            else
            {
                return instances[0];
            }
        }
    }
}
