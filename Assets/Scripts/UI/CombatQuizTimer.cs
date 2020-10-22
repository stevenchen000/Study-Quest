using CombatSystem;
using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatQuizTimer : MonoBehaviour
{
    private float answerTime;
    private bool isRunning = false;
    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            slider.value = (5 - answerTime) / 5;
            answerTime += Time.deltaTime;

            if(answerTime > 5)
            {
                QuizManager.quiz.AnswerQuestion("");
            }
        }
    }

    private void OnEnable()
    {
        isRunning = true;
        answerTime = 0;
    }

    public void SetAnswerTime() 
    { 
        CombatManager.combat.SetAnswerTime(answerTime);
        isRunning = false;
    }
}
