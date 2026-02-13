namespace RoguelikeDungeonSimulator.Systems
{
    using RoguelikeDungeonSimulator.Models;
    using RoguelikeDungeonSimulator.Systems.Interfaces;

    /// <summary>
    /// 地牢生成系统 - 负责生成地牢结构和房间
    /// 
    /// 设计模式：Factory Pattern（工厂模式）
    /// - GenerateDungeon() 工厂方法创建整个地牢
    /// - GenerateRoom() 工厂方法创建单个房间
    /// - 隐藏了房间生成的复杂逻辑
    /// 
    /// 策略：
    /// - 关卡难度逐渐增加
    /// - 最后一个房间为Boss房间
    /// - 房间之间线性连接
    /// </summary>
    public class DungeonGenerator : IDungeonGenerator
    {
        private Random random;

        public DungeonGenerator()
        {
            random = new Random();
        }

        /// <summary>
        /// 生成完整的地牢
        /// </summary>
        public List<Room> GenerateDungeon(int difficulty, int numberOfRooms)
        {
            var rooms = new List<Room>();

            for (int i = 0; i < numberOfRooms; i++)
            {
                bool isBossRoom = (i == numberOfRooms - 1); // 最后一个房间是Boss房
                int roomDifficulty = difficulty + (i * difficulty / numberOfRooms); // 难度递增
                
                var room = GenerateRoom(i, roomDifficulty, isBossRoom);
                rooms.Add(room);
            }

            // 连接房间
            ConnectRooms(rooms);

            return rooms;
        }

        /// <summary>
        /// 生成单个房间
        /// 
        /// 工厂模式实现：
        /// - 自动设置房间难度、敌人、物品
        /// - Boss房间有特殊的生成配置
        /// </summary>
        public Room GenerateRoom(int roomId, int difficulty, bool isBossRoom = false)
        {
            string roomName = isBossRoom 
                ? $"Boss房间 - {GetBossName()}" 
                : $"地牢房间 {roomId + 1}";

            var room = new Room(roomId, roomName, Math.Min(difficulty, 5)); // 难度上限为5
            room.IsBossRoom = isBossRoom;

            // 生成房间内容（敌人和物品）
            room.GenerateContent();

            // 添加描述
            room.Description = isBossRoom 
                ? "一个宏大而危险的房间。可以感受到强大敌人的气息。"
                : $"一个充满了危险的地牢房间。难度等级: {difficulty}";

            return room;
        }

        /// <summary>
        /// 连接房间（建立房间之间的导航链接）
        /// </summary>
        public void ConnectRooms(List<Room> rooms)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i > 0)
                    rooms[i].PreviousRoomId = rooms[i - 1].Id;

                if (i < rooms.Count - 1)
                    rooms[i].NextRoomId = rooms[i + 1].Id;
            }
        }

        /// <summary>
        /// 随机获取Boss名称（便于扩展）
        /// </summary>
        private string GetBossName()
        {
            string[] bossNames = new[]
            {
                "龙王",
                "深渊领主",
                "魔法师之王",
                "远古巨兽",
                "影之君主",
                "腐蚀之主",
                "暗魔导师"
            };

            return bossNames[random.Next(bossNames.Length)];
        }
    }
}
