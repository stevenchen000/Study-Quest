using DungeonSystem;
using QuizSystem;
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
        SelectAction,
        AnswerQuestion,
        CharacterAttack,
        BattleOver,
        TransitionOut
    }

    public enum CharacterAction
    {
        Attack,
        Ability,
        Flee,
        None
    }

    public class CombatManager : FloorManager
    {
        public static CombatManager combat;
        public QuizUI quizUi;

        public StateEnum currentState;
        public StateEnum newState;
        public float currStateDuration = 0;

        public Fighter player;
        public Fighter enemy;

        public float enemyAttackChance = 0;
        public CharacterAction currentAction = CharacterAction.None;
        public Fighter currentAttacker;
        
        public bool hasAnswered = false;
        public bool answeredCorrectly = false;

        private QuizManager quiz;

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
            //DeactivateQuizUI();
            quiz.SubscribeToOnQuestionAnswered(QuestionAnswered);
        }

        // Update is called once per frame
        void Update()
        {
            RunState();

            if(currentState == newState)
            {
                currStateDuration += Time.deltaTime;
            }
            else
            {
                currentState = newState;
                currStateDuration = 0;
            }
        }

        private void OnDestroy()
        {
            quiz.UnsubscribeFromOnQuestionAnswered(QuestionAnswered);
        }




        public override void Initialize()
        {
            ChangeState(StateEnum.SelectAction);
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
                case StateEnum.SelectAction:
                    _SelectAction();
                    break;
                case StateEnum.AnswerQuestion:
                    _AnswerQuestion();
                    break;
                case StateEnum.CharacterAttack:
                    _CharacterAttack();
                    break;
                case StateEnum.BattleOver:
                    _BattleOver();
                    break;
                case StateEnum.TransitionOut:
                    _TransitionOut();
                    break;
            }
        }

        private void _TransitionIn()
        {
            
        }

        private void _SelectAction()
        {
            if (currStateDuration == 0)
            {
                float rand = UnityEngine.Random.Range(0,1);

                if(rand < enemyAttackChance)
                {
                    currentAction = CharacterAction.Ability;
                }
                else
                {
                    currentAction = CharacterAction.Attack;
                }
            }
            else
            {
                //await action
            }

            if(currentAction != CharacterAction.None)
            {
                ChangeState(StateEnum.AnswerQuestion);
            }
        }

        private void _AnswerQuestion()
        {
            if(currStateDuration == 0)
            {
                quiz.AskQuestion();
            }

            if (hasAnswered)
            {
                ChangeState(StateEnum.CharacterAttack);
            }
        }

        private void _CharacterAttack()
        {
            if (currStateDuration == 0)
            {
                if (answeredCorrectly)
                {
                    //player attack
                    switch (currentAction)
                    {
                        case CharacterAction.Attack:
                            enemy.TakeDamage(1);
                            player.PlayAnimation("Attack");
                            Debug.Log("Player attacked");
                            break;
                        case CharacterAction.Ability:
                            enemy.TakeDamage(2);
                            player.PlayAnimation("Attack");
                            Debug.Log("Player counterattacked");
                            break;
                        case CharacterAction.Flee:
                            break;
                        case CharacterAction.None:
                            break;
                    }
                }
                else
                {
                    //enemy attack
                    switch (currentAction)
                    {
                        case CharacterAction.Ability:
                            player.TakeDamage(2);
                            enemy.PlayAnimation("Attack");
                            Debug.Log("Enemy used a strong attack");
                            break;
                        default:
                            player.TakeDamage(1);
                            enemy.PlayAnimation("Attack");
                            Debug.Log("Enemy attacked");
                            break;
                    }
                }
                
            }
        }

        /// <summary>
        /// Called at end of attack animation
        /// </summary>
        public void CheckBattleState()
        {
            hasAnswered = false;
            answeredCorrectly = false;

            if (!player.isAttacking && !enemy.isAttacking)
            {
                if (player.IsDead() || enemy.IsDead())
                {
                    ChangeState(StateEnum.BattleOver);
                }
                else
                {
                    ChangeState(StateEnum.SelectAction);
                }
            }
        }
            

        private void _BattleOver()
        {
            if (currStateDuration == 0)
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
            }
        }

        private IEnumerator _BattleOverTimer()
        {
            WaitForSeconds timer = new WaitForSeconds(2);

            yield return timer;

            ChangeState(StateEnum.TransitionOut);
        }

        private void _TransitionOut()
        {
            DungeonManager manager = FindObjectOfType<DungeonManager>();
            manager.FinishFloor();
        }

        private void ChangeState(StateEnum state)
        {
            newState = state;
        }


        public void SelectAction(CharacterAction action)
        {
            currentAction = action;
        }

        #endregion




        public void QuestionAnswered(bool correct)
        {
            hasAnswered = true;
            answeredCorrectly = correct;
        }










        private void DeactivateQuizUI()
        {
            quizUi.gameObject.SetActive(false);
        }

        public void ActivateQuizUI()
        {
            //quizUi.gameObject.SetActive(true);
            quiz.AskQuestion();
        }




        private void SavePlayerHealth()
        {
            player.data.currentHealth = player.currentHealth;
        }
    }
}