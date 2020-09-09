using FlashcardSystem;
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


        public void AddQuestion(Question newQuestion)
        {
            if(questions != null && !QuestionAlreadyExists(newQuestion))
            {
                questions.Add(newQuestion);
            }
        }

        public void AddQuestions(List<Question> questions)
        {
            for(int i = 0; i < questions.Count; i++)
            {
                AddQuestion(questions[i]);
            }
        }

        public static List<Question> CreateQuestionsFromFlashcards(List<Flashcard> cards, bool reverse = false)
        {
            List<Question> questions = new List<Question>();
            if(cards.Count < 4)
            {
                Debug.Log("Not enough cards");
                return questions;
            }

            for(int i = 0; i < cards.Count; i++)
            {
                Question question = new Question();
                question.SetQuestionType(QuestionType.MultipleChoice);
                question.question = reverse ? cards[i].GetBackText() : cards[i].GetFrontText();
                question.answer = reverse ? cards[i].GetFrontText() : cards[i].GetBackText();

                while(question.GetNumberOfWrongChoices() < 3)
                {
                    int rand = UnityEngine.Random.Range(0, cards.Count);
                    string newChoice = reverse ? cards[rand].GetFrontText() : cards[rand].GetBackText();
                    question.AddWrongChoice(newChoice);
                }

                questions.Add(question);
            }

            return questions;
        }

        private bool QuestionAlreadyExists(Question question)
        {
            bool result = false;

            for(int i = 0; i < questions.Count; i++)
            {
                string questionText = questions[i].question;
                if (questionText.Equals(question.question))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }


    }
}
