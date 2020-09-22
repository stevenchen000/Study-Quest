using FlashcardSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QuizSystem
{
    [CreateAssetMenu(menuName = "Question Sheet", fileName = "Question Sheet")]
    public class QuestionSheet : ScriptableObject, ISerializationCallbackReceiver
    {
        [Tooltip("Name and paht for file to get questions from")]
        public string filename;
        public QuestionType defaultQuestionType;
        [Tooltip("Click to add questions from file")]
        public bool addQuestionsFromFile = false;
        [Tooltip("Click to set questions from file (removes all current questions)")]
        public bool setQuestionsFromFile = false;
        public bool reverse = false;

        public List<Question> questions = new List<Question>();



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




        public void OnBeforeSerialize()
        {
            if(addQuestionsFromFile || setQuestionsFromFile)
            {
                AddQuestionsFromFile(filename);
                addQuestionsFromFile = false;
                setQuestionsFromFile = false;
            }
        }

        public void OnAfterDeserialize()
        {
            
        }


        private void AddQuestionsFromFile(string filename)
        {
            FileStream file = File.OpenRead(filename);
            StreamReader reader = new StreamReader(file);


            List<Question> questions = new List<Question>();

            string line;
            while((line = reader.ReadLine()) != null){
                string[] text = line.Split('\t');
                string question = reverse ? text[1] : text[0];
                string answer = reverse ? text[0] : text[1];
                Question newQuestion = new Question(question, answer);
                questions.Add(newQuestion);
                newQuestion.type = defaultQuestionType;
            }

            switch (defaultQuestionType)
            {
                case QuestionType.TrueFalse:
                    break;
                case QuestionType.MultipleChoice:
                    SetChoices(questions);
                    break;
                case QuestionType.FillInTheBlank:
                    break;
            }


            if (setQuestionsFromFile)
            {
                this.questions = questions;
            }
            else
            {
                this.questions.AddRange(questions);
            }
        }

        private void SetChoices(List<Question> questions)
        {
            for(int i = 0; i < questions.Count; i++)
            {
                while(questions[i].GetNumberOfWrongChoices() < 3)
                {
                    int rand = UnityEngine.Random.Range(0, questions.Count);

                    questions[i].AddWrongChoice(questions[rand].GetAnswer());
                }
            }
        }
    }
}
