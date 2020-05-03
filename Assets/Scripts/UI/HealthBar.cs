using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Fighter fighter;
    public Slider healthSlider;
    public Text fighterName;
    public Camera mainCamera;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        fighterName.text = fighter.name;
        UpdateSlider(fighter.currentHealth, fighter.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider(fighter.currentHealth, fighter.maxHealth);
        FollowTarget();
    }

    public void LinkHealthBarToFighter(Fighter fighter) {
        this.fighter = fighter;
        fighter.OnHealthChanged += UpdateSlider;
    }

    public void UpdateSlider(float currentHealth, float maxHealth) {
        healthSlider.value = currentHealth / maxHealth;
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = mainCamera.WorldToScreenPoint(fighter.transform.position);
        transform.position = targetPosition + offset;
    }
}
