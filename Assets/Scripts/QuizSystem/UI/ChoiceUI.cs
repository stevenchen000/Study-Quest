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
        public Text textbox;
        public string text;
        public bool isCorrect = false;

        public delegate void ReceiveInput(string choice);
        public event ReceiveInput OnButtonPressed;
        
        // Start is called before the first frame update
        void Start()
        {
            containerUI = transform.GetComponentInParent<ChoiceBoxesUI>();

            if (button == null) {
                button = transform.GetComponent<Button>();
            }
            button.onClick.AddListener(ChooseAnswer);

            if (image == null) {
                image = transform.GetComponent<Image>();
            }

            if(textbox == null)
            {
                textbox = transform.GetComponentInChildren<Text>();
            }
        }

        private void ChooseAnswer() {
            bool isCorrect = QuizManager.quizzer.AnswerQuestion(text);

            if (isCorrect)
            {
                image.color = Color.green;
            }
            else {
                image.color = Color.red;
            }
            Debug.Log("Answered question");
        }

        public void SetChoice(string newText) {
            ResetChoice();
            EnableChoice();
            textbox.text = newText;
            text = newText;
        }


        public void AddListener(ReceiveInput method) {
            OnButtonPressed += method;
        }

        public void RemoveListeners() {
            button.onClick.RemoveAllListeners();
        }

        public void DisableChoice() {
            gameObject.SetActive(false);
        }

        public void FlashButton() {
            if (isCorrect)
            {
                image.color = Color.green;
            }
            else {
                image.color = Color.red;
            }
        }


        private void EnableChoice() {
            gameObject.SetActive(true);
        }

        private void ResetChoice() {
            image.color = Color.white;
        }

    }
}