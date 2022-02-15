using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class PotionController : Item
{

    [Serializable]
    public class HealthPotionSettings
    {
        public float healingAmount;
        public bool isHealth = false;
    }

    [Serializable]
    public class ManaPotionSettings
    {
        public float manaAmount;
        public bool isMana = false;
    }
    

    public float potionEffectCooldown;
    public float lastEffected;
    public float potionDuration;
    public float tempDuration;
    
    public HealthPotionSettings healthPotionSettings = new HealthPotionSettings();
    public ManaPotionSettings manaPotionSettings = new ManaPotionSettings();
    
    
    public void prepare()
    {
        tempDuration = potionDuration;
    }

    

    public override void Use()
    {

        
        if (name.Equals("HealthPotion"))
        {
            Debug.Log("Called Use in side of Potion Controller For Health!");
            prepare();
            GameManager.instance.potionController = this;
            if (GameManager.instance.player.hp == GameManager.instance.player.maxHP)
            {
                Debug.Log("Cant use that potion you are already full health!");
                //GameManager.instance.ShowText("Cant use that potion you are already full health!", 15, Color.blue, GameManager.instance.player.transform.position, Vector3.up * 30, 1.5f);
                return;
            }
            
            RemoveFromInventory();
            base.Use();
            healthPotionSettings.isHealth = true;
        }

        if (name.Equals("ManaPotion"))
        {
            Debug.Log("Called Use in side of Potion Controller For Mana!");
            prepare();
            GameManager.instance.potionController = this;
            if (GameManager.instance.player.mana == GameManager.instance.player.maxMana)
            {
                Debug.Log("Cant use that potion you are already full mana!");
                //GameManager.instance.ShowText("Cant use that potion you are already full health!", 15, Color.blue, GameManager.instance.player.transform.position, Vector3.up * 30, 1.5f);
                return;
            }

            RemoveFromInventory();
            base.Use();
            manaPotionSettings.isMana = true;
        }
        
        
        

    }
    

}