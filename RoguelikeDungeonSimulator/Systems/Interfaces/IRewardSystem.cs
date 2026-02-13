namespace RoguelikeDungeonSimulator.Systems.Interfaces
{
    using RoguelikeDungeonSimulator.Models;
    using RoguelikeDungeonSimulator.Models.Interfaces;

    /// <summary>
    /// 奖励信息接口
    /// </summary>
    public interface IReward
    {
        int ExperienceGained { get; }
        int GoldGained { get; }
        List<Item> ItemsGained { get; }
        Dictionary<string, int> StatsBoost { get; }
    }

    /// <summary>
    /// 奖励系统接口
    /// 
    /// 设计模式：Strategy Pattern（策略模式）
    /// - 定义奖励计算的统一接口
    /// - 不同的敌人/任务可以有不同的奖励策略
    /// - 易于添加新的奖励类型
    /// </summary>
    public interface IRewardSystem
    {
        IReward CalculateReward(ICombatant defeatedEnemy);
        void ApplyReward(Player player, IReward reward);
        List<Item> GenerateLootItems(int difficulty);
    }
}
