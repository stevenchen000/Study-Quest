using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StatSystem
{
    [System.Serializable]
    public class Stat
    {
        public string statName;
        [SerializeField]
        private int baseValue;
        private int addedValue;
        [Tooltip("Percents written as whole numbers")]
        private int percentMultiplier = 100;
        private bool foldout = false;



        public void AddBaseValue(int value) { baseValue += value; }
        public void RemoveBaseValue(int value) { baseValue -= value; }
        public void SetBaseValue(int value) { baseValue = value; }
        
        public void AddAddedValue(int value) { addedValue += value; }
        public void RemoveAddedValue(int value) { addedValue -= value; }
        public void SetAddedValue(int value) { addedValue = value; }
        public void ResetAddedValue() { addedValue = 0; }
        
        public void AddPercentMultiplier(int value) { percentMultiplier += value; }
        public void RemovePercentMultiplier(int value) { percentMultiplier -= value; }
        public void SetPercentMultiplier(int value) { percentMultiplier = value; }
        public void ResetPercentMultiplier() { percentMultiplier = 100; }

        
        public Stat(string name)
        {
            statName = name;
        }

        public Stat(Stat newStat)
        {
            statName = newStat.statName;
            baseValue = newStat.baseValue;
        }


        
        public int GetCurrentValue()
        {
            return (baseValue + addedValue) * percentMultiplier / 100;
        }


        public void GUI()
        {
            EditorGUILayout.BeginHorizontal();
            foldout = EditorGUILayout.Foldout(foldout, statName);
            EditorGUILayout.LabelField($"Total Value: {GetCurrentValue()}");
            EditorGUILayout.EndHorizontal();

            
            if (foldout)
            {
                EditorGUI.indentLevel++;
                statName = EditorGUILayout.TextField("Stat Name", statName);
                baseValue = EditorGUILayout.IntField("Base Stat", baseValue);
                addedValue = EditorGUILayout.IntField("Stat Adder", addedValue);
                percentMultiplier = EditorGUILayout.IntField("Stat Multiplier", percentMultiplier);

                
                EditorGUI.indentLevel--;
            }
        }
    }
}