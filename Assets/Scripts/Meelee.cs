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
            GameManager.instance.enemyAI = collide.GetComponent<EnemyAI>();
            collide.GetComponent<EnemyAI>().ReceiveDamage(100);
            //coll.GetComponent<Fighter>().reciveDamage(100);

        }
    }
}
