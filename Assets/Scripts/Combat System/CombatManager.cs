using DungeonSystem;
using QuizSystem;
using SOEventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CombatSystem
{
    public enum StateEnum
    {
        TransitionIn,
        AskQuestion,
        AnswerQuestion,
        CharacterAttack,
        AwaitTime,
        AwaitAttack,
        BattleOver,
        TransitionOut
    }

    public class CombatManager : FloorManager
    {
        public static CombatManager combat;
        public QuizUI quizUi;

        
        public StateEnum currentState;
        public float currStateDuration = 0;

        public Fighter player;
        public Fighter enemy;

        public float enemyAttackChance = 0;
        public Fighter currentAttacker;
        
        public bool hasAnswered = false;
        public bool answeredCorrectly = false;

        private QuizManager quiz;
        private FloorProjectionManager projection;

        private bool answerTimeSet = false;
        private float answerTime;

        [SerializeField]
        private float strongSkillThreshold;
        [SerializeField]
        private float midSkillThreshold;

        // Start is called before the first frame update
        void Awake()
        {
            if (combat == null)
            {
                combat = this;
                quiz = QuizManager.quiz;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            projection = FindObjectOfType<FloorProjectionManager>();
        }

        // Update is called once per frame
        void Update()
        {
            RunState();
        }




        public override void Initialize()
        {
            ChangeState(StateEnum.AskQuestion);
        }




        #region States

        /* States */

        public void RunState()
        {
            switch (currentState)
            {
                case StateEnum.TransitionIn:
                    _TransitionIn();
                    break;
                case StateEnum.AskQuestion:
                    _AskQuestion();
                    break;
                case StateEnum.AnswerQuestion:
                    _AnswerQuestion();
                    break;
                case StateEnum.CharacterAttack:
                    _CharacterAttack();
                    break;
                case StateEnum.AwaitAttack:
                    _AwaitAttack();
                    break;
                case StateEnum.BattleOver:
                    _BattleOver();
                    break;
                case StateEnum.TransitionOut:
                    _TransitionOut();
                    break;
            }
            currStateDuration += Time.deltaTime;
        }

        private void _TransitionIn()
        {
            
        }

        private void _AskQuestion()
        {
            quiz.AskQuestion();
            ChangeState(StateEnum.AnswerQuestion);
        }

        private void _AnswerQuestion()
        {
            if (hasAnswered)
            {
                ChangeState(StateEnum.AwaitTime);
            }
        }

        private void _AwaitTime()
        {
            if (answerTimeSet)
            {
                answerTimeSet = false;
                ChangeState(StateEnum.CharacterAttack);
            }
        }

        private void _CharacterAttack()
        {
            if (answeredCorrectly)
            {
                player.Attack(enemy, TimeToSkillNumber());
                Debug.Log("Player attacked");
            }
            else
            {
                enemy.Attack(player);
                Debug.Log("Enemy attacked");
            }

            ChangeState(StateEnum.AwaitAttack);
        }

        private void _AwaitAttack()
        {
            if (!enemy.isAttacking && !player.isAttacking)
            {
                CheckBattleState();
            }
        }

        /// <summary>
        /// Called at end of attack animation
        /// </summary>
        private void CheckBattleState()
        {
            hasAnswered = false;
            answeredCorrectly = false;

            enemy.CheckIfDead();
            player.CheckIfDead();

            if (!player.isAttacking && !enemy.isAttacking)
            {
                if (player.IsDead() || enemy.IsDead())
                {
                    ChangeState(StateEnum.BattleOver);
                }
                else
                {
                    ChangeState(StateEnum.AskQuestion);
                }
            }
        }
            

        private void _BattleOver()
        {
            if (player.IsDead())
            {
                Debug.Log("Game over. Press Enter to return");
            }
            else
            {
                Debug.Log("You win!");
            }

            SavePlayerHealth();
            StartCoroutine(_BattleOverTimer());
            ChangeState(StateEnum.TransitionOut);
        }

        private IEnumerator _BattleOverTimer()
        {
            WaitForSeconds timer = new WaitForSeconds(2);

            yield return timer;
            projection.EndDungeonEvent();
            ChangeState(StateEnum.TransitionOut);
        }

        private void _TransitionOut()
        {
            
        }

        private void ChangeState(StateEnum newState)
        {
            switch (currentState)
            {
                case StateEnum.TransitionIn:
                    break;
                case StateEnum.AnswerQuestion:
                    break;
                case StateEnum.CharacterAttack:
                    hasAnswered = false;
                    answeredCorrectly = false;
                    break;
                case StateEnum.BattleOver:
                    break;
                case StateEnum.TransitionOut:
                    break;
            }

            switch (newState)
            {
                case StateEnum.TransitionIn:
                    break;
                case StateEnum.AnswerQuestion:
                    break;
                case StateEnum.CharacterAttack:
                    break;
                case StateEnum.BattleOver:
                    break;
                case StateEnum.TransitionOut:
                    break;
            }
            currentState = newState;
            currStateDuration = 0;
        }



        #endregion




        public void QuestionAnswered(bool correct)
        {
            hasAnswered = true;
            answeredCorrectly = correct;
        }

        


        private void SavePlayerHealth()
        {
            player.data.currentHealth = player.currentHealth;
        }

        public void SetAnswerTime(float time)
        {
            answerTimeSet = true;
            answerTime = time;
            ChangeState(StateEnum.CharacterAttack);
        }

        private int TimeToSkillNumber()
        {
            int result = 0;
            
            if(answerTime < strongSkillThreshold)
            {
                result = 0;
            }else if(answerTime < midSkillThreshold)
            {
                result = 1;
            }
            else
            {
                result = 2;
            }

            return result;
        }
    }
}