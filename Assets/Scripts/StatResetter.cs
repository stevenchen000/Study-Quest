using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatResetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterData playerData = WorldState.GetPlayerData();
        playerData.currentHealth = playerData.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
