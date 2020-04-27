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

        public string[] choices;

        public delegate void AnsweredQuestion(bool isCorrect);
        public event AnsweredQuestion OnQuestionAnswered;
    
        
        public float disableTime = 2f;
        private float timer = 0;
        private bool prepareToDisableTimer = false;

        public void Start()
        {
            QuizManager.quizzer.OnQuestionAsked += SetQuestion;
            QuizManager.quizzer.OnAnswerReceived += StartUIDisableTimer;
        }

        private void Update()
        {
            if (prepareToDisableTimer) {
                timer += Time.deltaTime;

                if(timer > disableTime)
                {
                    DisableGUI();
                    ResetTimer();
                }
            }
        }

        public void SetQuestion(Question question) {
            currentQuestion = question;
            questionText.text = question.question;
            EnableGUI();

            switch (question.type)
            {
                case QuestionType.TrueFalse:
                    choices = question.GetAllChoices();
                    multipleChoiceUI.SetChoices(choices);
                    //correctIndex = currentQuestion.GetSolution() == "true" ? 0 : 1;
                    //multipleChoiceUI.SetChoices(questions, correctIndex);
                    break;
                case QuestionType.MultipleChoice:
                    choices = question.GetAllChoices();
                    multipleChoiceUI.SetChoices(choices);
                    /*Debug.Log("Test");
                    questions = question.GetAllChoices();
                    correctIndex = question.GetSolutionIndex(questions);
                    Debug.Log(correctIndex);
                    Debug.Log(questions);
                    multipleChoiceUI.SetChoices(questions, correctIndex);
                    Debug.Log("test2");*/
                    break;
                case QuestionType.FillInTheBlank:
                    break;
            }
        }

        private void StartUIDisableTimer(bool isCorrect) {
            timer = 0;
            prepareToDisableTimer = true;
        }

        private void ResetTimer() {
            prepareToDisableTimer = false;
            timer = 0;
        }

        public void DisableGUI()
        {
            gameObject.SetActive(false);
            multipleChoiceUI.DisableChoices();
        }

        public void EnableGUI() {
            gameObject.SetActive(true);
        }
    }
}
