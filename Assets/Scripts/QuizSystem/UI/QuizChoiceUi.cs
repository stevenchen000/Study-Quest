using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace QuizSystem
{
    public class QuizChoiceUi : MonoBehaviour
    {
        public TMP_Text textUI;
        public string text;
        public Button button;
        private QuizUI ui;

        public Color defaultColor = Color.gray;
        public Color incorrectColor = Color.red;
        public Color correctColor = Color.green;

        private void Start()
        {
            ui = transform.GetComponentInParent<QuizUI>();
        }

        public void UpdateText(string newText)
        {
            textUI.text = newText;
            text = newText;
        }

        public void AnswerQuestion()
        {
            bool correct = QuizManager.quiz.AnswerQuestion(text);

            if (correct)
            {
                MarkCorrect();
            }
            else
            {
                MarkIncorrect();
            }
        }

        public void MarkCorrect()
        {
            ChangeButtonColor(correctColor);
        }

        public void MarkIncorrect()
        {
            ChangeButtonColor(incorrectColor);
        }

        public void ResetButton()
        {
            ChangeButtonColor(defaultColor);
        }

        private void ChangeButtonColor(Color color)
        {
            ColorBlock block = button.colors;
            block.normalColor = color;
            block.highlightedColor = color;
        }
    }
}
