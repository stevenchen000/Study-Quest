using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SOEventSystem;

namespace DialogueSystem{
    public enum DialogueState{
        Inactive,
        WritingText,
        AwaitingInput
    }
    public class DialogueUI : MonoBehaviour
    {

        public TMP_Text nameText;
        public TMP_Text dialogueText;
        private CanvasGroup cgroup;

        public EventSO onDialogueStartEvent;
        public EventSO onDialogueEndEvent;

        private DialogueTree currentDialogue;
        private DialogueNode currentNode;
        private string fullText;
        private string currentText = "";
        private float currentTextIndex = 0;
        public int textSpeed = 5;

        public List<KeyCode> progressTextKeys = new List<KeyCode>();

        private DialogueState state;
        private void Awake()
        {
            cgroup = transform.GetComponent<CanvasGroup>();
        }


        private void Update()
        {
            switch (state)
            {
                case DialogueState.Inactive:
                    break;
                case DialogueState.WritingText:
                    WriteTextState();
                    break;
                case DialogueState.AwaitingInput:
                    AwaitInputState();
                    break;
            }
        }

        private void WriteTextState()
        {
            currentTextIndex += textSpeed * Time.deltaTime;
            currentTextIndex = Mathf.Min(currentTextIndex, fullText.Length);
            currentText = currentNode.GetDialogueText().Substring(0, (int)currentTextIndex);

            if (currentTextIndex == fullText.Length)
            {
                ChangeState(DialogueState.AwaitingInput);
            }

            if (Input.GetMouseButtonDown(0) || ProgressTextKeyClicked())
            {
                currentText = fullText;
                ChangeState(DialogueState.AwaitingInput);
            }

            UpdateUIText();
        }

        private void AwaitInputState()
        {
            if (Input.GetMouseButtonDown(0) || ProgressTextKeyClicked())
            {
                InitializeNode();
                if (currentNode != null)
                {
                    ChangeState(DialogueState.WritingText);
                }else
                {
                    ResetNode();
                    ChangeState(DialogueState.Inactive);
                }
            }
        }

        private void InitializeNode()
        {
            currentNode = currentDialogue.GetNextNode(currentNode);
            currentText = "";
            currentTextIndex = 0;
            if (currentNode != null)
            {
                fullText = currentNode.GetDialogueText();
            }
        }

        private void ResetNode()
        {
            currentNode = null;
        }

        private void UpdateUIText()
        {
            nameText.text = currentNode.GetCharacterName();
            dialogueText.text = currentText;
        }


        private void ChangeState(DialogueState newState)
        {
            if(state == DialogueState.Inactive)
            {
                cgroup.alpha = 1;
                cgroup.interactable = true;
                cgroup.blocksRaycasts = true;
            }
            else if(state == DialogueState.AwaitingInput)
            {
                currentText = "";
                currentTextIndex = 0;
            }
            state = newState;

            if(state == DialogueState.Inactive)
            {
                cgroup.alpha = 0;
                cgroup.interactable = false;
                cgroup.blocksRaycasts = false;
                onDialogueEndEvent.CallEvent();
            }
        }

        public void SetDialogue(DialogueTree dialogue)
        {
            if(state == DialogueState.Inactive){
                currentDialogue = dialogue;
                InitializeNode();
                onDialogueStartEvent.CallEvent();
                ChangeState(DialogueState.WritingText);
            }
        }

        private bool ProgressTextKeyClicked()
        {
            bool result = false;

            for(int i = 0; i < progressTextKeys.Count; i++)
            {
                if (Input.GetKeyDown(progressTextKeys[i]))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}