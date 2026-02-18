/// <summary>
/// Interface for all equipment items
/// </summary>
public interface IEquipment
{
    string Name { get; }
    int AttackBonus { get; }
    int DefenseBonus { get; }
    int MaxHPBonus { get; }

    void ApplyBonus(Player player);
    void RemoveBonus(Player player);
}

/// <summary>
/// Base class for all equipment types
/// </summary>
public abstract class Equipment : IEquipment
{
    public string Name { get; protected set; }
    public int AttackBonus { get; protected set; }
    public int DefenseBonus { get; protected set; }
    public int MaxHPBonus { get; protected set; }

    protected Equipment(string name, int attackBonus = 0, int defenseBonus = 0, int maxHPBonus = 0)
    {
        Name = name;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        MaxHPBonus = maxHPBonus;
    }

    public virtual void ApplyBonus(Player player)
    {
        player.Attack += AttackBonus;
        player.Defense += DefenseBonus;
        player.MaxHP += MaxHPBonus;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✓ Equipped: {Name}");
        Console.ResetColor();
    }

    public virtual void RemoveBonus(Player player)
    {
        player.Attack -= AttackBonus;
        player.Defense -= DefenseBonus;
        player.MaxHP -= MaxHPBonus;
    }

    public override string ToString()
    {
        return $"{Name} (+{AttackBonus} ATK, +{DefenseBonus} DEF, +{MaxHPBonus} HP)";
    }
}

// ═════════════════════════════════════════════════════════════════════════
// WEAPONS
// ═════════════════════════════════════════════════════════════════════════

public class RustyBlade : Equipment
{
    public RustyBlade() : base("Épée rouillée", attackBonus: 3) { }
}

public class Longsword : Equipment
{
    public Longsword() : base("Épée longue", attackBonus: 7) { }
}

public class HeroSword : Equipment
{
    public HeroSword() : base("Épée du héros", attackBonus: 15) { }
}

// ═════════════════════════════════════════════════════════════════════════
// ARMOR
// ═════════════════════════════════════════════════════════════════════════

public class LightArmor : Equipment
{
    public LightArmor() : base("Armure légère", defenseBonus: 2) { }
}

public class ChainArmor : Equipment
{
    public ChainArmor() : base("Armure en chaîne", defenseBonus: 5) { }
}

public class DragonArmor : Equipment
{
    public DragonArmor() : base("Armure du dragon", defenseBonus: 8, maxHPBonus: 15) { }
}

// ═════════════════════════════════════════════════════════════════════════
// RINGS
// ═════════════════════════════════════════════════════════════════════════

public class LifeRing : Equipment
{
    public LifeRing() : base("Anneau de vie", maxHPBonus: 10) { }
}

public class PowerRing : Equipment
{
    public PowerRing() : base("Anneau de puissance", attackBonus: 3) { }
}

public class RingOfTheAncients : Equipment
{
    public RingOfTheAncients() : base("Anneau des anciens", attackBonus: 5, defenseBonus: 3, maxHPBonus: 10) { }
}
