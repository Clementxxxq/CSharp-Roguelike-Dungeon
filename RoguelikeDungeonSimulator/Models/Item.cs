namespace RoguelikeDungeonSimulator.Models
{
    /// <summary>
    /// 代表游戏中的物品（装备、消耗品等）
    /// 
    /// 设计模式：Factory Pattern + Strategy Pattern（工厂模式 + 策略模式）
    /// 
    /// 工厂模式：
    /// - 构造函数根据物品类型 (type) 和稀有度 (rarity) 创建不同的物品
    /// - 隐藏物品创建的复杂逻辑，使用者只需指定类型和稀有度
    /// - 便于新增物品类型
    /// 
    /// 策略模式：
    /// - SetItemStats() 方法根据物品类型采用不同的策略计算属性
    /// - Weapon: 主要提升攻击力
    /// - Armor: 主要提升防御力
    /// - Potion: 提升治疗能力
    /// - Ring: 均衡加成
    /// 
    /// 易于维护和扩展：添加新物品类型时，只需在 SetItemStats 中添加新的 case
    /// </summary>
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Weapon, Armor, Potion, Consumable
        public string Description { get; set; }
        
        // 属性修正
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public int HealthBonus { get; set; }
        
        // 物品等级
        public int Rarity { get; set; } // 1=普通, 2=罕见, 3=传奇

        /// <summary>
        /// 创建一个新物品
        /// </summary>
        public Item(string name, string type, int rarity = 1)
        {
            Id = new Random().Next(1000, 9999);
            Name = name;
            Type = type;
            Rarity = rarity;
            Description = "";
            AttackBonus = 0;
            DefenseBonus = 0;
            HealthBonus = 0;
            
            SetItemStats(type, rarity);
        }

        /// <summary>
        /// 根据物品类型和稀有度设置属性
        /// 
        /// 策略模式实现：
        /// - 每种物品类型（Weapon, Armor, Potion, Ring）有各自的属性计算策略
        /// - 稀有度作为倍数调整基础属性值
        /// - 通过 switch-case 实现不同策略的选择和应用
        /// </summary>
        private void SetItemStats(string type, int rarity)
        {
            switch (type.ToLower())
            {
                case "weapon":
                    AttackBonus = 5 + (rarity - 1) * 5;
                    Description = $"一把强大的武器 (攻击+{AttackBonus})";
                    break;
                    
                case "armor":
                    DefenseBonus = 3 + (rarity - 1) * 3;
                    Description = $"一件防护甲 (防御+{DefenseBonus})";
                    break;
                    
                case "potion":
                    HealthBonus = 30 + (rarity - 1) * 20;
                    Description = $"一瓶生命药剂 (恢复{HealthBonus}生命值)";
                    break;
                    
                case "ring":
                    AttackBonus = 2 + (rarity - 1) * 2;
                    DefenseBonus = 2 + (rarity - 1) * 2;
                    Description = $"一枚魔法戒指 (攻击+{AttackBonus}, 防御+{DefenseBonus})";
                    break;
                    
                default:
                    Description = "一件未知的物品";
                    break;
            }
        }

        /// <summary>
        /// 将物品状态导出为字典（用于序列化）
        /// </summary>
        public Dictionary<string, object> ToDict()
        {
            return new Dictionary<string, object>
            {
                { "id", Id },
                { "name", Name },
                { "type", Type },
                { "description", Description },
                { "attackBonus", AttackBonus },
                { "defenseBonus", DefenseBonus },
                { "healthBonus", HealthBonus },
                { "rarity", Rarity }
            };
        }
    }
}
