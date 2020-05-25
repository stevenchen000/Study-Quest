using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StatSystem
{
    [CustomEditor(typeof(CharacterStatsSO))]
    public class CharacterStatSOEditor : Editor
    {

        CharacterStatsSO stats;

        public void OnEnable()
        {
            stats = (CharacterStatsSO)target;
        }

        public override void OnInspectorGUI()
        {
            stats.GUI();
        }
    }
}