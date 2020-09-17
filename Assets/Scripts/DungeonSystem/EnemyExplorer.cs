using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DungeonSystem
{
    public class EnemyExplorer : MonoBehaviour
    {

        public float movementSpeed = 4.5f;
        public CharacterData data;
        public PlayerExplorer player;


        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<PlayerExplorer>();
            SetupModel();
        }

        // Update is called once per frame
        void Update()
        {
            MoveTowardPlayer();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
            {
                string combatName = WorldState.GetCombatName();
                Vector3 playerPosition = collision.transform.position;
                WorldState.SetDungeonPosition(playerPosition);
                UnityUtilities.LoadLevel(combatName);
            }
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



        private void SetupModel()
        {
            GameObject model = data.model;
            GameObject child = Instantiate(model);

            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3();
        }
    }
}