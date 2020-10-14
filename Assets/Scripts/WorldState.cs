using DialogueSystem;
using DungeonSystem;
using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldState : MonoBehaviour
{

    public static WorldState world;

    public string dungeonLevelName = "Test_Dungeon_Board";
    public Vector3 dungeonPosition = new Vector3(0, 0, 0);

    public string hubName = "LoadScreen";
    public Vector3 hubPosition;
    
    public int currentFloor = 1;
    public int newFloor = 1;
    public CharacterData enemyData;
    public CharacterData playerData;

    public DungeonData dungeonData;
    public DungeonDifficulty dungeonDifficulty;

    public QuestionSheet questions;

    private DialogueUI dialogueUI;

    private void Awake()
    {
        if(world == null)
        {
            world = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        
    }
    

    public static string GetDungeonName() { return world.dungeonLevelName; }
    public static void SetDungeonName(string newDungeonName) { world.dungeonLevelName = newDungeonName; }
    public static Vector3 GetDungeonPosition() { return world.dungeonPosition; }
    public static void SetDungeonPosition(Vector3 newDungeonPosition) { world.dungeonPosition = newDungeonPosition; }

    public static string GetHubName() { return world.hubName; }
    public static Vector3 GetHubPosition() { return world.hubPosition; }
    public static void SetHubPosition(Vector3 position) { world.hubPosition = position; }

    //public static string GetCombatName() { return world.combatName; }

    public static CharacterData GetCharacterData() { return world.enemyData; }
    public static void SetCharacterData(CharacterData newData) { world.enemyData = newData; }
    public static CharacterData GetPlayerData() { return world.playerData; }
    public static void SetPlayerData(CharacterData newData) { world.playerData = newData; }

    public static void SetQuestionSheet(QuestionSheet newQuestions) { world.questions = newQuestions; }
    public static QuestionSheet GetQuestionSheet() { return world.questions; }

    public static int GetCurrentFloorNumber() { return world.currentFloor; }
    public static void SetCurrentFloorNumber(int floor) { world.currentFloor = floor; }
    public static void IncrementFloor() { world.currentFloor++; }
    public static void ResetFloorNumber() { world.currentFloor = 1; }

    public static DungeonData GetDungeonData() { return world.dungeonData; }
    public static void SetDungeonData(DungeonData newDungeonData) { world.dungeonData = newDungeonData; }
    public static DungeonDifficulty GetDungeonDifficulty() { return world.dungeonDifficulty; }
    public static void SetDungeonDifficulty(DungeonDifficulty difficulty) { world.dungeonDifficulty = difficulty; }

    public static void SetDialogue(DialogueTree dialogue) { world.dialogueUI.SetDialogue(dialogue); }
}
