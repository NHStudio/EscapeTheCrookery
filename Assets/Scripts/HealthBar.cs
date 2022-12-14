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
        playerParams = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParameters>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerParams.maxHitPoints;
        healthBar.value = playerParams.maxHitPoints;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
