using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class LevelUI : BaseUIBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private string levelFormat = "Level {0}";
        [SerializeField] private Button restartButton;

        protected override void Init()
        {
            base.Init();
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            GameController.Instance.OnLevelLoaded += OnLevelLoaded;
            GameController.Instance.OnLevelStartLoading += OnLevelStartLoading;
        }
        
        private void OnLevelLoaded(int sceneId)
        {
            restartButton.interactable = true;
            SetLevel();
        }

        private void OnLevelStartLoading(int levelNumber)
        {
            restartButton.interactable = false;
        }

        private void SetLevel()
        {
            levelText.text = string.Format(levelFormat, SLS.Data.Game.Level.Value);
        }

        private void OnRestartButtonClicked()
        {
            GameController.Instance.RestartLevel();
            Taptic.Selection();
        }
    }
}