using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectAnimationElement
    {
        public float startTime;
        public float duration;



        public void RunAnimation()
        {

        }

        public bool AnimationIsRunning(Timer timer)
        {
            return timer.DuringTime(startTime, startTime + duration);
        }

        public bool AnimationIsDone(Timer timer)
        {
            return timer.PassedTime(startTime + duration);
        }
    }
}
