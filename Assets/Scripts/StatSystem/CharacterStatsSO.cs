using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(menuName = "Stats/Character Stats")]
    public class CharacterStatsSO : ScriptableObject
    {
        public CharacterStatPreset preset;
        public List<Stat> stats = new List<Stat>();
        private List<Stat> savedStats = new List<Stat>();




        private bool ContainsStat(StatData data)
        {
            bool result = false;

            for(int i = 0; i < stats.Count; i++)
            {
                if (stats[i].statName.Equals(data.statName, StringComparison.InvariantCultureIgnoreCase));
            }

            return result;
        }

        private bool StatHasBeenSaved(StatData data)
        {
            bool result = false;

            for(int i = 0; i < savedStats.Count; i++)
            {
                if (savedStats[i].statName.Equals(data.statName))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private Stat GetSavedStat(StatData data)
        {
            Stat result = null;

            for(int i = 0; i < savedStats.Count; i++)
            {
                if(savedStats[i].statName.Equals(data.statName, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = savedStats[i];
                    result.UpdateStatData(data);
                    break;
                }
            }

            return result;
        }

        private void AddStat(StatData data)
        {
            if (!ContainsStat(data))
            {
                if (StatHasBeenSaved(data))
                {
                    stats.Add(GetSavedStat(data));
                }
                else
                {
                    Stat newStat = new Stat(data);
                    stats.Add(newStat);
                    savedStats.Add(newStat);
                }
            }
        }

        private void SetPreset(CharacterStatPreset newPreset)
        {
            if(newPreset != null && newPreset != preset)
            {
                preset = newPreset;
                UpdateStatFromPreset();
            }
        }

        public void UpdateStatFromPreset()
        {
            stats = new List<Stat>();
            for (int i = 0; i < preset.stats.Count; i++)
            {
                StatData newData = preset.stats[i];
                AddStat(newData);
            }
        }

        public void GUI()
        {
            CharacterStatPreset newPreset = (CharacterStatPreset)EditorGUILayout.ObjectField("Stat Preset", preset, typeof(CharacterStatPreset), true);
            if(newPreset != preset)
            {
                SetPreset(newPreset);
            }

            for(int i = 0; i < stats.Count; i++)
            {
                stats[i].GUI();
            }
        }
    }
}
