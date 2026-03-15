using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameMediator : AbstractMediator
{
    private static GameMediator instance;

    public static GameMediator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameMediator();
            }
            return instance;
        }
    }
    private GameMediator()
    {
        RegisterController(new PlayerController());
        RegisterController(new EnemyController());
    }
}

public class PlayerController : AbstractController
{
}

public class EnemyController : AbstractController
{
}