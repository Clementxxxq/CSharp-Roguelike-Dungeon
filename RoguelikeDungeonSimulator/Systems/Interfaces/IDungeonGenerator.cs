namespace RoguelikeDungeonSimulator.Systems.Interfaces
{
    using RoguelikeDungeonSimulator.Models;

    /// <summary>
    /// 地牢生成系统接口
    /// 
    /// 设计模式：Factory Pattern（工厂模式）
    /// - 定义地牢生成的统一接口
    /// - 不同的地牢类型可以有不同的生成策略
    /// - 易于扩展新的地牢生成算法
    /// </summary>
    public interface IDungeonGenerator
    {
        List<Room> GenerateDungeon(int difficulty, int numberOfRooms);
        Room GenerateRoom(int roomId, int difficulty, bool isBossRoom = false);
        void ConnectRooms(List<Room> rooms);
    }
}
