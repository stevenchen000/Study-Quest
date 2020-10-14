using DialogueSystem;
using DungeonSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public DialogueTree tree;
    public DialogueUI ui;

    private void Start()
    {
        ui = FindObjectOfType<DialogueUI>();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ui.SetDialogue(tree);
        }
    }

    public void Test()
    {
        
    }
}
