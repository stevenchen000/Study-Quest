using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DungeonSystem
{
    public class PlayerExplorer : MonoBehaviour
    {

        public float movementSpeed = 5;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetMovementInput();
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            EnemyExplorer enemy = collision.transform.GetComponent<EnemyExplorer>();

            if(enemy != null)
            {
                SceneManager.LoadScene("Test_Solo_Combat");
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