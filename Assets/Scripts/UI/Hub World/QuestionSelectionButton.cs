using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSelectionButton : MonoBehaviour
{
    private QuestionSheet sheet;
    public TMP_Text text;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(SetQuestionSheet);
        button.onClick.AddListener(LoadLevel);
    }

    public void SetupButton(QuestionSheet newSheet)
    {
        sheet = newSheet;
        text.text = sheet.name;
    }

    private void SetQuestionSheet()
    {
        WorldState.SetQuestionSheet(sheet);
        //QuizManager.quiz.SetNewQuestions(sheet);
    }

    private void LoadLevel()
    {
        string level = WorldState.GetDungeonData().GetLevelName();
        UnityUtilities.LoadLevel(level);
        
    }
}
