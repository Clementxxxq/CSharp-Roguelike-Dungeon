// ╔═══════════════════════════════════════════════════════════════════════════╗
// ║                    ROGUELIKE DUNGEON SIMULATOR                            ║
// ║                     Main Entry Point                                      ║
// ╚═══════════════════════════════════════════════════════════════════════════╝

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Start the game with default difficulty
GameManager game = new GameManager(difficultyLevel: 1);
game.Start();
