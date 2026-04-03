namespace UIManager
{
    public sealed class StartCommand : IUICommand
    {
        private readonly GameManager gameManager;

        public StartCommand(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public string Execute()
        {
            return gameManager.StartGame();
        }
    }

    public sealed class AttackCommand : IUICommand
    {
        private readonly GameManager gameManager;

        public AttackCommand(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public string Execute()
        {
            return gameManager.EnterCurrentRoom(PlayerAction.Attack);
        }
    }

    public sealed class DefendCommand : IUICommand
    {
        private readonly GameManager gameManager;

        public DefendCommand(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public string Execute()
        {
            return gameManager.EnterCurrentRoom(PlayerAction.Defend);
        }
    }

    public sealed class SkillCommand : IUICommand
    {
        private readonly GameManager gameManager;

        public SkillCommand(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public string Execute()
        {
            return gameManager.EnterCurrentRoom(PlayerAction.Skill);
        }
    }

    public sealed class RestartCommand : IUICommand
    {
        private readonly GameManager gameManager;

        public RestartCommand(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public string Execute()
        {
            return gameManager.RestartGame();
        }
    }
}
