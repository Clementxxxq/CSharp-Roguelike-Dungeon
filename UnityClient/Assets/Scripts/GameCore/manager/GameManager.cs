using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Player player;
    private readonly CombatSystem combatSystem = new CombatSystem();
    private readonly RoomManager roomManager = new RoomManager();

    // Observer: UIManager 等系统可订阅这些事件。
    public event Action<int, int> OnHPChanged;
    public event Action<int, string> OnRoomChange;

    // State Pattern: 当前游戏状态。
    private IGameState state;

    public bool IsGameStarted { get; private set; }
    public bool IsGameOver { get; private set; }
    public int CurrentRoom => roomManager.CurrentRoom;
    public int LastRoomEnemyCount { get; private set; }
    public string LastRoomType { get; private set; } = "未开始";

    public int PlayerHP => player?.HP ?? 0;
    public int PlayerMaxHP => player?.MaxHP ?? 0;
    public int PlayerAttack => player?.Attack ?? 0;

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
        // ===== 原始 StartGame（保留）=====
        // Player.ResetInstance();
        // player = Player.GetInstance();
        //
        // IsGameStarted = true;
        // IsGameOver = false;
        // roomManager.ResetProgress();
        // LastRoomEnemyCount = 0;
        // LastRoomType = "普通房间";
        //
        // return "游戏开始！点击 Attack 进入第 1 个房间。";

        if (!(state is MenuState) && IsGameStarted && !IsGameOver)
        {
            return "游戏已开始。";
        }

        Player.ResetInstance();
        player = Player.GetInstance();

        IsGameStarted = true;
        IsGameOver = false;
        roomManager.ResetProgress();
        LastRoomEnemyCount = 0;
        LastRoomType = "普通房间";

        ChangeState(new PlayingState());
        RaiseHPChanged();
        RaiseRoomChanged();

        return "游戏开始！点击 Attack 进入第 1 个房间。";
    }

    public string RestartGame()
    {
        // ===== 原始 RestartGame（保留）=====
        // return StartGame();

        ChangeState(new MenuState());
        return StartGame();
    }

    public string EnterCurrentRoom()
    {
        // ===== 原始 EnterCurrentRoom（保留）=====
        // if (!IsGameStarted)
        // {
        //     return "请先点击 Start Game。";
        // }
        //
        // if (IsGameOver)
        // {
        //     return "你已死亡，请点击 Restart。";
        // }
        //
        // if (player == null)
        // {
        //     return "战斗状态异常，请点击 Restart。";
        // }
        //
        // Room room = roomManager.CreateCurrentRoom();
        // LastRoomEnemyCount = room.Enemies.Count;
        // LastRoomType = roomManager.GetCurrentRoomTypeLabel();
        //
        // CombatResult result = combatSystem.RunCombat(player, room);
        // string log = "进入第 " + CurrentRoom + " 房间（" + LastRoomType + "）。\n" + result.CombatLog;
        //
        // if (!result.PlayerWon)
        // {
        //     IsGameOver = true;
        //     log += "\n你死亡了！点击 Restart 重新开始。";
        //     return log;
        // }
        //
        // player.GainExperience(result.XPGained);
        //
        // if (roomManager.IsFinalRoom())
        // {
        //     IsGameOver = true;
        //     log += "\n你击败了 Boss，通关！";
        //     return log;
        // }
        //
        // roomManager.MoveToNextRoom();
        // log += "\n战斗胜利，点击 Attack 进入第 " + CurrentRoom + " 房间。";
        // return log;

        return state?.EnterCurrentRoom(this) ?? "状态异常，请点击 Restart。";
    }

    // PlayingState 的战斗主逻辑。
    internal string EnterCurrentRoomInternal()
    {
        if (player == null)
        {
            return "战斗状态异常，请点击 Restart。";
        }

        Room room = roomManager.CreateCurrentRoom();
        LastRoomEnemyCount = room.Enemies.Count;
        LastRoomType = roomManager.GetCurrentRoomTypeLabel();
        RaiseRoomChanged();

        CombatResult result = combatSystem.RunCombat(player, room);
        string log = "进入第 " + CurrentRoom + " 房间（" + LastRoomType + "）。\n" + result.CombatLog;
        RaiseHPChanged();

        if (!result.PlayerWon)
        {
            IsGameOver = true;
            ChangeState(new GameOverState());
            log += "\n你死亡了！点击 Restart 重新开始。";
            return log;
        }

        player.GainExperience(result.XPGained);

        if (roomManager.IsFinalRoom())
        {
            IsGameOver = true;
            ChangeState(new GameOverState());
            log += "\n你击败了 Boss，通关！";
            return log;
        }

        roomManager.MoveToNextRoom();
        RaiseRoomChanged();
        log += "\n战斗胜利，点击 Attack 进入第 " + CurrentRoom + " 房间。";
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
            return "请先点击 Start Game。";
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
            return "你已死亡，请点击 Restart。";
        }
    }
}
