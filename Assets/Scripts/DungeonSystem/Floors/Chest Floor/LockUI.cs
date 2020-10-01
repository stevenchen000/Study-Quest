using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockUI : MonoBehaviour
{
    public GameObject incorrectIcon;
    public GameObject correctIcon;
    public bool isCorrect = false;

    // Start is called before the first frame update
    void Start()
    {
        correctIcon.SetActive(false);
        incorrectIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkCorrect()
    {
        isCorrect = true;
        correctIcon.SetActive(true);
    }

    public void MarkIncorrect()
    {
        isCorrect = false;
        incorrectIcon.SetActive(true);
    }
    
}
