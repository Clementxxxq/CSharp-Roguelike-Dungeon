public interface IEquipment
{
    string Name { get; }
    int AttackBonus { get; }
    int DefenseBonus { get; }
    int MaxHPBonus { get; }

    void ApplyBonus(Player player);
    void RemoveBonus(Player player);
}