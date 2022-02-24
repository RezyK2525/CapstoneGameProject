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
        
    }

    public SpellSettings spellSettings = new SpellSettings();
    public override void Use()
    {
        GameManager.instance.spellController = this;
        if (name.Equals("Fireball"))
        {
            Debug.Log("Called Use in side of SpellController for Fireball");

            spellSettings.isFireball = true;

        }



    }

}
