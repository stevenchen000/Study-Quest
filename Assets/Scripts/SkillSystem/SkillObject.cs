using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    public class SkillObject : MonoBehaviour
    {
        public float lifetime;
        public SkillObjectAnimation anim;
        private Timer timer;

        private SkillCaster caster;
        private Skill skill;

        private Rigidbody2D rb;
        private Collider2D trigger;

        private void Start()
        {
            timer = new Timer();
            rb = transform.GetComponent<Rigidbody2D>();
            trigger = transform.GetComponent<Collider2D>();
        }

        private void Update()
        {
            timer.Tick();
            anim.RunAnimation(this, caster, caster.GetTarget(), timer);

            if(timer.AtTime(lifetime))
            {
                ResetSkillObject();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log($"Collided with ${collision.gameObject}");
            if (!collision.gameObject.CompareTag(caster.tag))
            {
                gameObject.SetActive(false);
            }
        }



        public void SetupSkillObject(SkillCaster caster, Skill skill, SkillObjectCreationData data)
        {
            if (timer == null) timer = new Timer();
            timer.ResetTimer();
            transform.gameObject.SetActive(true);
            this.caster = caster;
            this.skill = skill;

            anim = data.animation;

            lifetime = data.lifetime;

            GameObject obj = skill.GetGameObjectAtIndex(data.skillObjIndex);
            GameObject instObj = Instantiate(obj);
            instObj.transform.parent = transform;
            instObj.transform.position = transform.position;
            instObj.transform.rotation = transform.rotation;

            Transform baseTransform = null;

            switch (data.target)
            {
                case SkillObjectTarget.Caster:
                    baseTransform = caster.transform;
                    break;
                case SkillObjectTarget.Creator:
                    baseTransform = caster.transform;
                    break;
                case SkillObjectTarget.Target:
                    baseTransform = caster.GetTarget().transform;
                    break;
            }

            transform.position = baseTransform.position +
                                 transform.right * transform.localScale.x / Mathf.Abs(transform.localScale.x) * data.targetPositionOffset.x +
                                 transform.up * data.targetPositionOffset.y;

            transform.rotation = new Quaternion(0, 0, 0, 0);

            if (data.parent)
            {
                transform.parent = baseTransform;
            }
        }



        private void ResetSkillObject()
        {
            transform.gameObject.SetActive(false);
        }







    }
}
