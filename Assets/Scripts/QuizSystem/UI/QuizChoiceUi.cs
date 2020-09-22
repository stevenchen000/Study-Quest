using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CombatSystem;

namespace QuizSystem
{
    public class QuizChoiceUi : MonoBehaviour
    {
        public TMP_Text textUI;
        public string text;
        public Button button;
        private QuizUI ui;

        public KeyCode keycode;
        public Color defaultColor = Color.gray;
        public Color incorrectColor = Color.red;
        public Color correctColor = Color.green;

        private void Start()
        {
            ui = transform.GetComponentInParent<QuizUI>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(keycode))
            {
                AnswerQuestion();
            }
        }

        public void UpdateText(string newText)
        {
            string keycodeName = keycode.ToString();
            int keycodeLength = keycodeName.Length;
            keycodeName = keycodeName.Substring(keycodeLength-1);

            textUI.text = $"{keycodeName}) {newText}";
            text = newText;
            ResetButton();
        }

        public void AnswerQuestion()
        {
            bool correct = QuizManager.quiz.AnswerQuestion(text);
            Debug.Log("Answered question: " + text);
            CombatManager.combat.QuestionAnswered(correct);
            
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
            Debug.Log("Marked correct");
            ChangeButtonColor(correctColor);
        }

        public void MarkIncorrect()
        {
            Debug.Log("Marked incorrect");
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
            block.pressedColor = color;
            block.selectedColor = color;
            button.colors = block;
        }
    }
}
