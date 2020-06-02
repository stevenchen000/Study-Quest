using CombatSystem;
using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EffectSystem
{
    public class EffectManager : MonoBehaviour
    {
        private IFighter fighter;
        private CharacterStats stats;

        // Start is called before the first frame update
        void Start()
        {
            fighter = transform.GetComponent<IFighter>();
            stats = transform.GetComponent<CharacterStats>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}