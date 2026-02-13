namespace RoguelikeDungeonSimulator.Models
{
    /// <summary>
    /// 代表地牢中的一个房间
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
