// Interface pour les entités du jeu
public interface IEntity
{
    int HP { get; set; }
    int MaxHP { get; set; }
    int Attack { get; set; }
    int Defense { get; set; }

    void TakeDamage(int damage);
    int CalculateDamage(IEntity target);
    bool IsAlive();
}
