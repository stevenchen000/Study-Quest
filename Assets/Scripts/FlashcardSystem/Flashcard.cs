using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlashcardSystem
{
    [System.Serializable]
    public class Flashcard
    {
        private string frontText;
        private string backText;

        public Flashcard(string newFrontText, string newBackText)
        {
            frontText = newFrontText;
            backText = newBackText;
        }


        public string GetFrontText() { return frontText; }
        public void SetFrontText(string newText) { frontText = newText; }
        public string GetBackText() { return backText; }
        public void SetBackText(string newText) { backText = newText; }
    }
}