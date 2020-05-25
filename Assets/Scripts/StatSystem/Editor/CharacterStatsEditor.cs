﻿using System.Collections;
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
        }
    }
}