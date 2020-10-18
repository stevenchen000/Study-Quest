using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum LoadScreenState
{
    FadingOut,
    FadingIn,
    Active,
    Inactive
}

public class LoadScreenPanel : MonoBehaviour
{
    public EventSO fadeInEvent;
    public EventSO fadeOutEvent;
    public Color defaultColor;
    public float fadeTime;
    private CanvasGroup cgroup;

    private LoadScreenState state = LoadScreenState.Active;
    private string levelToLoad;

    private void Awake()
    {
        cgroup = transform.GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        ChangeState(LoadScreenState.FadingIn);
        cgroup.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        RunState();
    }

    private void RunState()
    {
        switch (state)
        {
            case LoadScreenState.FadingOut:
                _FadeLoadScreenOut();
                break;
            case LoadScreenState.FadingIn:
                _FadeLoadScreenIn();
                break;
            case LoadScreenState.Active:
                break;
            case LoadScreenState.Inactive:
                break;
        }
    }

    private void _FadeLoadScreenOut()
    {
        float addAmount = 1f / fadeTime * Time.deltaTime;
        cgroup.alpha += addAmount;

        if (cgroup.alpha == 1)
        {
            ChangeState(LoadScreenState.Active);
        }
    }

    private void _FadeLoadScreenIn()
    {
        float removeAmount = 1f / fadeTime * Time.deltaTime;
        cgroup.alpha -= removeAmount;

        if (cgroup.alpha == 0)
        {
            ChangeState(LoadScreenState.Inactive);
        }
    }

    private void ChangeState(LoadScreenState newState)
    {
        switch (state)
        {
            case LoadScreenState.FadingOut:
                break;
            case LoadScreenState.FadingIn:
                break;
            case LoadScreenState.Active:
                break;
            case LoadScreenState.Inactive:
                break;
        }
        switch (newState)
        {
            case LoadScreenState.FadingOut:
                BlockRaycasts(true);
                break;
            case LoadScreenState.FadingIn:
                BlockRaycasts(true);
                break;
            case LoadScreenState.Active:
                BlockRaycasts(true);
                fadeOutEvent?.CallEvent();
                UnityUtilities.LoadLevel(levelToLoad);
                break;
            case LoadScreenState.Inactive:
                BlockRaycasts(false);
                fadeInEvent?.CallEvent();
                break;
        }

        state = newState;
    }

    private void BlockRaycasts(bool block)
    {
        cgroup.blocksRaycasts = block;
        cgroup.interactable = false;
    }

    /// <summary>
    /// Called when loading out of a scene
    /// </summary>
    public void StartFadeOut(string levelName)
    {
        levelToLoad = levelName;
        ChangeState(LoadScreenState.FadingOut);
    }

    public void StartFadeIn()
    {
        ChangeState(LoadScreenState.FadingIn);
    }
}
