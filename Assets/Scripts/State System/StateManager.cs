using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace StateSystem
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField]
        private List<ObjectState> states = new List<ObjectState>();
        private ObjectState currentState = null;
        private ObjectState defaultState = null;

        // Start is called before the first frame update
        void Start()
        {
            ChangeState(defaultState);
        }

        // Update is called once per frame
        void Update()
        {
            RunState();
            CheckTransitions();
        }

        private void CheckTransitions()
        {

        }

        private void RunState()
        {
            currentState?.RunState();
        }

        private void ChangeState(ObjectState newState)
        {
            currentState?.ExitState();
            newState?.EnterState();
            currentState = newState;
        }
        public void GoToDefaultState()
        {
            ChangeState(defaultState);
        }
    }
}