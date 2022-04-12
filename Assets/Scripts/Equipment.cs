using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    // Equipment Stat Modifiers
    //public float defenseModifier;
    public float strengthModifier;
    public float spellPowerModifier;

    public float hpModifier;
    public float maxHPModifier;
    public float manaModifier;
    public float maxManaModifier;
    public float manaRegenRateModifier;

    // Prefab holder
    public GameObject prefab;

    // Equipment system
    public EquipmentSlot equipSlot;

    public override void Use()
    {
        //base.Use();

        // Equip the item
        // EquipmentManager.instance.Equip(this);
        GameManager.instance.player.GetComponent<EquipmentManager>().Equip(this);
        RemoveFromHotbar();
        
    }

}

public enum EquipmentSlot
{
        Weapon,
        Spell,
        Spell2
}