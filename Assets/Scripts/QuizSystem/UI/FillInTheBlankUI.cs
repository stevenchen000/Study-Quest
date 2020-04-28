using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuizSystem
{
    public class FillInTheBlankUI : MonoBehaviour
    {
        public InputField inputField;
        public Button enterButton;
        public Image cross;
        public Image checkmark;

        private QuizManager quiz;

        // Start is called before the first frame update
        void Start()
        {
            quiz = QuizManager.quiz;
            enterButton.onClick.AddListener(AnswerQuestion);
        }

        // Update is called once per frame
        void Update()
        {
            if (inputField.IsInteractable() && Input.GetKeyDown(KeyCode.Return))
            {
                AnswerQuestion();
            }
        }



        //public functions

        public void DisableGUI()
        {
            gameObject.SetActive(false);
        }

        public void EnableGUI()
        {
            Debug.Log("Gui enabled");
            gameObject.SetActive(true);
            ResetText();
            
            DisableCrossAndCheckmark();
        }

        public void DisableInteraction()
        {
            inputField.interactable = false;
            enterButton.interactable = false;
        }

        public void EnableInteraction()
        {
            inputField.interactable = true;
            enterButton.interactable = true;
            inputField.Select();
        }

        public void MarkCorrectAnswer(string answer) {

        }

        public void MarkCorrect() {
            checkmark.gameObject.SetActive(true);
        }

        public void MarkIncorrect() {
            cross.gameObject.SetActive(true);
        }

        public void AnswerQuestion() {
            bool isCorrect = quiz.AnswerQuestion(inputField.text);

            if (isCorrect)
            {
                MarkCorrect();
            }
            else {
                MarkIncorrect();
            }
        }




        //helper functions

        private void ResetText()
        {
            inputField.text = "";
        }

        private void DisableCrossAndCheckmark() {
            cross.gameObject.SetActive(false);
            checkmark.gameObject.SetActive(false);
        }

    }
}