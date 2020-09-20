using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityUtilities
{
    

    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public static void RemoveChildren(Transform obj)
    {
        Transform child = GetFirstChild(obj);

        while (child != null)
        {
            child.transform.SetParent(null);
            child.gameObject.SetActive(false);
            child = GetFirstChild(obj);
        }
    }

    public static Transform GetFirstChild(Transform obj)
    {
        Transform child = null;
        try
        {
            child = obj.GetChild(0);
        }
        catch (Exception e)
        {

        }

        return child;
    }
}
