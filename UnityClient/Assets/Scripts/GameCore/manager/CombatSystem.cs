using System;
using System.Text;
using System.Linq;

public sealed class CombatResult
{
    public CombatResult(bool playerWon, int xpGained, string combatLog)
    {
        PlayerWon = playerWon;
        XPGained = xpGained;
        CombatLog = combatLog;
    }

    public bool PlayerWon { get; }
    public int XPGained { get; }
    public string CombatLog { get; }
}

public class CombatSystem
{
    public CombatResult RunCombat(Player player, Room room)
    {
        var logBuilder = new StringBuilder();
        int round = 1;

        logBuilder.AppendLine("=== Room " + room.RoomNumber + " Combat Start ===");

        while (player.IsAlive() && !room.AreAllEnemiesDefeated())
        {
            logBuilder.AppendLine("-- Round " + round + " --");
            logBuilder.AppendLine(BuildCombatState(player, room));

            HandlePlayerTurn(player, room, logBuilder);
            if (room.AreAllEnemiesDefeated())
                break;

            HandleEnemyTurn(player, room, logBuilder);
            round++;
        }

        bool playerWon = player.IsAlive();
        int xpGained = 0;

        if (playerWon)
        {
            xpGained = room.Enemies.Sum(e => e.XPReward);
            room.IsCleared = true;
            logBuilder.AppendLine("Room " + room.RoomNumber + " cleared, XP gained: " + xpGained + ".");
        }
        else
        {
            logBuilder.AppendLine("You were defeated.");
        }

        return new CombatResult(playerWon, xpGained, logBuilder.ToString().TrimEnd());
    }

    private static string BuildCombatState(Player player, Room room)
    {
        var summary = new StringBuilder();
        summary.AppendLine("Player -> HP: " + player.HP + "/" + player.MaxHP + ", ATK: " + player.Attack + ", DEF: " + player.Defense);
        summary.AppendLine("Enemies:");

        var aliveEnemies = room.Enemies.Where(e => e.IsAlive()).ToList();
        for (int i = 0; i < aliveEnemies.Count; i++)
        {
            Enemy enemy = aliveEnemies[i];
            summary.AppendLine((i + 1) + ". " + enemy.Name + " (" + enemy.HP + "/" + enemy.MaxHP + " HP)");
        }

        return summary.ToString().TrimEnd();
    }

    private static void HandlePlayerTurn(Player player, Room room, StringBuilder logBuilder)
    {
        var target = room.Enemies.FirstOrDefault(e => e.IsAlive());
        if (target == null)
            return;

        int damage = player.CalculateDamage(target);
        target.TakeDamage(damage);
        logBuilder.AppendLine("Player attacks " + target.Name + ", dealing " + damage + " damage.");
    }

    private static void HandleEnemyTurn(Player player, Room room, StringBuilder logBuilder)
    {
        foreach (Enemy enemy in room.Enemies.Where(e => e.IsAlive()))
        {
            int rawDamage = enemy.CalculateDamage(player);
            int expectedDamage = Math.Max(1, rawDamage - player.Defense);

            logBuilder.AppendLine(enemy.Name + " counterattacks, dealing " + expectedDamage + " damage.");
            player.TakeDamage(rawDamage);

            if (!player.IsAlive())
                break;
        }
    }
}
