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

	public void SetMax(float health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetValue(float health)
	{
		slider.value = health;
<<<<<<< HEAD:Assets/Scripts/StatusBar.cs
		fill.color = gradient.Evaluate(slider.normalizedValue);
=======
		//fill.color = gradient.Evaluate(slider.normalizedValue);
>>>>>>> d884476ade1b6a72278daa933d6592ba3c3840bc:Assets/Scripts/HealthBar.cs
	}

}
