 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{

    void Interact(IInteractor interactor);
    bool IsColliding(Collider2D collider);


}
