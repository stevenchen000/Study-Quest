using CombatSystem;
using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatQuizButton : MonoBehaviour
{
    private Button button;
    private string answer;
    private TMP_Text textbox;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color correctColor;
    [SerializeField]
    private Color incorrectColor;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInChildren<Animator>();
        button = transform.GetComponentInChildren<Button>();
        textbox = transform.GetComponentInChildren<TMP_Text>();
        button.onClick.AddListener(AnswerQuestion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }



    public void SetAnswer(string answer)
    {
        this.answer = answer;
        textbox.text = answer;
        ChangeButtonColor(defaultColor);

        float randTime = Random.Range(0f, 1f);
        Debug.Log(randTime);
        anim.Play("Answer Float", 0, randTime);
    }

    private void AnswerQuestion()
    {
        bool correct = QuizManager.quiz.AnswerQuestion(answer);
        if (correct)
        {
            MarkCorrect();
        }
        else
        {
            MarkIncorrect();
        }
    }


    public void MarkCorrect()
    {
        ChangeButtonColor(correctColor);
    }

    public void MarkIncorrect()
    {
        ChangeButtonColor(incorrectColor);
    }
    public string GetText() { return answer; }
    private void ChangeButtonColor(Color color)
    {
        ColorBlock block = button.colors;
        block.disabledColor = color;
        block.highlightedColor = color;
        block.normalColor = color;
        block.pressedColor = color;
        block.selectedColor = color;

        button.colors = block;
    }
}
