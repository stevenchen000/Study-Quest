using QuizSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CombatSystem
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager battle;

        public QuizUI quizUI;

        public IFighter player;
        public IFighter enemy;
        
        public CombatState battleState;

        public HealthBar playerHealthBar;
        public HealthBar enemyHealthBar;
        

        public void Awake()
        {
            if (battle == null)
            {
                battle = this;
            }
            else {
                Destroy(this);
            }
            
        }

        public void Start()
        {
            battleState = CombatState.AwaitingInput;
            quizUI.AskQuestion();

            player = FindObjectOfType<PlayerFighter>();
            playerHealthBar.SetTarget(player);
            enemy = FindObjectOfType<EnemyFighter>();
            enemyHealthBar.SetTarget(enemy);
        }

        public void Update()
        {

            switch (battleState)
            {
                case CombatState.TransitionIn:
                    break;
                case CombatState.AwaitingInput:

                    break;
                case CombatState.Attacking:
                    Attacking();
                    break;
                case CombatState.BattleOver:
                    if (EnemyIsDead())
                    {
                        Debug.Log("You won!");
                    }
                    else
                    {
                        Debug.Log("Game Over");
                    }
                    battleState = CombatState.TransitionOut;
                    break;
                case CombatState.TransitionOut:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene("Test_Dungeon");
                    }
                    break;
            }
        }

        
        private void Attacking()
        {
            if(!player.IsAttacking() && !enemy.IsAttacking())
            {
                if(PlayerIsDead() || EnemyIsDead())
                {
                    battleState = CombatState.BattleOver;
                }
                else
                {
                    battleState = CombatState.AwaitingInput;
                    quizUI.AskQuestion();
                }
            }
        }





        //public functions


        public IFighter GetTarget(IFighter fighter)
        {
            return fighter == player ? enemy : player;
        }
        
        
        public void AnswerQuestion(bool isCorrect)
        {
            if (battleState == CombatState.AwaitingInput)
            {
                if (isCorrect)
                {
                    player.Attack(enemy);
                }
                else
                {
                    enemy.Attack(player);
                }
                battleState = CombatState.Attacking;
            }
        }






        //combat functions
        

        private bool PlayerIsDead() {
            return player.IsDead();
        }

        private bool EnemyIsDead() {
            return enemy.IsDead();
        }

        private bool BattleIsStillGoing() {
            return !PlayerIsDead() && !EnemyIsDead();
        }


    }
}
