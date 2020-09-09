using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuizSystem
{
    public class QuizManager : MonoBehaviour
    {
        public QuestionSheet sheet;
        public List<Question> questions = new List<Question>();

        public static QuizManager quiz;

        public Question currentQuestion;
        public int currentIndex = 0;
        

        private void Awake()
        {
            if (quiz == null)
            {
                quiz = this;
                DontDestroyOnLoad(this);
            }
            else {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            SetNewQuestions(sheet);
        }




        //public functions

        public Question GetCurrentQuestion()
        {
            Question result = null;

            if(questions.Count > currentIndex)
            {
                result = questions[currentIndex];
            }

            return result;
        }

        public Question GetNextQuestion()
        {
            currentIndex = (currentIndex + 1) % questions.Count;
            SetQuestion();

            return questions[currentIndex];
        }


        public bool AnswerQuestion(string answer)
        {
            return currentQuestion.CheckAnswer(answer);
        }


        public void SetNewQuestions(QuestionSheet newQuestions)
        {
            sheet = newQuestions;
            questions = new List<Question>();
            questions.AddRange(sheet.GetQuestions());
            ScrambleQuestions();
            currentIndex = 0;
            currentQuestion = questions[currentIndex];
        }



        /// <summary>
        /// Rearranges the order of the questions
        /// </summary>
        private void ScrambleQuestions()
        {
            for (int i = 0; i < questions.Count; i++)
            {
                int rand = UnityEngine.Random.Range(0, questions.Count);
                Question temp = questions[i];
                questions[i] = questions[rand];
                questions[rand] = temp;
            }
        }



        /// <summary>
        /// Updates the currentQuestion based on currentIndex
        /// </summary>
        private void SetQuestion()
        {
            currentQuestion = questions[currentIndex];
        }

        public void SetQuestion(Question newQuestion)
        {
            currentQuestion = newQuestion;
        }


    }
}