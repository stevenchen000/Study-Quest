using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    public class VarEventSO<T> : ScriptableObject
    {
        public string description;
        public delegate void VarEvent(T var);
        private event VarEvent _event;


        public void SubscribeToEvent(VarEvent func)
        {
            _event += func;
        }

        public void UnsubscribeFromEvent(VarEvent func)
        {
            _event -= func;
        }

        public void CallEvent(T var)
        {
            _event?.Invoke(var);
        }
    }
}