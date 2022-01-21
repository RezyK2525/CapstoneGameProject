using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int[] damagePoint = { 1, 2, 3 };
    public float[] pushForce = { 25, 24, 25 };

    //Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

 
    private Animator anim;


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
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);

        }
    }


    
    public void Swing(){

        anim.SetTrigger("Swing");
    }
    

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // Change stats

    }

    // Might not need this
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    }




}
