using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour, IInteractor
{
    private List<IInteractable> interactables = new List<IInteractable>();
    private bool isLocked = false;
    
    public UnityEvent OnLockEvent;
    public UnityEvent OnUnlockEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isLocked)
        {
            if(interactables.Count > 0)
            {
                interactables[0].Interact(this);
            }
        }
    }

    private void OnDestroy()
    {
        OnLockEvent.RemoveAllListeners();
        OnUnlockEvent.RemoveAllListeners();
    }


    public void LockInteractor()
    {
        isLocked = true;
        OnLockEvent?.Invoke();
    }

    public void UnlockInteractor()
    {
        isLocked = false;
        OnUnlockEvent?.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactables.Add(interactable);
            Debug.Log($"Trigger found: {collision.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactables.Remove(interactable);
            Debug.Log($"Trigger left: {collision.name}");
        }
    }

    private IInteractable GetClosestInteractable()
    {
        IInteractable result = null;
        
        for(int i = 0; i < interactables.Count; i++)
        {
            
        }

        return result;
    }

    
}
