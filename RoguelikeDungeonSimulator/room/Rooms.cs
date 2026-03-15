// Types de salles disponibles dans le donjon
public enum RoomType
{
    Combat,
    Boss
}

// Classe de base abstraite pour toutes les salles du donjon
public abstract class Room
{
    public int RoomNumber { get; set; }
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
    public bool IsCleared { get; set; }

    // Vérifie si tous les ennemis de la salle sont vaincus
    public bool AreAllEnemiesDefeated()
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.IsAlive())
                return false;
        }
        return true;
    }

    public override string ToString()
    {
        string status = IsCleared ? "✅ Terminée" : "⚔️ En cours";
        return $"Salle {RoomNumber} | Ennemis: {Enemies.Count} | {status}";
    }
}
