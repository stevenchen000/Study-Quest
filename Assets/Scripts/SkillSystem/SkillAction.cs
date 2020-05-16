using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    [System.Serializable]
    public class SkillAction
    {
        public SkillActionType animationType;
        public float startTime;
        public float duration;

        public string animationName;

        public AudioClip soundEffect;
        
        public int gameObjectIndex;

        public SkillObject so;
        public SkillObjectTarget targetType;
        public Vector2 offset;

        public SkillObjectCreationData data;

        public void RunAnimation(SkillCaster caster, Skill skill)
        {
            switch (animationType)
            {
                case SkillActionType.PlayAnimation:
                    Animator anim = caster.GetAnimator();
                    PlayAnimation(anim);
                    break;
                case SkillActionType.PlaySound:
                    AudioSource source = caster.GetAudioSource();
                    PlaySound(source);
                    break;
                case SkillActionType.InstantiateObject:
                    InstantiateSkillObject(caster, skill, data);
                    break;
                case SkillActionType.MoveObject:
                    MoveObjectToPosition(caster);
                    break;
            }
        }

        public bool IsRunning(Timer timer)
        {
            return timer.AtTime(startTime);
        }

        public bool IsFinished(Timer timer)
        {
            return timer.PassedTime(startTime + duration);
        }


        private void PlayAnimation(Animator anim)
        {
            anim.Play(animationName);
        }

        private void PlaySound(AudioSource source)
        {
            source.clip = soundEffect;
            source.Play();
        }

        private SkillObject CreateSkillObject(SkillCaster caster)
        {
            SkillObject so = null;

            return so;
        }

        private void MoveObjectToPosition(SkillCaster caster)
        {
            switch (targetType)
            {
                case SkillObjectTarget.Caster:
                    caster.transform.position = caster.transform.position + 
                                                caster.transform.forward * offset.x +
                                                caster.transform.up * offset.y;
                    break;
                case SkillObjectTarget.Creator:
                    break;
                case SkillObjectTarget.Target:
                    SkillCaster target = caster.GetTarget();
                    Vector3 targetPosition = target.GetStartingPosition();

                    caster.transform.position = targetPosition +
                                                target.transform.right * offset.x * target.transform.localScale.x +
                                                target.transform.up * offset.y;
                    Debug.Log("Moved to target position");
                    break;
            }

        }

        private void InstantiateSkillObject(SkillCaster caster, Skill skill, SkillObjectCreationData data)
        {
            SkillObject obj = PoolManager.pool.PullObject(0).GetComponent<SkillObject>();
            obj.SetupSkillObject(caster, skill, data);
        }
    }
}
