public class BossRoom : Room
{
    public BossRoom(int roomNumber, Enemy boss)
    {
        RoomNumber = roomNumber;
        Enemies.Add(boss); 
        IsCleared = false;
    }

}
