using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(menuName = "Stats/Character Stat Preset")]
    public class CharacterStatPreset : ScriptableObject
    {
        public List<StatData> stats = new List<StatData>();
    }
}
