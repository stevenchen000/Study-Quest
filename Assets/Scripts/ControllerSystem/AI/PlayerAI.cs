using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatSystem;
using UnityEngine;

namespace ControllerSystem
{
    public class PlayerAI : ControllerAI
    {
        public override void ControlCharacter(Fighter fighter, CombatManager battle)
        {
            
        }

        public override void EndTurn(Fighter fighter, CombatManager battle)
        {

        }

        public override void RunTurn(Fighter fighter, CombatManager battle)
        {
            if (fighter.target == null)
            {
                fighter.target = battle.GetRandomTarget(fighter);
                Debug.Log(fighter.target);
            }

            fighter.target.TakeDamage(fighter);
            fighter.state = FighterState.EndingTurn;
        }

        public override void SelectSkill(Fighter fighter, CombatManager battle)
        {
            fighter.state = FighterState.AnsweringQuestion;
           // battle.AskQuestion(fighter.AnsweredQuestion);
        }
    }
}
