using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuizSystem
{
    public class ChoiceUI : MonoBehaviour
    {
        public ChoiceBoxesUI containerUI;

        public Button button;
        public Image image;
        public Image cross;
        public Image checkmark;
        public Text textbox;

        public string text;

        private QuizManager quiz;

        // Start is called before the first frame update
        void Start()
        {
            containerUI = transform.GetComponentInParent<ChoiceBoxesUI>();
            button = transform.GetComponent<Button>();
            textbox = transform.GetComponentInChildren<Text>();
            quiz = QuizManager.quiz;

            button.onClick.AddListener(ChooseAnswer);
        }




        //public functions

        public void DisableChoice()
        {
            gameObject.SetActive(false);
        }

        public void EnableChoice()
        {
            gameObject.SetActive(true);
        }

        public void DisableInteraction()
        {
            button.interactable = false;
        }

        public void EnableInteraction()
        {
            button.interactable = true;
        }

        public void MarkCorrect() {
            checkmark.gameObject.SetActive(true);
        }

        public void MarkIncorrect() {
            cross.gameObject.SetActive(true);
        }




        
        //event functions

        private void ChooseAnswer() {
            bool isCorrect = quiz.AnswerQuestion(text);

            ShowCrossOrCheck(isCorrect);
        }

        public void SetChoice(string newText) {
            ResetChoice();
            EnableChoice();
            textbox.text = newText;
            text = newText;
        }
        

        public void ShowCrossOrCheck(bool isCorrect) {
            if (isCorrect)
            {
                MarkCorrect();
            }
            else {
                MarkIncorrect();
            }
        }

        public void DisableCrossAndCheck() {
            checkmark.gameObject.SetActive(false);
            cross.gameObject.SetActive(false);
        }

        public void FlashCorrectIfAnswerSame(string answer) {
            if (text.Equals(answer, StringComparison.InvariantCultureIgnoreCase)) {
                ShowCrossOrCheck(true);
            }
        }
        

        private void ResetChoice() {
            DisableCrossAndCheck();
        }

    }
}