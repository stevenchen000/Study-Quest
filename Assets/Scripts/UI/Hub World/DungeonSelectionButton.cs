using DungeonSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSelectionButton : MonoBehaviour
{

    private DungeonData dungeon;
    public TMP_Text dungeonNameText;
    private Button button;
    private DungeonSelectionUI ui;
    public EventSO onDungeonSelect;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(SelectDungeon);
        button.onClick.AddListener(onDungeonSelect.CallEvent);
        ui = FindObjectOfType<DungeonSelectionUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetDungeonData(DungeonData newDungeon)
    {
        dungeon = newDungeon;
        dungeonNameText.text = dungeon.GetDungeonName();
    }

    private void SelectDungeon()
    {
        WorldState.SetDungeonData(dungeon);
    }
}
