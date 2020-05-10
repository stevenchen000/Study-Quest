using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(menuName = "Character Stat Preset")]
    public class CharacterStatPreset : ScriptableObject
    {
        public List<Stat> stats;
    }
}
