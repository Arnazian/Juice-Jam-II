using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
	/*
	[SerializeField] private bool useScale = false;
	[SerializeField] private float healthValueMax = 100;
	[SerializeField] private Image healthBarFill;
	[SerializeField] private TextMeshProUGUI sliderTextMesh;

	
	public float GetHealth => _health;
	public float GetMaxHealth => maxHealth;
	
	private float healthValueCur;
	

	private void Awake()
	{
		healthValueCur = healthValueMax;
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
    

	public void ChangeHealthBarValue(float newAmount)
    {
		healthValueCur = newAmount;
		UpdateHealthBar();
    }

	public void SetMaxHealth(float newAmount) 
	{ 
		healthValueMax = newAmount;
		UpdateHealthBar();
	}
    private void UpdateHealthBar()
    {
	    var fillAmount = healthValueCur / healthValueMax;
	    if (!useScale)
		    healthBarFill.fillAmount = fillAmount;
	    else
		    healthBarFill.transform.DOScaleX(fillAmount, 0.5f);
	    healthValueCur = Mathf.Clamp(healthValueCur, 0, healthValueMax);
	    if(sliderTextMesh)
		    sliderTextMesh.text = healthValueCur + " / " + healthValueMax;
	}
	*/

}
