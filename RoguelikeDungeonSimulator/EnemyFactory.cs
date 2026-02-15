// Factory Method Pattern - Centralise la création des ennemis
public class EnemyFactory
{
    public enum EnemyType
    {
        Rat,
        Warrior,
        Mage,
        Boss
    }

    // Crée un ennemi du type spécifié
    public static Enemy CreateEnemy(EnemyType type)
    {
        return type switch
        {
            EnemyType.Rat => new DungeonRat(),
            EnemyType.Warrior => new FallenWarrior(),
            EnemyType.Mage => new CorruptedMage(),
            EnemyType.Boss => new DungeonLord(),
            _ => throw new ArgumentException($"Type d'ennemi inconnu: {type}")
        };
    }

    // Crée un ennemi à partir d'une chaîne de caractères
    public static Enemy CreateEnemy(string enemyName)
    {
        return enemyName.ToLower() switch
        {
            "rat" => new DungeonRat(),
            "warrior" or "guerrier" => new FallenWarrior(),
            "mage" => new CorruptedMage(),
            "boss" or "seigneur" => new DungeonLord(),
            _ => throw new ArgumentException($"Type d'ennemi inconnu: {enemyName}")
        };
    }

// Crée une liste d'ennemis aléatoires pour peupler un donjon
    public static List<Enemy> GenerateEnemyWave(int count)
    {
        var random = new Random();
        var enemies = new List<Enemy>();
        var types = new[] { EnemyType.Rat, EnemyType.Warrior, EnemyType.Mage };

        for (int i = 0; i < count; i++)
        {
            var randomType = types[random.Next(types.Length)];
            enemies.Add(CreateEnemy(randomType));
        }

        return enemies;
    }
}
