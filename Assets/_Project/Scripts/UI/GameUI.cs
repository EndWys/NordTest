using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        public static GameUI Instance { get; private set; }

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _enemyCountText;
        [SerializeField] private GameObject _centerPanel;
        [SerializeField] private TextMeshProUGUI _centerTitleText;
        [SerializeField] private TextMeshProUGUI _centerSubtitleText;

        public void Init()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetEnemyCount(int count)
        {
            _enemyCountText.text = $"Enemies Left: {count}";
        }

        public void ShowWinScreen()
        {
            _centerPanel.SetActive(true);
            _centerTitleText.text = "You Win!";
            _centerSubtitleText.text = "Waiting for restart...";
        }

        public void ShowGameOverScreen()
        {
            _centerPanel.SetActive(true);
            _centerTitleText.text = "Game Over";
            _centerSubtitleText.text = "Waiting for respawn...";
        }

        public void HideCenterPanel()
        {
            _centerPanel.SetActive(false);
        }
    }
}