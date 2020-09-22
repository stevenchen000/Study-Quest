using QuizSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DungeonSystem
{
    public enum DungeonDifficulty
    {
        Easy,
        Medium,
        Hard
    }
    [CreateAssetMenu(menuName = "Dungeon Data")]
    public class DungeonData : ScriptableObject
    {
        public string dungeonName;
        public List<DungeonFloorData> floors = new List<DungeonFloorData>();
        public QuestionSheet easyQuestions;
        public QuestionSheet mediumQuestions;
        public QuestionSheet hardQuestions;

        public QuestionSheet GetQuestionSheet(DungeonDifficulty difficulty)
        {
            QuestionSheet result = easyQuestions;

            switch (difficulty)
            {
                case DungeonDifficulty.Easy:
                    result = easyQuestions;
                    break;
                case DungeonDifficulty.Medium:
                    result = mediumQuestions;
                    break;
                case DungeonDifficulty.Hard:
                    result = hardQuestions;
                    break;
            }

            return result;
        }

        public string GetDungeonName()
        {
            return dungeonName;
        }
    }
}