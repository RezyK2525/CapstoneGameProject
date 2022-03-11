using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
	// public GameObject healthBarUI;
	public StatusBar healthBar;
	public StatusBar manaBar;
	public StatusBar cooldownBar;

	public KeyCode pauseKey;
	public GameObject pauseHUD;
	public GameObject[] secondaryPauseMenus;

   [SerializeField] private TMP_Text healthValue = null;
	[SerializeField] private TMP_Text manaValue = null;
	[SerializeField] private TMP_Text moneyValue = null;

	// Start is called before the first frame update
	void Start()
	{
		healthBar.SetMax(GameManager.instance.player.maxHP);
		manaBar.SetMax(GameManager.instance.player.maxMana);
		// cooldownBar.SetMax(GameManager.instance.player.maxMana);
	}
	
    // Update is called once per frame
    void Update()
    {
		// check for pause
		if (Input.GetKeyDown(pauseKey))
        {
			if (!GameManager.instance.isPaused)
            {
				GameManager.instance.PauseGame();
				pauseHUD.SetActive(true);

            }
            else
            {
				// reset menu to initial state
                foreach ( GameObject menu in secondaryPauseMenus)
					menu.SetActive(false);
				pauseHUD.transform.GetChild(0).gameObject.SetActive(true);


				GameManager.instance.ResumeGame();
				pauseHUD.SetActive(false);
            }

		}
	

		// update values
		healthBar.SetValue(GameManager.instance.player.hp);
		manaBar.SetValue(GameManager.instance.player.mana);

		healthValue.text = ""+GameManager.instance.player.hp;
		manaValue.text = ""+GameManager.instance.player.mana;
		moneyValue.text = ""+GameManager.instance.player.money;
	}


}
