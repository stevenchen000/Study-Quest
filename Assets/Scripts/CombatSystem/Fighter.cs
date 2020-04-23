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

        public Fighter target;
        public bool hasAnswered = false;
        public bool answeredCorrectly = false;
        private bool wasWarned = false;

        public delegate void HealthChanged(float currentHealth, float maxHealth);
        public HealthChanged OnHealthChanged;
        protected CombatManager battle;
        public FighterState state;

        protected Vector2 startingPosition;
        public float moveDistance = 1f;
        public float distanceThreshold = 0.1f;
        protected bool movedRight = false;
        protected bool animationDone = false;
        protected bool hasDealtDamage = false;
        protected float previousFrame = 0;
        protected float currentFrame = 0;


        // Start is called before the first frame update
        void Start()
        {
            startingPosition = transform.position;
            battle = CombatManager.battle;
            state = FighterState.AwaitingTurn;
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case FighterState.StartingTurn:
                    state = FighterState.SelectingSkill;
                    break;
                case FighterState.SelectingSkill:
                    SelectSkill();
                    break;
                case FighterState.AnsweringQuestion:
                    AwaitAnswer();
                    break;
                case FighterState.RunningAbility:
                    RunTurn();
                    break;
                case FighterState.Defending:
                    break;
                case FighterState.EndingTurn:
                    EndTurn();
                    break;
                case FighterState.AwaitingTurn:
                    break;
            }
        }

        public virtual void StartTurn() {
            if (state == FighterState.AwaitingTurn) {
                state = FighterState.StartingTurn;
                turnIsOver = false;
                hasAnswered = false;
            }
        }

        public virtual void SelectSkill() {
            //Select a skill to use
            state = FighterState.AnsweringQuestion;
            battle.AskQuestion(AnsweredQuestion);
        }

        private void AwaitAnswer() {
            if (hasAnswered) {
                if (wasWarned)
                {
                    state = FighterState.Defending;
                }
                else
                {
                    state = FighterState.RunningAbility;
                }
            }
        }


        protected virtual void RunTurn() {
            Tick();

            if (!movedRight)
            {
                MoveRight();
            }
            else {
                if (!hasDealtDamage) {
                    target = battle.GetRandomTarget(this);
                    target.TakeDamage(this);
                    hasDealtDamage = true;
                }
                MoveLeft();
            }

            if (animationDone)
            {
                state = FighterState.EndingTurn;
                animationDone = false;
                movedRight = false;
                hasDealtDamage = false;
                ResetTime();
            }
        }

        protected virtual void Defend() {

        }

        protected virtual void EndTurn()
        {
            state = FighterState.AwaitingTurn;
            turnIsOver = true;
            hasAnswered = false;
            answeredCorrectly = false;
        }

        public bool TurnIsOver() {
            return turnIsOver;
        }

        public virtual void TakeDamage(Fighter fighter) {
            int damage = fighter.strength - defense;
            damage = damage < 0 ? 0 : damage;
            damage = answeredCorrectly ? damage / 2 : damage;
            currentHealth -= damage;
            Debug.Log($"{gameObject.name} took {damage} damage.");

            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public bool IsDead() {
            return currentHealth <= 0;
        }


        /// <summary>
        /// Used for quiz events
        /// </summary>
        /// <param name="isCorrect"></param>
        public void AnsweredQuestion(bool isCorrect) {
            answeredCorrectly = isCorrect;
            hasAnswered = true;
        }

        /// <summary>
        /// Called by enemy when about to attack
        /// </summary>
        public void WarnOfAttack() {
            wasWarned = true;
            battle.AskQuestion(AnsweredQuestion);
            state = FighterState.AnsweringQuestion;
        }

        /// <summary>
        /// Called by enemy when turn is over
        /// </summary>
        public void UnwarnOfAttack() {
            wasWarned = false;
            state = FighterState.AwaitingTurn;
        }


        protected void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        protected void ResetTime() {
            previousFrame = 0;
            currentFrame = 0;
        }



        protected void MoveRight() {
            Vector2 endPosition = startingPosition + (Vector2)transform.right * moveDistance;
            transform.position = Vector2.Lerp(transform.position, endPosition, 0.2f);

            float distanceDifference = (endPosition - (Vector2)transform.position).magnitude;
            if (distanceDifference <= distanceThreshold)
            {
                movedRight = true;
            }
        }

        protected void MoveLeft() {
            transform.position = Vector2.Lerp(transform.position, startingPosition, 0.2f);
            float distanceDifference = (startingPosition - (Vector2)transform.position).magnitude;
            if (distanceDifference <= distanceThreshold)
            {
                movedRight = false;
                animationDone = true;
            }
        }
    }
}