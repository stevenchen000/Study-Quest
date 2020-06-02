using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public IFighter fighter;
    public Slider healthSlider;
    public Text fighterName;
    public Camera mainCamera;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //fighterName.text = fighter.name;
        UpdateSlider(fighter.GetCurrentHealth(), fighter.GetMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider(fighter.GetCurrentHealth(), fighter.GetMaxHealth());
        FollowTarget();
    }

    public void LinkHealthBarToFighter(IFighter fighter) {
        this.fighter = fighter;
    }

    public void UpdateSlider(int currentHealth, int maxHealth) {
        float clampedMax = maxHealth < 1 ? 1 : maxHealth;
        healthSlider.value = currentHealth / clampedMax;
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = mainCamera.WorldToScreenPoint(fighter.GetPosition());
        transform.position = targetPosition + offset;
    }

    public void SetTarget(IFighter target)
    {
        fighter = target;
    }
}
