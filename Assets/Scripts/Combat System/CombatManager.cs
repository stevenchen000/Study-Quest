using QuizSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public class CombatManager : MonoBehaviour
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

        // Start is called before the first frame update
        void Awake()
        {
            if (combat == null)
            {
                combat = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            DeactivateQuizUI();
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
            ChangeState(StateEnum.AnswerQuestion);
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
                ActivateQuizUI();
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
                            Debug.Log("Player attacked");
                            break;
                        case CharacterAction.Ability:
                            enemy.TakeDamage(2);
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
                            Debug.Log("Enemy used a strong attack");
                            break;
                        default:
                            player.TakeDamage(1);
                            Debug.Log("Enemy attacked");
                            break;
                    }
                }
            }

            if(player.IsDead() || enemy.IsDead())
            {
                ChangeState(StateEnum.BattleOver);
            }
            else
            {
                ChangeState(StateEnum.SelectAction);
            }
        }

        private void _BattleOver()
        {
            if (currStateDuration == 0)
            {
                if (player.IsDead())
                {
                    //display game over screen
                }
                else
                {
                    //display victory screen
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    ChangeState(StateEnum.TransitionOut);
                }
            }
        }

        private void _TransitionOut()
        {
            if (player.IsDead())
            {
                //Go back to dungeon
            }
            else
            {
                //Go back to title screen
            }
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
            quizUi.gameObject.SetActive(true);
            quizUi.AskQuestion();
        }

        /*public void ChangeState(int index)
        {
            
            if (currState != null)
            {
                currState.EndState(this);
            }
            currState = states.states[index];
            currState.StartState(this);
        }

        public void ChangeState(CombatState state)
        {
            if (currState != null)
            {
                currState.EndState(this);
            }
            currState = state;
            currState.StartState(this);
        }*/

    }
}