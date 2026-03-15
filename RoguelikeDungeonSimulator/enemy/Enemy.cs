// Classe de base pour tous les ennemis
public abstract class Enemy : IEntity
{
    public string Name { get; protected set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int XPReward { get; protected set; }

    protected Enemy(string name, int hp, int attack, int defense, int xpReward)
    {
        Name = name;
        HP = hp;
        MaxHP = hp;
        Attack = attack;
        Defense = defense;
        XPReward = xpReward;
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

        Console.WriteLine($"{Name} subit {reducedDamage} dégâts (PV: {HP}/{MaxHP})");
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

    public override string ToString()
    {
        return $"{Name} - PV: {HP}/{MaxHP}, Attaque: {Attack}, Défense: {Defense}";
    }
}
