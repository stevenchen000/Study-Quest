using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QuizSystem
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public QuestionType type;
        [Tooltip("List of wrong answers to use for multiple choice")]
        public string answer;
        public List<string> wrongChoices = new List<string>();
        //public Solution solution;
        public bool includesAllOfTheAbove = false;

        public Question() {

        }

        public Question(string newQuestion, string newAnswer)
        {
            question = newQuestion;
            answer = newAnswer;
        }



        public bool CheckAnswer(string answer) {
            return string.Compare(answer, this.answer, true) == 0;
        }

        public string GetAnswer()
        {
            return answer;
        }

        public int GetSolutionIndex(List<string> choices) {
            int index = -1;

            for (int i = 0; i < choices.Count; i++) {
                if (CheckAnswer(choices[i])) {
                    index = i;
                    break;
                }
            }

            return index;
        }
        

        public void SetQuestionType(QuestionType newType) {
            type = newType;
        }

        public void AddWrongChoice(string text)
        {
            if (QuestionDoesNotContainChoice(text))
            {
                wrongChoices.Add(text);
            }
        }

        private bool QuestionDoesNotContainChoice(string text)
        {
            return !wrongChoices.Contains(text) && answer != text;
        }

        public int GetNumberOfWrongChoices()
        {
            return wrongChoices.Count;
        }


        public List<string> GetAllChoices() {
            List<string> choices = new List<string>();

            switch (type)
            {
                case QuestionType.TrueFalse:
                    choices.Add("true");
                    choices.Add("false");
                    break;
                case QuestionType.MultipleChoice:
                    choices = GetScrambledChoices();
                    break;
                case QuestionType.FillInTheBlank:
                    break;
            }

            return choices;
        }

        private List<string> GetScrambledChoices() {
            List<string> choices = GetUnscrambledChoices();
            
            for (int i = 0; i < choices.Count; i++) {
                int rand = UnityEngine.Random.Range(0, choices.Count);
                string temp = choices[i];
                choices[i] = choices[rand];
                choices[rand] = temp;
            }

            return choices;
        }

        private List<string> GetUnscrambledChoices() {
            List<string> result = new List<string>();
            result.AddRange(wrongChoices);
            result.Add(answer);

            return result;
        }
    }
}
