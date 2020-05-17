using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EffectSystem
{
    public class EffectSO : ScriptableObject
    {
        public List<Effect> effects = new List<Effect>();
        /// <summary>
        /// Effects that run at start
        /// </summary>
        public List<Effect> applyEffects = new List<Effect>();
        /// <summary>
        /// Effects that run when effect expires naturally
        /// </summary>
        public List<Effect> endEffects = new List<Effect>();
        /// <summary>
        /// Effects that run when effect is forcefully removed
        /// </summary>
        public List<Effect> removeEffects = new List<Effect>();
        /// <summary>
        /// Effects that run either every frame or at an interval
        /// </summary>
        public List<Effect> updateEffects = new List<Effect>();


        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
            switch (effect.timing)
            {
                case EffectTiming.OnApply:
                    applyEffects.Add(effect);
                    break;
                case EffectTiming.EveryFrame:
                    updateEffects.Add(effect);
                    break;
                case EffectTiming.AtInterval:
                    updateEffects.Add(effect);
                    break;
                case EffectTiming.OnExpire:
                    endEffects.Add(effect);
                    break;
                case EffectTiming.OnDispel:
                    removeEffects.Add(effect);
                    break;
            }
        }

        public void RemoveEffect(int index)
        {
            Effect effect = effects[index];
            effects.RemoveAt(index);

            switch (effect.timing)
            {
                case EffectTiming.OnApply:
                    applyEffects.Remove(effect);
                    break;
                case EffectTiming.EveryFrame:
                    updateEffects.Remove(effect);
                    break;
                case EffectTiming.AtInterval:
                    updateEffects.Remove(effect);
                    break;
                case EffectTiming.OnExpire:
                    endEffects.Remove(effect);
                    break;
                case EffectTiming.OnDispel:
                    removeEffects.Remove(effect);
                    break;
            }
        }
    }
}
