using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : Interactable
{
    public Transform player;

    public override void Interact()
    {
        player.position = new Vector3(11.886f, 2.489f, 29.929f);
        Debug.Log("Player teleported");
        
        // Do something here
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            player = col.transform;
        }
    }
}
