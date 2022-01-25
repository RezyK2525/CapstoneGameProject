using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class PotionController : Item
{

    public int healingAmount = 1;

    public float potionEffectCooldown = 2.5f;
    public bool isActive = false;
    public float lastEffected;
    public float potionDuration;
    public Sprite sprite;
    public float tempDuration;
    
    public void prepare()
    {
        tempDuration = potionDuration;
    }

    

    public override void Use()
    {
        prepare();
        GameManager.instance.potionController = this;
        if (GameManager.instance.player.hitPoint == GameManager.instance.player.maxHitPoint)
        {
            GameManager.instance.ShowText("Cant use that potion you are already full health!", 15, Color.blue, GameManager.instance.player.transform.position, Vector3.up * 30, 1.5f);
            return;
        }
        RemoveFromInventory();
        base.Use();
        isActive = true;


    }
        

    }
    
    
    




