using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    private EventSO onPause;
    [SerializeField]
    private EventSO onUnpause;
    [SerializeField]
    private Button unpauseButton;
    [SerializeField]
    private Button quitButton;
    private CanvasGroup cgroup;
    [SerializeField]
    private KeyCode pauseButton = KeyCode.Escape;

    private bool isEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        unpauseButton?.onClick.AddListener(UnpauseButtonFunction);
        quitButton?.onClick.AddListener(QuitButtonFunction);
        cgroup = transform.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pauseButton) && !isEnabled)
        {
            EnableScreen();
        }
    }

    private void UnpauseButtonFunction()
    {
        DisableScreen();
    }

    private void QuitButtonFunction()
    {
        Application.Quit();
    }

    private void EnableScreen()
    {
        cgroup.alpha = 1;
        cgroup.blocksRaycasts = true;
        cgroup.interactable = true;
        isEnabled = true;
        onPause?.CallEvent();
    }

    private void DisableScreen()
    {
        cgroup.alpha = 0;
        cgroup.blocksRaycasts = false;
        cgroup.interactable = false;
        isEnabled = false;
        onUnpause?.CallEvent();
    }
}
