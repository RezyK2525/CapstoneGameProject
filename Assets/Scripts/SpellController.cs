using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Inventory/Spell")]
public class SpellController : Item
{

    [Serializable]
    public class SpellSettings
    {
        public bool isFireball = false;

        public bool isGates = false;

        public bool isMagicBullet = false;

    }

    public SpellSettings spellSettings = new SpellSettings();
    public override void Use()
    {
        GameManager.instance.spellController = this;
        if (name.Equals("Fireball"))
        {
            // Debug.Log("Called Use in side of SpellController for Fireball");

            spellSettings.isFireball = true;

        }
        
        if (name.Equals("MagicBullet"))
        {
            // Debug.Log("Called Use in side of SpellController for MagicBullet");

            spellSettings.isMagicBullet = true;

        }
        
        if (name.Equals("Gates"))
        {
            Debug.Log("Called Use in side of SpellController for Gates");

            spellSettings.isGates = true;

        }



    }

}
