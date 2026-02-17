public class Player : IEntity
{
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public Player()
    {
        MaxHP = 100;
        HP = MaxHP;
        Attack = 10;
        Defense = 5;
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

        Console.WriteLine($"Vous subissez {reducedDamage} dégâts (PV: {HP}/{MaxHP})");
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
}