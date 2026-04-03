// Joueur - Singleton Pattern
// Une seule instance du joueur existe dans le jeu
public class Player : IEntity
{
    private static Player instance;

    private const int POWER_STRIKE_COOLDOWN_TURNS = 3;
    private const float POWER_STRIKE_MULTIPLIER = 1.8f;

    // Statistiques
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    // Équipement (temporairement désactivé)

    // Progression
    private Experience experienceSystem;

    public int CurrentLevel => experienceSystem.CurrentLevel;
    public int CurrentXP => experienceSystem.CurrentXP;
    public int XPToNextLevel => experienceSystem.XPForNextLevel();
    public int PowerStrikeCooldownRemaining { get; private set; }

    // Statistiques de base
    private const int BASE_HP = 100;
    private const int BASE_ATTACK = 15;
    private const int BASE_DEFENSE = 5;

    // Récompenses par niveau: +10 HP, +2 ATK, +1 DEF
    private const int HP_PER_LEVEL = 10;
    private const int ATTACK_PER_LEVEL = 2;
    private const int DEFENSE_PER_LEVEL = 1;

    private Player()
    {
        HP = BASE_HP;
        MaxHP = BASE_HP;
        Attack = BASE_ATTACK;
        Defense = BASE_DEFENSE;
        experienceSystem = new Experience();
        PowerStrikeCooldownRemaining = 0;
    }


    public static Player GetInstance()
    {
        instance ??= new Player();
        return instance;
    }

    public static void ResetInstance()
    {
        instance = null;
    }


    public void AttackEnemy(Enemy enemy)
    {
        int damage = CalculateDamage(enemy);
        enemy.TakeDamage(damage);
    }


    public void TakeDamage(int damage)
    {
        TakeDamageWithDefense(damage, false);
    }

    public int TakeDamageWithDefense(int damage, bool isDefending)
    {
        int reducedDamage = isDefending ? (int)System.Math.Ceiling(damage * 0.5f) : damage;
        reducedDamage -= Defense;
        if (reducedDamage < 1)
            reducedDamage = 1;

        HP -= reducedDamage;
        if (HP < 0)
            HP = 0;

        return reducedDamage;
    }

    public int CalculateDamage(IEntity target)
    {
        return Attack;
    }

    public bool CanUsePowerStrike()
    {
        return PowerStrikeCooldownRemaining <= 0;
    }

    public int UsePowerStrike(IEntity target)
    {
        int baseDamage = CalculateDamage(target);
        int skillDamage = (int)System.Math.Ceiling(baseDamage * POWER_STRIKE_MULTIPLIER);
        PowerStrikeCooldownRemaining = POWER_STRIKE_COOLDOWN_TURNS;
        return skillDamage;
    }

    public void TickSkillCooldown()
    {
        if (PowerStrikeCooldownRemaining > 0)
        {
            PowerStrikeCooldownRemaining--;
        }
    }


    public bool GainExperience(int xpAmount)
    {
        if (xpAmount <= 0)
            return false;

        int previousLevel = experienceSystem.CurrentLevel;
        bool leveledUp = experienceSystem.GainXP(xpAmount);

        // Bonus de statistiques à chaque montée de niveau
        if (experienceSystem.CurrentLevel > previousLevel)
        {
            ApplyLevelUpBonuses();
        }

        return leveledUp;
    }


    private void ApplyLevelUpBonuses()
    {
        int levelDifference = experienceSystem.CurrentLevel - 1;
        int equipmentAttackBonus = 0;
        int equipmentDefenseBonus = 0;
        int equipmentHealthBonus = 0;

        MaxHP = BASE_HP + (levelDifference * HP_PER_LEVEL) + equipmentHealthBonus;
        Attack = BASE_ATTACK + (levelDifference * ATTACK_PER_LEVEL) + equipmentAttackBonus;
        Defense = BASE_DEFENSE + (levelDifference * DEFENSE_PER_LEVEL) + equipmentDefenseBonus;

        HP = MaxHP;
    }

    // Équipement temporairement désactivé

    public bool IsAlive()
    {
        return HP > 0;
    }

}
