using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //Singleton
    private Player player;
    private readonly CombatSystem combatSystem = new CombatSystem();
    private readonly RoomManager roomManager = new RoomManager();

    public event Action<int, int> OnHPChanged; //Observer
    public event Action<int, string> OnRoomChange;

    private IGameState state; //State

    public bool IsGameStarted { get; private set; }
    public bool IsGameOver { get; private set; }
    public int CurrentRoom => roomManager.CurrentRoom;
    public int LastRoomEnemyCount { get; private set; }
    public int LastRoomTotalEnemies { get; private set; }
    public string LastRoomType { get; private set; } = "Not started";
    public string LastEnemyIntentText { get; private set; } = "-";
    public string LastEnemyInfoText { get; private set; } = "-";
    public string LastRoomWaveProgress
    {
        get
        {
            int total = Math.Max(1, LastRoomTotalEnemies);
            int current = GetCurrentWaveIndex();
            return current + "/" + total;
        }
    }

    public int PlayerHP => player?.HP ?? 0;
    public int PlayerMaxHP => player?.MaxHP ?? 0;
    public int PlayerAttack => player?.Attack ?? 0;
    public int PlayerDefense => player?.Defense ?? 0;
    public int PlayerLevel => player?.CurrentLevel ?? 1;
    public int PlayerXP => player?.CurrentXP ?? 0;
    public int PlayerXPToNextLevel => player?.XPToNextLevel ?? 1;
    public int PlayerSkillCooldown => player?.PowerStrikeCooldownRemaining ?? 0;

    private int GetCurrentWaveIndex()
    {
        if (LastRoomTotalEnemies <= 0)
            return 1;

        if (LastRoomEnemyCount <= 0)
            return LastRoomTotalEnemies;

        int defeatedCount = LastRoomTotalEnemies - LastRoomEnemyCount;
        return Math.Min(LastRoomTotalEnemies, Math.Max(1, defeatedCount + 1));
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        ChangeState(new MenuState());
    }

    public void ChangeState(IGameState newState)
    {
        if (newState == null)
        {
            return;
        }

        state?.Exit(this);
        state = newState;
        state.Enter(this);
    }

    public void RaiseHPChanged()
    {
        OnHPChanged?.Invoke(PlayerHP, PlayerMaxHP);
    }

    public void RaiseRoomChanged()
    {
        OnRoomChange?.Invoke(CurrentRoom, LastRoomType);
    }

    public string StartGame()
    {
        if (!(state is MenuState) && IsGameStarted && !IsGameOver)
        {
            return "Game has already started.";
        }

        Player.ResetInstance();
        player = Player.GetInstance();

        IsGameStarted = true;
        IsGameOver = false;
        roomManager.ResetProgress();
        LastRoomEnemyCount = 0;
        LastRoomTotalEnemies = 0;
        LastRoomType = "Normal Room";
        LastEnemyIntentText = "-";
        LastEnemyInfoText = "-";

        ChangeState(new PlayingState());
        RaiseHPChanged();
        string firstRoomLog = EnterRoom();
        return "Game started!\n" + firstRoomLog;
    }

    public string RestartGame()
    {
        ChangeState(new MenuState());
        return StartGame();
    }

    public string EnterCurrentRoom()
    {
        return EnterRoom();
    }

    public string EnterRoom()
    {
        if (player == null)
        {
            return "Combat state error, please click Restart.";
        }

        Room room = roomManager.CreateCurrentRoom();
        LastRoomTotalEnemies = room.Enemies.Count;
        LastRoomEnemyCount = room.Enemies.Count(e => e.IsAlive());
        LastRoomType = roomManager.GetCurrentRoomTypeLabel();
        LastEnemyIntentText = combatSystem.PreviewEnemyIntents(player, room);
        LastEnemyInfoText = BuildEnemyInfoSummary(room);

        RaiseRoomChanged();

        return "Room " + CurrentRoom + " (" + LastRoomType + ") ready.";
    }

    public string EnterCurrentRoom(PlayerAction action)
    {
        return state?.EnterCurrentRoom(this, action) ?? "State error, please click Restart.";
    }

    public interface IGameState
    {
        void Enter(GameManager gm);
        void Exit(GameManager gm);
        string EnterCurrentRoom(GameManager gm, PlayerAction action);
    }

    private sealed class MenuState : IGameState
    {
        public void Enter(GameManager gm)
        {
        }

        public void Exit(GameManager gm)
        {
        }

        public string EnterCurrentRoom(GameManager gm, PlayerAction action)
        {
            return "Please click Start Game first.";
        }
    }

    private sealed class PlayingState : IGameState
    {
        public void Enter(GameManager gm)
        {
        }

        public void Exit(GameManager gm)
        {
        }

        public string EnterCurrentRoom(GameManager gm, PlayerAction action)
        {
            return gm.EnterCurrentRoomInternal(action);
        }
    }

    private sealed class GameOverState : IGameState
    {
        public void Enter(GameManager gm)
        {
        }

        public void Exit(GameManager gm)
        {
        }

        public string EnterCurrentRoom(GameManager gm, PlayerAction action)
        {
            return "You are dead, please click Restart.";
        }
    }

    internal string EnterCurrentRoomInternal(PlayerAction action)
    {
        if (player == null)
        {
            return "Combat state error, please click Restart.";
        }

        Room room = roomManager.CreateCurrentRoom();

        CombatResult result = combatSystem.RunCombat(player, room, action);
        LastRoomEnemyCount = room.Enemies.Count(e => e.IsAlive());
        LastEnemyIntentText = result.EnemyIntentText;
        LastEnemyInfoText = BuildEnemyInfoSummary(room);
        string log = "Room " + CurrentRoom + " (" + LastRoomType + ").\n" + result.CombatLog;
        RaiseHPChanged();
        RaiseRoomChanged();

        if (!result.IsCombatFinished)
        {
            return log;
        }

        if (!result.PlayerWon)
        {
            IsGameOver = true;
            ChangeState(new GameOverState());
            log += "\nYou died! Click Restart to start over.";
            return log;
        }

        player.GainExperience(result.XPGained);
        RaiseHPChanged();

        if (roomManager.IsFinalRoom())
        {
            IsGameOver = true;
            ChangeState(new GameOverState());
            log += "\nYou defeated the boss and cleared the game!";
            return log;
        }

        roomManager.MoveToNextRoom();
        LastRoomEnemyCount = 0;
        LastRoomTotalEnemies = 0;
        LastEnemyIntentText = "-";
        LastEnemyInfoText = "-";
        RaiseRoomChanged();
        log += "\nBattle won, click Attack/Defend/Skill to enter room " + CurrentRoom + ".";
        return log;
    }

    private static string BuildEnemyInfoSummary(Room room)
    {
        var aliveEnemies = room.Enemies.Where(e => e.IsAlive()).ToList();
        if (aliveEnemies.Count == 0)
        {
            return "-";
        }

        return string.Join("\n", aliveEnemies.Select(enemy =>
            enemy.Name + " HP:" + enemy.HP + "/" + enemy.MaxHP +
            " ATK:" + enemy.Attack +
            " DEF:" + enemy.Defense));
    }
}
