using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPanel : MonoBehaviour
{
    public int numOfLocks = 3;
    public LockUI lockPrefab;
    public List<LockUI> locks = new List<LockUI>();
    private int currLock = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numOfLocks; i++)
        {
            LockUI obj = Instantiate(lockPrefab);
            obj.transform.SetParent(transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            locks.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnswerCorrectly()
    {
        locks[currLock].MarkCorrect();
        currLock++;
    }

    public void AnswerIncorrectly()
    {
        currLock++;
    }

    public bool IsUnlocked()
    {
        int wrong = 0;

        for (int i = 0; i < locks.Count; i++)
        {
            if (locks[i].isLocked)
            {
                wrong++;
            }
        }

        return wrong <= (locks.Count/2);
    }

    public bool IsFinished()
    {
        return currLock == locks.Count;
    }
}
