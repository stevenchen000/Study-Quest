using CombatSystem;
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
        public List<Question> questions;

        public static QuizManager quiz;

        public int currentIndex = -1;
        public Question currentQuestion;
        
        private bool askingQuestion = false;
        

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

        public void AskQuestion()
        {
            if (!askingQuestion)
            {
                SetNextQuestion();
                askingQuestion = true;
            }
        }

        public bool AnswerQuestion(string answer) {
            bool isCorrect = currentQuestion.GetSolution().Equals(answer, StringComparison.InvariantCultureIgnoreCase);
            askingQuestion = false;

            return isCorrect;
        }

        
        public string GetCurrentSolution() {
            return currentQuestion.GetSolution();
        }

        public void SetNewQuestions(QuestionSheet newQuestions)
        {
            sheet = newQuestions;
            questions = new List<Question>();
            questions.AddRange(sheet.GetQuestions());
            ScrambleQuestions();
            ResetQuestions();
        }



        //Helper Functions

        private void SetNextQuestion()
        {
            currentIndex = (currentIndex + 1) % questions.Count;
            currentQuestion = questions[currentIndex];
        }

        private void ResetQuestions()
        {
            currentIndex = -1;
            currentQuestion = null;
            askingQuestion = false;
        }

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




        

    }
}