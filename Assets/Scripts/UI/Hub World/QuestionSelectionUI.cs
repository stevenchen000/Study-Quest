using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSelectionUI : MonoBehaviour
{
    private List<QuestionSheet> questions = new List<QuestionSheet>();
    public QuestionSelectionButton button;
    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        questions.AddRange(Resources.LoadAll<QuestionSheet>("Question Sheets"));
        for(int i = 0; i < questions.Count; i++)
        {
            QuestionSelectionButton qbutton = Instantiate(button);
            qbutton.SetupButton(questions[i]);
            qbutton.transform.SetParent(content.transform);
        }
    }

    
}
