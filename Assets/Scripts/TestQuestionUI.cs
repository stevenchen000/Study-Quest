using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestionUI : MonoBehaviour
{

    public QuizUI quiz;
    public QuestionSheet sheet;

    public int currentIndex = 0;

    public delegate void AskQuestion(Question question);
    public delegate void ReceiveAnswer(bool isCorrect);
    public delegate void SendAnswer(string answer);
    public event AskQuestion OnQuestionAsked;
    public event ReceiveAnswer OnAnswerReceived;
    public event SendAnswer SendCorrectAnswer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            quiz.SetQuestion(sheet.GetQuestionAt(currentIndex));
            currentIndex = (currentIndex + 1) % sheet.GetNumberOfQuestion();
        }
    }
}
