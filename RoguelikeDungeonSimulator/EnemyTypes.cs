using System;
using System.Collections.Generic;
public class DungeonRat : Enemy
{
    public DungeonRat() : base("Rat des Donjons", 20, 5, 0, 10)
    {
    }
}

public class FallenWarrior : Enemy
{
    public FallenWarrior() : base("Guerrier Déchu", 40, 10, 3, 20)
    {
    }
}

public class CorruptedMage : Enemy
{
    public CorruptedMage() : base("Mage Corrompu", 30, 15, 1, 30)
    {
    }
}

public class DungeonLord : Enemy
{
    public DungeonLord() : base("Seigneur du Donjon", 120, 20, 5, 100)
    {
    }
}
