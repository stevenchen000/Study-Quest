﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool pool;
    private Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();
    
    public void Awake()
    {
        InitPool();
    }


    public static GameObject Instantiate(GameObject go)
    {
        return pool.PullObject(go);
    }

    public static T Instantiate<T>(T go) where T : Component
    {
        GameObject tempObj = pool.PullObject(go.gameObject);
        return tempObj.GetComponent<T>();
    }






    /// <summary>
    /// Adds the object into the pool
    /// </summary>
    /// <param name="go"></param>
    /// <param name="amount"></param>
    public void AddObject(GameObject go, int amount = 1) {
        if (!_pool.ContainsKey(go)) {
            _pool.Add(go, new List<GameObject>(amount));
        }

        for (int i = 0; i < amount; i++) {
            GameObject tempObj = GameObject.Instantiate(go);
            tempObj.SetActive(false);
            _pool[go].Add(tempObj);
        }
    }


    /// <summary>
    /// Grabs the object from the pool if it exists, otherwise instantiate a new object to add into the list
    /// Sets the object to active
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public GameObject PullObject(GameObject go) {
        if (!_pool.ContainsKey(go)) {
            AddObject(go);
        }

        GameObject result = null;

        List<GameObject> objList = _pool[go];

        for (int i = 0; i < objList.Count; i++) {
            if (!objList[i].activeInHierarchy) {
                result = objList[i];
                break;
            }
        }

        if (result == null) {
            AddObject(go, 3);
            result = objList[objList.Count - 1];
        }

        result.SetActive(true);

        return result;
    }



    private void InitPool() {
        if(pool == null)
        {
            pool = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
