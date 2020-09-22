using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventScript : MonoBehaviour
{
    public void CheckEventState()
    {
        CombatManager combat = FindObjectOfType<CombatManager>();
        combat.CheckBattleState();
    }
}
