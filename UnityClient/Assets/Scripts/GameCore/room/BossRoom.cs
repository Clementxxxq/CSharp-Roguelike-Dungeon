public class BossRoom : Room
{
    public BossRoom(int roomNumber, Enemy boss)
    {
        RoomNumber = roomNumber;
        CurrentWave = 1;
        MaxWaves = 1;
        Enemies.Add(boss); 
        IsCleared = false;
    }

}
