using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonTrigger : MonoBehaviour, IInteractable
{
    public Collider2D collider;
    public string dungeonName;

    public void Start()
    {
        collider = transform.GetComponent<Collider2D>();
    }



    public void Interact(IInteractor interactor)
    {
        WorldState.ResetCurrentFloorNumber();
        WorldState.SetDungeonName(dungeonName);
        WorldState.SetDungeonPosition(new Vector3(0,0,0));
        SceneLoader.LoadDungeon(dungeonName);
    }

    public bool IsColliding(Collider2D targetCollider)
    {
        return collider.IsTouching(targetCollider);
    }
}
