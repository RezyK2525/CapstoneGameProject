using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHolder : MonoBehaviour
{

    
    void Update()
    {
        
        if (GameManager.instance.potionController.healthPotionSettings.isHealth)
        {
            if (GameManager.instance.potionController.tempDuration > 0)
            {
                if (Time.time - GameManager.instance.potionController.lastEffected > GameManager.instance.potionController.potionEffectCooldown)
                {
                    GameManager.instance.potionController.lastEffected = Time.time;
                    //GameManager.instance.player.HealPot(GameManager.instance.potionController.healthPotionSettings.healingAmount);
                    if (GameManager.instance.player.hp == GameManager.instance.player.maxHP)
                    {
                        return;
                    }
                    GameManager.instance.player.hp += GameManager.instance.potionController.healthPotionSettings.healingAmount;
                    GameManager.instance.player.healthBar.SetValue(GameManager.instance.player.hp);
                    if (GameManager.instance.player.hp > GameManager.instance.player.maxHP)
                    {
                        GameManager.instance.player.hp = GameManager.instance.player.maxHP;
                    }
                    
                }
                GameManager.instance.potionController.tempDuration -= Time.deltaTime;
            }
            else
            {
                GameManager.instance.potionController.lastEffected = 0.0f;
                GameManager.instance.potionController.healthPotionSettings.isHealth = false;
                
                
            }
            
        }
        
        
        if (GameManager.instance.potionController.manaPotionSettings.isMana)
        {
            if (GameManager.instance.potionController.tempDuration > 0)
            {
                if (Time.time - GameManager.instance.potionController.lastEffected > GameManager.instance.potionController.potionEffectCooldown)
                {
                    GameManager.instance.potionController.lastEffected = Time.time;
                    //GameManager.instance.player.ManaPotRegen(GameManager.instance.potionController.manaPotionSettings.manaAmount);
                    if (GameManager.instance.player.mana == GameManager.instance.player.maxMana)
                    {
                        return;
                    }
                    GameManager.instance.player.mana += GameManager.instance.potionController.manaPotionSettings.manaAmount;
                    GameManager.instance.player.manaBar.SetValue(GameManager.instance.player.mana);
                    if (GameManager.instance.player.mana > GameManager.instance.player.maxMana)
                    {
                        GameManager.instance.player.mana = GameManager.instance.player.maxMana;
                    }
                    
                }
                GameManager.instance.potionController.tempDuration -= Time.deltaTime;
            }
            else
            {
                GameManager.instance.potionController.lastEffected = 0.0f;
                GameManager.instance.potionController.manaPotionSettings.isMana = false;
                
                
            }
            
        }
        
        
        
        
        
        
        
    }
    
}