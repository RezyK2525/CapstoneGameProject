using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 0.5f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    //all fighters can receivedamage/die
    protected virtual void ReceiveDamage(Damage dmg){

        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero,0.5f);


            if(hitPoint <= 0){
                hitPoint = 0;
                Death();

            }

        }

    }

    protected virtual void Death(){



    }



}
