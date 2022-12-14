using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerParameters playerParams;

    private void Start()
    {
        playerParams.OnTakeDamage += UpdateHealthBar;
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerParams.HitPoints;
        healthBar.value = playerParams.HitPoints;
    }
    
    private void UpdateHealthBar(int health)
    {
        healthBar.value = playerParams.HitPoints;
    }
    
    private void OnDestroy()
    {
        playerParams.OnTakeDamage -= UpdateHealthBar;
    }
}
