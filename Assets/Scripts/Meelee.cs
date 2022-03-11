using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meelee : MonoBehaviour
{ 

    void OnTriggerEnter(Collider coll)
    {
        // Player Attacking Enemy
        if(coll.tag == "Enemy")
        {
            Collider collide = coll;
            GameManager.instance.enemyAiMovement = collide.GetComponent<EnemyAiMovement>();
            collide.GetComponent<EnemyAiMovement>().ReceiveDamage(100);
            //coll.GetComponent<Fighter>().reciveDamage(100);

        }
    }
}
