using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSystem;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Skill skill;
    public SkillBar skillBar;

    public Text skillTextUI;

    private void Start()
    {
        if(skillBar == null)
        {
            skillBar = FindObjectOfType<SkillBar>();
        }
    }


    public void SelectSkill()
    {
        skillBar.SelectSkill(skill);
        skillBar.DisableGUI();
    }

    public void SetSkill(Skill newSkill)
    {
        skill = newSkill;
        skillTextUI.text = skill.skillName;
    }

    public void EnableGUI()
    {
        gameObject.SetActive(true);
    }

    public void DisableGUI()
    {
        gameObject.SetActive(false);
    }
}