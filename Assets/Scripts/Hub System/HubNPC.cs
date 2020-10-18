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
    [SerializeField]
    private bool canInteract;
    [SerializeField]
    private GameObject speechBubble;

    // Start is called before the first frame update
    void Start()
    {
        if (canInteract)
        {
            ShowSpeechBubble();
        }
        else
        {
            HideSpeechBubble();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        WorldState.LoadLevel(sceneName);
    }

    public void Interact(IInteractor interactor)
    {
        if (canInteract)
        {
            interactor.LockInteractor();
            currentInteractor = interactor;
            HideSpeechBubble();
            AddInteractEvent();

            WorldState.PlayDialogue(dialogue);
        }
    }

    public void EndInteraction()
    {
        currentInteractor.UnlockInteractor();
        currentInteractor = null;
        ShowSpeechBubble();
        endInteractionEvent?.Invoke();
    }
    
    public void ShowSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
        }
    }

    public void HideSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
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
