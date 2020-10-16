using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StateSystem
{
    public class ObjectState
    {
        [SerializeField]
        private string stateName;
        [SerializeField]
        private UnityEvent enterStateEvent;
        [SerializeField]
        private UnityEvent exitStateEvent;
        [SerializeField]
        private UnityEvent runStateEvent;

        public void EnterState() { enterStateEvent?.Invoke(); }
        public void ExitState() { exitStateEvent?.Invoke(); }
        public void RunState() { runStateEvent?.Invoke(); }
    }
}