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

    // Obtient l'instance unique du joueur (Singleton)
    public static Player GetInstance()
    {
        if (instance == null)
            instance = new Player();
        return instance;
    }

    public Player()
    {
        Name = "Héros";
        MaxHP = BASE_HP;
        HP = BASE_HP;
        Attack = BASE_ATTACK;
        Defense = BASE_DEFENSE;
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