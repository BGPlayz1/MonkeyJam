using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthFill;
    public Stats stats;

   

    private void Update()
    {
        // Calculate the health percentage
        float healthPercentage = (float)stats.CurrentHealth / (float)stats.MaxHealth;

        // Update the health bar UI
        healthFill.fillAmount = healthPercentage;
    }
}

