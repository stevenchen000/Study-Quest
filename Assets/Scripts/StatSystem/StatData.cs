using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatSystem
{
    [CreateAssetMenu(menuName = "Stats/Stat Data")]
    public class StatData : ScriptableObject
    {
        public string statName;
        public bool isSlider = false;
        public bool hasAdder = false;
        public bool hasPercentAdder = false;
        public bool hasPercentMultiplier = false;
    }
}