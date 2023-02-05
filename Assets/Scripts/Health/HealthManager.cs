using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour, IDamageable
{
    public Slider healthBarFill;
    public float maxHealth;

    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }

    public virtual void Damage(float amount)
    {
        currentHealth -= Mathf.Round(amount);
        UpdateHealth();
    }

    public virtual void Heal(float amount)
    {
        currentHealth += Mathf.Round(amount);
        UpdateHealth();
    }

    public void SetHealth(float newValue)
    {
        currentHealth = newValue;
        UpdateHealth();
    }

    protected virtual void UpdateHealth()
    {
        healthBarFill.value = currentHealth;
        if(healthBarFill.transform.parent.GetComponentInChildren<TMP_Text>() != null)
            healthBarFill.transform.parent.GetComponentInChildren<TMP_Text>().text = $"{currentHealth}/{maxHealth}";
    }
}
