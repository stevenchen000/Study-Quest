using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace QuizSystem
{
    public class QuizUI : MonoBehaviour
    {
        public Question currentQuestion;
        public Text questionText;
        public ChoiceBoxesUI multipleChoiceUI;
        public FillInTheBlankUI fillInTheBlankUI;

        public string[] choices;

        public delegate void AnsweredQuestion(bool isCorrect);
        public event AnsweredQuestion OnQuestionAnswered;
    
        
        public float disableTime = 2f;
        private float timer = 0;
        private bool prepareToDisableTimer = false;


        private QuizManager quiz;

        public void Start()
        {
            quiz = QuizManager.quiz;
            DisableGUI();

            quiz.OnQuestionAsked += SetQuestion;
            quiz.OnAnswerReceived += StartUIDisableTimer;
            quiz.OnAnswerReceived += StopUIInteractions;
            quiz.ReceiveCorrectAnswer += MarkCorrectAnswer;
        }

        private void Update()
        {
            if (prepareToDisableTimer) {
                timer += Time.deltaTime;

                if(timer > disableTime)
                {
                    DisableGUI();
                    ResetTimer();
                    quiz.AskQuestion();
                }
            }
        }




        //event functions

        private void SetQuestion(Question question) {
            EnableGUI();
            currentQuestion = question;
            questionText.text = question.question;

            switch (question.type)
            {
                case QuestionType.TrueFalse:
                    SetupMultipleChoiceGUI();
                    break;
                case QuestionType.MultipleChoice:
                    SetupMultipleChoiceGUI();
                    break;
                case QuestionType.FillInTheBlank:
                    SetupFillInTheBlankGUI();
                    break;
            }
        }

        private void StopUIInteractions(bool isCorrect) {
            switch (currentQuestion.type)
            {
                case QuestionType.TrueFalse:
                    multipleChoiceUI.DisableChoiceInteractions();
                    break;
                case QuestionType.MultipleChoice:
                    multipleChoiceUI.DisableChoiceInteractions();
                    break;
                case QuestionType.FillInTheBlank:
                    fillInTheBlankUI.DisableInteraction();
                    break;
            }
        }
        
        private void StartUIDisableTimer(bool isCorrect)
        {
            timer = 0;
            prepareToDisableTimer = true;
        }

        private void MarkCorrectAnswer(string answer) {
            switch (currentQuestion.type)
            {
                case QuestionType.TrueFalse:
                    multipleChoiceUI.MarkCorrectAnswer(answer);
                    break;
                case QuestionType.MultipleChoice:
                    multipleChoiceUI.MarkCorrectAnswer(answer);
                    break;
                case QuestionType.FillInTheBlank:
                    fillInTheBlankUI.MarkCorrectAnswer(answer);
                    break;
            }
        }





        //helper functions


        private void ResetTimer() {
            prepareToDisableTimer = false;
            timer = 0;
        }

        private void DisableGUI()
        {
            gameObject.SetActive(false);
            multipleChoiceUI.DisableGUI();
            fillInTheBlankUI.DisableGUI();
        }

        private void EnableGUI() {
            gameObject.SetActive(true);
        }

        private void SetupMultipleChoiceGUI() {
            choices = currentQuestion.GetAllChoices();
            multipleChoiceUI.SetChoices(choices);
            multipleChoiceUI.EnableGUI();
            multipleChoiceUI.EnableChoiceInteractions();
        }

        private void SetupFillInTheBlankGUI()
        {
            fillInTheBlankUI.EnableGUI();
            fillInTheBlankUI.EnableInteraction();
        }
    }
}
