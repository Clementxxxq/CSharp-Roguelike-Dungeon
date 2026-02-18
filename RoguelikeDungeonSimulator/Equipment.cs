public abstract class Equipment : IEquipment
{
    public string Name { get; protected set; }
    public EquipmentRarity Rarity { get; protected set; }
    public int AttackBonus { get; protected set; }
    public int DefenseBonus { get; protected set; }
    public int MaxHPBonus { get; protected set; }

    protected Equipment(string name, EquipmentRarity rarity, int attackBonus = 0, int defenseBonus = 0, int maxHPBonus = 0)
    {
        Name = name;
        Rarity = rarity;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        MaxHPBonus = maxHPBonus;
    }

    public virtual void ApplyBonus(Player player)
    {
        player.Attack += AttackBonus;
        player.Defense += DefenseBonus;
        player.MaxHP += MaxHPBonus;
    }

    public virtual void RemoveBonus(Player player)
    {
        player.Attack -= AttackBonus;
        player.Defense -= DefenseBonus;
        player.MaxHP -= MaxHPBonus;
    }
}

public class RustyBlade : Equipment
{
    public RustyBlade() : base("Épée rouillée", EquipmentRarity.Basic, attackBonus: 3) { }
}

public class Longsword : Equipment
{
    public Longsword() : base("Épée longue", EquipmentRarity.Advanced, attackBonus: 7) { }
}

public class HeroSword : Equipment
{
    public HeroSword() : base("Épée du héros", EquipmentRarity.Legendary, attackBonus: 20) { }
}

public class LightArmor : Equipment
{
    public LightArmor()
        : base("Armure légère", EquipmentRarity.Basic, defenseBonus: 2) { }
}

public class ChainArmor : Equipment
{
    public ChainArmor()
        : base("Armure en chaîne", EquipmentRarity.Advanced, defenseBonus: 5) { }
}

public class DragonArmor : Equipment
{
    public DragonArmor()
        : base("Armure du dragon", EquipmentRarity.Legendary, defenseBonus: 10, maxHPBonus: 20) { }
}

public class LifeRing : Equipment
{
    public LifeRing()
        : base("Anneau de vie", EquipmentRarity.Basic, maxHPBonus: 10) { }
}

public class PowerRing : Equipment
{
    public PowerRing()
        : base("Anneau de puissance", EquipmentRarity.Advanced, attackBonus: 5) { }
}

public class RingOfTheAncients : Equipment
{
    public RingOfTheAncients()
        : base("Anneau des anciens", EquipmentRarity.Legendary, attackBonus: 8, defenseBonus: 5, maxHPBonus: 15) { }
}