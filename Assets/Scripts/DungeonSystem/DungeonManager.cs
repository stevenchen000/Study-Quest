using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonSystem
{
    public class DungeonManager : MonoBehaviour
    {
        public PlayerExplorer player;
        public int currentFloor = 1;

        public DungeonData data;
        public Vector3 playerLocation;
        //list of monsters in area


        public void Start()
        {
            player = FindObjectOfType<PlayerExplorer>();
            player.transform.position = WorldState.GetDungeonPosition();
        }


        public void Test()
        {
            
        }

    }
}