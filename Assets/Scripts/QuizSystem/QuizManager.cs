using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuizSystem
{
    public class QuizManager : MonoBehaviour
    {
        public QuestionSheet questions;
        public static QuizManager quiz;

        public int currentIndex = -1;
        public Question currentQuestion;
        
        private bool askingQuestion = false;


        public delegate void AskQuestionDelegate(Question question);
        public delegate void ReceiveAnswerDelegate(bool isCorrect);
        public delegate void SendAnswerDelegate(string answer);

        public event AskQuestionDelegate OnQuestionAsked;
        public event ReceiveAnswerDelegate OnAnswerReceived;
        /// <summary>
        /// Calls when incorrect answer is given
        /// </summary>
        public event SendAnswerDelegate ReceiveCorrectAnswer;

        private void Awake()
        {
            if (quiz == null)
            {
                quiz = this;
            }
            else {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }




        //public functions

        public void AskQuestion()
        {
            //Debug.Log("Attempting to ask question");
            if (!askingQuestion)
            {
                SetNextQuestion();
                askingQuestion = true;
                OnQuestionAsked?.Invoke(currentQuestion);
                //Debug.Log("Question asked");
            }
        }

        public bool AnswerQuestion(string answer) {
            bool isCorrect = currentQuestion.GetSolution().Equals(answer, StringComparison.InvariantCultureIgnoreCase);
            OnAnswerReceived?.Invoke(isCorrect);
            if (!isCorrect)
            {
                ReceiveCorrectAnswer?.Invoke(currentQuestion.GetSolution());
            }
            askingQuestion = false;

            return isCorrect;
        }

        
        public string GetCurrentSolution() {
            return currentQuestion.GetSolution();
        }





        //Listener functions

        public void AddListenerOnQuestionAsked(AskQuestionDelegate method) {
            OnQuestionAsked += method;
        }
        public void RemoveListenerOnQuestionAsked(AskQuestionDelegate method) {
            OnQuestionAsked -= method;
        }

        public void AddListenerOnAnswerReceived(ReceiveAnswerDelegate method) {
            OnAnswerReceived += method;
        }
        public void RemoveListenerOnAnswerReceived(ReceiveAnswerDelegate method) {
            OnAnswerReceived -= method;
        }

        public void AddListenerReceiveCorrectAnswer(SendAnswerDelegate method) {
            ReceiveCorrectAnswer += method;
        }
        public void RemoveListenerReceiveCorrectAnswer(SendAnswerDelegate method)
        {
            ReceiveCorrectAnswer -= method;
        }




        
        //Helper Functions

        private void SetNextQuestion() {
            currentIndex = (currentIndex + 1) % questions.GetNumberOfQuestion();
            currentQuestion = questions.GetQuestionAt(currentIndex);
        }
        
    }
}