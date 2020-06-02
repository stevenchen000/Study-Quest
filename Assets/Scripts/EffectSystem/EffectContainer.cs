using CombatSystem;
using StatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectSystem
{
    public class EffectContainer
    {
        public EffectSO effect;
        public IFighter caster;
        public CharacterStats statsSnapshot;
    }
}
