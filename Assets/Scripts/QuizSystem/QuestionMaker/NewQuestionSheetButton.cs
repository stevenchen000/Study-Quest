using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace QuizSystem
{
    public class NewQuestionSheetButton : MonoBehaviour
    {
        public GameObject content;
        public GameObject buttonPrefab;
        public string assetPath;
        public string assetName;
        public string assetExtension = "asset";
        public Button button;
        public TMP_Text text;

        private void Start()
        {
            text.text = "Create New Sheet";
        }

        public void CreateSheet()
        {
            Debug.Log($"New sheet created");
            QuestionSheet asset = ScriptableObject.CreateInstance<QuestionSheet>();

            AssetDatabase.CreateAsset(asset, $"{assetPath}/{assetName}.{assetExtension}");
            AssetDatabase.SaveAssets();

            CreateButton(asset);
        }

        private void CreateButton(QuestionSheet sheet)
        {
            GameObject obj = Instantiate(buttonPrefab);
            QuestionSheetButton button = obj.GetComponent<QuestionSheetButton>();
            
            obj.transform.SetParent(content.transform);
            int childCount = content.transform.childCount;
            obj.transform.SetSiblingIndex(childCount - 2);

            button.SetSheet(sheet);
        }
    }
}
