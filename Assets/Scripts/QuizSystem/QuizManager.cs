using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuizSystem
{
    public class QuizManager : MonoBehaviour
    {
        public QuestionSheet questions;

        public int currentIndex = -1;
        public Question currentQuestion;

        public QuestionUI questionUI;
        public ChoiceBoxesUI choicesUI;

        public List<Fighter> targets;


        public delegate void SelectedAnswer(bool isCorrect);
        public event SelectedAnswer OnSelectAnswer;
        

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Initting QuizManager");
            targets = new List<Fighter>();
            SetNextQuestion();
            choicesUI.OnSelectAnswer += SelectAnswer;
            DisableGUI();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AskQuestion()
        {
            SetNextQuestion();
            SetupQuestionUI(currentQuestion);
            EnableGUI();
        }

        public void AwaitAnswer(SelectedAnswer awaitAnswerFunction)
        {
            OnSelectAnswer += awaitAnswerFunction;
        }




        private void SelectAnswer(string answer) {
            if (currentQuestion.CheckSolution(answer))
            {
                Debug.Log("O Correct!");
                OnSelectAnswer?.Invoke(true);
            }
            else {
                Debug.Log("X Wrong!");
                OnSelectAnswer?.Invoke(false);
            }
            DisableGUI();
        }





        private void DisableGUI() {
            gameObject.SetActive(false);
            questionUI.gameObject.SetActive(false);
            choicesUI.gameObject.SetActive(false);
            OnSelectAnswer = null;
        }

        private void EnableGUI() {
            gameObject.SetActive(true);
            questionUI.gameObject.SetActive(true);
            choicesUI.gameObject.SetActive(true);
        }

        private void SetNextQuestion() {
            currentIndex = (currentIndex + 1) % questions.GetNumberOfQuestion();
            currentQuestion = questions.GetQuestionAt(currentIndex);
        }

        private void SetupQuestionUI(Question question) {
            questionUI.SetQuestion(question.question);
            Debug.Log("Setting up choices");
            choicesUI.SetChoices(question.GetAllChoices());
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