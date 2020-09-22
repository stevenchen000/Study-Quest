using CombatSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace QuizSystem
{
    public enum QuizUIState
    {
        AwaitingAnswer,
        DisplayingCorrectAnswer,
        Deactivated
    }

    public class QuizUI : MonoBehaviour
    {
        public Question currentQuestion;

        public QuizTextUI ui;
        public GameObject choicesPanel;
        public List<QuizChoiceUi> choices = new List<QuizChoiceUi>();

        public GameObject inputPanel;
        public TMP_InputField inputField;

        private QuizManager quiz;
        private CombatManager combat;

        private QuizUIState state;

        public void Start()
        {
            quiz = QuizManager.quiz;
            ui = transform.GetComponentInChildren<QuizTextUI>();
            choices.AddRange(transform.GetComponentsInChildren<QuizChoiceUi>());
            combat = FindObjectOfType<CombatManager>();
            
            quiz.SubscribeToOnQuestionAsked(AskQuestion);
        }

        private void Update()
        {
            
        }

        private void OnDestroy()
        {
            quiz.UnsubscribeFromOnQuestionAsked(AskQuestion);
        }

        #region States

        private void RunState()
        {
            switch (state)
            {
                case QuizUIState.AwaitingAnswer:

                    break;
                case QuizUIState.DisplayingCorrectAnswer:
                    break;
                case QuizUIState.Deactivated:
                    break;
            }
        }

        private void AwaitingAnswerState()
        {

        }

        private void DisplayCorrectAnswerState()
        {

        }

        private void DeactivatedState()
        {

        }

        #endregion



        public void AskQuestion(Question question)
        {
            currentQuestion = question;
            gameObject.SetActive(true);
            SetupTextUI();
            HideUIs();
            switch (currentQuestion.type)
            {
                case QuestionType.TrueFalse:
                    SetupChoiceUI();
                    break;
                case QuestionType.MultipleChoice:
                    SetupChoiceUI();
                    break;
                case QuestionType.FillInTheBlank:
                    SetupInputUI();
                    break;
            }
        }

        public void AnswerQuestion(bool correct)
        {
            combat.QuestionAnswered(correct);
        }





        private void SetupTextUI()
        {
            ui.SetText(currentQuestion.question);
        }

        private void SetupChoiceUI()
        {
            choicesPanel.SetActive(true);
            List<string> choiceList = currentQuestion.GetAllChoices();
            for (int i = 0; i < choiceList.Count; i++)
            {
                choices[i].UpdateText(choiceList[i]);
            }
        }

        private void SetupInputUI()
        {
            inputPanel.SetActive(true);
            inputField.Select();
            inputField.ActivateInputField();
            inputField.text = "";
        }

        private void HideUIs()
        {
            choicesPanel.SetActive(false);
            inputPanel.SetActive(false);
        }
    }
}
