namespace RoguelikeDungeonSimulator.Systems.Interfaces
{
    using RoguelikeDungeonSimulator.Models;
    using RoguelikeDungeonSimulator.Models.Interfaces;

    /// <summary>
    /// 战斗结果接口
    /// </summary>
    public interface ICombatResult
    {
        ICombatant? Winner { get; }
        ICombatant? Loser { get; }
        List<string> BattleLog { get; }
        int PlayerDamageDealt { get; }
        int EnemyDamageDealt { get; }
    }

    /// <summary>
    /// 战斗系统接口
    /// 
    /// 设计模式：Strategy Pattern（策略模式）
    /// - 定义战斗过程的统一接口
    /// - 不同的战斗类型可以实现不同的策略
    /// - 易于添加新的战斗模式（如多人战斗、BOSS战等）
    /// </summary>
    public interface ICombatSystem
    {
        ICombatResult ExecuteCombat(ICombatant player, ICombatant enemy);
        List<string> GetBattleLog();
        void ClearBattleLog();
    }
}
