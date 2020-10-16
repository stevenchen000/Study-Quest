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
        SelectAction,
        AskQuestion,
        AnswerQuestion,
        CharacterAttack,
        AwaitAttack,
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
        public float currStateDuration = 0;

        public Fighter player;
        public Fighter enemy;

        public float enemyAttackChance = 0;
        public CharacterAction currentAction = CharacterAction.None;
        public Fighter currentAttacker;
        
        public bool hasAnswered = false;
        public bool answeredCorrectly = false;

        private QuizManager quiz;
        private FloorProjectionManager projection;

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

        private void _SelectAction()
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

            ChangeState(StateEnum.AskQuestion);
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
                ChangeState(StateEnum.CharacterAttack);
            }
        }

        private void _CharacterAttack()
        {
            if (answeredCorrectly)
            {
                //player attack
                switch (currentAction)
                {
                    case CharacterAction.Attack:
                        player.Attack(enemy);
                        Debug.Log("Player attacked");
                        break;
                    case CharacterAction.Ability:
                        player.Attack(enemy);
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
                        enemy.Attack(player);
                        Debug.Log("Enemy used a strong attack");
                        break;
                    default:
                        enemy.Attack(player);
                        Debug.Log("Enemy attacked");
                        break;
                }
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
                case StateEnum.SelectAction:
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
                case StateEnum.SelectAction:
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

        


        private void SavePlayerHealth()
        {
            player.data.currentHealth = player.currentHealth;
        }
    }
}