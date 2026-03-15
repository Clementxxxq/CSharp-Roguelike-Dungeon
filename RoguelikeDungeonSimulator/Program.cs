class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Roguelike Dungeon Simulator";

        bool playAgain = true;
        while (playAgain)
        {
            Console.Clear();
            Console.WriteLine("=== Roguelike Dungeon Simulator ===");
            Console.Write("Nom du joueur (laisser vide pour 'Heros'): ");
            string playerName = Console.ReadLine() ?? string.Empty;

            var gameManager = new GameManager();
            gameManager.StartNewGame(string.IsNullOrWhiteSpace(playerName) ? "Heros" : playerName);
            gameManager.RunGameLoop();

            Console.Write("\nRejouer ? (o/n): ");
            string? retry = Console.ReadLine();
            playAgain = retry is not null && retry.Trim().Equals("o", StringComparison.OrdinalIgnoreCase);
        }

        Console.WriteLine("Merci d'avoir joue!");
    }
}
