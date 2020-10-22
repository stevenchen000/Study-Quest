using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum CombatQuizUIState
{ 
    Hidden,
    AwaitingText,
    AwaitingAnswer,
    Idle
}

public class CombatQuizUI : MonoBehaviour
{
    private QuestionTeletype teletype;
    private List<CombatQuizButton> buttons = new List<CombatQuizButton>();
    private Animator anim;
    private CanvasGroup cgroup;
    private CombatQuizUIState state;

    private CombatQuizTimer timer;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color correctColor;
    [SerializeField]
    private Color incorrectColor;

    [SerializeField]
    private float buttonPauseTime;

    private float answerTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = transform.GetComponentInChildren<CombatQuizTimer>();
        teletype = transform.GetComponent<QuestionTeletype>();
        buttons.AddRange(transform.GetComponentsInChildren<CombatQuizButton>());
        anim = transform.GetComponentInChildren<Animator>();
        cgroup = transform.GetComponent<CanvasGroup>();
        QuestionSheet sheet = QuizManager.quiz.sheet;
        QuizManager.quiz.SetNewQuestions(sheet);
        ChangeState(CombatQuizUIState.Hidden);

        teletype.OnTextFinished += () => ChangeState(CombatQuizUIState.AwaitingAnswer);
        teletype.OnTextFinished += () => anim.SetBool("read", true);
    }

    // Update is called once per frame
    void Update()
    {
        answerTime += Time.deltaTime;
    }

    public void ReactToQuestion(Question question)
    {
        anim.SetBool("read", false);
        teletype.gameObject.SetActive(true);
        teletype.ChangeQuestion(question.question);
        ChangeState(CombatQuizUIState.AwaitingText);
        List<string> choices = question.GetAllChoices();

        for(int i = 0; i < choices.Count; i++)
        {
            buttons[i].SetAnswer(choices[i]);
        }
    }

    private void ChangeState(CombatQuizUIState newState)
    {
        switch (state)
        {
            case CombatQuizUIState.Hidden:
                cgroup.alpha = 1;
                break;
            case CombatQuizUIState.AwaitingText:

                break;
            case CombatQuizUIState.AwaitingAnswer:
                break;
            case CombatQuizUIState.Idle:
                break;
        }
        state = newState;

        switch (newState)
        {
            case CombatQuizUIState.Hidden:
                cgroup.alpha = 0;
                cgroup.interactable = false;
                break;
            case CombatQuizUIState.AwaitingText:
                cgroup.interactable = false;
                HideAnswerButtons();
                break;
            case CombatQuizUIState.AwaitingAnswer:
                cgroup.interactable = true;
                StartCoroutine(ShowAnswerButtons());
                break;
            case CombatQuizUIState.Idle:
                cgroup.interactable = false;
                break;
        }
    }




    private void HideAnswerButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        timer.gameObject.SetActive(false);
    }

    private IEnumerator ShowAnswerButtons()
    {
        yield return new WaitForSeconds(buttonPauseTime);
        answerTime = 0;
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        timer.gameObject.SetActive(true);
    }

    public void AwaitAnswer(bool correct)
    {
        if (!correct)
        {
            MarkCorrectAnswer();
        }
        cgroup.interactable = false;
    }

    private void MarkCorrectAnswer()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if (QuizManager.quiz.currentQuestion.CheckAnswer(buttons[i].GetText()))
            {
                buttons[i].MarkCorrect();
                break;
            }
        }
    }
}
