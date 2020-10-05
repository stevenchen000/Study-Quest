using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityUtilities
{
    

    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Loads a scene onto the scene
    /// </summary>
    /// <param name="levelName"></param>
    public static void LoadLevelAdditive(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Loads a scene and applies an offset too all objects in it
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="offset"></param>
    public static void LoadSceneAdditive(string levelName, Vector3 offset)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        GameObject[] objs = scene.GetRootGameObjects();

        foreach(GameObject go in objs)
        {
            go.transform.position = go.transform.position + offset;
        }
    }

    /// <summary>
    /// Deletes a scene that has been loaded
    /// </summary>
    /// <param name="levelName"></param>
    public static void UnloadLevel(string levelName)
    {
        SceneManager.UnloadSceneAsync(levelName);
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


    public static string ReadFile(string filename)
    {
        FileStream file = File.OpenRead(filename);
        StreamReader reader = new StreamReader(file);
        string result = "";

        string line;
        while((line = reader.ReadLine()) != null){
            result += $"{line}\n";
        }

        return result.Trim();
    }
}
