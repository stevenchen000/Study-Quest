using DungeonSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonSelectionButton : MonoBehaviour
{

    private DungeonData dungeon;
    public TMP_Text dungeonNameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetDungeonData(DungeonData newDungeon)
    {
        dungeon = newDungeon;
    }
}
