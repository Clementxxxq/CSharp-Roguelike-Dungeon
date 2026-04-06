public interface ICombatant : IEntity
{
    int Attack { get; set; }
    int Defense { get; set; }

    void TakeDamage(int damage);
    int CalculateDamage(IEntity target);
}