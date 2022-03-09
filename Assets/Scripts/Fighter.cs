using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Stats
    public float maxHP = 100;
    public float hp = 100;
    public float maxMana = 100;
    public float mana = 100;
    public float manaRegenRate = 2f;
    public float strength = 25;
    public float defense = 25;
    public float spellPower = 25;

    //public EnemyAiMovement enemyAiMovement;
    public float hitPoint = 100f;
    public float maxHitPoint = 100f;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 0.5f;
    protected float lastImmune;

    //push ?? ?? ? ?? ? ?
    protected Vector3 pushDirection;


    //all fighters can receivedamage/die
    public void ReceiveDamage(float dmg){

        //Debug.Log(EquipmentManager.instance.currentEquipment[0].name);

        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            maxHP -= dmg + (EquipmentManager.instance.currentEquipment[0].strengthModifier * GameManager.instance.player.strength);
            //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength)).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);


            if(maxHP <= 0){
                maxHP = 0;
                Death();

            }
        } 
    }
   
    
    
    
    public void ReceiveMagicDamage(float dmg){
        //if(Time.time - lastImmune > immuneTime){
        if(true){
            lastImmune = Time.time;

            GameManager.instance.enemyAiMovement.enemyHp -= dmg;
            hitPoint -= dmg;// + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength);
            //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength)).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);



            GameManager.instance.enemyAiMovement.healthBar.healthBarUI.SetActive(true);
            GameManager.instance.enemyAiMovement.healthBar.SetValue(GameManager.instance.enemyAiMovement.enemyHp);
            
            
            //enemyAiMovement.healthBar.healthBarUI.SetActive(true);
            //enemyAiMovement.healthBar.SetHealth(hitPoint);
            
            if(hitPoint <= 0){
                hitPoint = 0;
                Death();

            }
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
