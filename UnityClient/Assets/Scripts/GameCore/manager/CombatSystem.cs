using System;
using System.Text;
using System.Linq;

public enum PlayerAction
{
    Attack,
    Defend,
    Skill
}

public sealed class CombatResult
{
    public CombatResult(bool isCombatFinished, bool playerWon, int xpGained, string combatLog, string enemyIntentText)
    {
        IsCombatFinished = isCombatFinished;
        PlayerWon = playerWon;
        XPGained = xpGained;
        CombatLog = combatLog;
        EnemyIntentText = enemyIntentText;
    }

    public bool IsCombatFinished { get; }
    public bool PlayerWon { get; }
    public int XPGained { get; }
    public string CombatLog { get; }
    public string EnemyIntentText { get; }
}

public class CombatSystem
{
    public string PreviewEnemyIntents(Player player, Room room)
    {
        PrepareEnemyIntents(room, player);
        return BuildEnemyIntentSummary(room);
    }

    public CombatResult RunCombat(Player player, Room room, PlayerAction action)
    {
        var logBuilder = new StringBuilder();
        logBuilder.AppendLine("Round " + room.RoomNumber);
        logBuilder.AppendLine("Action: " + action);

        if (!player.IsAlive())
        {
            logBuilder.AppendLine("You are already defeated.");
            return new CombatResult(true, false, 0, logBuilder.ToString().TrimEnd(), "-");
        }

        if (room.AreAllEnemiesDefeated())
        {
            int xpAlready = room.Enemies.Sum(e => e.XPReward);
            room.IsCleared = true;
            logBuilder.AppendLine("Room already cleared.");
            return new CombatResult(true, true, xpAlready, logBuilder.ToString().TrimEnd(), "-");
        }

        PrepareEnemyIntents(room, player);
        logBuilder.AppendLine(BuildCombatState(player, room));

        bool defendThisRound = HandlePlayerTurn(player, room, action, logBuilder);
        if (!room.AreAllEnemiesDefeated())
        {
            HandleEnemyTurn(player, room, defendThisRound, logBuilder);
        }

        player.TickSkillCooldown();

        bool roomCleared = room.AreAllEnemiesDefeated();
        bool playerAlive = player.IsAlive();
        bool isCombatFinished = roomCleared || !playerAlive;
        bool playerWon = playerAlive && roomCleared;
        int xpGained = 0;
        string nextEnemyIntentText = "-";

        if (playerWon)
        {
            xpGained = room.Enemies.Sum(e => e.XPReward);
            room.IsCleared = true;
            logBuilder.AppendLine("Room " + room.RoomNumber + " cleared, XP gained: " + xpGained + ".");
        }
        else if (!playerAlive)
        {
            logBuilder.AppendLine("You were defeated.");
        }
        else
        {
            nextEnemyIntentText = BuildEnemyIntentSummary(room);
            logBuilder.AppendLine("Round ended. Choose your next action.");
        }

        return new CombatResult(isCombatFinished, playerWon, xpGained, logBuilder.ToString().TrimEnd(), nextEnemyIntentText);
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

    private static bool HandlePlayerTurn(Player player, Room room, PlayerAction action, StringBuilder logBuilder)
    {
        var target = room.Enemies.FirstOrDefault(e => e.IsAlive());
        if (target == null)
            return false;

        if (action == PlayerAction.Defend)
        {
            logBuilder.AppendLine("Player braces for impact. Incoming damage reduced this round.");
            return true;
        }

        if (action == PlayerAction.Skill && player.CanUsePowerStrike())
        {
            int skillDamage = player.UsePowerStrike(target);
            target.TakeDamage(skillDamage);
            logBuilder.AppendLine("Player uses Power Strike on " + target.Name + ", dealing " + skillDamage + " damage.");
            return false;
        }

        int damage = player.CalculateDamage(target);
        target.TakeDamage(damage);
        logBuilder.AppendLine("Player attacks " + target.Name + ", dealing " + damage + " damage.");
        return false;
    }

    private static void HandleEnemyTurn(Player player, Room room, bool playerDefending, StringBuilder logBuilder)
    {
        foreach (Enemy enemy in room.Enemies.Where(e => e.IsAlive()))
        {
            if (enemy.CurrentIntent == EnemyIntentType.Defend)
            {
                logBuilder.AppendLine(enemy.Name + " chooses to defend.");
                continue;
            }

            int rawDamage = enemy.CalculateDamage(player);
            int actualDamage = player.TakeDamageWithDefense(rawDamage, playerDefending);

            logBuilder.AppendLine(enemy.Name + " counterattacks, dealing " + actualDamage + " damage.");

            if (!player.IsAlive())
                break;
        }
    }

    private static void PrepareEnemyIntents(Room room, Player player)
    {
        foreach (Enemy enemy in room.Enemies.Where(e => e.IsAlive()))
        {
            enemy.DecideIntent(player);
        }
    }

    private static string BuildEnemyIntentSummary(Room room)
    {
        var aliveEnemies = room.Enemies.Where(e => e.IsAlive()).ToList();
        if (aliveEnemies.Count == 0)
        {
            return "-";
        }

        return string.Join(", ", aliveEnemies.Select(e => e.Name + ": " + e.GetIntentText()));
    }
}
