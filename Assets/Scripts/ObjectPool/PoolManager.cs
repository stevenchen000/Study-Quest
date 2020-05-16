using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager pool;
    private ObjectPool _pool = new ObjectPool();
    public List<GameObject> objectsToAdd = new List<GameObject>();

    public void Awake()
    {
        if (pool == null)
        {
            pool = this;
        }
        else {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < objectsToAdd.Count; i++)
        {
            _pool.AddObject(objectsToAdd[i]);
        }
    }

    public GameObject PullObject(int index)
    {
        return _pool.PullObject(objectsToAdd[index]);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
