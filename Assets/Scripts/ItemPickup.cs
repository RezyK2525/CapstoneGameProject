using UnityEngine;

public class ItemPickup : Collectable
{

    public Item item;
    protected override void OnCollect()
    {

        //base.OnCollect();

        if (!collected)
        {

            collected = true;
            bool waspickedUp = Inventory.instance.Add(item);

            if (waspickedUp)
            {
                Destroy(gameObject);
            }
  
        }
    }
}
