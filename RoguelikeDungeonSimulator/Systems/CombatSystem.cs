namespace RoguelikeDungeonSimulator.Systems
{
    using RoguelikeDungeonSimulator.Models;
    using RoguelikeDungeonSimulator.Models.Interfaces;
    using RoguelikeDungeonSimulator.Systems.Interfaces;

    /// <summary>
    /// 战斗结果信息
    /// 
    /// 数据类（Data Class）：持有战斗的结果信息
    /// - 胜负双方
    /// - 伤害统计
    /// - 战斗日志
    /// </summary>
    public class CombatResult : ICombatResult
    {
        public ICombatant? Winner { get; set; }
        public ICombatant? Loser { get; set; }
        public List<string> BattleLog { get; set; }
        public int PlayerDamageDealt { get; set; }
        public int EnemyDamageDealt { get; set; }

        public CombatResult()
        {
            BattleLog = new List<string>();
            PlayerDamageDealt = 0;
            EnemyDamageDealt = 0;
        }
    }

    /// <summary>
    /// 战斗系统 - 处理回合制战斗逻辑
    /// 
    /// 设计模式：Strategy Pattern（策略模式）
    /// - ExecuteCombat() 是战斗过程的统一入口
    /// - 战斗规则清晰：1. 玩家先手 2. 轮流攻击 3. 血条为0结束
    /// - 易于添加新的战斗变体（BOSS战、多人战等）
    /// 
    /// 单一责任原则：
    /// - 只负责战斗逻辑
    /// - 不涉及UI显示、数据保存等其他职责
    /// </summary>
    public class CombatSystem : ICombatSystem
    {
        private List<string> battleLog;

        public CombatSystem()
        {
            battleLog = new List<string>();
        }

        /// <summary>
        /// 执行完整的战斗过程
        /// 
        /// 算法流程：
        /// 1. 初始化战斗信息
        /// 2. 回合制循环：
        ///    a. 玩家攻击
        ///    b. 检查敌人是否死亡
        ///    c. 敌人反击
        ///    d. 检查玩家是否死亡
        /// 3. 返回战斗结果
        /// </summary>
        public ICombatResult ExecuteCombat(ICombatant player, ICombatant enemy)
        {
            battleLog.Clear();
            var result = new CombatResult();

            // 初始化
            battleLog.Add($"=== 战斗开始 ===");
            battleLog.Add($"{player.Name} vs {enemy.Name}");
            battleLog.Add($"{player.Name} 血量: {player.CurrentHealth}/{player.MaxHealth}");
            battleLog.Add($"{enemy.Name} 血量: {enemy.CurrentHealth}/{enemy.MaxHealth}");
            battleLog.Add("---");

            int roundNumber = 0;

            // 回合制战斗循环
            while (player.IsAlive() && enemy.IsAlive())
            {
                roundNumber++;
                battleLog.Add($"第 {roundNumber} 回合");

                // ===== 玩家攻击 =====
                int playerDamage = player.CalculateDamage();
                battleLog.Add($"{player.Name} 发动攻击！");
                
                enemy.TakeDamage(playerDamage);
                battleLog.Add($"{player.Name} 造成 {playerDamage} 点伤害");
                battleLog.Add($"{enemy.Name} 剩余血量: {enemy.CurrentHealth}/{enemy.MaxHealth}");
                result.PlayerDamageDealt += playerDamage;

                // 检查敌人是否死亡
                if (!enemy.IsAlive())
                {
                    battleLog.Add($"{enemy.Name} 被击败！");
                    result.Winner = player;
                    result.Loser = enemy;
                    break;
                }

                battleLog.Add("---");

                // ===== 敌人反击 =====
                int enemyDamage = enemy.CalculateDamage();
                battleLog.Add($"{enemy.Name} 发动反击！");
                
                player.TakeDamage(enemyDamage);
                battleLog.Add($"{enemy.Name} 造成 {enemyDamage} 点伤害");
                battleLog.Add($"{player.Name} 剩余血量: {player.CurrentHealth}/{player.MaxHealth}");
                result.EnemyDamageDealt += enemyDamage;

                // 检查玩家是否死亡
                if (!player.IsAlive())
                {
                    battleLog.Add($"{player.Name} 被击败！");
                    result.Winner = enemy;
                    result.Loser = player;
                    break;
                }

                battleLog.Add("---");
            }

            // 战斗结束信息
            battleLog.Add("=== 战斗结束 ===");
            battleLog.Add($"胜者: {result.Winner?.Name ?? "未知"}");
            battleLog.Add($"总伤害统计 - {player.Name}: {result.PlayerDamageDealt}, {enemy.Name}: {result.EnemyDamageDealt}");
            result.BattleLog = new List<string>(battleLog);

            return result;
        }

        /// <summary>
        /// 获取战斗日志
        /// </summary>
        public List<string> GetBattleLog()
        {
            return new List<string>(battleLog);
        }

        /// <summary>
        /// 清除战斗日志
        /// </summary>
        public void ClearBattleLog()
        {
            battleLog.Clear();
        }
    }
}
