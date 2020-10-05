using DungeonSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSelectionUI : MonoBehaviour
{
    public DungeonSelectionButton buttonPrefab;
    public GameObject content;
    public List<DungeonData> dungeonsList = new List<DungeonData>();

    private void Awake()
    {
        dungeonsList.AddRange(Resources.LoadAll<DungeonData>("Dungeons"));
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < dungeonsList.Count; i++)
        {
            DungeonSelectionButton button = Instantiate(buttonPrefab);
            button.SetDungeonData(dungeonsList[i]);
            button.transform.SetParent(content.transform);
        }
    }
}
