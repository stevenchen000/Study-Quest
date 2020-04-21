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
        public Button[] buttons;
        public Text[] buttonTextBoxes;
        public string[] text;

        public delegate void SelectAnswerDelegate(string answer);
        public event SelectAnswerDelegate OnSelectAnswer;

        public void Start()
        {
            Debug.Log("Initting ChoiceBoxesGUI");
            InitButtons();
            InitTextBoxes();
            text = new string[4];
            InitListeners();
        }

        public void SelectAnswer(int index)
        {
            OnSelectAnswer?.Invoke(text[index]);
        }


        public void SetChoices(string[] newChoices) {
            text = new string[4];
            Debug.Log("Choices init");
            
            for (int i = 0; i < newChoices.Length; i++) {
                text[i] = newChoices[i];
                buttonTextBoxes[i].text = newChoices[i];
            }
        }




        private void InitButtons() {
            buttons = transform.GetComponentsInChildren<Button>();
        }

        private void InitTextBoxes() {
            Debug.Log("Text boxes init");
            buttonTextBoxes = new Text[4];

            for (int i = 0; i < buttons.Length; i++) {
                buttonTextBoxes[i] = buttons[i].GetComponentInChildren<Text>();
            }
        }

        private void InitListeners() {
            for (int i = 0; i < buttons.Length; i++) {
                AddListenerAtIndex(i);
            }
        }

        private void AddListenerAtIndex(int index)
        {
            buttons[index].onClick.AddListener(() => SelectAnswer(index));
        }
    }
}
