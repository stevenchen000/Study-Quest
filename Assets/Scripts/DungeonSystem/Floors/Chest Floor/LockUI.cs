using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockUI : MonoBehaviour
{
    public GameObject lockedIcon;
    public GameObject unlockedIcon;
    public bool isLocked = true;

    // Start is called before the first frame update
    void Start()
    {
        unlockedIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkCorrect()
    {
        isLocked = false;
        unlockedIcon.SetActive(true);
        lockedIcon.SetActive(false);
    }

    public void MarkIncorrect()
    {
        isLocked = true;
    }
}
