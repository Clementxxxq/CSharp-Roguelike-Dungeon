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
            return gameManager.EnterCurrentRoom();
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
