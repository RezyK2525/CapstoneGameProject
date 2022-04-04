using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // All items the player can interact with:
    // Chest, Items, Buffs, Money

    public float radius = 3f;
    //Transform player;
    bool hasInteracted = false;

    void Start()   
    {
        //player = 
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted");
        hasInteracted = true;
        // Do something here
    }


    public void Update()
    {
        if (!hasInteracted)
        {
            float distance = Vector3.Distance(GameManager.instance.player.transform.position, transform.position);
            if (distance <= radius)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.instance.hud.interactField.gameObject.SetActive(false);
                    Interact();
                }
                
                
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // make it so only the player who is in distance has it turned on
            // use collider? move hud into player instead of GM?
            GameManager.instance.hud.interactField.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.hud.interactField.gameObject.SetActive(false);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
