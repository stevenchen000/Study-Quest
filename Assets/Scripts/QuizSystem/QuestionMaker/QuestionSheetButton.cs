using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuizSystem
{
    public class QuestionSheetButton : MonoBehaviour
    {
        public QuestionSheet sheet;
        public Button button;
        public TMP_Text text;

        public void SetSheet(QuestionSheet newSheet)
        {
            sheet = newSheet;
            text.text = sheet.name;
        }

        public void LoadSheet()
        {
            Debug.Log($"Sheet loaded : {sheet.name}");
        }
    }
}