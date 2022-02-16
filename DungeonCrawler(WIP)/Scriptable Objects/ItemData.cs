using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items", order = 55)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemType;
    public Sprite icon;

    [Header("Weapon Stats")]
    public float damage;
    public float attackTimer;
    public float hitChance;
    public bool isRanged;
    public bool isAmmo;

    [Header("Armour Stats")]
    public float protection;

    [Header("Potion Stats")]
    public float healEffect;

    [Header("Note Stats")]
    public string noteContent;

    [Header("Other Stats")]
    public float weight;
    public bool isEquipable;
    public bool isConsumable;
}
