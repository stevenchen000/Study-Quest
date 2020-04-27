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
        public static QuizManager quizzer;

        public int currentIndex = -1;
        public Question currentQuestion;

        public QuizUI questionUI;

        public List<Fighter> targets;

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
            if (quizzer == null)
            {
                quizzer = this;
            }
            else {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Initting QuizManager");
            targets = new List<Fighter>();
            SetNextQuestion();
            //DisableGUI();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                AskQuestion();
            }
        }

        public void AskQuestion()
        {
            Debug.Log("Attempting to ask question");
            if (!askingQuestion || askingQuestion)
            {
                SetNextQuestion();
                //SetupQuestionUI(currentQuestion);
                //EnableGUI();
                askingQuestion = true;
                OnQuestionAsked?.Invoke(currentQuestion);
                Debug.Log("Question asked");
            }
        }

        public bool AnswerQuestion(string answer) {
            bool isCorrect = currentQuestion.GetSolution().Equals(answer, StringComparison.InvariantCultureIgnoreCase);
            OnAnswerReceived?.Invoke(isCorrect);
            if (!isCorrect)
            {
                ReceiveCorrectAnswer?.Invoke(currentQuestion.GetSolution());
            }

            return isCorrect;
        }




        private void SelectAnswer(string answer) {
            if (currentQuestion.CheckSolution(answer))
            {
                Debug.Log("O Correct!");
                //OnSelectAnswer?.Invoke(true);
            }
            else {
                Debug.Log("X Wrong!");
               // OnSelectAnswer?.Invoke(false);
            }
            askingQuestion = false;
            DisableGUI();
        }





        private void DisableGUI() {
            gameObject.SetActive(false);
            questionUI.gameObject.SetActive(false);
            //OnSelectAnswer = null;
        }

        private void EnableGUI() {
            gameObject.SetActive(true);
            questionUI.gameObject.SetActive(true);
        }

        private void SetNextQuestion() {
            currentIndex = (currentIndex + 1) % questions.GetNumberOfQuestion();
            currentQuestion = questions.GetQuestionAt(currentIndex);
        }

        private void SetupQuestionUI(Question question) {
            questionUI.SetQuestion(question);
        }

        private void AddTarget(Fighter fighter) {
            targets.Add(fighter);
        }

        private void AddTargets(List<Fighter> fighters) {
            targets.AddRange(fighters);
        }

        private void ResetTargets() {
            targets = new List<Fighter>();
        }
    }
}