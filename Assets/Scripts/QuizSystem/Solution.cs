using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSystem
{
    [System.Serializable]
    public class Solution
    {
        public QuestionType type;
        public string solution;
        public bool isTrue;

        public string GetSolution() {
            string result = solution;

            switch (type)
            {
                case QuestionType.TrueFalse:
                    if (isTrue)
                    {
                        result = "true";
                    }
                    else
                    {
                        result = "false";
                    }
                    break;
                case QuestionType.MultipleChoice:
                    result = solution;
                    break;
                case QuestionType.FillInTheBlank:
                    result = solution;
                    break;
            }

            return result;
        }

        public bool CheckSolution(string answer) {
            bool result = false;

            switch (type)
            {
                case QuestionType.TrueFalse:
                    if (isTrue)
                    {
                        result = answer == "true";
                    }
                    else {
                        result = answer == "false";
                    }
                    break;
                case QuestionType.MultipleChoice:
                    result = solution.Equals(answer, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case QuestionType.FillInTheBlank:
                    result = solution.Equals(answer, StringComparison.InvariantCultureIgnoreCase);
                    break;
            }

            return result;
        }
    }
}
