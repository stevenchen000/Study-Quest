using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ControllerSystem
{
    public abstract class ControllerAI : ScriptableObject
    {
        public abstract void ControlCharacter(Fighter fighter, CombatManager battle);

        public abstract void RunTurn(Fighter fighter, CombatManager battle);

        public abstract void EndTurn(Fighter fighter, CombatManager battle);

        public abstract void SelectSkill(Fighter fighter, CombatManager battle);
    }
}
