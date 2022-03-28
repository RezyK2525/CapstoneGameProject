using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
	public GameObject healthBarUI;
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMax(float max)
	{
		slider.maxValue = max;
		slider.value = max;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetValue(float max)
	{
		slider.value = max;

		fill.color = gradient.Evaluate(slider.normalizedValue);

		//fill.color = gradient.Evaluate(slider.normalizedValue);

	}

}
