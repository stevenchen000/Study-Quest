using DialogueSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HubNPC : MonoBehaviour, IInteractable
{

    public UnityEvent interactEvent;
    public UnityEvent endInteractionEvent;
    private IInteractor currentInteractor;
    public DialogueTree dialogue;
    public EventSO onDialogueEndEvent;

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
        currentInteractor = interactor;
        AddInteractEvent();

        WorldState.PlayDialogue(dialogue);
    }

    public void EndInteraction()
    {
        currentInteractor.UnlockInteractor();
        currentInteractor = null;
        endInteractionEvent?.Invoke();
    }
    

    private void AddInteractEvent()
    {
        onDialogueEndEvent.SubscribeToEvent(interactEvent.Invoke);
        onDialogueEndEvent.SubscribeToEvent(RemoveInteractEvent);
    }

    private void RemoveInteractEvent()
    {
        onDialogueEndEvent.UnsubscribeFromEvent(interactEvent.Invoke);
        onDialogueEndEvent.UnsubscribeFromEvent(RemoveInteractEvent);
    }
}
