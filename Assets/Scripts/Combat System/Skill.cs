using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SkillCastData
{
    public Fighter caster;
    public Fighter target;
    public float currentTime = 0;
    public float previousTime = 0;

    public SkillCastData(Fighter caster, Fighter target)
    {
        this.caster = caster;
        this.target = target;
    }

    public void Tick()
    {
        previousTime = currentTime;
        currentTime += Time.deltaTime;
    }

    public bool BetweenTime(float time)
    {
        return time < currentTime && time >= previousTime;
    }
}


[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject
{
    [SerializeField]
    private string animationName;
    [SerializeField]
    private float crossfade = 0.2f;
    [SerializeField]
    private int potency;
    [SerializeField]
    private float attackHitTime;
    [SerializeField]
    private bool offsetFromTarget = true;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float skillDuration;

    public void StartSkill(SkillCastData castData)
    {
        SetPosition(castData);
        Fighter caster = castData.caster;
        caster.PlayAnimation(animationName, crossfade);
    }

    public void RunSkill(SkillCastData castData)
    {
        castData.Tick();

        if (castData.BetweenTime(attackHitTime))
        {
            Fighter target = castData.target;
            target.TakeDamage(potency);
        }
    }

    public bool IsRunning(SkillCastData castData)
    {
        return castData.currentTime < skillDuration;
    }


    private void SetPosition(SkillCastData castData)
    {
        Vector3 finalPosition;
        Transform targetTrans;

        if (offsetFromTarget)
        {
            targetTrans = castData.target.transform;
        }
        else
        {
            targetTrans = castData.target.transform;
        }

        float directionScale = targetTrans.localScale.x > 0 ? 1 : -1;
        Vector3 correctedOffset = new Vector3(offset.x * directionScale, 
                                              offset.y, 
                                              0);
        finalPosition = targetTrans.position + correctedOffset;

        castData.caster.transform.position = finalPosition;
    }

}
