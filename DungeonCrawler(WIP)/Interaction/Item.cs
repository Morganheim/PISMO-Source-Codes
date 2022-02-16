using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

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

    private void Start()
    {
        icon = data.icon;
        damage = data.damage;
        attackTimer = data.attackTimer;
        hitChance = data.hitChance;
        isRanged = data.isRanged;
        isAmmo = data.isAmmo;
        protection = data.protection;
        healEffect = data.healEffect;
        noteContent = data.noteContent;
        weight = data.weight;
        isEquipable = data.isEquipable;
        isConsumable = data.isConsumable;
    }
}
