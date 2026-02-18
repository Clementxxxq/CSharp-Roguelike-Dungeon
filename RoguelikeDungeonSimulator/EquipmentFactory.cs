public abstract class EquipmentFactory
{
    public abstract IEquipment CreateWeapon();
    public abstract IEquipment CreateArmor();
    public abstract IEquipment CreateRing();

    public static EquipmentFactory GetFactory(EquipmentRarity rarity)
    {
        return rarity switch
        {
            EquipmentRarity.Basic => new BasicEquipmentFactory(),
            EquipmentRarity.Advanced => new AdvancedEquipmentFactory(),
            EquipmentRarity.Legendary => new LegendaryEquipmentFactory(),
            _ => throw new ArgumentException($"Rareté inconnue: {rarity}")
        };
        //Choisir la factory en fonction de la rareté demandée
    }
}

public enum EquipmentRarity
{
    Basic,
    Advanced,
    Legendary
}

public class BasicEquipmentFactory : EquipmentFactory
// Implémente la création d'équipements basiques
{
    public override IEquipment CreateWeapon() => new RustyBlade();
    public override IEquipment CreateArmor() => new LightArmor();
    public override IEquipment CreateRing() => new LifeRing();
}

public class AdvancedEquipmentFactory : EquipmentFactory
// Implémente la création d'équipements avancés
{
    public override IEquipment CreateWeapon() => new Longsword();
    public override IEquipment CreateArmor() => new ChainArmor();
    public override IEquipment CreateRing() => new PowerRing();
}

public class LegendaryEquipmentFactory : EquipmentFactory
// Implémente la création d'équipements légendaires
{
    public override IEquipment CreateWeapon() => new HeroSword();
    public override IEquipment CreateArmor() => new DragonArmor();
    public override IEquipment CreateRing() => new RingOfTheAncients();
}