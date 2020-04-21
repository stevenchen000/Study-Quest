using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace CombatSystem
{
    [System.Serializable]
    public class Party
    {
        public List<Fighter> allFighters;
        public List<Fighter> livingFighters;
        public List<Fighter> deadFighters;

        public Party() {
            allFighters = new List<Fighter>();
            livingFighters = new List<Fighter>();
            deadFighters = new List<Fighter>();
        }

        public void UpdatePartyLivingStatus() {
            int index = 0;

            while (index < livingFighters.Count) {
                if (livingFighters[index].IsDead()) {
                    deadFighters.Add(livingFighters[index]);
                    livingFighters.RemoveAt(index);
                    continue;
                }
                index++;
            }
        }


        public void AddPartyMember(Fighter fighter) {
            allFighters.Add(fighter);

            if (fighter.IsDead())
            {
                deadFighters.Add(fighter);
            }
            else {
                livingFighters.Add(fighter);
            }
        }

        public bool PartyIsDead() {
            return livingFighters.Count == 0;
        }

        public Fighter GetFirstLivingFighter() {
            Fighter result = null;
            if (!PartyIsDead()) {
                result = livingFighters[0];
            }

            return result;
        }

        public Fighter GetRandomLivingFighter() {
            Fighter result = null;

            if (!PartyIsDead()) {
                int rand = Random.Range(0, GetNumberOfLivingMembers());
                result = livingFighters[rand];
            }

            return result;
        }

        public int GetNumberOfLivingMembers() {
            return livingFighters.Count;
        }

        public bool FighterIsInParty(Fighter fighter) {
            return allFighters.Contains(fighter);
        }
    }
}
