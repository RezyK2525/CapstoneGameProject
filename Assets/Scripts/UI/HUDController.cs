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
	public GameObject deathScreen;
	public GameObject[] secondaryPauseMenus;
	

   [SerializeField] private TMP_Text healthValue = null;
	[SerializeField] private TMP_Text manaValue = null;
	[SerializeField] private TMP_Text moneyValue = null;
	[SerializeField] public TMP_Text gainedMoney = null;
	[SerializeField] public TMP_Text interactField;

	// Start is called before the first frame update
	void Start()
	{
		
		interactField.gameObject.SetActive(false);
		gainedMoney.gameObject.SetActive(false);
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
		healthBar.SetValue(GameManager.instance.player.stats.hp);
		manaBar.SetValue(GameManager.instance.player.stats.mana);

		healthValue.text = ""+GameManager.instance.player.stats.hp;
		manaValue.text = ""+GameManager.instance.player.stats.mana;
		moneyValue.text = ""+GameManager.instance.player.money;
		gainedMoney.text = "+" + GameManager.instance.player.gainedMoney;
    }
	public void SetMax()
    {
		healthBar.SetMax(GameManager.instance.player.stats.maxHP);
		manaBar.SetMax(GameManager.instance.player.stats.maxMana);
	}

	public void ExitGame()
    {
		GameManager.instance.ExitGame();
    }

}
