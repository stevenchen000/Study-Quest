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
        [HideInInspector]
        public List<ChoiceUI> buttons = new List<ChoiceUI>();
        public string[] choices;
        public int correctAnswerIndex = -1;

        public GameObject buttonPrefab;

        private QuizManager quiz;
        
        public void Start()
        {
            quiz = QuizManager.quiz;
            //InitButtons();
            choices = new string[4];
            InitButtons();
            //add events here
        }




        //public functions

        public void EnableGUI() {
            gameObject.SetActive(true);
        }

        public void DisableGUI() {
            gameObject.SetActive(false);
        }
        
        public void DisableChoiceInteractions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].DisableInteraction();
            }
        }

        public void EnableChoiceInteractions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].EnableInteraction();
            }
        }

        public void MarkCorrectAnswer(string answer) {
            for (int i = 0; i < buttons.Count; i++) {
                if (buttons[i].text.Equals(answer, StringComparison.InvariantCultureIgnoreCase)) {
                    buttons[i].MarkCorrect();
                    break;
                }
            }
        }





        //event functions

        public void SetChoices(string[] newChoices) {
            choices = newChoices;
            
            for (int i = 0; i < buttons.Count; i++) {
                if (i < newChoices.Length)
                {
                    buttons[i].SetChoice(newChoices[i]);
                    buttons[i].EnableChoice();
                }
                else {
                    buttons[i].DisableChoice();
                }
            }
        }




        //Helper functions

        private void InitButtons()
        {
            buttons = new List<ChoiceUI>(4);
            buttons.AddRange(transform.GetComponentsInChildren<ChoiceUI>());
        }

    }
}
