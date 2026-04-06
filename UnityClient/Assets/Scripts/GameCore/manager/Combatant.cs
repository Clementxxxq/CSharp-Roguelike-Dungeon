using System;

public abstract class Combatant : ICombatant
{
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    protected Combatant(int hp, int attack, int defense)
    {
        HP = hp;
        MaxHP = hp;
        Attack = attack;
        Defense = defense;
    }

    public virtual void TakeDamage(int damage)
    {
        ApplyDamage(damage, false);
    }

    public virtual int CalculateDamage(IEntity target)
    {
        return Attack;
    }

    public bool IsAlive()
    {
        return HP > 0;
    }

    protected int ApplyDamage(int damage, bool isDefending)
    {
        int reducedDamage = isDefending ? (int)Math.Ceiling(damage * 0.5f) : damage;
        reducedDamage -= Defense;
        if (reducedDamage < 1)
            reducedDamage = 1;

        HP -= reducedDamage;
        if (HP < 0)
            HP = 0;

        return reducedDamage;
    }
}