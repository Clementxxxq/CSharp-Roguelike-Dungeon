
using System;

public enum EnemyIntentType
{
    Attack,
    Defend
}

// Classe de base pour tous les ennemis
public abstract class Enemy : Combatant
{
    public string Name { get; protected set; }
    public int XPReward { get; protected set; }
    public EnemyIntentType CurrentIntent { get; protected set; }

    protected Enemy(string name, int hp, int attack, int defense, int xpReward)
        : base(hp, attack, defense)
    {
        Name = name;
        XPReward = xpReward;
        CurrentIntent = EnemyIntentType.Attack;
    }

    public virtual EnemyIntentType DecideIntent(Player player)
    {
        if (HP <= Math.Max(1, MaxHP / 2))
        {
            CurrentIntent = EnemyIntentType.Defend;
            return CurrentIntent;
        }

        CurrentIntent = EnemyIntentType.Attack;
        return CurrentIntent;
    }

    public string GetIntentText()
    {
        return CurrentIntent == EnemyIntentType.Defend ? "Defend" : "Attack";
    }

}
