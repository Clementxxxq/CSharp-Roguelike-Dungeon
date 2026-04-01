using System;
public static class RoomFactory
{
    private static Random random = new Random();

    public static Room CreateRoom(RoomType type, int roomNumber = 1)
    {
        return type switch
        {
            RoomType.Combat => CreateCombatRoom(roomNumber),
            RoomType.Boss => CreateBossRoom(roomNumber),
            _ => throw new ArgumentException($"Type de chambre inconnu: {type}")
        };
    }

    private static Room CreateCombatRoom(int roomNumber)
    {
        int enemyCount = 1 + (roomNumber / 2);  
        var enemies = EnemyFactory.CreateEnemies(enemyCount, roomNumber);
        return new CombatRoom(roomNumber, enemies);
    }

    private static Room CreateBossRoom(int roomNumber)
    {
        Enemy boss = new Boss();
        return new BossRoom(roomNumber, boss);
    }
}
