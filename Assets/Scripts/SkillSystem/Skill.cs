using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(menuName = "Skill")]
    public class Skill : ScriptableObject
    {
        public string skillName;
        public TargetType targettingType;
        public float totalTime;

        public List<SkillAction> actions;

        [Tooltip("List of objects to use for skill")]
        public List<GameObject> skillObjects;

        public void RunSkill(SkillCaster caster, Timer timer)
        {

        }
    }
}
