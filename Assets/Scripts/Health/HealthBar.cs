using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private bool useScale = false;
	[SerializeField] private float maxHealth = 100;
	[SerializeField] private Image healthBarFill;
	[SerializeField] private TextMeshProUGUI sliderTextMesh;

	public float GetHealth => _health;
	public float GetMaxHealth => maxHealth;
	
	private float _health;

	private void Awake()
	{
		_health = maxHealth;
		UpdateHealthBar();
	}

	public void Damage(float amount)
	{
		_health -= Mathf.Round(amount);
		UpdateHealthBar();
	}

	public void Heal(float amount)
	{
		_health += Mathf.Round(amount);
		UpdateHealthBar();
	}
	public void SetHealth(float health)
    {
	    _health = Mathf.Round(health);
	    UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
	    var fillAmount = _health / maxHealth;
	    if (!useScale)
		    healthBarFill.fillAmount = fillAmount;
	    else
		    healthBarFill.transform.DOScaleX(fillAmount, 0.5f);
	    _health = Mathf.Clamp(_health, 0, maxHealth);
	    if(sliderTextMesh)
		    sliderTextMesh.text = _health + " / " + maxHealth;
    }
}
