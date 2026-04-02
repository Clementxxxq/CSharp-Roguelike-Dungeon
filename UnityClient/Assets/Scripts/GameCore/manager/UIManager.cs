using UnityEngine;
using UnityEngine.UI;

namespace UIManager
{
    public interface IUICommand
    {
        string Execute();
    }

    public class UIManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject gameOverPanel;

        [Header("Status UI")]
        [SerializeField] private Text enemyText;
        [SerializeField] private Text logText;
        [SerializeField] private Scrollbar hpScrollbar;
        [SerializeField] private Scrollbar xpScrollbar;
        [SerializeField] private Text hpBarText;
        [SerializeField] private Text xpBarText;

        [Header("References")]
        [SerializeField] private GameManager gameManager;

        private IUICommand startCommand;
        private IUICommand attackCommand;
        private IUICommand restartCommand;

        private void Awake()
        {
            if (gameManager == null)
            {
                Debug.LogError("UIManager requires a GameManager reference from Inspector.", this);
                enabled = false;
            }
        }

        private void OnEnable()
        {
            SubscribeToGameManagerEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromGameManagerEvents();
        }

        private void Start()
        {
            if (gameManager == null)
            {
                return;
            }

            if (logText == null)
            {
                Debug.LogWarning("UIManager: logText is not assigned. Logs will not be visible.", this);
            }

            InitCommands();
            SetUIState(UIState.Start);
            RefreshPlayerStatusUI();
        }

        private void SubscribeToGameManagerEvents()
        {
            if (gameManager == null)
            {
                return;
            }

            gameManager.OnHPChanged -= UpdateHPText;
            gameManager.OnRoomChange -= UpdateRoomText;
            gameManager.OnHPChanged += UpdateHPText;
            gameManager.OnRoomChange += UpdateRoomText;
        }

        private void UnsubscribeFromGameManagerEvents()
        {
            if (gameManager == null)
            {
                return;
            }

            gameManager.OnHPChanged -= UpdateHPText;
            gameManager.OnRoomChange -= UpdateRoomText;
        }

        private void InitCommands()
        {
            startCommand = new StartCommand(gameManager);
            attackCommand = new AttackCommand(gameManager);
            restartCommand = new RestartCommand(gameManager);
        }

        public void OnStartGame()
        {
            string log = startCommand.Execute();
            SetUIState(UIState.Playing);
            AppendLogSimple(log, 12);
        }

        public void OnAttack()
        {
            string log = attackCommand.Execute();
            AppendLogSimple(log, 12);

            if (gameManager.IsGameOver)
            {
                SetUIState(UIState.GameOver);
            }
        }

        public void OnRestartGame()
        {
            string log = restartCommand.Execute();
            SetUIState(UIState.Playing);
            SetLog(log);
        }

        private enum UIState
        {
            Start,
            Playing,
            GameOver
        }

        private void SetUIState(UIState state)
        {
            if (startPanel != null)
                startPanel.SetActive(state == UIState.Start);

            if (gameplayPanel != null)
                gameplayPanel.SetActive(state != UIState.Start);

            if (gameOverPanel != null)
                gameOverPanel.SetActive(state == UIState.GameOver);
        }

        private void AppendLogSimple(string newLog, int maxLines)
        {
            if (logText == null)
                return;

            if (string.IsNullOrEmpty(newLog))
                return;

            string fullLog = string.IsNullOrEmpty(logText.text) ? newLog : logText.text + "\n" + newLog;

            string[] lines = fullLog.Split('\n');
            if (lines.Length > maxLines)
            {
                string[] recentLines = new string[maxLines];
                System.Array.Copy(lines, lines.Length - maxLines, recentLines, 0, maxLines);
                logText.text = string.Join("\n", recentLines);
            }
            else
            {
                logText.text = fullLog;
            }

            Canvas.ForceUpdateCanvases();
        }

        private void SetLog(string message)
        {
            if (logText != null)
            {
                logText.text = message;
            }
        }

        private void UpdateHPText(int currentHp, int maxHp)
        {
            if (gameManager == null)
            {
                return;
            }

            RefreshPlayerStatusUI(currentHp, maxHp, gameManager.PlayerXP, gameManager.PlayerXPToNextLevel);
        }

        private void RefreshPlayerStatusUI()
        {
            if (gameManager == null)
            {
                return;
            }

            RefreshPlayerStatusUI(
                gameManager.PlayerHP,
                gameManager.PlayerMaxHP,
                gameManager.PlayerXP,
                gameManager.PlayerXPToNextLevel);
        }

        private void RefreshPlayerStatusUI(int currentHp, int maxHp, int currentXp, int xpToNext)
        {
            int safeMaxHp = Mathf.Max(1, maxHp);
            int safeXpToNext = Mathf.Max(1, xpToNext);
            float hpNormalized = Mathf.Clamp01((float)Mathf.Clamp(currentHp, 0, safeMaxHp) / safeMaxHp);
            float xpNormalized = Mathf.Clamp01((float)Mathf.Clamp(currentXp, 0, safeXpToNext) / safeXpToNext);

            if (hpScrollbar != null)
            {
                hpScrollbar.size = hpNormalized;
            }

            if (xpScrollbar != null)
            {
                xpScrollbar.size = xpNormalized;
            }

            if (hpBarText != null)
            {
                hpBarText.text = currentHp + "/" + safeMaxHp;
            }

            if (xpBarText != null)
            {
                xpBarText.text = currentXp + "/" + safeXpToNext;
            }
        }

        private void UpdateRoomText(int roomNumber, string roomType)
        {
            RefreshPlayerStatusUI();

            if (enemyText != null)
            {
                enemyText.text =
                    "Room Type: " + roomType + "\n" +
                    "Enemy Count: " + gameManager.LastRoomEnemyCount;
            }
        }
    }
}
