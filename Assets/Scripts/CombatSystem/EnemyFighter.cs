using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class EnemyFighter : Fighter
    {
        
        
        public override void SelectSkill()
        {
            state = FighterState.RunningAbility;
            target = battle.GetRandomTarget(this);
            
            //target.WarnOfAttack();
            //battle.AskQuestion(AwaitingAnswer);
        }

        protected override void Defend()
        {

        }

        protected override void RunTurn()
        {
            timer.Tick();

            if (timer.AtTime(1)) {
                state = FighterState.EndingTurn;
                target.TakeDamage(this);
            }
        }

        public override void EndTurn()
        {
            timer.Tick();

            if (timer.AtTime(2))
            {
                timer.ResetTimer();
                state = FighterState.AwaitingTurn;
                target.UnwarnOfAttack();
                turnIsOver = true;
                hasAnswered = false;
                answeredCorrectly = false;
            }
        }

        public override void TakeDamage(Fighter fighter)
        {
            int damage = fighter.strength - defense;
            damage = damage < 0 ? 0 : damage;
            damage = fighter.answeredCorrectly ? damage * 2 : damage;
            currentHealth -= damage;
            Debug.Log($"{gameObject.name} took {damage} damage.");

            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            battle.ChangeGUIText($"{gameObject.name} took {damage} damage.");
        }

        public void AwaitingAnswer(bool isCorrect) {
            hasAnswered = true;
        }
    }
}