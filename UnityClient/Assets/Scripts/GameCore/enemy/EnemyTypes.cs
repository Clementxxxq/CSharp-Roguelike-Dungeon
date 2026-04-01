using System;
using System.Collections.Generic;
public class Rat : Enemy
{
    public Rat() : base("Rat", 1, 1, 1, 100)
    {
    }
}

public class Warrior : Enemy
{
    public Warrior() : base("Warrior", 1, 1, 1, 100)
    {
    }
}

public class Mage : Enemy
{
    public Mage() : base("Mage", 1, 1, 1, 100)
    {
    }
}

public class Boss : Enemy
{
    public Boss() : base("Boss", 1, 1, 1, 100)
    {
    }
}
