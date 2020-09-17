using System;
using System.Collections;
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

        private bool IsAttacking = false;
        private Animator anim;

        public CharacterData data;
        public bool isPlayer = false;

        private void Start()
        {
            anim = transform.GetComponent<Animator>();
            SetupCharacter();
        }

        private void OnDestroy()
        {
            if (isPlayer)
            {
                data.currentHealth = currentHealth;
            }
        }







        public void Attack(Fighter target)
        {
            if (!IsAttacking)
            {
                StartCoroutine(_Attack(target));
            }
        }

        private IEnumerator _Attack(Fighter target)
        {
            IsAttacking = true;

            PlayAnimation(attackAnim);

            yield return new WaitForSeconds(attackTime);
            target.TakeDamage(1);
            yield return new WaitForSeconds(attackTime);

            IsAttacking = false;
        }




        /* Animation Functions*/

        public void PlayAnimation(string animation, float crossfade = 0)
        {
            anim.CrossFade(animation, crossfade);
        }







        /* Damage Functions */

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
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

        private void SetupCharacter()
        {
            RemoveChildren();
            SetupData();
            CreateChild();
            SetupStats();
        }

        private void RemoveChildren()
        {
            Transform child = null;

            try
            {
                transform.GetChild(0);
            }catch(Exception e)
            {

            }

            while (child != null)
            {
                child.transform.SetParent(null);
                child.gameObject.SetActive(false);
                child = transform.GetChild(0);
            }
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
            model.transform.parent = transform;
            model.transform.localPosition = new Vector3();
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
    }
}
