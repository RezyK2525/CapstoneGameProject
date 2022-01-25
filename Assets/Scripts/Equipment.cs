using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{

    public EquipmentSlot equipSlot;
    public int damage;
    public int pushForce;
    public Sprite sprite;

    public override void Use()
    {

        base.Use();

        // Equip the item
        EquipmentManager.instance.Equip(this);
        GameManager.instance.inventoryUI.EquipWeapon(this);
        RemoveFromInventory();
    }
}
public enum EquipmentSlot
{
        Weapon,
        Ability,
        Potion,
}