using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                string[] instanceStrings = AssetDatabase.FindAssets("t:" + typeof(T).Name);
                if (instanceStrings == null || instanceStrings.Length == 0)
                {
                    Debug.LogException(new System.Exception("No instance of singleton " + typeof(T).ToString() + " found but it's required."));
                    UnityEditor.EditorApplication.isPlaying = false;
                    return null;
                }
                if (instanceStrings.Length > 1)
                {
                    Debug.LogException(new System.Exception("Multiple instances of singleton " + typeof(T).ToString() + " created."));
                    UnityEditor.EditorApplication.isPlaying = false;
                    return null;
                }
                else
                {
                    string instancePath = AssetDatabase.GUIDToAssetPath(instanceStrings[0]);
                    instance = AssetDatabase.LoadAssetAtPath<T>(instancePath);
                    return instance;
                }
            }
            return instance;
        }
    }
}
