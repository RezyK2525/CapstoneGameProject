using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestScript : Interactable
{

    public int MoneyAmount = 25;


    public override void Interact()
    {
        base.Interact();
        GameManager.instance.player.money += MoneyAmount;
    }

}
