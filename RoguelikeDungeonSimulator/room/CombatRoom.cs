public class CombatRoom : Room
{
    public CombatRoom(int roomNumber, List<Enemy> enemies)
    {
        RoomNumber = roomNumber;
        Enemies = enemies;
        IsCleared = false;
    }
}
