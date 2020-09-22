using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSystem
{
    [CreateAssetMenu(menuName = "Dungeon Floor")]
    public class DungeonFloorData : ScriptableObject
    {
        public string levelName;
        public GameObject symbol;
        public Color color;

        protected virtual void _SetupDungeon()
        {

        }

        public void LoadLevel()
        {
            _SetupDungeon();
            UnityUtilities.LoadLevelAdditive(levelName);
        }

        public void UnloadLevel()
        {
            UnityUtilities.UnloadLevel(levelName);
        }
    }
}