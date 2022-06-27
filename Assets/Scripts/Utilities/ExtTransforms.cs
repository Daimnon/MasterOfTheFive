using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyimmediatly = false)
    {
        foreach (Transform child in t)
        {
            if (destroyimmediatly)
            {
                MonoBehaviour.DestroyImmediate(child.gameObject);
            }
            else
            {
                MonoBehaviour.Destroy(child.gameObject);
            }
        }
    }
}
