//Singleton Pattern - Assure qu'il n'y a qu'une seule instance du joueur
public class Player : IEntity
{
    private static Player instance;

    public string Name { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    private const int BASE_HP = 100;
    private const int BASE_ATTACK = 10;
    private const int BASE_DEFENSE = 5;
    private const int HP_PER_LEVEL = 10;
    private const int ATTACK_PER_LEVEL = 2;
    private const int DEFENSE_PER_LEVEL = 1;

    public IEquipment EquippedWeapon { get; private set; }
    public IEquipment EquippedArmor { get; private set; }
    public IEquipment EquippedRing { get; private set; }

    private Experience experience;

    // Obtient l'instance unique du joueur (Singleton)
    public static Player GetInstance()
    {
        if (instance == null)
            instance = new Player();
        return instance;
    }

    private Player()
    {
        Name = "Héros";
        MaxHP = BASE_HP;
        HP = BASE_HP;
        Attack = BASE_ATTACK;
        Defense = BASE_DEFENSE;
        experience = new Experience();
    }

    public void TakeDamage(int damage)
    {
        // Réduction des dégâts basée sur la défense
        int reducedDamage = damage - Defense;
        if (reducedDamage < 1)
            reducedDamage = 1;

        HP -= reducedDamage;
        if (HP < 0)
            HP = 0;
    }
    public void AttackEnemy(Enemy enemy)
    {
        int damage = CalculateDamage(enemy);

        enemy.TakeDamage(damage);
    }
    public int CalculateDamage(IEntity target)
    {
        // Calcul des dégâts inflligés à l'adversaire
        return Attack;
    }

    public bool IsAlive()
    {
        return HP > 0;
    }

    public void EquipWeapon(IEquipment weapon)
    {
        if (EquippedWeapon != null)
            EquippedWeapon.RemoveBonus(this);

        EquippedWeapon = weapon;
        EquippedWeapon.ApplyBonus(this);
    }
    public void EquipArmor(IEquipment armor)
    {
        if (EquippedArmor != null)
            EquippedArmor.RemoveBonus(this);

        EquippedArmor = armor;
        EquippedArmor.ApplyBonus(this);
    }
    public void EquipRing(IEquipment ring)
    {
        if (EquippedRing != null)
            EquippedRing.RemoveBonus(this);

        EquippedRing = ring;
        EquippedRing.ApplyBonus(this);
    }

    public void GainExperience(int xpAmount)
    {
        bool leveledUp = experience.GainXP(xpAmount);

        if (leveledUp)
        {
            ApplyLevelUpBonuses();
        }
    }

    private void ApplyLevelUpBonuses()
    {
        int levelDifference = experience.CurrentLevel - 1;
        MaxHP = BASE_HP + (levelDifference * HP_PER_LEVEL);
        Attack = BASE_ATTACK + (levelDifference * ATTACK_PER_LEVEL);
        Defense = BASE_DEFENSE + (levelDifference * DEFENSE_PER_LEVEL);

        HP = MaxHP;

    }
}