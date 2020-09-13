using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace QuizSystem
{
    public class QuizTextUI : MonoBehaviour
    {
        public TMP_Text text;
        public string question;

        public void SetText(string newText)
        {
            question = newText;
            text.text = newText;
        }

    }
}
