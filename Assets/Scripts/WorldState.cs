using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{

    public static WorldState world;

    public string dungeonName = "Test_Dungeon";
    public Vector3 dungeonPosition = new Vector3(0, 0, 0);

    public string hubName = "Test_Hub_World";
    public Vector3 hubPosition;

    public string combatName = "Test_Solo_Combat";
    public int currentFloor = 1;
    public int newFloor = 1;
    public CharacterData characterData;
    public CharacterData playerData;

    public QuestionSheet questions;

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

    private void Update()
    {
        
    }


    public static string GetDungeonName() { return world.dungeonName; }
    public static void SetDungeonName(string newDungeonName) { world.dungeonName = newDungeonName; }
    public static Vector3 GetDungeonPosition() { return world.dungeonPosition; }
    public static void SetDungeonPosition(Vector3 newDungeonPosition) { world.dungeonPosition = newDungeonPosition; }

    public static string GetHubName() { return world.hubName; }
    public static Vector3 GetHubPosition() { return world.hubPosition; }
    public static void SetHubPosition(Vector3 position) { world.hubPosition = position; }

    public static string GetCombatName() { return world.combatName; }

    public static CharacterData GetCharacterData() { return world.characterData; }
    public static void SetCharacterData(CharacterData newData) { world.characterData = newData; }
    public static CharacterData GetPlayerData() { return world.playerData; }
    public static void SetPlayerData(CharacterData newData) { world.playerData = newData; }

    public static QuestionSheet GetQuestionSheet() { return world.questions; }
    public static void SetQuestionSheet(QuestionSheet newQuestions) { world.questions = newQuestions; }

    public static int GetCurrentFloorNumber() { return world.currentFloor; }
    public static void SetCurrentFloorNumber(int floor) { world.currentFloor = floor; }
    public static void IncrementFloor() { world.currentFloor++; }
    public static void ResetFloorNumber() { world.currentFloor = 1; }
}
