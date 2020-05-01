using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    public class SkillAnimation
    {
        public SkillAnimationType animationType;

        public string animationName;

        public AudioClip soundEffect;

        public SkillObject skillObj;
        

        public void RunAnimation(SkillCaster caster)
        {
            switch (animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    Animator anim = caster.GetAnimator();
                    PlayAnimation(anim);
                    break;
                case SkillAnimationType.PlaySound:
                    AudioSource source = caster.GetAudioSource();
                    PlaySound(source);
                    break;
                case SkillAnimationType.InstantiateObject:
                    break;
                case SkillAnimationType.MoveObject:
                    break;
            }
        }




        public void PlayAnimation(Animator anim)
        {
            anim.Play(animationName);
        }

        public void PlaySound(AudioSource source)
        {
            source.clip = soundEffect;
            source.Play();
        }

        public SkillObject CreateSkillObject(SkillCaster caster)
        {
            SkillObject so = null;

            return so;
        }

        public void MoveObjectToPosition(GameObject obj)
        {
            obj.transform.position += obj.transform.forward * 5 * Time.deltaTime;
        }
    }
}
