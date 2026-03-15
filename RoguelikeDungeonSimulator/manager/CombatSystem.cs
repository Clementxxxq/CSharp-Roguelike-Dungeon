public sealed class CombatResult
{
    public CombatResult(bool playerWon, int xpGained)
    {
        PlayerWon = playerWon;
        XPGained = xpGained;
    }

    public bool PlayerWon { get; }
    public int XPGained { get; }
}

public class CombatSystem
{
    public CombatResult RunCombat(Player player, Room room)
    {
        int round = 1;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n=== Debut combat - Salle {room.RoomNumber} ===");
        Console.ResetColor();

        while (player.IsAlive() && !room.AreAllEnemiesDefeated())
        {
            Console.WriteLine($"\n-- Tour {round} --");
            DisplayCombatState(player, room);

            HandlePlayerTurn(player, room);
            if (room.AreAllEnemiesDefeated())
                break;

            HandleEnemyTurn(player, room);
            round++;
        }

        bool playerWon = player.IsAlive();
        int xpGained = 0;

        if (playerWon)
        {
            xpGained = room.Enemies.Sum(e => e.XPReward);
            room.IsCleared = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSalle {room.RoomNumber} nettoyee! XP gagnee: {xpGained}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nVous avez ete vaincu...");
            Console.ResetColor();
        }

        return new CombatResult(playerWon, xpGained);
    }

    private static void DisplayCombatState(Player player, Room room)
    {
        Console.WriteLine($"Joueur -> HP: {player.HP}/{player.MaxHP}, ATK: {player.Attack}, DEF: {player.Defense}");
        Console.WriteLine("Ennemis:");

        var aliveEnemies = room.Enemies.Where(e => e.IsAlive()).ToList();
        for (int i = 0; i < aliveEnemies.Count; i++)
        {
            Enemy enemy = aliveEnemies[i];
            Console.WriteLine($"{i + 1}. {enemy.Name} ({enemy.HP}/{enemy.MaxHP} HP)");
        }
    }

    private static void HandlePlayerTurn(Player player, Room room)
    {
        while (true)
        {
            Console.WriteLine("Actions: 1) Attaquer  2) Voir equipement  3) Passer");
            int choice = ReadChoice(1, 3);

            if (choice == 2)
            {
                Console.WriteLine(player.GetEquipmentSummary());
                continue;
            }

            if (choice == 3)
            {
                Console.WriteLine("Vous passez votre tour.");
                break;
            }

            var aliveEnemies = room.Enemies.Where(e => e.IsAlive()).ToList();
            Console.WriteLine("Choisissez une cible:");
            for (int i = 0; i < aliveEnemies.Count; i++)
            {
                Enemy enemy = aliveEnemies[i];
                Console.WriteLine($"{i + 1}. {enemy.Name} ({enemy.HP}/{enemy.MaxHP} HP)");
            }

            int targetChoice = ReadChoice(1, aliveEnemies.Count);
            Enemy target = aliveEnemies[targetChoice - 1];

            Console.WriteLine($"Vous attaquez {target.Name}!");
            player.AttackEnemy(target);
            break;
        }
    }

    private static void HandleEnemyTurn(Player player, Room room)
    {
        foreach (Enemy enemy in room.Enemies.Where(e => e.IsAlive()))
        {
            int rawDamage = enemy.CalculateDamage(player);
            int expectedDamage = Math.Max(1, rawDamage - player.Defense);

            Console.WriteLine($"{enemy.Name} attaque et inflige {expectedDamage} degats.");
            player.TakeDamage(rawDamage);

            if (!player.IsAlive())
                break;
        }
    }

    private static int ReadChoice(int min, int max)
    {
        while (true)
        {
            Console.Write($"> ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
                return choice;

            Console.WriteLine($"Choix invalide. Entrez un nombre entre {min} et {max}.");
        }
    }
}