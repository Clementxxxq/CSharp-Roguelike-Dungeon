using System.Collections.Generic;

public class CombatRoom : Room
{
    public CombatRoom(int roomNumber, List<Enemy> enemies, int maxWaves = 3)
    {
        RoomNumber = roomNumber;
        CurrentWave = 1;
        MaxWaves = maxWaves < 1 ? 1 : maxWaves;
        Enemies = enemies;
        IsCleared = false;
    }

    public bool HasNextWave => CurrentWave < MaxWaves;

    public bool AdvanceWave()
    {
        if (!HasNextWave)
        {
            return false;
        }

        CurrentWave++;
        Enemies = EnemyFactory.CreateEnemies(GetEnemyCountForWave(), RoomNumber + CurrentWave - 1);
        IsCleared = false;
        return true;
    }

    private int GetEnemyCountForWave()
    {
        return 1 + (RoomNumber / 2) + (CurrentWave - 1);
    }
}
