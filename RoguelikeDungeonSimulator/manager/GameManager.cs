public class GameManager
{
    private readonly CombatSystem combatSystem = new();
    private readonly List<Room> dungeonRooms = new();

    private Player player = null!;

    public bool IsVictory { get; private set; }
    public int CurrentRoomNumber { get; private set; }

    public void StartNewGame(string playerName)
    {
        Player.ResetInstance();
        player = Player.GetInstance();

        if (!string.IsNullOrWhiteSpace(playerName))
            player.Name = playerName.Trim();

        BuildDungeon();
        CurrentRoomNumber = 1;
        IsVictory = false;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\nBienvenue {player.Name} dans le donjon!");
        Console.ResetColor();
    }

    public void RunGameLoop()
    {
        foreach (Room room in dungeonRooms)
        {
            CurrentRoomNumber = room.RoomNumber;
            ShowRoomBanner(room);

            CombatResult combatResult = combatSystem.RunCombat(player, room);
            if (!combatResult.PlayerWon)
            {
                ShowDefeat();
                return;
            }

            ResolveProgression(combatResult.XPGained);

            bool isBossRoom = room is BossRoom;
            if (!isBossRoom)
            {
                OfferReward(room.RoomNumber);
                ShowPlayerSummary();
                WaitForContinue();
                continue;
            }

            IsVictory = true;
            ShowVictory();
            return;
        }
    }

    private void BuildDungeon()
    {
        dungeonRooms.Clear();

        const int combatRoomCount = 4;
        for (int i = 1; i <= combatRoomCount; i++)
            dungeonRooms.Add(RoomFactory.CreateRoom(RoomType.Combat, i));

        dungeonRooms.Add(RoomFactory.CreateRoom(RoomType.Boss, combatRoomCount + 1));
    }

    private void ResolveProgression(int xpGained)
    {
        int previousLevel = player.CurrentLevel;
        bool leveledUp = player.GainExperience(xpGained);

        Console.WriteLine($"XP actuel: {player.CurrentXP}/{player.XPToNextLevel}");
        if (leveledUp)
            Console.WriteLine($"Niveau: {previousLevel} -> {player.CurrentLevel}");
    }

    private void OfferReward(int roomNumber)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n=== Recompense de salle ===");
        Console.ResetColor();

        EquipmentFactory factory = EquipmentFactory.GetFactory(GetRarityForRoom(roomNumber));
        IEquipment weapon = factory.CreateWeapon();
        IEquipment armor = factory.CreateArmor();
        IEquipment ring = factory.CreateRing();

        Console.WriteLine($"1. Equiper arme    -> {weapon}");
        Console.WriteLine($"2. Equiper armure  -> {armor}");
        Console.WriteLine($"3. Equiper anneau  -> {ring}");
        Console.WriteLine("4. Recuperer 30 HP");

        int choice = ReadChoice(1, 4);
        switch (choice)
        {
            case 1:
                player.EquipWeapon(weapon);
                break;
            case 2:
                player.EquipArmor(armor);
                break;
            case 3:
                player.EquipRing(ring);
                break;
            case 4:
                int previousHp = player.HP;
                player.HP = Math.Min(player.MaxHP, player.HP + 30);
                Console.WriteLine($"Soin: {previousHp} -> {player.HP} HP");
                break;
        }
    }

    private static EquipmentRarity GetRarityForRoom(int roomNumber)
    {
        if (roomNumber <= 2)
            return EquipmentRarity.Basic;

        if (roomNumber <= 4)
            return EquipmentRarity.Advanced;

        return EquipmentRarity.Legendary;
    }

    private static void ShowRoomBanner(Room room)
    {
        bool isBossRoom = room is BossRoom;
        string roomLabel = isBossRoom ? "Salle du Boss" : $"Salle de Combat {room.RoomNumber}";

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n========== {roomLabel} ==========");
        Console.ResetColor();
    }

    private void ShowPlayerSummary()
    {
        Console.WriteLine("\nEtat du joueur:");
        Console.WriteLine($"Nom: {player.Name}");
        Console.WriteLine($"HP: {player.HP}/{player.MaxHP} | ATK: {player.Attack} | DEF: {player.Defense}");
        Console.WriteLine($"Niveau: {player.CurrentLevel} | XP: {player.CurrentXP}/{player.XPToNextLevel}");
        Console.WriteLine(player.GetEquipmentSummary());
    }

    private static void ShowVictory()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nLe Boss est vaincu. Victoire!");
        Console.ResetColor();
    }

    private static void ShowDefeat()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nFin de partie. Le donjon vous a englouti.");
        Console.ResetColor();
    }

    private static void WaitForContinue()
    {
        Console.WriteLine("\nAppuyez sur Entree pour continuer...");
        Console.ReadLine();
    }

    private static int ReadChoice(int min, int max)
    {
        while (true)
        {
            Console.Write($"> ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;

            Console.WriteLine($"Choix invalide. Entrez un nombre entre {min} et {max}.");
        }
    }
}