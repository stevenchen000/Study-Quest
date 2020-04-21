using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class Fighter : MonoBehaviour
    {
        public bool turnIsOver = false;
        public int currentHealth = 100;
        public int maxHealth = 100;
        public int strength = 10;
        public int defense = 5;
        public int speed = 10;

        private Fighter target;
        private bool hasAnswered = false;
        private bool answeredCorrectly = false;

        public delegate void HealthChanged(float currentHealth, float maxHealth);
        public HealthChanged OnHealthChanged;
        private CombatManager battle;

        

        // Start is called before the first frame update
        void Start()
        {
            battle = CombatManager.battle;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartTurn() {
            turnIsOver = false;
            hasAnswered = false;
            answeredCorrectly = false;
            battle.AskQuestion();
            battle.AwaitAnswer(AnsweredQuestion);
        }

        public void RunTurn() {
            if (target == null) {
                target = CombatManager.battle.GetRandomTarget(this);
            }

            if (hasAnswered) {
                target.TakeDamage(this, answeredCorrectly);
                turnIsOver = true;
            }
        }

        public void EndTurn()
        {
            hasAnswered = false;
            answeredCorrectly = false;
        }

        public bool TurnIsOver() {
            return turnIsOver;
        }

        public void TakeDamage(Fighter fighter, bool answeredCorrectly) {
            int damage = fighter.strength - defense;
            damage = damage < 0 ? 0 : damage;
            damage = answeredCorrectly ? damage * 2 : damage;

            currentHealth -= damage;

            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public bool IsDead() {
            return currentHealth <= 0;
        }

        public void AnsweredQuestion(bool isCorrect) {
            answeredCorrectly = isCorrect;
            hasAnswered = true;
        }


        
    }
}