using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    public class Fighter : MonoBehaviour
    {
        public int currentHealth = 10;
        public int maxHealth = 10;

        public float attackTime = 2;

        public string attackAnim = "Attack";
        public List<Skill> skills = new List<Skill>();
        private Skill currentSkill;

        public bool isAttacking = false;
        private Animator anim;
        private Vector3 defaultPosition;

        public CharacterData data;
        public bool isPlayer = false;

        private void Start()
        {
            SetupCharacter();
            defaultPosition = transform.position;
        }
        


        


        public void Attack(Fighter target, int skillNumber = 0)
        {
            if (!isAttacking)
            {
                currentSkill = GetSkill(skillNumber);
                StartCoroutine(_Attack(target));
            }
        }

        private IEnumerator _Attack(Fighter target)
        {
            isAttacking = true;

            SkillCastData castData = new SkillCastData(this, target);
            currentSkill.StartSkill(castData);

            while (currentSkill.IsRunning(castData))
            {
                yield return null;
                currentSkill.RunSkill(castData);
            }

            transform.position = defaultPosition;

            isAttacking = false;
        }




        /* Animation Functions*/

        public void PlayAnimation(string animation, float crossfade = 0)
        {
            anim.CrossFade(animation, crossfade);
        }

        public void SetAnimationBool(string boolName, bool value)
        {
            anim.SetBool(boolName, value);
        }





        /* Damage Functions */

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
            anim.Play("Stagger");
            
        }

        public bool CheckIfDead()
        {
            if (currentHealth == 0)
            {
                SetAnimationBool("isDead", true);
            }

            return currentHealth == 0;
        }

        public void HealHealth(int healing)
        {
            currentHealth += healing;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }

        public bool IsDead()
        {
            return currentHealth == 0;
        }

        public bool IsAttacking() { return isAttacking; }

        private void SetupCharacter()
        {
            UnityUtilities.RemoveChildren(transform);
            SetupData();
            CreateChild();
            SetupStats();
            anim = transform.GetComponentInChildren<Animator>();
        }

        private void SetupData()
        {
            if (isPlayer)
            {
                data = WorldState.GetPlayerData();
            }
            else
            {
                data = WorldState.GetCharacterData();
            }
        }

        private void CreateChild()
        {
            GameObject model = Instantiate(data.model);
            Vector3 localScale = model.transform.localScale;
            model.transform.parent = transform;
            model.transform.localPosition = new Vector3();

            model.transform.localScale = localScale;
        }

        private void SetupStats()
        {
            int currentHealth = data.currentHealth;
            int maxHealth = data.maxHealth;

            this.maxHealth = maxHealth;

            if (isPlayer)
            {
                this.currentHealth = currentHealth;
            }
            else
            {
                this.currentHealth = maxHealth;
            }
        }

        private void SelectRandomSkill()
        {
            int numOfSkills = skills.Count;
            int rand = UnityEngine.Random.Range(0, numOfSkills);

            currentSkill = skills[rand];
        }

        private Skill GetSkill(int index)
        {
            return skills[index];
        }
    }
}
