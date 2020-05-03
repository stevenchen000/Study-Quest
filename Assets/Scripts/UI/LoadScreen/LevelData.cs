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
        public string name;
        public string levelName;
        public QuestionSheet sheet;
    }
}
