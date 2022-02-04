using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public EnemyAiMovement enemyAiMovement;
    public float hitPoint = 100f;
    public float maxHitPoint = 100f;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 0.5f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    
    
    
    /*
    //all fighters can receivedamage/die
    protected virtual void ReceiveDamage(Damage dmg){

        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            //hitPoint -= dmg + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength);
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength)).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);


            if(hitPoint <= 0){
                hitPoint = 0;
                Death();

            }
        }
    }
    */
    
    
    
    public void ReceiveMagicDamage(float dmg){

        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;

            GameManager.instance.enemyAiMovement.enemyHp -= dmg;
            hitPoint -= dmg;// + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength);
            //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //GameManager.instance.ShowText((dmg.damageAmount + (GameManager.instance.inventoryUI.weaponSlot.getCurrentWeapon().percentModifier * GameManager.instance.player.strength)).ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);

            GameManager.instance.enemyAiMovement.healthBar.healthBarUI.SetActive(true);
            GameManager.instance.enemyAiMovement.healthBar.SetHealth(GameManager.instance.enemyAiMovement.enemyHp);
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
            GameManager.instance.player.healthBar.SetHealth(GameManager.instance.player.hp);

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
