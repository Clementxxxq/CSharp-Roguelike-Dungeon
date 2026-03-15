public enum RoomType
{
    Combat,
    Boss
}

public abstract class Room
{
    public int RoomNumber { get; set; }
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
    public bool IsCleared { get; set; }

    public bool AreAllEnemiesDefeated()
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.IsAlive())
                return false;
        }
        return true;
    }

}
