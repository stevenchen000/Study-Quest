using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
    public Fighter target;
    public Slider slider;
    public Image fill;
    public TMP_Text nameText;
    public TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateName();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }


    public void SetTarget(Fighter newTarget)
    {
        target = newTarget;
        UpdateHealth();
        UpdateName();
    }

    public void UpdateHealth()
    {
        int currentHealth = target.currentHealth;
        int maxHealth = target.maxHealth;
        slider.value = currentHealth / (float)maxHealth;

        if(currentHealth <= 0)
        {
            fill.gameObject.SetActive(false);
        }
        else
        {
            fill.gameObject.SetActive(true);
        }

        healthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void UpdateName()
    {
        nameText.text = target.name;
    }
}
