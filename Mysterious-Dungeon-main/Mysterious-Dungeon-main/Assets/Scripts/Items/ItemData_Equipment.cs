using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    public ItemEffect[] itemEffects;

    [Header("Major stats")]
    public int strengh; // 1 point increate damage
    public int agility; //1 point increate critrate
    public int intelligence; //1 point increate magic
    public int vitality; //1 point increate health;

    [Header("Offensive stats")]
    public int damage;
    public int critRate;
    public int critDamage; // defautlt 150%

    [Header("Defensive stats")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightningDamage;

    [Header("Craft requirement")]
    public List<InventoryItem> craftingMaterial;
    public void AddModifiter()
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        playerStat.strengh.AddModifier(strengh);
        playerStat.agility.AddModifier(agility);
        playerStat.intelligence.AddModifier(intelligence);
        playerStat.vitality.AddModifier(vitality);

        playerStat.damage.AddModifier(damage);
        playerStat.critRate.AddModifier(critRate);
        playerStat.critDamage.AddModifier(critDamage);

        playerStat.maxHealth.AddModifier(maxHealth);
        playerStat.armor.AddModifier(armor);
        playerStat.evasion.AddModifier(evasion);
        playerStat.magicResistance.AddModifier(magicResistance);

        playerStat.fireDamage.AddModifier(fireDamage);
        playerStat.iceDamage.AddModifier(iceDamage);
        playerStat.lightningDamage.AddModifier(lightningDamage);
    }

    public void ExecuteItemEffect()
    {
        foreach(var item in itemEffects)
        {
            item.ExecuteEffect();
        }
    }
    public void RemoveModifiter() 
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        playerStat.strengh.RemoveModifier(strengh);
        playerStat.agility.RemoveModifier(agility);
        playerStat.intelligence.RemoveModifier(intelligence);
        playerStat.vitality.RemoveModifier(vitality);

        playerStat.damage.RemoveModifier(damage);
        playerStat.critRate.RemoveModifier(critRate);
        playerStat.critDamage.RemoveModifier(critDamage);

        playerStat.maxHealth.RemoveModifier(maxHealth);
        playerStat.armor.RemoveModifier(armor);
        playerStat.evasion.RemoveModifier(evasion);
        playerStat.magicResistance.RemoveModifier(magicResistance);

        playerStat.fireDamage.RemoveModifier(fireDamage);
        playerStat.iceDamage.RemoveModifier(iceDamage);
        playerStat.lightningDamage.RemoveModifier(lightningDamage);
    }
}
