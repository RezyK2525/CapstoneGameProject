using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Weapon")]
public class WeaponItem : Item
{

    public int damagePoint;
    public float pushForce;
    public Sprite sprite;

    readonly WeaponItem item;

    public override void Use()
    {
        // Equip This Weapon

    
        Debug.Log("Using " + name);
    }


}
