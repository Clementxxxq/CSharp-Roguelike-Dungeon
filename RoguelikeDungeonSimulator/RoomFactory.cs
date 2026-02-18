// Simple Factory Pattern for Room creation
public static class RoomFactory
{
    private static Random random = new Random();

    public static Room CreateRoom(RoomType type, int roomNumber = 1)
    {
        return type switch
        {
            RoomType.Combat => CreateCombatRoom(roomNumber),
            RoomType.Boss => CreateBossRoom(),
            _ => throw new ArgumentException($"Type de chambre inconnu: {type}")
        };
    }

    private static Room CreateCombatRoom(int difficulty)
    {
        int enemyCount = 1 + (difficulty / 2);  
        var enemies = EnemyFactory.CreateEnemies(enemyCount, difficulty);
        return new CombatRoom(enemies);
    }

    private static Room CreateBossRoom()
    {
        Enemy boss = new DungeonLord();
        return new BossRoom(boss);
    }
}
