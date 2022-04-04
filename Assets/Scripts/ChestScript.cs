using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestScript : Interactable
{

    public int moneyAmount = 25;


    public override void Interact()
    {
        base.Interact();
        GameManager.instance.player.gainedMoney = moneyAmount;
        GameManager.instance.player.money += moneyAmount;
        
        //GameManager.instance.player.hudSettings.hud.gainedMoney.gameObject.SetActive(true);
        GameManager.instance.hud.gainedMoney.gameObject.SetActive(true);
        Invoke("DisableGainedMoney", 1.5f);
    }

    void DisableGainedMoney()
    {
        GameManager.instance.hud.gainedMoney.gameObject.SetActive(false);
    }

}
