using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QuizSystem
{
    public class ChoiceBoxesUI : MonoBehaviour
    {
        public List<ChoiceUI> buttons;
        public string[] choices;
        public int correctAnswerIndex = -1;

        public GameObject buttonPrefab;

        public delegate void SelectAnswerDelegate(string answer);
        public event SelectAnswerDelegate OnSelectAnswer;

        public void Start()
        {
            Debug.Log("Initting ChoiceBoxesGUI");
            InitButtons();
            //DisableChoices();
            choices = new string[4];
            InitListeners();
        }

        private void SelectAnswer(string answer)
        {
            OnSelectAnswer?.Invoke(answer);
            FlashButtonColor(answer);
        }


        public void SetChoices(string[] newChoices) {
            choices = newChoices;
            
            for (int i = 0; i < buttons.Count; i++) {
                if (i < newChoices.Length)
                {
                    buttons[i].SetChoice(newChoices[i]);
                    buttons[i].gameObject.SetActive(true);
                }
                else {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

        public void AddListener(SelectAnswerDelegate method) {
            OnSelectAnswer += method;
        }

        public bool AnswerQuestion(string answer) {

            return false;
        }



        private void InitButtons() {
            buttons = new List<ChoiceUI>(4);
            buttons.AddRange(transform.GetComponentsInChildren<ChoiceUI>());
        }


        private void InitListeners() {
            for (int i = 0; i < buttons.Count; i++) {
                buttons[i].AddListener(SelectAnswer);
            }
        }

        public void DisableChoices() {
            for (int i = 0; i < buttons.Count; i++) {
                buttons[i].DisableChoice();
            }
        }

        private void FlashButtonColor(string answer) {
            for (int i = 0; i < buttons.Count; i++) {
                if (buttons[i].isCorrect || buttons[i].text == answer)
                {
                    buttons[i].FlashButton();
                }
            }
        }
    }
}
