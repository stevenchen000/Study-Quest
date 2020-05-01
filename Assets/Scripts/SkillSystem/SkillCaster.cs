using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    public class SkillCaster : MonoBehaviour
    {

        public Animator anim;
        public AudioSource audioSource;
        public Skill currentSkill;

        private float previousFrame;
        private float currentFrame;
        private List<SkillObject> skillObjects;


        // Start is called before the first frame update
        void Start()
        {
            SetAnimator();
            SetAudioSource();
        }

        // Update is called once per frame
        void Update()
        {
            if (IsCasting())
            {
                CastCurrentSkill();
            }
        }



        //public functions

        public void CastSkill(Skill skill)
        {
            if (!IsCasting())
            {
                currentSkill = skill;
                ResetTime();
                PrepareSkill(skill);
            }
            else
            {
                Debug.Log("Still casting another skill!");
            }
        }

        public bool IsCasting()
        {
            return currentSkill != null;
        }

        //getters and setters

        public Animator GetAnimator() { return anim; }
        private void SetAnimator() { anim = transform.GetComponentInChildren<Animator>(); }
        public AudioSource GetAudioSource() { return audioSource; }
        private void SetAudioSource() { audioSource = transform.GetComponent<AudioSource>(); }




        //private functions

        private void PrepareSkill(Skill skill)
        {

        }

        private void CastCurrentSkill()
        {

        }

        private void ResetCurrentSkill()
        {
            currentSkill = null;
        }




        //helper functions

        private void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        private void ResetTime()
        {
            previousFrame = 0;
            currentFrame = 0;
        }
    }
}