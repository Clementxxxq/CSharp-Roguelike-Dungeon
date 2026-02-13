namespace RoguelikeDungeonSimulator.Systems
{
    using RoguelikeDungeonSimulator.Models;
    using RoguelikeDungeonSimulator.Models.Interfaces;
    using RoguelikeDungeonSimulator.Systems.Interfaces;

    /// <summary>
    /// 奖励信息
    /// 
    /// 数据类（Data Class）：持有战斗后的奖励信息
    /// </summary>
    public class Reward : IReward
    {
        public int ExperienceGained { get; set; }
        public int GoldGained { get; set; }
        public List<Item> ItemsGained { get; set; }
        public Dictionary<string, int> StatsBoost { get; set; }

        public Reward()
        {
            ItemsGained = new List<Item>();
            StatsBoost = new Dictionary<string, int>
            {
                { "attack", 0 },
                { "defense", 0 },
                { "health", 0 }
            };
        }
    }

    /// <summary>
    /// 奖励系统 - 处理战斗后的奖励
    /// 
    /// 设计模式：Strategy Pattern（策略模式）
    /// - CalculateReward() 根据敌人计算不同的奖励
    /// - ApplyReward() 将奖励应用到玩家身上
    /// - GenerateLootItems() 随机生成战利品
    /// 
    /// 单一责任原则：
    /// - 只负责奖励计算和应用
    /// - 不涉及战斗、UI显示等其他职责
    /// </summary>
    public class RewardSystem : IRewardSystem
    {
        private Random random;

        public RewardSystem()
        {
            random = new Random();
        }

        /// <summary>
        /// 根据击败的敌人计算奖励
        /// 
        /// 奖励计算规则：
        /// - 经验值 = 敌人的基础奖励值
        /// - 金币 = 基础奖励值 (通常是经验的一半)
        /// - 战利品掉落 = 50% 概率
        /// </summary>
        public IReward CalculateReward(ICombatant defeatedEnemy)
        {
            var reward = new Reward();

            if (defeatedEnemy is Enemy enemy)
            {
                reward.ExperienceGained = enemy.ExperienceReward;
                reward.GoldGained = enemy.GoldReward;

                // 50% 概率掉落物品
                if (random.Next(0, 2) == 0)
                {
                    reward.ItemsGained = GenerateLootItems(enemy.Level);
                }
            }

            return reward;
        }

        /// <summary>
        /// 将奖励应用到玩家身上
        /// 
        /// 应用的效果：
        /// 1. 增加经验值（可能升级）
        /// 2. 增加金币
        /// 3. 添加战利品到清单
        /// 4. 应用属性加成
        /// </summary>
        public void ApplyReward(Player player, IReward reward)
        {
            // 增加经验
            player.GainExperience(reward.ExperienceGained);

            // 增加金币
            player.GainGold(reward.GoldGained);

            // 添加战利品
            foreach (var item in reward.ItemsGained)
            {
                player.AddItem(item);
                
                // 立即应用物品属性
                player.Attack += item.AttackBonus;
                player.Defense += item.DefenseBonus;
                
                if (item.HealthBonus > 0)
                {
                    player.MaxHealth += item.HealthBonus;
                    player.CurrentHealth = player.MaxHealth;
                }
            }

            // 应用属性加成
            if (reward.StatsBoost.ContainsKey("attack"))
                player.Attack += reward.StatsBoost["attack"];
            if (reward.StatsBoost.ContainsKey("defense"))
                player.Defense += reward.StatsBoost["defense"];
            if (reward.StatsBoost.ContainsKey("health"))
            {
                player.MaxHealth += reward.StatsBoost["health"];
                player.CurrentHealth = player.MaxHealth;
            }
        }

        /// <summary>
        /// 随机生成战利品物品
        /// 
        /// 逻辑：
        /// - 根据敌人难度确定物品稀有度
        /// - 随机选择物品类型
        /// - 最多生成2个物品
        /// </summary>
        public List<Item> GenerateLootItems(int difficulty)
        {
            var items = new List<Item>();
            
            // 战利品数量：1-2个
            int itemCount = random.Next(1, 3);
            
            // 稀有度：难度越高，稀有度越高
            int rarity = Math.Min(difficulty, 3);

            for (int i = 0; i < itemCount; i++)
            {
                string[] itemTypes = { "Weapon", "Armor", "Potion", "Ring" };
                string itemType = itemTypes[random.Next(itemTypes.Length)];
                
                var item = new Item($"{itemType} (掉落)", itemType, rarity);
                items.Add(item);
            }

            return items;
        }
    }
}
