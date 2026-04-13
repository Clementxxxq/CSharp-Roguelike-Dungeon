using System;
using System.Collections.Generic;
public class Rat : Enemy
{
    public Rat() : base("Rat", 20, 7, 1, 4)
    {
    }
}

public class Warrior : Enemy
{
    public Warrior() : base("Warrior", 34, 9, 3, 8)
    {
    }
}

public class Mage : Enemy
{
    public Mage() : base("Mage", 28, 11, 2, 10)
    {
    }
}

public class Boss : Enemy
{
    public Boss() : base("Boss", 62, 12, 4, 25)
    {
    }
}
