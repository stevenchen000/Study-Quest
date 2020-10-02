using DungeonSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{

    public Text text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EventCaller caller = transform.GetComponent<EventCaller>();
            caller.CallEvent();
        }
    }

    public void Test()
    {
        Debug.Log("Testing");
    }
}
