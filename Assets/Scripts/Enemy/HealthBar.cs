using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private HealthController healthController;
    [SerializeField] private float smoothSpeed = 5f;

    private float targetValue;

    public void Initialize(Transform targetTransform, HealthController health)
    {
        healthController = health;
        slider.value = 1f;
        targetValue = 1f;
    }

    private void Update()
    {
        if (healthController == null) return;

        float healthPercentage = (float)healthController.CurrentHealth / healthController.MaxHealth;
        targetValue = healthPercentage;

        slider.value = Mathf.MoveTowards(slider.value, targetValue, smoothSpeed * Time.deltaTime);
    }
}
