public interface IEntity
{
    int HP { get; set; }
    int MaxHP { get; set; }
    bool IsAlive();
}
