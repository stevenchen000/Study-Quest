using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HubNPC : MonoBehaviour, IInteractable
{

    public UnityEvent interactEvent;
    private IInteractor currentInteractor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        UnityUtilities.LoadLevel(sceneName);
    }

    public void Interact(IInteractor interactor)
    {
        interactor.LockInteractor();
        currentInteractor = null;
        interactEvent?.Invoke();
    }

    public void EndInteraction()
    {
        currentInteractor.UnlockInteractor();
        currentInteractor = null;
    }
    
}
