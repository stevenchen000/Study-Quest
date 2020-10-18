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

        private QuizUIState state = QuizUIState.Deactivated;
        private CanvasGroup cgroup;

        public void Start()
        {
            cgroup = GetComponent<CanvasGroup>();
            ChangeState(QuizUIState.Deactivated);
        }

        private void Update()
        {
            RunState();
        }

        private void OnDestroy()
        {
            
        }

        #region States

        private void RunState()
        {
            switch (state)
            {
                case QuizUIState.AwaitingAnswer:
                    AwaitingAnswerState();
                    break;
                case QuizUIState.DisplayingCorrectAnswer:
                    DisplayCorrectAnswerState();
                    break;
                case QuizUIState.Deactivated:
                    DeactivatedState();
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
            cgroup.alpha -= Time.deltaTime;
        }


        private void ChangeState(QuizUIState newState)
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
            switch (newState)
            {
                case QuizUIState.AwaitingAnswer:
                    cgroup.alpha = 1;
                    cgroup.interactable = true;
                    cgroup.blocksRaycasts = true;
                    break;
                case QuizUIState.DisplayingCorrectAnswer:
                    cgroup.interactable = false;
                    break;
                case QuizUIState.Deactivated:
                    cgroup.interactable = false;
                    cgroup.blocksRaycasts = false;
                    break;
            }
            state = newState;
        }


        #endregion



        public void ActivateUI(Question question)
        {
            currentQuestion = question;
            ChangeState(QuizUIState.AwaitingAnswer);
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
            ChangeState(QuizUIState.DisplayingCorrectAnswer);
        }

        public void DeactivateUI()
        {
            ChangeState(QuizUIState.Deactivated);
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
