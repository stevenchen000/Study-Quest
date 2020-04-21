using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QuizSystem
{
    [CreateAssetMenu(menuName = "Question Sheet", fileName = "Question Sheet")]
    public class QuestionSheet : ScriptableObject
    {
        public List<Question> questions;
        public List<bool> foldouts;

        public List<Question> GetQuestions() { return questions; }
        public int GetNumberOfQuestion() { return questions.Count; }
        public Question GetQuestionAt(int index) { return questions[index]; }
    }
}
