using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class TurnOrderManager
    {
        private List<Fighter> fighters;
        private int currentTurn = 0;
        private Fighter currentFighter;

        public TurnOrderManager(params Party[] partyList) {
            fighters = new List<Fighter>();

            for (int i = 0; i < partyList.Length; i++) {
                AddPartyToList(partyList[i]);
            }

            currentFighter = fighters[0];
        }

        public Fighter GetCurrentFighter() {
            return currentFighter;
        }

        /// <summary>
        /// Gets the next living fighter in the list
        /// </summary>
        /// <returns></returns>
        public Fighter GetNextFighter() {
            do
            {
                currentTurn = (currentTurn + 1) % fighters.Count;
                currentFighter = fighters[currentTurn];
            } while (currentFighter.IsDead());

            return currentFighter;
        }

        /// <summary>
        /// Returns the fighter turnsAhead in the future
        /// </summary>
        /// <param name="turnsAhead"></param>
        /// <returns></returns>
        public Fighter GetFutureFighter(int turnsAhead) {
            Fighter result = null;
            int turnsPassed = 0;
            int turnToCheck = currentTurn;
            
            while(turnsPassed < turnsAhead)
            {
                turnToCheck = (turnToCheck + 1) % fighters.Count;
                result = fighters[turnToCheck];
                if (!result.IsDead()) {
                    turnsPassed++;

                    if (turnsPassed == turnsAhead) {
                        result = fighters[turnToCheck];
                    }
                }
            } 

            return result;
        }





        //Private functions

        public void AddPartyToList(Party party) {
            List<Fighter> fighters = party.allFighters;
            for (int i = 0; i < fighters.Count; i++) {
                AddFighterToList(fighters[i]);
            }
        }

        public void AddFighterToList(Fighter fighter) {
            int left = 0;
            int right = fighters.Count;

            while (left < right) {
                int middle = left + right / 2;

                if (FighterIsFaster(fighter, fighters[middle]))
                {
                    right = middle;
                }
                else if (TargetIsFaster(fighter, fighters[middle]))
                {
                    left = middle;
                }
                else {
                    left = middle;
                    right = middle;
                }
            }

            fighters.Insert(left, fighter);
        }
        



        private bool FighterIsFaster(Fighter fighter, Fighter target) {
            return fighter.speed > target.speed;
        }

        private bool TargetIsFaster(Fighter fighter, Fighter target) {
            return target.speed > fighter.speed;
        }

    }
}
