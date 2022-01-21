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
        movement.ySpeed = 4.75f;
        movement.xSpeed = 5f;

    }


}
