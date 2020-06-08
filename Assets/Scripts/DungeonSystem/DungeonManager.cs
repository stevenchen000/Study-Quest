using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSystem
{
    public class DungeonManager : MonoBehaviour
    {
        public PlayerExplorer player;

        public void Start()
        {
            player = FindObjectOfType<PlayerExplorer>();
            player.transform.position = WorldState.GetDungeonPosition();
        }




    }
}