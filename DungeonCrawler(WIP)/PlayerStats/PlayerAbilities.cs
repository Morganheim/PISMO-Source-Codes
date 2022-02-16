using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private PlayerStats stats;

    private string[] abilityName =
    {
        // ATHLETICS (0, 1)
        "Quick Feet", "Quick Strike",
        // MELEE (2, 3)
        "Slash", "Flurry",
        // MENTALITY (4, 5)
        "Willpower", "Nirvana",
        // RANGED (6, 7)
        "Aim", "Head Shot",
        // SORCERY (8, 9)
        "Heal", "Fireball",
        // STRENGTH (10, 11)
        "Mule", "Brute"
    };

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    public float QuickFeet()
    {
        if(stats.unlockedAbilities.Contains("Quick Feet") && stats.dodge <= 4)
        {
            return 2; //add to dodge
        }
        else
        {
            return 0;
        }
    }

    public float QuickStrike()
    {
        if(stats.unlockedAbilities.Contains("Quick Strike"))
        {
            return 0.85f; //multiply with attack timer
        }
        else
        {
            return 1;
        }
    }

    public float Slash()
    {
        if (stats.unlockedAbilities.Contains("Slash") && stats.stamina >= SlashStaminaCost())
        {
            return 5; //extra melee damage
        }
        else
        {
            return 0;
        }
    }
    public float SlashStaminaCost()
    {
        if (stats.unlockedAbilities.Contains("Slash"))
        {
            return 5;
        }
        else
        {
            return 0;
        }
    }

    public bool Flurry()
    {
        int chance = Random.Range(0, 101);
        if (chance <= 15)
        {
            if (stats.unlockedAbilities.Contains("Flurry") && stats.stamina >= FlurryStaminaCost())
            {
                return true; //two attacks
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public float FlurryStaminaCost()
    {
        if (stats.unlockedAbilities.Contains("Flurry"))
        {
            return 15;
        }
        else
        {
            return 0;
        }
    }

    public float Willpower()
    {
        if (stats.unlockedAbilities.Contains("Willpower"))
        {
            return 0.75f; //multiply with stamina cost
        }
        else
        {
            return 1;
        }
    }

    public float Nirvana()
    {
        if (stats.unlockedAbilities.Contains("Nirvana"))
        {
            int chance = Random.Range(0, 101);
            if (chance <= 10)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 1;
        }
    }

    public float Aim()
    {
        if (stats.unlockedAbilities.Contains("Aim") && stats.stamina >= AimStaminaCost())
        {
            return 3; //extra ranged damage
        }
        else
        {
            return 0;
        }
    }
    public float AimStaminaCost()
    {
        if (stats.unlockedAbilities.Contains("Aim"))
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    public float HeadShot()
    {
        if (stats.unlockedAbilities.Contains("Head Shot") && stats.stamina >= HeadShotStaminaCost())
        {
            return 1.5f; //multiply with damage
        }
        else
        {
            return 1;
        }
    }
    public float HeadShotStaminaCost()
    {
        if (stats.unlockedAbilities.Contains("Head Shot"))
        {
            return 25;
        }
        else
        {
            return 0;
        }
    }

    public float HealAttackTimer()
    {
        if (stats.unlockedAbilities.Contains("Heal"))
        {
            return 7;
        }
        else
        {
            return 0;
        }
    }
    public float HealStaminaCost()
    {
        if (stats.unlockedAbilities.Contains("Heal"))
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }

    public float FireballAttackTimer()
    {
        if (stats.unlockedAbilities.Contains("Fireball"))
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
    public float FireballStaminaCost()
    {
        if (stats.unlockedAbilities.Contains("Fireball"))
        {
            return 20;
        }
        else
        {
            return 0;
        }
    }

    public float Mule()
    {
        if (stats.unlockedAbilities.Contains("Mule"))
        {
            return 15;
        }
        else
        {
            return 0;
        }
    }

    public float Brute()
    {
        if (stats.unlockedAbilities.Contains("Brute"))
        {
            int chance = Random.Range(0, 101);
            if (chance <= 15)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 1;
        }
    }
}
