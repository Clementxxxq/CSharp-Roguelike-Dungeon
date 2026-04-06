using System;
using System.Collections.Generic;
public class Rat : Enemy
{
    public Rat() : base("Rat", 20, 5, 0, 10)
    {
    }
}

public class Warrior : Enemy
{
    public Warrior() : base("Warrior", 40, 10, 3, 20)
    {
    }
}

public class Mage : Enemy
{
    public Mage() : base("Mage", 30, 15, 1, 30)
    {
    }
}

public class Boss : Enemy
{
    public Boss() : base("Boss", 120, 20, 5, 100)
    {
    }
}
