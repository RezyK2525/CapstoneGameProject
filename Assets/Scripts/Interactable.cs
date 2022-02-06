using UnityEngine;

public class Interactable : MonoBehaviour
{
    // All items the player can interact with:
    // Chest, Items, Buffs, Money

    public float radius = 3f;
    Transform player;
    bool hasInteracted = false;

    void Start()   
    {
        player = GameManager.instance.player.transform;
    }

    public virtual void Interact()
    {
        Debug.Log("Inteacted");
        hasInteracted = true;
        // Do something here
    }


    public void Update()
    {
        if (!hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                    
                }
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
