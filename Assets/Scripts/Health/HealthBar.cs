using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private TextMeshProUGUI sliderTextMesh;


	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

    public void SetHealth(float health)
	{
		slider.value = health;
	}

    public void UpdateHealthbarText(string maxValue, string value)
    {
        sliderTextMesh.text = maxValue + " / " + value;
    }
}
