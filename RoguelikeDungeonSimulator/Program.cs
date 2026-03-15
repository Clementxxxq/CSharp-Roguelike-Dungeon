<<<<<<< HEAD
﻿class Program
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
=======
﻿// Point d'entrée principal du simulateur de donjon roguelike
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Roguelike Dungeon Simulator ===");
        Console.WriteLine("En cours de développement...");
>>>>>>> e61297039b8a99fb0748cf4f19986d0ed5bc7f08
    }
}
