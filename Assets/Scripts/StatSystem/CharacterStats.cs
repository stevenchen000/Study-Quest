using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace StatSystem
{
    public class CharacterStats : MonoBehaviour, IStatContainer
    {
        public CharacterStatPreset preset;
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

        

        public void SetStats(CharacterStatPreset newPreset)
        {
            if (newPreset != preset && newPreset != null)
            {
                stats = new List<Stat>();
                for (int i = 0; i < newPreset.stats.Count; i++)
                {
                    StatData newStatData = newPreset.stats[i];
                    AddNewStat(newStatData);
                }
                preset = newPreset;
            }
        }


        public void AddNewStat(StatData data)
        {
            string statName = data.statName;

            if (!_statsDict.ContainsKey(statName))
            {
                stats.Add(new Stat(data));
            }
        }


        public void GUI()
        {
            CharacterStatPreset newPreset = (CharacterStatPreset)EditorGUILayout.ObjectField("Stat Preset", preset, typeof(CharacterStatPreset), true);
            if (newPreset != preset)
            {
                SetStats(newPreset);
                EditorUtility.SetDirty(this);
            }

            for(int i = 0; i < stats.Count; i++)
            {
                stats[i].GUI();
            }
        }
    }
}
