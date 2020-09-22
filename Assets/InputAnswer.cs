using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using QuizSystem;
using UnityEngine.EventSystems;

public class InputAnswer : MonoBehaviour
{

    public TMP_InputField inputField;
    public QuizManager quiz;
    // Start is called before the first frame update
    void Start()
    {
        inputField = transform.GetComponent<TMP_InputField>();
        quiz = QuizManager.quiz;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) && inputField.text != "")
        {
            AnswerQuestion();
        }


        
    }

    public void AnswerQuestion()
    {
        bool correct = quiz.AnswerQuestion(inputField.text);

        if (correct)
        {

        }
    }
}
