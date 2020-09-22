using DungeonSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{

    public DungeonDifficulty difficulty;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponentInChildren<Text>();
        text.text = difficulty.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDifficulty()
    {
        WorldState.SetDungeonDifficulty(difficulty);
    }
}
