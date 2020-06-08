using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{


    public static void LoadDungeon(string dungeonName)
    {
        WorldState.SetDungeonName(dungeonName);
        WorldState.SetDungeonPosition(new UnityEngine.Vector3(0, 0, 0));
        SceneManager.LoadScene(dungeonName);
    }

    public static void LoadDungeon(string dungeonName, Vector3 position)
    {
        WorldState.SetDungeonName(dungeonName);
        WorldState.SetDungeonPosition(position);
        SceneManager.LoadScene(dungeonName);
    }

    public static void LoadCombat()
    {
        SceneManager.LoadScene("Test_Solo_Combat");
    }

    public static void SaveDungeonPosition(Vector3 position)
    {
        WorldState.SetDungeonPosition(position);
    }
}
