using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace DungeonSystem
{
    public class PlayerExplorer : MonoBehaviour, IInteractor
    {

        public float movementSpeed = 5;
        List<IInteractable> interactables = new List<IInteractable>();
        public Collider2D collider;

        // Start is called before the first frame update
        void Start()
        {
            var tempInteracts = FindObjectsOfType<MonoBehaviour>().OfType<IInteractable>();
            foreach(IInteractable i in tempInteracts)
            {
                interactables.Add(i);
            }
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
        }
    }
}