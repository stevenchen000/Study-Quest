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
    public class TrueFalseBoxesUI : MonoBehaviour
    {
        public Button[] buttons;
        public Text[] buttonTextBoxes;
        public string[] text;

        public delegate void SelectAnswerDelegate(string answer);
        public event SelectAnswerDelegate OnSelectAnswer;

        public void Start()
        {
            InitButtons();
            InitTextBoxes();
            InitChoices();
            InitListeners();
        }

        public void SelectAnswer(int index)
        {
            OnSelectAnswer?.Invoke(text[index]);
        }


        private void InitChoices()
        {
            text = new string[2];
            text[0] = "true";
            buttonTextBoxes[0].text = "true";
            text[1] = "false";
            buttonTextBoxes[1].text = "false";
        }




        private void InitButtons()
        {
            buttons = transform.GetComponentsInChildren<Button>();
        }

        private void InitTextBoxes()
        {
            buttonTextBoxes = new Text[buttons.Length];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttonTextBoxes[i] = buttons[i].GetComponentInChildren<Text>();
            }
        }

        private void InitListeners()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                AddListenerAtIndex(i);
            }
        }

        private void AddListenerAtIndex(int index)
        {
            buttons[index].onClick.AddListener(() => SelectAnswer(index));
        }
    }
}
