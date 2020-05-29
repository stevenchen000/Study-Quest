using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    public class SkillCaster : MonoBehaviour
    {
        public Fighter user;

        public Animator anim;
        public AudioSource audioSource;
        public Skill currentSkill;

        public Skill testSkill;

        private Timer timer;
        private List<SkillObject> skillObjects;
        private List<SkillAction> skillActions;
        public SkillCaster target;


        // Start is called before the first frame update
        void Start()
        {
            SetAnimator();
            SetAudioSource();
            timer = new Timer();
            user = transform.GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.T))
            {
                CastSkill(testSkill);
            }*/

            if (IsCasting())
            {
                timer.Tick();
                RunSkill();
            }
        }



        //important functions

        private void RunSkill()
        {
            for(int i = 0; i < skillActions.Count; i++)
            {
                SkillAction action = skillActions[i];
                if (action.IsRunning(timer))
                {
                    action.RunAnimation(this, currentSkill);
                }
            }

            if (timer.AtTime(currentSkill.totalTime))
            {
                ResetCurrentSkill();
            }
        }





        //public functions

        public void CastSkill(Skill skill)
        {
            if (!IsCasting() && skill != null)
            {
                currentSkill = skill;
                timer.ResetTimer();
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

        public SkillCaster GetTarget()
        {
            return target;
        }

        //getters and setters

        public Animator GetAnimator() { return anim; }
        private void SetAnimator() { anim = transform.GetComponentInChildren<Animator>(); }
        public AudioSource GetAudioSource() { return audioSource; }
        private void SetAudioSource() { audioSource = transform.GetComponent<AudioSource>(); }
        public Vector2 GetStartingPosition() { return user.GetStartingPosition(); }



        //private functions

        private void PrepareSkill(Skill skill)
        {
            List<SkillAction> allActions = skill.actions;
            skillActions = new List<SkillAction>(allActions.Count);

            skillActions.AddRange(allActions);
        }

        private void ResetCurrentSkill()
        {
            currentSkill = null;
            timer.ResetTimer();
            transform.position = user.GetStartingPosition();
        }



        
    }
}