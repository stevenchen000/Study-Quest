using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;
using UnityEngine.UI;
using QuizSystem;

public class SkillBar : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();
    private List<SkillButton> buttons = new List<SkillButton>();

    public QuizUI quiz;


    // Start is called before the first frame update
    void Start()
    {
        if(quiz == null)
        {
            quiz = FindObjectOfType<QuizUI>();
        }
        buttons.AddRange(transform.GetComponentsInChildren<SkillButton>());
    }

    public void SelectSkill(Skill skill)
    {
        quiz.SelectSkill(skill);
    }

    public void SetSkills(List<Skill> newSkills)
    {
        skills = new List<Skill>(newSkills.Count);
        skills.AddRange(newSkills);
        EnableGUI();

        SetupSkillButtons();
    }



    public void DisableGUI()
    {
        gameObject.SetActive(false);
    }

    public void EnableGUI()
    {
        gameObject.SetActive(true);
    }



    private void SetupSkillButtons()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if(skills.Count > i)
            {
                buttons[i].EnableGUI();
                buttons[i].SetSkill(skills[i]);
            }
            else
            {
                buttons[i].DisableGUI();
            }
        }
    }

}
