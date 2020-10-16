using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    [CreateAssetMenu(menuName = "Events/Void Event")]
    public class EventSO : ScriptableObject
    {
        [TextArea(4,10)]
        public string description;
        public delegate void VoidEvent();
        private event VoidEvent _event;


        public void SubscribeToEvent(VoidEvent func)
        {
            _event += func;
        }

        public void UnsubscribeFromEvent(VoidEvent func)
        {
            _event -= func;
        }

        public void CallEvent()
        {
            Debug.Log($"Called event {name}");
            _event?.Invoke();
        }
    }
}