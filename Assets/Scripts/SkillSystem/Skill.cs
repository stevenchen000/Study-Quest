using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    public class Skill : ScriptableObject
    {
        public string skillName;
        public TargetType targettingType;
        public List<SkillObjectCreationData> skillObjectIndices;

        [Tooltip("List of objects to use for skill")]
        public List<GameObject> skillObjects;
    }
}
