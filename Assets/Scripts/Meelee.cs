using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meelee : MonoBehaviour
{ 

    void OnTriggerEnter(Collider collide)
    {
        // Player Attacking Enemy
        if(collide.tag == "Enemy")
        {
            GameManager.instance.enemyAI = collide.GetComponent<EnemyAI>();
            collide.GetComponent<EnemyAI>().ReceiveDamage(100);
            // collide.GetComponent<Fighter>().reciveDamage(100);

        }
    }

    public void enableSwordDmg()
    {
        EquipmentManager.instance.weaponHolder.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    public void disableSwordDmg()
    {
        EquipmentManager.instance.weaponHolder.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
