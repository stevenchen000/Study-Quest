using SkillSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    public class PlayerFighter : MonoBehaviour, IFighter
    {

        public int currentHealth = 5;
        public int maxHealth = 5;

        public Vector2 startingPosition;

        public SkillCaster caster;
        public Skill skill;

        private void Start()
        {
            startingPosition = transform.position;
            caster = transform.GetComponent<SkillCaster>();
        }


        public void Attack(IFighter target)
        {
            target.TakeDamage(20);
            caster.CastSkill(skill, target);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= 1;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public Vector2 GetStartingPosition()
        {
            return startingPosition;
        }

        public bool IsAttacking()
        {
            return caster.IsCasting();
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }

        public void SetStartingPosition(Vector3 position)
        {
            startingPosition = position;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public SkillCaster GetCaster()
        {
            return caster;
        }

        public Vector2 GetForwardVector()
        {
            return transform.right;
        }
    }
}
