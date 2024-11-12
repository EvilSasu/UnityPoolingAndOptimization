using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private HealthController health;
    private TextMeshProUGUI textUI;
    private void Start()
    {
        health = GetComponent<HealthController>();
        textUI = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textUI.text = health.CurrentHealth.ToString();
    }

    public void DealDamage(int dmg)
    {
        health.Deal(1);
        textUI.text = health.CurrentHealth.ToString();
    }
}
