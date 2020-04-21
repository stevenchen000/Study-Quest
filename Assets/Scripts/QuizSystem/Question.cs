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
        public string[] wrongChoices;
        public Solution solution;
        public bool includesAllOfTheAbove = false;

        public Question() {
            wrongChoices = new string[3];
            solution = new Solution();
        }



        public bool CheckSolution(string answer) {
            return solution.CheckSolution(answer);
        }

        public string GetSolution()
        {
            return solution.GetSolution();
        }

        public void SetQuestionType(QuestionType newType) {
            type = newType;
            solution.type = newType;
        }


        public string[] GetAllChoices() {
            string[] choices = null;

            switch (type)
            {
                case QuestionType.TrueFalse:
                    choices = new string[2] { "true", "false" };
                    break;
                case QuestionType.MultipleChoice:
                    choices = GetScrambledChoices();
                    break;
                case QuestionType.FillInTheBlank:
                    break;
            }

            return choices;
        }

        private string[] GetScrambledChoices() {
            string[] choices = GetUnscrambledChoices();

            for (int i = 0; i < choices.Length; i++) {
                int rand = UnityEngine.Random.Range(0, choices.Length);
                string temp = choices[i];
                choices[i] = choices[rand];
                choices[rand] = temp;
            }

            return choices;
        }

        private string[] GetUnscrambledChoices() {
            return new string[4] { wrongChoices[0], wrongChoices[1], wrongChoices[2], solution.GetSolution() };
        }
    }
}
