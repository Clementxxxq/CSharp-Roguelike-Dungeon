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

        [Header("Game Over UI")]
        [SerializeField] private Text gameOverLoseText;
        [SerializeField] private Text gameOverWinText;

        [Header("Status UI")]
        [SerializeField] private Text roomText;
        [SerializeField] private Text enemyText;
        [SerializeField] private Text logText;
        [SerializeField] private ScrollRect logScrollRect;
        [SerializeField] private Scrollbar hpScrollbar;
        [SerializeField] private Scrollbar xpScrollbar;
        [SerializeField] private Text hpBarText;
        [SerializeField] private Text xpBarText;
        [SerializeField] private Text playerInfoText; // 玩家等级和属性显示

        [Header("References")]
        [SerializeField] private GameManager gameManager;

        private IUICommand startCommand;
        private IUICommand attackCommand;
        private IUICommand defendCommand;
        private IUICommand skillCommand;
        private IUICommand restartCommand;
        private bool pendingScrollToBottom;

        private void Awake()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;

                if (gameManager == null)
                {
                    gameManager = FindFirstObjectByType<GameManager>();
                }

                if (gameManager == null)
                {
                    Debug.LogError("UIManager could not find GameManager. Assign it in Inspector or ensure one exists in scene.", this);
                    enabled = false;
                }
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
            if (gameManager == null) return;

            if (logText == null)
            {
                Debug.LogWarning("UIManager: logText is not assigned. Logs will not be visible.", this);
            }

            if (logScrollRect == null)
            {
                Debug.LogWarning("UIManager: logScrollRect is not assigned. Log auto-scroll is disabled.", this);
            }

            InitCommands();
            SetUIState(UIState.Start);
            RefreshPlayerStatusUI();
        }

        private void SubscribeToGameManagerEvents()
        {
            if (gameManager == null) return;

            gameManager.OnHPChanged -= UpdateHPText;
            gameManager.OnRoomChange -= UpdateRoomText;
            gameManager.OnHPChanged += UpdateHPText;
            gameManager.OnRoomChange += UpdateRoomText;
        }

        private void UnsubscribeFromGameManagerEvents()
        {
            if (gameManager == null) return;

            gameManager.OnHPChanged -= UpdateHPText;
            gameManager.OnRoomChange -= UpdateRoomText;
        }

        private void InitCommands()
        {
            startCommand = new StartCommand(gameManager);
            attackCommand = new AttackCommand(gameManager);
            defendCommand = new DefendCommand(gameManager);
            skillCommand = new SkillCommand(gameManager);
            restartCommand = new RestartCommand(gameManager);
        }

        private bool EnsureCommandsReady()
        {
            if (gameManager == null)
            {
                Debug.LogError("UIManager has no GameManager reference.", this);
                return false;
            }

            if (startCommand == null)
            {
                InitCommands();
            }

            return true;
        }

        public void OnStartGame()
        {
            if (!EnsureCommandsReady())
            {
                return;
            }

            string log = startCommand.Execute();
            SetUIState(gameManager.IsGameOver ? UIState.GameOver : UIState.Playing);
            AppendLogSimple(log);
        }

        public void OnAttack()
        {
            if (!EnsureCommandsReady())
            {
                return;
            }

            string log = attackCommand.Execute();
            AppendLogSimple(log);

            if (gameManager.IsGameOver)
            {
                SetUIState(UIState.GameOver);
            }
        }

        public void OnDefend()
        {
            if (!EnsureCommandsReady())
            {
                return;
            }

            string log = defendCommand.Execute();
            AppendLogSimple(log);

            if (gameManager.IsGameOver)
            {
                SetUIState(UIState.GameOver);
            }
        }

        public void OnSkill()
        {
            if (!EnsureCommandsReady())
            {
                return;
            }

            string log = skillCommand.Execute();
            AppendLogSimple(log);

            if (gameManager.IsGameOver)
            {
                SetUIState(UIState.GameOver);
            }
        }

        public void OnRestartGame()
        {
            if (!EnsureCommandsReady())
            {
                return;
            }

            string log = restartCommand.Execute();
            SetUIState(gameManager.IsGameOver ? UIState.GameOver : UIState.Playing);
            SetLog(string.Empty);
            AppendLogSimple(log);
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
                gameplayPanel.SetActive(state == UIState.Playing);

            if (gameOverPanel != null)
                gameOverPanel.SetActive(state == UIState.GameOver);

            if (gameOverLoseText != null)
            {
                bool showLose = state == UIState.GameOver && gameManager != null &&
                                gameManager.LastGameOverOutcome == GameManager.GameOverOutcome.Lose;
                gameOverLoseText.gameObject.SetActive(showLose);
                gameOverLoseText.text = "YOU DIE";
            }

            if (gameOverWinText != null)
            {
                bool showWin = state == UIState.GameOver && gameManager != null &&
                               gameManager.LastGameOverOutcome == GameManager.GameOverOutcome.Win;
                gameOverWinText.gameObject.SetActive(showWin);
                gameOverWinText.text = "YOU WIN";
            }
        }

        private void AppendLogSimple(string newLog)
        {
            if (logText == null || string.IsNullOrEmpty(newLog)) return;

            string fullLog = string.IsNullOrEmpty(logText.text) ? newLog : logText.text + "\n" + newLog;
            logText.text = fullLog;
            pendingScrollToBottom = true;
        }

        private void SetLog(string message)
        {
            if (logText != null)
            {
                logText.text = message;
                pendingScrollToBottom = true;
            }
        }

        private void LateUpdate()
        {
            if (!pendingScrollToBottom)
            {
                return;
            }

            pendingScrollToBottom = false;

            if (logScrollRect == null)
            {
                return;
            }

            Canvas.ForceUpdateCanvases();
            logScrollRect.verticalNormalizedPosition = 0f;
        }

        private void UpdateHPText(int currentHp, int maxHp)
        {
            if (gameManager == null) return;

            RefreshPlayerStatusUI(currentHp, maxHp, gameManager.CombatSystem.PlayerXP, gameManager.CombatSystem.PlayerXPToNextLevel);
        }

        private void RefreshPlayerInfoUI()
        {
            if (playerInfoText == null || gameManager == null) return;

            if (!gameManager.IsGameStarted)
            {
                playerInfoText.text = "Niveau: -  Attaque: -  Defense: -  Skill CD: -";
                return;
            }

            playerInfoText.text =
                $"Niveau: {gameManager.CombatSystem.PlayerLevel}  " +
                $"Attaque: {gameManager.CombatSystem.PlayerAttack}  " +
                $"Defense: {gameManager.CombatSystem.PlayerDefense}  " +
                $"Skill CD: {gameManager.CombatSystem.PlayerSkillCooldown}";
        }

        private void RefreshPlayerStatusUI()
        {
            if (gameManager == null) return;

            RefreshPlayerStatusUI(
                gameManager.CombatSystem.PlayerHP,
                gameManager.CombatSystem.PlayerMaxHP,
                gameManager.CombatSystem.PlayerXP,
                gameManager.CombatSystem.PlayerXPToNextLevel);
        }

        private void RefreshPlayerStatusUI(int currentHp, int maxHp, int currentXp, int xpToNext)
        {
            int safeMaxHp = Mathf.Max(1, maxHp);
            int safeXpToNext = Mathf.Max(1, xpToNext);

            float hpNormalized = Mathf.Clamp01((float)Mathf.Clamp(currentHp, 0, safeMaxHp) / safeMaxHp);
            float xpNormalized = Mathf.Clamp01((float)Mathf.Clamp(currentXp, 0, safeXpToNext) / safeXpToNext);

            if (hpScrollbar != null)
                hpScrollbar.size = hpNormalized;

            if (xpScrollbar != null)
                xpScrollbar.size = xpNormalized;

            if (hpBarText != null)
                hpBarText.text = currentHp + "/" + safeMaxHp;

            if (xpBarText != null)
                xpBarText.text = currentXp + "/" + safeXpToNext;

            RefreshPlayerInfoUI();
        }

        private void UpdateRoomText(int roomNumber, string roomType)
        {
            RefreshPlayerStatusUI();

            if (roomText != null)
            {
                roomText.text =
                    "Wave " + gameManager.LastRoomWaveProgress +
                    " Room: " + roomNumber +
                    " Room Type: " + roomType;
            }

            if (enemyText != null)
            {
                enemyText.text =
                    "Enemy Count: " + gameManager.LastRoomEnemyCount + "\n" +
                    "Next Intent: " + gameManager.LastEnemyIntentText + "\n" +
                    "Enemy Stats:\n" + gameManager.LastEnemyInfoText;
            }
        }
    }
}