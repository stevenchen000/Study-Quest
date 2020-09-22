using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript2 : MonoBehaviour
{

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            text.text = "";
            text.text += WorldState.GetDungeonData().dungeonName;
            text.text += " - ";
            text.text += WorldState.GetDungeonDifficulty().ToString();
        }catch(Exception e)
        {

        }
    }
}
