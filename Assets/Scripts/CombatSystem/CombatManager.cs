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
        public QuizManager quiz;

        public Party playerParty;
        public Party enemyParty;

        public bool battleHasStarted = false;
        public TurnOrderManager turnOrder;

        public Text guiText;

        public CombatState battleState;
        

        public void Awake()
        {
            if (battle == null)
            {
                battle = this;
            }
            else {
                Destroy(this);
            }
            quiz = QuizManager.quiz;
        }

        public void Start()
        {
            battleState = CombatState.StartTurn;
            ChangeGUIText("Battle has begun!");
            _InitParties();
            InitTurnOrder();
        }

        public void Update()
        {

            switch (battleState)
            {
                case CombatState.TransitionIn:
                    break;
                case CombatState.StartTurn:
                    StartTurn();
                    string name = turnOrder.GetCurrentFighter().name;
                    ChangeGUIText($"{name}'s turn.");
                    break;
                case CombatState.RunTurn:
                    CombatLoop();
                    break;
                case CombatState.EndTurn:
                    EndTurn();
                    break;
                case CombatState.BattleOver:
                    if (EnemyPartyIsDead())
                    {
                        ChangeGUIText("You won! Press Enter to return");
                    }
                    else {
                        ChangeGUIText("You lost! Press Enter to return");
                    }
                    battleState = CombatState.TransitionOut;
                    
                    break;
                case CombatState.TransitionOut:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene("Load Screen");
                    }
                    break;
            }
        }






        //public functions


        public Fighter GetRandomTarget(Fighter fighter)
        {
            Fighter result = null;

            if (FighterIsPlayer(fighter))
            {
                result = enemyParty.GetRandomLivingFighter();
            }
            else
            {
                result = playerParty.GetRandomLivingFighter();
            }

            return result;
        }


        /*
        public void AddListenerOnQuestionAsked(QuizManager.AskQuestionDelegate method)
        {
            quiz.AddListenerOnQuestionAsked(method);
        }
        public void RemoveListenerOnQuestionAsked(QuizManager.AskQuestionDelegate method)
        {
            quiz.RemoveListenerOnQuestionAsked(method);
        }

        public void AddListenerOnAnswerReceived(QuizManager.ReceiveAnswerDelegate method)
        {
            quiz.AddListenerOnAnswerReceived(method);
        }
        public void RemoveListenerOnAnswerReceived(QuizManager.ReceiveAnswerDelegate method)
        {
            quiz.RemoveListenerOnAnswerReceived(method);
        }

        public void AddListenerReceiveCorrectAnswer(QuizManager.SendAnswerDelegate method)
        {
            quiz.AddListenerReceiveCorrectAnswer(method);
        }
        public void RemoveListenerReceiveCorrectAnswer(QuizManager.SendAnswerDelegate method)
        {
            quiz.RemoveListenerReceiveCorrectAnswer(method);
        }*/

        public Fighter GetCurrentFighter()
        {
            return turnOrder.GetCurrentFighter();
        }








        //combat functions

        private void StartTurn() {
            Fighter currentFighter = turnOrder.GetCurrentFighter();
            currentFighter.StartTurn();
            if (currentFighter.turnHasStarted)
            {
                battleState = CombatState.RunTurn;
            }
        }

        private void CombatLoop()
        {
            Fighter fighter = turnOrder.GetCurrentFighter();
            //Debug.Log("Taking turn");

            if (fighter.TurnIsOver())
            {
                UpdateLivingStatus();
                //fighter.EndTurn();
                battleState = CombatState.EndTurn;
            }
        }

        private void EndTurn() {
            if (BattleIsStillGoing())
            {
                ProgressTurn();
                battleState = CombatState.StartTurn;
            }
            else {
                battleState = CombatState.BattleOver;
            }
        }


        private void ProgressTurn()
        {
            turnOrder.GetNextFighter();
        }


        private void UpdateLivingStatus()
        {
            playerParty.UpdatePartyLivingStatus();
            enemyParty.UpdatePartyLivingStatus();
        }




        




        //Helper functions

        private void _InitParties()
        {
            playerParty = new Party();
            enemyParty = new Party();
            Fighter[] fighters = GameObject.FindObjectsOfType<Fighter>();

            for (int i = 0; i < fighters.Length; i++) {
                Fighter fighter = fighters[i];

                if (FighterIsPlayer(fighter))
                {
                    playerParty.AddPartyMember(fighter);
                }
                else {
                    enemyParty.AddPartyMember(fighter);
                }
            }
        }

        private bool PlayerPartyIsDead() {
            return playerParty.PartyIsDead();
        }

        private bool EnemyPartyIsDead() {
            return enemyParty.PartyIsDead();
        }

        private bool BattleIsStillGoing() {
            return !PlayerPartyIsDead() && !EnemyPartyIsDead();
        }

        private bool FighterIsPlayer(Fighter fighter) {
            return fighter.tag == "Player";
        }

        private void InitTurnOrder() {
            turnOrder = new TurnOrderManager(playerParty, enemyParty);
        }


        public void ChangeGUIText(string newText) {
            guiText.text = newText;
        }

    }
}
