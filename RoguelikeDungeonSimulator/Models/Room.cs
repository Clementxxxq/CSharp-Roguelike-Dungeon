namespace RoguelikeDungeonSimulator.Models
{
    /// <summary>
    /// 代表地牢中的一个房间
    /// 
    /// 设计模式：Composite Pattern + Factory Pattern（组合模式 + 工厂模式）
    /// 
    /// 组合模式（Composite Pattern）：
    /// - Room 作为容器聚合了多个 Enemy 和 Item 对象
    /// - 提供统一的接口管理其子组件（AddEnemy, AddItem, RemoveEnemy 等）
    /// - 可以对整个房间进行操作（HasEnemies(), MarkAsCleared() 等）
    /// - 房间形成了一个树形结构：Dungeon -> Room -> Enemies/Items
    /// 
    /// 工厂模式（Factory Pattern）：
    /// - GenerateContent() 方法是一个工厂方法，自动生成房间内容
    /// - 根据房间难度随机创建不同数量和等级的敌人和物品
    /// - Boss房间有特殊的生成策略
    /// - 隐藏了房间内容生成的复杂逻辑
    /// 
    /// 责任分离：
    /// - 房间负责存储和管理其包含的实体
    /// - 房间本身是一个状态容器，易于序列化和网络传输
    /// </summary>
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        // 房间内容
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Item> Items { get; set; }
        
        // 房间属性
        public int Difficulty { get; set; } // 1-5, 5最难
        public bool IsBossRoom { get; set; }
        public bool IsCleared { get; set; }
        
        // 连接性
        public int? NextRoomId { get; set; }
        public int? PreviousRoomId { get; set; }

        /// <summary>
        /// 创建一个新房间
        /// </summary>
        public Room(int id, string name = "Unknown Room", int difficulty = 1)
        {
            Id = id;
            Name = name;
            Description = "";
            Width = 10;
            Height = 10;
            Difficulty = difficulty;
            IsBossRoom = false;
            IsCleared = false;
            Enemies = new List<Enemy>();
            Items = new List<Item>();
            NextRoomId = null;
            PreviousRoomId = null;
        }

        /// <summary>
        /// 添加敌人到房间
        /// 
        /// 组合模式实现：
        /// - Room 作为容器管理子组件 (Enemies 列表)
        /// - 提供统一的接口添加敌人
        /// - 隐藏内部集合的实现细节
        /// </summary>
        public void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
        }

        /// <summary>
        /// 移除敌人（当敌人被击败）
        /// </summary>
        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }

        /// <summary>
        /// 添加物品到房间
        /// 
        /// 组合模式实现：Room 作为容器统一管理其包含的所有组件（敌人和物品）
        /// </summary>
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// 从房间拿起物品
        /// </summary>
        public Item? TakeItem(int itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                Items.Remove(item);
            }
            return item;
        }

        /// <summary>
        /// 检查房间是否还有敌人
        /// </summary>
        public bool HasEnemies()
        {
            return Enemies.Count > 0;
        }

        /// <summary>
        /// 标记房间为已清空
        /// </summary>
        public void MarkAsCleared()
        {
            IsCleared = true;
        }

        /// <summary>
        /// 生成房间内容（随机敌人和物品）
        /// 
        /// 工厂模式实现：
        /// - 自动创建房间所需的所有实体对象
        /// - 根据难度等级生成相应的敌人和物品
        /// - Boss房间有特殊的生成逻辑
        /// - 隐藏了复杂的生成细节，调用者只需调用一个方法
        /// </summary>
        public void GenerateContent()
        {
            var random = new Random();
            
            // 生成敌人
            int enemyCount = random.Next(1, Difficulty + 2);
            for (int i = 0; i < enemyCount; i++)
            {
                string[] enemyTypes = { "Goblin", "Orc" };
                string enemyType = enemyTypes[random.Next(enemyTypes.Length)];
                var enemy = new Enemy($"{enemyType} {i + 1}", enemyType, Difficulty);
                
                enemy.X = random.Next(0, Width);
                enemy.Y = random.Next(0, Height);
                enemy.RoomId = Id;
                
                Enemies.Add(enemy);
            }
            
            // Boss房间特殊处理
            if (IsBossRoom)
            {
                Enemies.Clear();
                var boss = new Enemy("最终Boss", "Boss", Difficulty + 2);
                boss.X = Width / 2;
                boss.Y = Height / 2;
                boss.RoomId = Id;
                Enemies.Add(boss);
            }
            
            // 生成物品（战斗后奖励）
            if (random.Next(0, 2) == 0)
            {
                string[] itemTypes = { "Weapon", "Armor", "Potion" };
                string itemType = itemTypes[random.Next(itemTypes.Length)];
                var item = new Item($"{itemType} (Lv.{Difficulty})", itemType, Difficulty);
                Items.Add(item);
            }
        }

        /// <summary>
        /// 将房间状态导出为字典（用于序列化）
        /// </summary>
        public Dictionary<string, object> ToDict()
        {
            return new Dictionary<string, object>
            {
                { "id", Id },
                { "name", Name },
                { "description", Description ?? "" },
                { "width", Width },
                { "height", Height },
                { "difficulty", Difficulty },
                { "isBossRoom", IsBossRoom },
                { "isCleared", IsCleared },
                { "enemies", Enemies.Select(e => e.ToDict()).ToList() },
                { "items", Items.Select(i => i.ToDict()).ToList() },
                { "nextRoomId", NextRoomId ?? -1 },
                { "previousRoomId", PreviousRoomId ?? -1 }
            };
        }
    }
}
