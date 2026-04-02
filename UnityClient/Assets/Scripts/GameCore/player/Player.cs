// Joueur - Singleton Pattern
// Une seule instance du joueur existe dans le jeu
public class Player : IEntity
{
    private static Player instance;

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
        int reducedDamage = damage - Defense;
        if (reducedDamage < 1)
            reducedDamage = 1;

        HP -= reducedDamage;
        if (HP < 0)
            HP = 0;
    }

    public int CalculateDamage(IEntity target)
    {
        return Attack;
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
