using QuizSystem;
using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using TargettingSystem;
using UnityEngine;

namespace CombatSystem
{
    public class Fighter : MonoBehaviour, ITargettable
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
        public FighterState state = FighterState.AwaitingTurn;

        public Vector2 startingPosition;
        public Vector2 castingPosition;
        public float moveDistance = 1f;
        public float distanceThreshold = 0.1f;
        protected bool movedToCastingPosition = false;
        protected bool animationDone = false;
        protected bool hasDealtDamage = false;
        public bool takingTurn = false;

        protected bool hasSelectedSkill = false;
        public SkillCaster caster;
        public Skill currentSkill;


        public List<Skill> skills = new List<Skill>();

        public Vector2 forwardVector;

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
            caster = transform.GetComponent<SkillCaster>();
            forwardVector = transform.right;
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case FighterState.SelectingSkill:
                    AwaitSkill();
                    break;
                case FighterState.AnsweringQuestion:
                    AwaitAnswer();
                    break;
                case FighterState.Acting:
                    RunTurn();
                    break;
                /*case FighterState.Defending:
                    Defend();
                    break;*/
                case FighterState.EndingTurn:
                    EndTurn();
                    break;
                case FighterState.AwaitingTurn:
                    break;
            }

            /*if (Input.GetKeyDown(KeyCode.F))
            {
                AnswerQuestion(true);
                SelectSkill(testSkill);
            }*/

            //Debug.DrawRay(transform.position, GetForwardVector() * 5, Color.blue);
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

        public Vector2 GetForwardVector()
        {
            return forwardVector;
        }












        //combat functions
        

        protected virtual void AwaitSkill() {
            if (currentSkill != null)
            {
                state = FighterState.AnsweringQuestion;
            }
        }

        public void SelectSkill(Skill skill)
        {
            if (currentSkill == null && state == FighterState.SelectingSkill)
            {
                Debug.Log("test");
                currentSkill = skill;
                state = FighterState.AnsweringQuestion;
            }
        }

        private void AwaitAnswer() {
            if (hasAnswered) {
                state = FighterState.Acting;
            }
        }

        public void AnswerQuestion(bool isCorrect)
        {
            if (state == FighterState.AnsweringQuestion)
            {
                hasAnswered = true;
                answeredCorrectly = isCorrect;
            }
        }


        protected virtual void RunTurn() {
            if (answeredCorrectly)
            {
                timer.Tick();

                if (timer.AtTime(0))
                {
                    target = battle.GetRandomTarget(this);
                    caster.CastSkill(currentSkill);
                    target.TakeDamage(this);
                    hasDealtDamage = true;
                }

                if (animationDone)
                {
                    state = FighterState.EndingTurn;
                    animationDone = false;
                    movedToCastingPosition = false;
                    hasDealtDamage = false;
                    timer.ResetTimer();
                }
                else
                {
                    if (!caster.IsCasting())
                    {
                        animationDone = true;
                    }
                }
            }
            else
            {
                state = FighterState.EndingTurn;
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
                currentSkill = null;
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





        //ITargettable functions

        public string GetTag()
        {
            return tag;
        }

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void HealDamage(int damage)
        {
            throw new System.NotImplementedException();
        }



        //ITurnTaker functions

        public void StartTurn()
        {
            takingTurn = true;
            turnIsOver = false;
            turnHasStarted = true;
            state = FighterState.SelectingSkill;
        }


        public bool IsTakingTurn()
        {
            return takingTurn;
        }
    }
}