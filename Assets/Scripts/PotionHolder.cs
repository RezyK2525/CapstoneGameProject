using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHolder : MonoBehaviour
{

    
    void Update()
    {
        
        
        if (GameManager.instance.potionController.isActive)
        {
            if (GameManager.instance.potionController.tempDuration > 0)
            {
                if (Time.time - GameManager.instance.potionController.lastEffected > GameManager.instance.potionController.potionEffectCooldown)
                {
                    GameManager.instance.potionController.lastEffected = Time.time;
                    GameManager.instance.player.Heal(GameManager.instance.potionController.healingAmount);
                    
                }
                GameManager.instance.potionController.tempDuration -= Time.deltaTime;
            }
            else
            {
                GameManager.instance.potionController.lastEffected = 0.0f;
                GameManager.instance.potionController.isActive = false;
                
                
            }
            
        }
        
    }
    
}
