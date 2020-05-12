using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectAnimation
    {
        public List<SkillObjectAnimationElement> animations = new List<SkillObjectAnimationElement>();
        
        public void RunAnimation(SkillObject so, SkillCaster caster, SkillCaster target, Timer timer)
        {
            for(int i = 0; i < animations.Count; i++)
            {
                SkillObjectAnimationElement anim = animations[i];

                if (anim.AnimationIsRunning(timer)) {
                    anim.RunAnimation(so, caster, target, timer);
                }
            }
        }

        public bool AnimationIsDone(Timer timer)
        {
            bool finished = true;

            for (int i = 0; i < animations.Count; i++)
            {
                SkillObjectAnimationElement anim = animations[i];

                if (!anim.AnimationIsDone(timer))
                {
                    finished = false;
                    break;
                }
            }

            return finished;
        }
    }
}