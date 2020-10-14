using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem{
    [Serializable]
    public class DialogueNode
    {
        [SerializeField]
        private string characterName;
        [SerializeField]
        [TextArea(1, 4)]
        private string dialogueText;

        public DialogueNode(string name, string text)
        {
            characterName = name;
            dialogueText = text;
        }

        public string GetCharacterName(){ return characterName; }
        public string GetDialogueText(){ return dialogueText; }
        public void AddLine(string line)
        {
            dialogueText += $"\n{line}";
        }
    }
}