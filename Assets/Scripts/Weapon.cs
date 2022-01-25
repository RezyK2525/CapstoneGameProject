using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{ 
    //Damage struct
    public int damagePoint = 1;
    public float pushForce = 25;
    public SpriteRenderer spriteRenderer;


    protected override void Start(){

        base.Start();
    }

    protected override void OnCollide(Collider2D coll){

        if(coll.tag == "Fighter"){
            if(coll.name == "Player"){
                return;
            }

            //create a new damage object then send it to fighter weve hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void SwapWeapon(Equipment item)
    {
        damagePoint = item.damage;
        pushForce = item.pushForce;
        spriteRenderer.sprite = item.sprite;
    }

    public void SwapHand()
    {
        damagePoint = 1;
        pushForce = 10;
        spriteRenderer.sprite = null;
    }





}
