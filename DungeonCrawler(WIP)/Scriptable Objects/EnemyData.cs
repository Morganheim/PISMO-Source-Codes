using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName ="Characters/Enemies", order =55)]
public class EnemyData : ScriptableObject
{
    public string enemyType;

    public float hp;
    public float stamina;

    public float staminaRegenRate;

    public float hitChance;

    public int dodge;
    public int protection;

    public float poisonResistance;
    public float fireResistance;
    public float mentalResistance;

    public float attackTimer;

    public float movementSpeed;
    public float attackSpeed;

    public bool freeze = false;

    public int expGain;

    [Header("Regular Attack")]
    public int damage;
    public int damagePerSecond;
    public float damageDuration;
    public int attackStaminaCost;
    public string damageType1;
    public string damageType2;

    [Header("Special Attack")]
    public string specialAttack;
    public int specialAttackDamage;
    public int specialAttackDamagePerSecond;
    public float specialAttackDamageDuration;
    public string specialAttackDamageType;
    public int specialAttackStaminaCost;
    public string specialAttackeffect;

    
}
