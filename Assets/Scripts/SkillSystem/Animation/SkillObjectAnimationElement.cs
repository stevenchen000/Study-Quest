using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectAnimationElement
    {
        public SkillObjectAnimationType animationType;
        public float startTime;
        public float duration;
        public float speed;


        public void RunAnimation(SkillObject so, SkillCaster caster, SkillCaster target, Timer timer)
        {
            switch (animationType)
            {
                case SkillObjectAnimationType.MoveForwards:
                    MoveForwards(so);
                    break;
                case SkillObjectAnimationType.TurnTowardsTarget:
                    TurnTowardsTarget(so, target);
                    break;
                case SkillObjectAnimationType.DestroyObject:
                    GameObject.Destroy(so);
                    break;
            }
        }



        //helper functions
        private void MoveForwards(SkillObject so) {
            so.transform.position += so.transform.right * speed * Time.deltaTime * so.transform.localScale.x;
        }

        private void TurnTowardsTarget(SkillObject so, SkillCaster target)
        {
            Vector2 objectForward = so.transform.right * so.transform.localScale.x;
            Vector2 targetVector = target.transform.position - so.transform.position;
            float angleBetween = Vector2.SignedAngle(objectForward, targetVector);
            float clampedAngle = Mathf.Clamp(angleBetween, -speed, speed);

            so.transform.Rotate(new Vector3(0,0,clampedAngle));
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
