using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{

    public static WorldState world;

    public string dungeonName;
    public Vector3 dungeonPosition;

    public Vector3 hubPosition;

    private void Awake()
    {
        if(world == null)
        {
            world = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


    public static string GetDungeonName() { return world.dungeonName; }
    public static void SetDungeonName(string newDungeonName) { world.dungeonName = newDungeonName; }
    public static Vector3 GetDungeonPosition() { return world.dungeonPosition; }
    public static void SetDungeonPosition(Vector3 newDungeonPosition) { world.dungeonPosition = newDungeonPosition; }
    public static Vector3 GetHubPosition() { return world.hubPosition; }
    public static void SetHubPosition(Vector3 position) { world.hubPosition = position; }

}
