using QuizSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
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
                        ChangeGUIText("You won!");
                    }
                    else {
                        ChangeGUIText("You lost. Better luck next time!");
                    }
                    break;
                case CombatState.TransitionOut:
                    break;
            }
        }

        private void StartTurn() {
            turnOrder.GetCurrentFighter().StartTurn();
            battleState = CombatState.RunTurn;
        }

        private void CombatLoop()
        {
            Fighter fighter = turnOrder.GetCurrentFighter();
            //Debug.Log("Taking turn");

            if (fighter.TurnIsOver())
            {
                UpdateLivingStatus();
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


        /*public void AskQuestion() {
            quiz.AskQuestion();
            quiz.AddListener(awaitAnswerFunction);
        }*/
        





        public void ProgressTurn() {
            turnOrder.GetNextFighter();
        }

        public Fighter GetRandomTarget(Fighter fighter) {
            Fighter result = null;
            
            if (FighterIsPlayer(fighter))
            {
                result = enemyParty.GetRandomLivingFighter();
            }
            else {
                result = playerParty.GetRandomLivingFighter();
            }
            
            return result;
        }

        public void UpdateLivingStatus() {
            playerParty.UpdatePartyLivingStatus();
            enemyParty.UpdatePartyLivingStatus();
        }

        
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
