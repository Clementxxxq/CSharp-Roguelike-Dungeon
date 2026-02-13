namespace RoguelikeDungeonSimulator.Models
{
    /// <summary>
    /// 代表游戏中的敌人
    /// </summary>
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Goblin, Orc, Boss 等
        
        // 基础统计数据
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        
        // 奖励
        public int ExperienceReward { get; set; }
        public int GoldReward { get; set; }
        
        // 难度等级
        public int Level { get; set; }
        
        // 位置信息
        public int X { get; set; }
        public int Y { get; set; }
        public int RoomId { get; set; }

        /// <summary>
        /// 创建一个新敌人
        /// </summary>
        public Enemy(string name, string type = "Goblin", int level = 1)
        {
            Id = new Random().Next(1000, 9999);
            Name = name;
            Type = type;
            Level = level;
            
            // 根据类型和等级设置基础属性
            SetStatsByType(type, level);
            
            CurrentHealth = MaxHealth;
            ExperienceReward = level * 10;
            GoldReward = level * 5;
            
            X = 0;
            Y = 0;
            RoomId = 0;
        }

        /// <summary>
        /// 根据敌人类型和等级设置属性
        /// </summary>
        private void SetStatsByType(string type, int level)
        {
            switch (type.ToLower())
            {
                case "goblin":
                    MaxHealth = 20 + (level - 1) * 5;
                    Attack = 8 + (level - 1) * 2;
                    Defense = 2 + (level - 1) * 1;
                    break;
                    
                case "orc":
                    MaxHealth = 40 + (level - 1) * 8;
                    Attack = 12 + (level - 1) * 3;
                    Defense = 4 + (level - 1) * 1;
                    break;
                    
                case "boss":
                    MaxHealth = 100 + (level - 1) * 20;
                    Attack = 20 + (level - 1) * 5;
                    Defense = 8 + (level - 1) * 2;
                    ExperienceReward = level * 50;
                    GoldReward = level * 30;
                    break;
                    
                default: // 默认小敌人
                    MaxHealth = 15 + (level - 1) * 3;
                    Attack = 6 + (level - 1) * 1;
                    Defense = 1;
                    break;
            }
        }

        /// <summary>
        /// 敌人受到伤害
        /// </summary>
        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            CurrentHealth -= actualDamage;
            
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        /// <summary>
        /// 检查敌人是否还活着
        /// </summary>
        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }

        /// <summary>
        /// 计算敌人的攻击伤害（带随机浮动）
        /// </summary>
        public int CalculateDamage()
        {
            Random rand = new Random();
            // 伤害在 Attack * 0.8 到 Attack * 1.2 之间随机波动
            int variance = rand.Next((int)(Attack * 0.8), (int)(Attack * 1.2) + 1);
            return variance;
        }

        /// <summary>
        /// 将敌人状态导出为字典（用于序列化）
        /// </summary>
        public Dictionary<string, object> ToDict()
        {
            return new Dictionary<string, object>
            {
                { "id", Id },
                { "name", Name },
                { "type", Type },
                { "maxHealth", MaxHealth },
                { "currentHealth", CurrentHealth },
                { "attack", Attack },
                { "defense", Defense },
                { "level", Level },
                { "experienceReward", ExperienceReward },
                { "goldReward", GoldReward },
                { "x", X },
                { "y", Y },
                { "roomId", RoomId }
            };
        }
    }
}
