namespace RoguelikeDungeonSimulator.Models
{
    /// <summary>
    /// 代表游戏中的玩家角色
    /// 
    /// 设计模式：Entity Pattern（实体模式）
    /// - Player 是游戏中的核心实体，持有玩家的所有状态数据
    /// - 遵循数据与行为的单一责任原则
    /// - 支持序列化导出（ToDict），便于持久化和网络传输
    /// </summary>
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // 基础统计数据
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        
        // 游戏数据
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        
        // 清单
        public List<Item> Inventory { get; set; }
        
        // 位置信息
        public int CurrentRoomId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// 创建一个新玩家
        /// </summary>
        public Player(string name = "Hero")
        {
            Id = 1;
            Name = name;
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            Attack = 15;
            Defense = 5;
            Experience = 0;
            Gold = 0;
            Level = 1;
            Inventory = new List<Item>();
            CurrentRoomId = 0;
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// 玩家受到伤害
        /// </summary>
        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            CurrentHealth -= actualDamage;
            
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        /// <summary>
        /// 玩家恢复生命值
        /// </summary>
        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
        }

        /// <summary>
        /// 检查玩家是否还活着
        /// </summary>
        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }

        /// <summary>
        /// 玩家获得经验
        /// </summary>
        public void GainExperience(int exp)
        {
            Experience += exp;
            
            // 简单的升级机制：每100经验升一级
            int newLevel = Experience / 100 + 1;
            if (newLevel > Level)
            {
                LevelUp(newLevel - Level);
            }
        }

        /// <summary>
        /// 升级
        /// </summary>
        private void LevelUp(int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                Level++;
                MaxHealth += 10;
                CurrentHealth = MaxHealth;
                Attack += 3;
                Defense += 1;
            }
        }

        /// <summary>
        /// 获得金币
        /// </summary>
        public void GainGold(int amount)
        {
            Gold += amount;
        }

        /// <summary>
        /// 添加物品到清单
        /// </summary>
        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        /// <summary>
        /// 移动玩家
        /// </summary>
        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 将玩家状态导出为字典（用于序列化）
        /// </summary>
        public Dictionary<string, object> ToDict()
        {
            return new Dictionary<string, object>
            {
                { "id", Id },
                { "name", Name },
                { "maxHealth", MaxHealth },
                { "currentHealth", CurrentHealth },
                { "attack", Attack },
                { "defense", Defense },
                { "experience", Experience },
                { "gold", Gold },
                { "level", Level },
                { "currentRoomId", CurrentRoomId },
                { "x", X },
                { "y", Y }
            };
        }
    }
}
