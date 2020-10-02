using DungeonSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSelectionUI : MonoBehaviour
{
    public List<DungeonData> dungeonsList = new List<DungeonData>();

    private void Awake()
    {
        dungeonsList.AddRange(Resources.LoadAll<DungeonData>("Dungeons"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
