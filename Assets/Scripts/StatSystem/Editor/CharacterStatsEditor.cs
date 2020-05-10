using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StatSystem
{
    [CustomEditor(typeof(CharacterStats))]
    public class CharacterStatsEditor : Editor
    {

        private CharacterStats stats;
        public static CharacterStatPreset preset;

        public void OnEnable()
        {
            stats = (CharacterStats)target;
        }

        public override void OnInspectorGUI()
        {
            stats.GUI();

            GUILayout.Space(10);

            preset = (CharacterStatPreset)EditorGUILayout.ObjectField("Stat Preset", preset, typeof(CharacterStatPreset));

            if (GUILayout.Button("Apply Preset") && preset != null)
            {
                stats.SetStats(preset.stats);
            }
        }
    }
}