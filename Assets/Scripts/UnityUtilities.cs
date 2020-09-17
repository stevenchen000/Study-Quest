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
        Transform child = null;

        try
        {
            obj.GetChild(0);
        }
        catch (Exception e)
        {

        }

        while (child != null)
        {
            child.transform.SetParent(null);
            child.gameObject.SetActive(false);
            child = obj.GetChild(0);
        }
    }
}
