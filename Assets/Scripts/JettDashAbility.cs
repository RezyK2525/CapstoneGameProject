using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JettDashAbility : Ability {

    public float dashVelocity;

    public override void Activate(GameObject parent){

        Mover movement = parent.GetComponent<Mover>();   
        movement.xSpeed = dashVelocity;
        movement.ySpeed = dashVelocity;
     
    }

    public override void BeginCooldown(GameObject parent)
    {
        Mover movement = parent.GetComponent<Mover>();
        movement.xSpeed = 0.75f;
        movement.ySpeed = 1.0f;
    }


}
