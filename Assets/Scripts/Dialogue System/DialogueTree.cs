using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Tree")]
    public class DialogueTree : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        [TextArea(5,20)]
        private string fullText = "";
        private string previousText = "";
        

        [SerializeField]
        public List<DialogueNode> nodes = new List<DialogueNode>();

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public DialogueNode GetNextNode(DialogueNode node)
        {
            DialogueNode result = nodes[0];

            if(node != null)
            {
                for(int i = 0; i < nodes.Count; i++)
                {
                    if(node == nodes[i])
                    {
                        if(i == nodes.Count - 1)
                        {
                            result = null;
                            break;
                        }
                        else
                        {
                            result = nodes[i + 1];
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public void OnBeforeSerialize()
        {
            if(previousText != fullText)
            {
                TextToNodes();
                previousText = fullText;
            }
        }

        public void OnAfterDeserialize()
        {
            
        }

        private void TextToNodes()
        {
            string[] lines = fullText.Split('\n');
            nodes = new List<DialogueNode>();

            DialogueNode newNode = null;
            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                string[] splitLine = line.Split('\t');
                int length = splitLine.Length;

                if (length == 0 || splitLine[0] == "")
                {
                    continue;
                }
                else if (length == 1)
                {
                    if (newNode == null)
                    {
                        newNode = new DialogueNode("???", splitLine[0]);
                    }
                    else
                    {
                        newNode.AddLine(splitLine[0]);
                    }
                }
                else
                {
                    if (newNode != null)
                    {
                        nodes.Add(newNode);
                    }
                    newNode = new DialogueNode(splitLine[0], splitLine[1]);
                }
            }
            nodes.Add(newNode);
        }
    }
}