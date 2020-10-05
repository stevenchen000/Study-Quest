using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    private bool isMyTurn = false;
    private bool isFlipping = false;

    private void OnMouseDown()
    {
        TryToChangeTurn();
    }

    private void TryToChangeTurn()
    {
        if (!isFlipping)
        {
            StartCoroutine(ChangeTurn());
        }
    }

    private IEnumerator ChangeTurn()
    {
        isFlipping = true;

        isMyTurn = !isMyTurn;
        float rotation = isMyTurn ? 0 : 180;
        float targetRotation = isMyTurn ? 180 : 0;

        var tweener = DOTween.To(() => rotation, x => rotation = x, targetRotation, 1f)
            .OnUpdate(() => transform.eulerAngles = new Vector3(rotation, 0f, 0f))
            .SetEase(Ease.OutBounce);
        
        while (tweener.IsActive()) { yield return null; }

        isFlipping = false;
    }
}
