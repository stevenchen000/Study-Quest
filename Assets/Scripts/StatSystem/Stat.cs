using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StatSystem
{
    [System.Serializable]
    public class Stat
    {
        public StatData statData;

        public string statName = "<Unnamed Stat>";

        public bool isSlider;
        public bool hasAdder;
        public bool hasPercentAdder;
        public bool hasPercentMultiplier;

        [Tooltip("Used for sliding stats")]
        public int currentValue;
        public int baseValue;

        public int addedValue;

        public int percentAdder = 100;
        [Tooltip("Percents written as whole numbers")]
        public int percentMultiplier = 100;
        public bool foldout = false;



        public void AddCurrentValue(int value) {
            currentValue += value;
            currentValue = Mathf.Min(currentValue, GetTotalValue());
        }
        public void RemoveCurrentValue(int value) {
            currentValue -= value;
            currentValue = Mathf.Max(value, 0);
        }
        public void SetCurrentValue(int value)
        {
            currentValue = Mathf.Max(value, 0);
            currentValue = Mathf.Min(currentValue, GetTotalValue());
        }
        public void ResetCurrentValue()
        {
            currentValue = GetTotalValue();
        }

        public void AddBaseValue(int value) { baseValue += value; }
        public void RemoveBaseValue(int value) { baseValue -= value; }
        public void SetBaseValue(int value) { baseValue = value; }
        
        public void AddAddedValue(int value) { addedValue += value; }
        public void RemoveAddedValue(int value) { addedValue -= value; }
        public void SetAddedValue(int value) { addedValue = value; }
        public void ResetAddedValue() { addedValue = 0; }

        public void AddPercentAdder(int value) { percentAdder += value; }
        public void RemovePercentAdder(int value) { percentAdder -= value; }
        public void SetPercentAdder(int value) { percentAdder = value; }
        public void ResetPercentAdder() { percentAdder = 100; }
        
        /// <summary>
        /// Multiplies the percentMultiplier
        /// 100 = 1.0
        /// </summary>
        /// <param name="value"></param>
        public void IncreasePercentMultiplier(int value) { percentMultiplier *= value; }
        public void DecreasePercentMultiplier(int value) { percentMultiplier /= value; }
        public void SetPercentMultiplier(int value) { percentMultiplier = value; }
        public void ResetPercentMultiplier() { percentMultiplier = 100; }

        
        public Stat(string name)
        {
            statName = name;
        }

        public Stat(StatData data)
        {
            statData = data;
            statName = data.statName;
            isSlider = data.isSlider;
            hasAdder = data.hasAdder;
            hasPercentAdder = data.hasPercentAdder;
            hasPercentMultiplier = data.hasPercentMultiplier;
        }

        public Stat(Stat newStat)
        {
            statName = newStat.statName;
            baseValue = newStat.baseValue;
        }


        public void SetStatData(StatData data)
        {
            if(statData != data)
            {
                statData = data;
                statName = data.statName;
                isSlider = data.isSlider;
                hasAdder = data.hasAdder;
                hasPercentAdder = data.hasPercentAdder;
                hasPercentMultiplier = data.hasPercentMultiplier;

                percentMultiplier = 100;

            }
        }

        /// <summary>
        /// If Stat is a slidering stat, then grab the current value
        /// Otherwise, grab the total value
        /// </summary>
        /// <returns></returns>
        public int GetCurrentValue() { return isSlider ? currentValue : GetTotalValue(); }
        
        public int GetTotalValue()
        {
            int addedValue = hasAdder ? this.addedValue : 0;
            int percentAdder = hasPercentAdder ? this.percentAdder : 100;
            int percentMultiplier = hasPercentMultiplier ? this.percentMultiplier : 100;

            return (baseValue + addedValue) * percentAdder / 100 * percentMultiplier / 100;
        }

        public void UpdateStatData(StatData data)
        {
            statData = null;
            SetStatData(data);
        }





        public void GUI()
        {
            EditorGUILayout.BeginHorizontal();
            foldout = EditorGUILayout.Foldout(foldout, statName, true);
            string labelName = statName;
            string labelValue = isSlider ? $"{currentValue} / {GetTotalValue()}" : $"{GetTotalValue()}";
            EditorGUILayout.LabelField($"{labelName}: {labelValue}");
            EditorGUILayout.EndHorizontal();

            
            if (foldout)
            {
                EditorGUI.indentLevel++;

                if (isSlider)
                {
                    int newCurrentValue = (int)EditorGUILayout.Slider("Current Value", currentValue, 0, GetTotalValue());
                    SetCurrentValue(newCurrentValue);
                }

                int newBaseValue = EditorGUILayout.IntField("Base Stat", baseValue);
                SetBaseValue(newBaseValue);

                if (hasAdder)
                {
                    int newAddedValue = EditorGUILayout.IntField("Stat Adder", addedValue);
                    SetAddedValue(newAddedValue);
                }

                if (hasPercentAdder)
                {
                    int newPercentAdder = EditorGUILayout.IntField("Percent Adder", percentAdder);
                    SetPercentAdder(newPercentAdder);
                }

                if (hasPercentMultiplier)
                {
                    int newPercentMultiplier = EditorGUILayout.IntField("Stat Multiplier", percentMultiplier);
                    SetPercentMultiplier(newPercentMultiplier);
                }
                
                EditorGUI.indentLevel--;
            }
        }
    }
}