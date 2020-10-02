using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOEventSystem
{
    public class EventCaller : MonoBehaviour
    {
        public EventSO eventObject;

        public void CallEvent()
        {
            if(eventObject != null)
            {
                eventObject.CallEvent();
            }
        }
    }
}