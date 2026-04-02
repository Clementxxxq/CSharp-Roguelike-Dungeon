using UnityEngine;
using System;

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
    public string LastRoomType { get; private set; } = "Not started";

    public int PlayerHP => player?.HP ?? 0;
    public int PlayerMaxHP => player?.MaxHP ?? 0;
    public int PlayerAttack => player?.Attack ?? 0;
    public int PlayerLevel => player?.CurrentLevel ?? 1;
    public int PlayerXP => player?.CurrentXP ?? 0;
    public int PlayerXPToNextLevel => player?.XPToNextLevel ?? 1;

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
        LastRoomType = "Normal Room";

        ChangeState(new PlayingState());
        RaiseHPChanged();
        RaiseRoomChanged();

        return "Game started! Click Attack to enter room 1.";
    }

    public string RestartGame()
    {
        ChangeState(new MenuState());
        return StartGame();
    }

    public string EnterCurrentRoom()
    {
        return state?.EnterCurrentRoom(this) ?? "State error, please click Restart.";
    }

    // Main combat flow used by PlayingState.
    internal string EnterCurrentRoomInternal()
    {
        if (player == null)
        {
            return "Combat state error, please click Restart.";
        }

        Room room = roomManager.CreateCurrentRoom();
        LastRoomEnemyCount = room.Enemies.Count;
        LastRoomType = roomManager.GetCurrentRoomTypeLabel();
        RaiseRoomChanged();

        CombatResult result = combatSystem.RunCombat(player, room);
        string log = "Entered room " + CurrentRoom + " (" + LastRoomType + ").\n" + result.CombatLog;
        RaiseHPChanged();

        if (!result.PlayerWon)
        {
            IsGameOver = true;
            ChangeState(new GameOverState());
            log += "\nYou died! Click Restart to start over.";
            return log;
        }

        player.GainExperience(result.XPGained);
        // XP/level can change here, so notify UI again.
        RaiseHPChanged();

        if (roomManager.IsFinalRoom())
        {
            IsGameOver = true;
            ChangeState(new GameOverState());
            log += "\nYou defeated the boss and cleared the game!";
            return log;
        }

        roomManager.MoveToNextRoom();
        RaiseRoomChanged();
        log += "\nBattle won, click Attack to enter room " + CurrentRoom + ".";
        return log;
    }

    public interface IGameState
    {
        void Enter(GameManager gm);
        void Exit(GameManager gm);
        string EnterCurrentRoom(GameManager gm);
    }

    private sealed class MenuState : IGameState
    {
        public void Enter(GameManager gm)
        {
        }

        public void Exit(GameManager gm)
        {
        }

        public string EnterCurrentRoom(GameManager gm)
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

        public string EnterCurrentRoom(GameManager gm)
        {
            return gm.EnterCurrentRoomInternal();
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

        public string EnterCurrentRoom(GameManager gm)
        {
            return "You are dead, please click Restart.";
        }
    }
}
