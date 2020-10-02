using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    public class VarEventListener<T> : MonoBehaviour
    {
        public VarEventSO<T> eventSO;
        public UnityEvent<T> unityEvent;

        // Start is called before the first frame update
        void Start()
        {
            if(eventSO != null)
            {
                eventSO.SubscribeToEvent(CallEvent);
            }
        }

        private void OnDestroy()
        {
            if(eventSO != null)
            {
                eventSO.UnsubscribeFromEvent(CallEvent);
            }
        }

        private void CallEvent(T var)
        {
            unityEvent.Invoke(var);
        }
    }
}