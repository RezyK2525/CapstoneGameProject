using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Inventory/Buff")]
public class BuffController : Item
{
    
    [Serializable]
    public class BuffTypesSettings
    {

        public float HP;
        public float MANA;
        public float STRENGTH;
        public float SPELLPOWER;
        public float ManaRegenRate;
        public float DEFENSE;

        

    }


    public BuffTypesSettings buffTypesSettings = new BuffTypesSettings();

    public override void Use()
    {

        if (name.Equals("HPbuff"))
        {
            GameManager.instance.player.stats.maxHP += buffTypesSettings.HP;
        }
        if (name.Equals("MANAbuff"))
        {
            GameManager.instance.player.stats.maxMana += buffTypesSettings.MANA;
        }
        if (name.Equals("STRENGTHbuff"))
        {
            GameManager.instance.player.stats.strength += buffTypesSettings.STRENGTH;
        }
        if (name.Equals("SPELLPOWERbuff"))
        {
            GameManager.instance.player.stats.spellPower += buffTypesSettings.SPELLPOWER;
        }
        if (name.Equals("ManaRegenRatebuff"))
        {
            GameManager.instance.player.stats.manaRegenRate += buffTypesSettings.ManaRegenRate;
        }
        if (name.Equals("DEFENSEbuff"))
        {
            GameManager.instance.player.stats.defense += buffTypesSettings.DEFENSE;
        }
    }
    


}
