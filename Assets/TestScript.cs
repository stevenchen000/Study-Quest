using DungeonSystem;
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
        DungeonManager dungeon = FindObjectOfType<DungeonManager>();
        string dungeonName = dungeon.data.floors.Count.ToString();
        text.text = dungeonName;
    }
}
