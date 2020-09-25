using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuizSystem
{
    public class QuestionSheetScrollLoader : MonoBehaviour
    {
        public GameObject content;
        public string path = "Question Sheet";
        public QuestionSheetButton buttonPrefab;

        // Start is called before the first frame update
        void Start()
        {
            LoadQuestionSheets();
        }

        
        public void LoadQuestionSheets()
        {
            //List<QuestionSheet> sheets = new List<QuestionSheet>();
            QuestionSheet[] sheets = Resources.LoadAll<QuestionSheet>(path);
            Debug.Log(sheets.Length);
            for(int i = 0; i < sheets.Length; i++)
            {
                QuestionSheetButton button = Instantiate(buttonPrefab);
                button.SetSheet(sheets[i]);
                button.transform.SetParent(content.transform);
                int childCount = content.transform.childCount;
                button.transform.SetSiblingIndex(childCount - 2);
            }
        }
    }
}