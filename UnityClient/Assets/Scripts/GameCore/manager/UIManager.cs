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
        [SerializeField] private Text playerText;
        [SerializeField] private Text enemyText;
        [SerializeField] private Text logText;
        [SerializeField] private Button startButton;
        [SerializeField] private Button attackButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private GameManager gameManager;

        private IUICommand startCommand;
        private IUICommand attackCommand;
        private IUICommand restartCommand;

        private void Start()
        {
            if (gameManager == null)
            {
                gameManager = Object.FindFirstObjectByType<GameManager>();
            }

            InitCommands();
            SetGameplayUIVisible(false);
            ShowStartOnlyButtons();

            UpdateUI();
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
            SetGameplayUIVisible(true);
            ShowPlayingButtons();
            AppendLogWithLimit(log, 8);
            UpdateUI();
        }

        public void OnAttack()
        {
            string log = attackCommand.Execute();
            AppendLogWithLimit(log, 8);

            if (gameManager.IsGameOver)
            {
                ShowGameOverButtons();
            }

            UpdateUI();
        }

        public void OnRestartGame()
        {
            string log = restartCommand.Execute();
            SetGameplayUIVisible(true);
            ShowPlayingButtons();
            SetLog(log);
            UpdateUI();
        }

        private void SetGameplayUIVisible(bool visible)
        {
            if (playerText != null)
                playerText.gameObject.SetActive(visible);

            if (enemyText != null)
                enemyText.gameObject.SetActive(visible);

            if (logText != null)
                logText.gameObject.SetActive(visible);
        }

        private void ShowStartOnlyButtons()
        {
            if (startButton != null)
            {
                startButton.gameObject.SetActive(true);
                startButton.interactable = true;
            }

            if (attackButton != null)
            {
                attackButton.gameObject.SetActive(false);
                attackButton.interactable = false;
            }

            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(false);
                restartButton.interactable = false;
            }
        }

        private void ShowPlayingButtons()
        {
            if (startButton != null)
            {
                startButton.gameObject.SetActive(false);
                startButton.interactable = false;
            }

            if (attackButton != null)
            {
                attackButton.gameObject.SetActive(true);
                attackButton.interactable = true;
            }

            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(false);
                restartButton.interactable = false;
            }
        }

        private void ShowGameOverButtons()
        {
            if (startButton != null)
            {
                startButton.gameObject.SetActive(false);
                startButton.interactable = false;
            }

            if (attackButton != null)
            {
                attackButton.gameObject.SetActive(false);
                attackButton.interactable = false;
            }

            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(true);
                restartButton.interactable = true;
            }
        }

        private void AppendLogWithLimit(string newLog, int maxLines)
        {
            if (logText == null)
                return;

            string fullLog = logText.text + "\n" + newLog;
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
        }

        private void SetLog(string message)
        {

            logText.text = message;
        
        }

        private void UpdateUI()
        {

            playerText.text =
                "Player HP: " + gameManager.PlayerHP + "/" + gameManager.PlayerMaxHP + "\n" +
                "ATK: " + gameManager.PlayerAttack + "\n" +
                "Room: " + gameManager.CurrentRoom;

            enemyText.text =
                "Room Type: " + gameManager.LastRoomType + "\n" +
                "Enemy Count: " + gameManager.LastRoomEnemyCount;

        }
    }
}
