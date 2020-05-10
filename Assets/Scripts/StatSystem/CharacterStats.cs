using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StatSystem
{
    public class CharacterStats : MonoBehaviour
    {

        public List<Stat> stats = new List<Stat>();
        private Dictionary<string, Stat> _statsDict = new Dictionary<string, Stat>();



        public int GetStatValue(string statName)
        {
            int result = 0;
            bool resultFound = false;

            for(int i = 0; i < stats.Count; i++)
            {
                if (stats[i].statName.Equals(statName, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = stats[i].GetCurrentValue();
                    resultFound = true;
                    break;
                }
            }

            if (!resultFound)
            {
                Debug.Log("Stat not found");
            }

            return result;
        }

        public void SetStats(List<Stat> newStats)
        {
            stats = new List<Stat>();
            for(int i = 0; i < newStats.Count; i++)
            {
                Stat newStat = newStats[i];
                this.stats.Add(new Stat(newStat));
            }
        }





        public void GUI()
        {
            int index = -1;

            for(int i = 0; i < stats.Count; i++)
            {
                stats[i].GUI();
                /*if(GUILayout.Button("Remove Stat"))
                {
                    index = i;
                }*/
            }

            if (index >= 0)
            {
                stats.RemoveAt(index);
            }
            /*
            if(GUILayout.Button("Add Stat"))
            {
                stats.Add(new Stat());
            }*/
        }
    }
}
