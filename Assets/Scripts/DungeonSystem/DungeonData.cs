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

    [Serializable]
    public class DungeonFloorChance
    {
        public DungeonFloorData data;
        [Range(0,1)]
        public float chance;
    }


    [CreateAssetMenu(menuName = "Dungeon Data")]
    public class DungeonData : ScriptableObject
    {
        public string dungeonName;
        public List<DungeonFloorChance> chances = new List<DungeonFloorChance>();
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


        public DungeonFloorData GetRandomFloor()
        {
            DungeonFloorData floor = chances[0].data;
            
            float rand = UnityEngine.Random.Range(0f, 1f);
            float totalChance = 0;
            for(int i = 0; i < chances.Count; i++)
            {
                totalChance += chances[i].chance;

                if (rand < totalChance)
                {
                    floor = chances[i].data;
                    break;
                }
            }

            return floor;
        }
    }
}