using DungeonSystem;
using QuizSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadScreen
{
    [System.Serializable]
    public class LevelData
    {
        public string levelName = "Test_Dungeon_Board";
        public DungeonData dungeon;
    }
}
