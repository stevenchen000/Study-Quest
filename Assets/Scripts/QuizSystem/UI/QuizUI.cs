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
        }

        private void Update()
        {
            
        }

        public void AskQuestion()
        {
            currentQuestion = quiz.GetNextQuestion();
            ui.SetText(currentQuestion.question);
        }

        public void AnswerQuestion(bool correct)
        {
            combat.QuestionAnswered(correct);
        }
    }
}
