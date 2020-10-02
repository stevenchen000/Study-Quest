using DungeonSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorProjection : MonoBehaviour
{
    private Animator anim;
    private DungeonManager dungeon;
    public EventSO OnDeactivateFloor;

    private void Start()
    {
        anim = transform.GetComponent<Animator>();
        dungeon = FindObjectOfType<DungeonManager>();
    }

    public void ActivateProjection()
    {
        anim.SetBool("isActive", true);
    }

    public void DeactivateProjection()
    {
        anim.SetBool("isActive", false);
    }

    




    public void ProgressDungeonFloor()
    {
        dungeon.ProgressFloor();
    }

    public void StartDungeonEvent()
    {
        dungeon.InitializeDungeonEvent();
    }
}
