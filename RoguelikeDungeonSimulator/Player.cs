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

        Console.WriteLine($"✨ Joueur créé: {Name}");
        Console.WriteLine($"   HP: {HP}, ATK: {Attack}, DEF: {Defense}");
    }

    /// <summary>
    /// Obtient l'instance unique du joueur (Singleton)
    /// </summary>
    public static Player GetInstance()
    {
        if (instance == null)
            instance = new Player();
        return instance;
    }

    /// <summary>
    /// Réinitialise l'instance unique (utile pour les tests)
    /// </summary>
    public static void ResetInstance()
    {
        instance = null;
    }

    /// <summary>
    /// Le joueur attaque un ennemi
    /// </summary>
    public void AttackEnemy(Enemy enemy)
    {
        int damage = CalculateDamage(enemy);
        Console.WriteLine($"\n⚔️  {Name} attaque {enemy.Name} pour {damage} dégâts!");
        enemy.TakeDamage(damage);
    }

    /// <summary>
    /// Le joueur reçoit des dégâts
    /// </summary>
    public void TakeDamage(int damage)
    {
        int reducedDamage = damage - Defense;
        if (reducedDamage < 1)
            reducedDamage = 1;

        HP -= reducedDamage;
        if (HP < 0)
            HP = 0;

        Console.WriteLine($"💔 {Name} subit {reducedDamage} dégâts (PV: {HP}/{MaxHP})");
    }

    /// <summary>
    /// Calcule les dégâts infligés
    /// </summary>
    public int CalculateDamage(IEntity target)
    {
        return Attack;
    }

    /// <summary>
    /// Le joueur gagne de l'expérience
    /// </summary>
    public void GainExperience(int xpAmount)
    {
        experienceSystem.GainXP(xpAmount);

        // Bonus de statistiques à chaque montée de niveau
        if (experienceSystem.CurrentLevel > 1)
        {
            ApplyLevelUpBonuses();
        }
    }

    /// <summary>
    /// Applique les bonus de montée de niveau
    /// </summary>
    private void ApplyLevelUpBonuses()
    {
        int levelDifference = experienceSystem.CurrentLevel - 1;
        MaxHP = BASE_HP + (levelDifference * HP_PER_LEVEL);
        Attack = BASE_ATTACK + (levelDifference * ATTACK_PER_LEVEL);
        Defense = BASE_DEFENSE + (levelDifference * DEFENSE_PER_LEVEL);

        // Restaurer les PV complètement
        HP = MaxHP;

        Console.WriteLine($"📊 Statistiques augmentées!");
        Console.WriteLine($"   HP: {MaxHP}, ATK: {Attack}, DEF: {Defense}");
    }

    /// <summary>
    /// Équipe une arme
    /// </summary>
    public void EquipWeapon(IEquipment weapon)
    {
        if (EquippedWeapon != null)
        {
            EquippedWeapon.RemoveBonus(this);
        }

        EquippedWeapon = weapon;
        weapon.ApplyBonus(this);
    }

    /// <summary>
    /// Équipe une armure
    /// </summary>
    public void EquipArmor(IEquipment armor)
    {
        if (EquippedArmor != null)
        {
            EquippedArmor.RemoveBonus(this);
        }

        EquippedArmor = armor;
        armor.ApplyBonus(this);
    }

    /// <summary>
    /// Équipe un anneau
    /// </summary>
    public void EquipRing(IEquipment ring)
    {
        if (EquippedRing != null)
        {
            EquippedRing.RemoveBonus(this);
        }

        EquippedRing = ring;
        ring.ApplyBonus(this);
    }

    /// <summary>
    /// Vérifie si le joueur est vivant
    /// </summary>
    public bool IsAlive()
    {
        return HP > 0;
    }

    public override string ToString()
    {
        var equipment = new System.Collections.Generic.List<string>();
        if (EquippedWeapon != null) equipment.Add(EquippedWeapon.Name);
        if (EquippedArmor != null) equipment.Add(EquippedArmor.Name);
        if (EquippedRing != null) equipment.Add(EquippedRing.Name);

        string equipmentStr = equipment.Count > 0 ? string.Join(", ", equipment) : "Aucun";

        return $"{Name} | Niveau {experienceSystem.CurrentLevel} | PV: {HP}/{MaxHP} | ATK: {Attack} | DEF: {Defense}\n" +
               $"Équipement: {equipmentStr}";
    }
}
