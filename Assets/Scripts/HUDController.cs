using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
	// public GameObject healthBarUI;
	public StatusBar healthBar;
	public StatusBar manaBar;

    
	// Start is called before the first frame update
    void Start()
    {
		healthBar.SetMax(GameManager.instance.player.maxHP);
		manaBar.SetMax(GameManager.instance.player.maxMana);
	}

    // Update is called once per frame
    void Update()
    {
		healthBar.SetValue(GameManager.instance.player.hp);
		manaBar.SetValue(GameManager.instance.player.mana);
	}


}
