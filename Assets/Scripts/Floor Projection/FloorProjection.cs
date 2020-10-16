using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorProjection : MonoBehaviour
{
    public EventSO onFloorStart;
    public EventSO onFloorDeactivate;

    public void StartFloor()
    {
        onFloorStart?.CallEvent();
    }

    public void DeactivateFloor()
    {
        onFloorDeactivate?.CallEvent();
    }
}
