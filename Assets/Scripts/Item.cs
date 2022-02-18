using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isPurchased = false;
    public int value;

    public virtual void Use ()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);

        GameManager.instance.hotbar.AddItem(this);
        //Debug.Log(GameManager.instance.hotbar);
        //Debug.Log(this);

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);

    }

}
