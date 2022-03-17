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
                    if (GameManager.instance.player.stats.hp == GameManager.instance.player.stats.maxHP)
                    {
                        return;
                    }
                    GameManager.instance.player.stats.hp += GameManager.instance.potionController.healthPotionSettings.healingAmount;
                    GameManager.instance.player.hudSettings.healthBar.SetValue(GameManager.instance.player.stats.hp);
                    if (GameManager.instance.player.stats.hp > GameManager.instance.player.stats.maxHP)
                    {
                        GameManager.instance.player.stats.hp = GameManager.instance.player.stats.maxHP;
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
                    if (GameManager.instance.player.stats.mana == GameManager.instance.player.stats.maxMana)
                    {
                        return;
                    }
                    GameManager.instance.player.stats.mana += GameManager.instance.potionController.manaPotionSettings.manaAmount;
                    GameManager.instance.player.hudSettings.manaBar.SetValue(GameManager.instance.player.stats.mana);
                    if (GameManager.instance.player.stats.mana > GameManager.instance.player.stats.maxMana)
                    {
                        GameManager.instance.player.stats.mana = GameManager.instance.player.stats.maxMana;
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