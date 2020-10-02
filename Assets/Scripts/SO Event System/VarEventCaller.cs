using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOEventSystem
{
    public class VarEventCaller<T> : MonoBehaviour
    {
        public VarEventSO<T> eventObject;

        public void CallEvent(T var)
        {
            if(eventObject != null)
            {
                eventObject.CallEvent(var);
            }
        }
    }
}