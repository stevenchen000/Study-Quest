using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    public class EventListener : MonoBehaviour
    {
        public EventSO eventSO;
        public UnityEvent unityEvent;

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

        private void CallEvent()
        {
            unityEvent.Invoke();
        }
    }
}