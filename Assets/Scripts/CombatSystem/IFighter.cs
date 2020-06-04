using SkillSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    public interface IFighter
    {

        void TakeDamage(int damage);
        //void DealDamage(IFighter target);
        void Attack(IFighter target);
        bool IsAttacking();
        bool IsDead();

        int GetCurrentHealth();
        int GetMaxHealth();

        Vector2 GetPosition();
        Vector2 GetStartingPosition();
        Vector2 GetForwardVector();
        Transform GetTransform();
        void SetStartingPosition(Vector3 position);

        SkillCaster GetCaster();
    }
}
