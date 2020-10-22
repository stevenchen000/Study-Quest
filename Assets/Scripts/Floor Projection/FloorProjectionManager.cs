using DG.Tweening;
using DungeonSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorProjectionManager : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    [Space(20)]
    private RectTransform projTransform;
    [SerializeField]
    private Ease ease;
    [SerializeField]
    private float animTime = 1.5f;

    [Space(20)]
    [SerializeField]
    private EventSO onActivateFloor;
    [SerializeField]
    private EventSO onDeactivateFloor;
    [SerializeField]
    private EventSO onFloorStart;
    [SerializeField]
    private EventSO onFloorEnd;

    [Space(20)]
    [SerializeField]
    private Camera projCam;


    private void Start()
    {
        anim = transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
    }




    /// <summary>
    /// Starts the animation for the projection appearing
    /// Calls event OnActivateFloor
    /// </summary>
    public void ActivateProjection()
    {
        TweenIn();
        //anim.SetBool("isActive", true);
    }


    /// <summary>
    /// Starts the animation for the projection disappearing
    /// Calls Event OnFloorEnd
    /// </summary>
    public void EndDungeonEvent()
    {
        TweenOut();
        //onFloorEnd.CallEvent();
        //anim.SetBool("isActive", false);
    }

    public Vector3 GetCameraOffset()
    {
        Vector3 result =  projCam.transform.position;
        result = new Vector3(result.x, result.y, 0);
        return result;
    }


    private void TweenIn()
    {
        onActivateFloor?.CallEvent();
        var tweener = projTransform.DOScale(new Vector3(1, 1, 1), animTime);
        tweener.OnComplete(() => onFloorStart.CallEvent());
        tweener.SetEase(ease);
    }

    private void TweenOut()
    {
        onFloorEnd?.CallEvent();
        var tweener = projTransform.DOScale(new Vector3(0, 0, 0), animTime);
        tweener.OnComplete(() => onDeactivateFloor?.CallEvent());
        tweener.SetEase(ease);
    }
    
}
