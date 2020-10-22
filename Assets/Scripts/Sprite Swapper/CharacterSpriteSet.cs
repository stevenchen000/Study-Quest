using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteSwapper
{
    [Serializable]
    public class SpriteSetData {
        public string partName;
        public Sprite sprite;
    }

    [CreateAssetMenu(menuName = "Sprite Swapper/Sprite Swap Palette")]
    public class CharacterSpriteSet : ScriptableObject
    {
        public List<SpriteSetData> data = new List<SpriteSetData>();
    }
}