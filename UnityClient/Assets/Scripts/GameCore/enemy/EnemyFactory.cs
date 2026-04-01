using System;
using System.Collections.Generic;
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
            EnemyType.Rat => new Rat(),
            EnemyType.Warrior => new Warrior(),
            EnemyType.Mage => new Mage(),
            EnemyType.Boss => new Boss(),
            _ => throw new ArgumentException($"Type d'ennemi inconnu: {type}")
        };
    }

    // Crée un ennemi à partir d'une chaîne de caractères
    public static Enemy CreateEnemy(string enemyName)
    {
        return enemyName.ToLower() switch
        {
            "rat" => new Rat(),
            "warrior" => new Warrior(),
            "mage" => new Mage(),
            "boss" => new Boss(),
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
    /// <summary>
    /// Creates a list of enemies with difficulty scaling
    /// </summary>
    public static List<Enemy> CreateEnemies(int count, int difficultyLevel = 1)
    {
        var random = new Random();
        var enemies = new List<Enemy>();

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = SelectEnemyByDifficulty(difficultyLevel, random);
            enemies.Add(enemy);
        }

        return enemies;
    }

    private static Enemy SelectEnemyByDifficulty(int difficultyLevel, Random random)
    {
        int roll = random.Next(100);

        if (difficultyLevel <= 2)
        {
            return roll < 80 
                ? new Rat()
                : new Warrior();
        }
        else if (difficultyLevel <= 4)
        {
            if (roll < 40)
                return new Rat();
            else if (roll < 80)
                return new Warrior();
            else
                return new Mage();
        }
        else
        {
            return roll < 50 
                ? new Warrior()
                : new Mage();
        }
    }}
