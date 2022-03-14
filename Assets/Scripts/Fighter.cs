using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Stats
    public float maxHP;
    public float hp;
    public float maxMana;
    public float mana;
    public float manaRegenRate;
    public float strength;
    public float defense;
    public float spellPower;

    //public EnemyAiMovement enemyAiMovement
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 0.5f;
    protected float lastImmune;

    //push ?? ?? ? ?? ? ?
    protected Vector3 pushDirection;


    //all fighters can receivedamage/die
    public void ReceiveDamage(float dmg){

        //Debug.Log(EquipmentManager.instance.currentEquipment[0].name);
        Debug.Log("Damage: " + dmg);
        Debug.Log("Enemy Health: " + GameManager.instance.enemyAI.hp);
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            //Debug.Log("DMG BIG DAMAGE: " + dmg + (EquipmentManager.instance.currentEquipment[0].strengthModifier * GameManager.instance.player.strength));
            GameManager.instance.enemyAI.hp -= dmg + (EquipmentManager.instance.currentEquipment[0].strengthModifier * GameManager.instance.player.strength);
            //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength)).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);


            if(GameManager.instance.enemyAI.hp <= 0){
                GameManager.instance.enemyAI.hp = 0;
                Death();

            }
        } 
    }
   
    
    
    
    public void ReceiveMagicDamage(float dmg){
        //if(Time.time - lastImmune > immuneTime){

        
        

            lastImmune = Time.time;

            GameManager.instance.enemyAI.hp -= dmg;
            // + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength);
            //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength)).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);



            GameManager.instance.enemyAI.healthBar.healthBarUI.SetActive(true);
            GameManager.instance.enemyAI.healthBar.SetValue(GameManager.instance.enemyAI.hp);
            
            
            //enemyAiMovement.healthBar.healthBarUI.SetActive(true);
            //enemyAiMovement.healthBar.SetHealth(hitPoint);
            
            if(GameManager.instance.enemyAI.hp <= 0){
                GameManager.instance.enemyAI.hp = 0;
                Death();

            }
        
    }

    
    public void PlayerReceiveMagicDamage(float dmg)
    {
        //double dmgReduction = 0.2 * GameManager.instance.player.defense;
        
        
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            

            GameManager.instance.player.hp -= dmg;
            //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount - dmgReduction).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);

            GameManager.instance.player.healthBar.SetValue(GameManager.instance.player.hp);

            ////////GameManager.instance.player.healthBar.SetHealth(GameManager.instance.player.hp);


            //if(hitPoint <= 0){
                //hitPoint = 0;
                //Death();

            //}
        }
    }
    
/*
    protected virtual void PlayerReceiveDamage(Damage dmg)
    {
        //double dmgReduction = 0.2 * GameManager.instance.player.defense;
        
        
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            
            //Debug.Log((0.2 * GameManager.instance.player.defense));
            //GameManager.instance.player.hp -= (dmg.damageAmount - dmgReduction);
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount - dmgReduction).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);


            if(hitPoint <= 0){
                hitPoint = 0;
                Death();

            }
        }
    }
    
*/

    protected virtual void Death(){
        


    }


}
