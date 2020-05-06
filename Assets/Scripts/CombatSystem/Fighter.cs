using QuizSystem;
using SkillSystem;
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
        public bool turnHasStarted = false;
        private bool wasWarned = false;

        public delegate void HealthChanged(float currentHealth, float maxHealth);
        public HealthChanged OnHealthChanged;
        protected CombatManager battle;
        public FighterState state;

        public Vector2 startingPosition;
        public Vector2 castingPosition;
        public float moveDistance = 1f;
        public float distanceThreshold = 0.1f;
        protected bool movedToCastingPosition = false;
        protected bool animationDone = false;
        protected bool hasDealtDamage = false;

        protected bool hasSelectedSkill = false;
        public Skill currentSkill;

        protected Timer timer;


        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Question asked");
            timer = new Timer();
            startingPosition = transform.position;
            castingPosition = startingPosition + (Vector2)(transform.right * moveDistance);
            battle = CombatManager.battle;
            state = FighterState.AwaitingTurn;
            
            battle.AddListenerOnAnswerReceived(QuestionAnswered);
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
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
                    Defend();
                    break;
                case FighterState.EndingTurn:
                    EndTurn();
                    break;
                case FighterState.AwaitingTurn:
                    break;
            }
        }



        //public functions

        public bool TurnIsOver()
        {
            return turnIsOver;
        }

        public virtual void TakeDamage(Fighter fighter)
        {
            int damage = fighter.strength - defense;
            damage = damage < 0 ? 0 : damage;
            damage = answeredCorrectly ? damage / 2 : damage;
            currentHealth -= damage;
            Debug.Log($"{gameObject.name} took {damage} damage.");

            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            battle.ChangeGUIText($"{gameObject.name} took {damage} damage.");
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }


        public Vector2 GetStartingPosition()
        {
            return startingPosition;
        }



        //event functions

        private void QuestionAnswered(bool isCorrect) {
            Debug.Log("Answered question");
            switch (state)
            {
                case FighterState.AnsweringQuestion:
                    state = FighterState.RunningAbility;
                    answeredCorrectly = isCorrect;
                    hasAnswered = true;
                    break;
                case FighterState.Defending:
                    answeredCorrectly = isCorrect;
                    break;
            }
        }







        //combat functions

        public virtual void StartTurn() {
            turnIsOver = false;
            MoveToCastPosition();

            if (movedToCastingPosition)
            {
                state = FighterState.SelectingSkill;
                hasAnswered = false;
                turnHasStarted = true;
            }
        }
        


        public virtual void SelectSkill() {
            //Select a skill to use
            battle.AskQuestion();
            state = FighterState.AnsweringQuestion;
        }

        private void AwaitAnswer() {
            if (hasAnswered) {
                state = FighterState.RunningAbility;
            }
        }


        protected virtual void RunTurn() {
            timer.Tick();

            if (timer.AtTime(1)) { 
                target = battle.GetRandomTarget(this);
                target.TakeDamage(this);
                hasDealtDamage = true;
            }

            if (hasDealtDamage) {
                ReturnToStartingPosition();
            }

            if (animationDone)
            {
                state = FighterState.EndingTurn;
                animationDone = false;
                movedToCastingPosition = false;
                hasDealtDamage = false;
                timer.ResetTimer();
            }
        }

        protected virtual void Defend() {

        }

        public virtual void EndTurn()
        {
            timer.Tick();
            if (timer.AtTime(2))
            {
                state = FighterState.AwaitingTurn;
                turnIsOver = true;
                hasAnswered = false;
                answeredCorrectly = false;
                turnHasStarted = false;
                timer.ResetTimer();
            }
        }

        

        

        /// <summary>
        /// Called by enemy when about to attack
        /// </summary>
        public void WarnOfAttack() {
            wasWarned = true;
            //battle.AskQuestion(AnsweredQuestion);
            state = FighterState.AnsweringQuestion;
        }

        /// <summary>
        /// Called by enemy when turn is over
        /// </summary>
        public void UnwarnOfAttack() {
            wasWarned = false;
            state = FighterState.AwaitingTurn;
        }

        



        protected void MoveToCastPosition() {
            GoToPosition(castingPosition);
            float distanceDifference = (castingPosition - (Vector2)transform.position).magnitude;
            if (distanceDifference <= distanceThreshold)
            {
                movedToCastingPosition = true;
            }
        }

        protected void ReturnToStartingPosition() {
            GoToPosition(startingPosition);
            float distanceDifference = (startingPosition - (Vector2)transform.position).magnitude;
            if (distanceDifference <= distanceThreshold)
            {
                movedToCastingPosition = false;
                animationDone = true;
            }
        }

        protected void GoToPosition(Vector2 position) {
            transform.position = Vector2.Lerp(transform.position, position, 0.2f);
        }
        
    }
}