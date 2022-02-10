using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    // Inventory UI
    public Sprite icon;
    
    // Equipment Stat Modifiers
    //public float defenseModifier;
    public float strengthModifier;
    public float spellPowerModifier;

    public float hpModifier;
    public float maxHPModifier;
    public float manaModifier;
    public float maxManaModifier;
    public float manaRegenRateModifier;

    // Equipment system
    public EquipmentSlot equipSlot;

    public override void Use()
    {
        base.Use();

        // Equip the item
        EquipmentManager.instance.Equip(this);
        ///GameManager.instance.inventoryUI.EquipWeapon(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot
{
        Weapon,
        Spell,
        Spell2,
        Potion,
        Potion2,
}