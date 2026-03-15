// Joueur - Singleton Pattern
// Une seule instance du joueur existe dans le jeu
public class Player : IEntity
{
    private static Player instance;

    // Statistiques
    public string Name { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    // Équipement
    public IEquipment EquippedWeapon { get; private set; }
    public IEquipment EquippedArmor { get; private set; }
    public IEquipment EquippedRing { get; private set; }

    // Progression
    private Experience experienceSystem;

    public int CurrentLevel => experienceSystem.CurrentLevel;

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
        Name = "Héros";
        HP = BASE_HP;
        MaxHP = BASE_HP;
        Attack = BASE_ATTACK;
        Defense = BASE_DEFENSE;
        experienceSystem = new Experience();

        Console.WriteLine($"   HP: {HP}, ATK: {Attack}, DEF: {Defense}");
    }


    public static Player GetInstance()
    {
        if (instance == null)
            instance = new Player();
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


    public void GainExperience(int xpAmount)
    {
        experienceSystem.GainXP(xpAmount);

        // Bonus de statistiques à chaque montée de niveau
        if (experienceSystem.CurrentLevel > 1)
        {
            ApplyLevelUpBonuses();
        }
    }


    private void ApplyLevelUpBonuses()
    {
        int levelDifference = experienceSystem.CurrentLevel - 1;
        MaxHP = BASE_HP + (levelDifference * HP_PER_LEVEL);
        Attack = BASE_ATTACK + (levelDifference * ATTACK_PER_LEVEL);
        Defense = BASE_DEFENSE + (levelDifference * DEFENSE_PER_LEVEL);

        HP = MaxHP;

        Console.WriteLine($"   HP: {MaxHP}, ATK: {Attack}, DEF: {Defense}");
    }

    public void EquipWeapon(IEquipment weapon)
    {
        if (EquippedWeapon != null)
        {
            EquippedWeapon.RemoveBonus(this);
        }

        EquippedWeapon = weapon;
        weapon.ApplyBonus(this);
    }
    public void EquipArmor(IEquipment armor)
    {
        if (EquippedArmor != null)
        {
            EquippedArmor.RemoveBonus(this);
        }

        EquippedArmor = armor;
        armor.ApplyBonus(this);
    }

    public void EquipRing(IEquipment ring)
    {
        if (EquippedRing != null)
        {
            EquippedRing.RemoveBonus(this);
        }

        EquippedRing = ring;
        ring.ApplyBonus(this);
    }

    public bool IsAlive()
    {
        return HP > 0;
    }


}
