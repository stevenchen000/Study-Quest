using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using CombatSystem;

namespace DungeonSystem
{
    public class PlayerExplorer : MonoBehaviour, IInteractor
    {

        public float movementSpeed = 5;
        List<IInteractable> interactables = new List<IInteractable>();
        public CharacterData data;
        public Collider2D collider;
        public bool defaultIsLeft = true;
        public bool useHubPosition;

        // Start is called before the first frame update
        void Start()
        {
            if (!useHubPosition)
            {
                transform.position = WorldState.GetDungeonPosition();
            }
            else
            {
                transform.position = WorldState.GetHubPosition();
            }
            data = WorldState.GetPlayerData();
            SetupModel();

            var tempInteracts = FindObjectsOfType<MonoBehaviour>().OfType<IInteractable>();
            interactables.AddRange(tempInteracts);

            collider = transform.GetComponent<Collider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            GetMovementInput();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                foreach(IInteractable i in interactables)
                {
                    if (i.IsColliding(collider))
                    {
                        i.Interact(this);
                        break;
                    }
                }
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            EnemyExplorer enemy = collision.transform.GetComponent<EnemyExplorer>();

            if(enemy != null)
            {
                WorldState.SetDungeonPosition(transform.position);
                SceneLoader.LoadCombat();
            }
        }





        private void GetMovementInput()
        {
            Vector3 direction = Input.GetAxis("Horizontal") * transform.right;
            direction += Input.GetAxis("Vertical") * transform.up;

            Move(direction, movementSpeed);
        }




        public void Move(Vector3 direction, float speed)
        {
            Vector3 clampedDirection = direction;

            if(direction.magnitude > 1)
            {
                clampedDirection = direction / direction.magnitude;
            }

            transform.position += clampedDirection * speed * Time.deltaTime;
            FaceDirection(clampedDirection);
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
            bool result = false;

            if (defaultIsLeft)
            {
                result = !SameSign(direction.x, transform.localScale.x);
            }
            else
            {
                result = SameSign(direction.x, transform.localScale.x);
            }

            return result;
        }

        private bool SameSign(float num1, float num2)
        {
            return (num1 <= 0 && num2 <= 0) || (num1 >= 0 && num2 >= 0);
        }



        private void SetupModel()
        {
            UnityUtilities.RemoveChildren(transform);
            GameObject model = data.model;
            GameObject child = Instantiate(model);

            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3();
        }

    }
}