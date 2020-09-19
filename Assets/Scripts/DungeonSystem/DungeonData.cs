using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DungeonFloorData
{
    public string floorName;
    public Vector3 startingPosition;
}

[CreateAssetMenu(menuName = "Dungeon Data")]
public class DungeonData : ScriptableObject
{
    public List<string> floorNames = new List<string>();

    public bool IsLastFloor(int currentFloor)
    {
        return currentFloor == (floorNames.Count - 1);
    }

    public Vector3 GetStartingPoint(int currentFloor)
    {
        return new Vector3();
    }
}
