using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DungeonSystem
{
    public class EnemyExplorer : MonoBehaviour
    {

        public float movementSpeed = 4.5f;
        public PlayerExplorer player;


        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<PlayerExplorer>();
        }

        // Update is called once per frame
        void Update()
        {
            MoveTowardPlayer();
        }


        private void MoveTowardPlayer()
        {
            Vector3 direction = player.transform.position - transform.position;
            Move(direction, movementSpeed);
        }




        public void Move(Vector3 direction, float speed)
        {
            Vector3 clampedDirection = direction;

            if (direction.magnitude > 1)
            {
                clampedDirection = direction / direction.magnitude;
            }

            transform.position += clampedDirection * speed * Time.deltaTime;
        }

    }
}