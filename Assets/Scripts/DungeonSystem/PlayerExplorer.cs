using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using CombatSystem;

namespace DungeonSystem
{
    public class PlayerExplorer : MonoBehaviour
    {

        public float movementSpeed = 5;
        public CharacterData data;
        public Animator anim;

        private Rigidbody2D rb;

        private Vector3 movePosition;
        private bool isMoving = false;

        // Start is called before the first frame update
        void Start()
        {
            rb = transform.GetComponent<Rigidbody2D>();

            data = WorldState.GetPlayerData();
            SetupModel();

            anim = transform.GetComponentInChildren<Animator>();

            movePosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMoving)
            {
                if (Vector3.Distance(movePosition, transform.position) >= 0.1f)
                {
                    Move();
                }
                else
                {
                    ChangeMovementState(false);
                    rb.velocity = new Vector3();
                }
            }
        }



        private void ChangeMovementState(bool movement)
        {
            if (movement != isMoving)
            {
                isMoving = movement;
                anim.SetBool("isMoving", isMoving);

                if (isMoving)
                {
                    anim.CrossFade("Run", 0.2f);
                }
                else
                {
                    anim.CrossFade("Idle", 0.2f);
                }
            }
        }



        public void MoveToPosition(Vector3 position)
        {
            ChangeMovementState(true);
            movePosition = position;
        }
        
        public bool IsMoving()
        {
            return isMoving;
        }
        
        public void HealCharacter()
        {
            data.HealCharacter();
        }






        //Movement

        private void Move()
        {
            Vector3 direction = movePosition - transform.position;
            direction = direction.magnitude > 1 ? direction.normalized : direction;
            FaceDirection(direction);

            rb.velocity = direction * movementSpeed;
        }

        private void FaceDirection(Vector3 direction)
        {
            if(direction.x != 0)
            {
                if (!IsFacingRightDirection(direction))
                {
                    Vector3 currScale = transform.localScale;
                    transform.localScale = new Vector3(-currScale.x, currScale.y, currScale.z);
                }
            }
        }

        private bool IsFacingRightDirection(Vector3 direction)
        {
            float x = direction.x;
            float scaleX = transform.localScale.x;
            return (x > 0 && scaleX > 0) || (x < 0 && scaleX < 0);
        }






        private void SetupModel()
        {
            UnityUtilities.RemoveChildren(transform);
            GameObject model = data.model;
            GameObject child = Instantiate(model);

            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3();
        }

        public void SetAnimationBool(string boolName, bool value)
        {
            anim.SetBool(boolName, value);
        }
    }
}