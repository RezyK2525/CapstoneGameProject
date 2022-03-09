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
            Fighter enemy = coll.GetComponent<Fighter>();
            enemy.ReceiveDamage(100);
        }
    }
}
