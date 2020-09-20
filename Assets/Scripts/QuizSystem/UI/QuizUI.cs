using CombatSystem;
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

        public QuizTextUI ui;
        public List<QuizChoiceUi> choices = new List<QuizChoiceUi>();

        private QuizManager quiz;
        private CombatManager combat;

        public void Start()
        {
            quiz = QuizManager.quiz;
            ui = transform.GetComponentInChildren<QuizTextUI>();
            choices.AddRange(transform.GetComponentsInChildren<QuizChoiceUi>());
            combat = FindObjectOfType<CombatManager>();

            quiz.SubscribeToOnQuestionAsked(AskQuestion);
            gameObject.SetActive(false);
        }

        private void Update()
        {
            
        }

        public void AskQuestion(Question question)
        {
            currentQuestion = question;
            SetupTextUI();
            SetupChoiceUI();
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
            List<string> choiceList = currentQuestion.GetAllChoices();
            for (int i = 0; i < choiceList.Count; i++)
            {
                choices[i].UpdateText(choiceList[i]);
            }
        }
    }
}
