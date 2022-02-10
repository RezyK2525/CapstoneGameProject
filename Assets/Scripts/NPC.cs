using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Shop shop;

    public override void Interact()
    {
        //base.Interact();
        shop.OpenShop();
    }
}
