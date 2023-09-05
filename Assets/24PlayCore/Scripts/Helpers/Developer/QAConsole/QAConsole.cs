using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace TFPlay.DeveloperUtilities
{
    public class QAConsole : BaseDeveloperTool
    {
        [Header("Level")]
        [SerializeField] private TMP_InputField levelInputField;
        [SerializeField] private Button loadLevelButton;
        [SerializeField] private TextMeshProUGUI levelNameText;
        [SerializeField] private TextMeshProUGUI levelNumberText;
        [SerializeField] private TextMeshProUGUI buildVersionText;
        [Header("Coins")]
        [SerializeField] private Button addCoinsButton;
        [SerializeField] private Button removeCoinsButton;
        [SerializeField] private int coinAmount;
        [Header("FPS")]
        [SerializeField] private Button showFPSButton;
        [SerializeField] private GameObject FPSCounter;
        [Header("Core")]
        [SerializeField] private Button winLevelButton;
        [SerializeField] private Button loseLevelButton;
        [Header("UI")]
        [SerializeField] private Button toggleMenuUIButton;
        [SerializeField] private TextMeshProUGUI toggleMenuUIText;

        private bool showUI = true;
        private static List<IQAHideableContent> hideableContents = new List<IQAHideableContent>();

        protected override void InitInternal()
        {
            loadLevelButton.onClick.AddListener(LoadLevel);
            addCoinsButton.onClick.AddListener(AddCoins);
            removeCoinsButton.onClick.AddListener(ClearCoins);
            showFPSButton.onClick.AddListener(ToggleFPSCounter);
            winLevelButton.onClick.AddListener(WinLevel);
            loseLevelButton.onClick.AddListener(LoseLevel);
            toggleMenuUIButton.onClick.AddListener(ToggleMenuUI);
        }

        protected override void TogglePanel()
        {
            base.TogglePanel();
            if (isOpened)
            {
                ShowInformation();
            }
        }

        private void ToggleFPSCounter()
        {
            FPSCounter.SetActive(!FPSCounter.activeSelf);
        }

        private void LoadLevel()
        {
            if (int.TryParse(levelInputField.text, out int levelNumber))
            {
                isOpened = false;
                content.SetInactive();
                SLS.Data.Game.Level.Value = levelNumber;
                GameController.Instance.LoadLevel();
            }
        }

        private void ShowInformation()
        {
            if (SceneManager.sceneCount > 1)
            {
                levelNameText.text = string.Format("SCENE NAME: {0}", SceneManager.GetSceneAt(1).name);
                levelNumberText.text = string.Format("SCENE NUMBER IN BUILD: {0}", SceneManager.GetSceneAt(1).buildIndex);
            }

            buildVersionText.text = string.Format("BUILD VERSION: {0}", Application.version);
        }

        private void AddCoins()
        {
            SLS.Data.Game.Coins.Value += coinAmount;
        }

        private void ClearCoins()
        {
            SLS.Data.Game.Coins.Value = 0;
        }

        private void WinLevel()
        {
            GameController.Instance.LevelEnd(true);
        }

        private void LoseLevel()
        {
            GameController.Instance.LevelEnd(false);
        }

        private void ToggleMenuUI()
        {
            showUI = !showUI;
            toggleMenuUIText.text = showUI ? "Hide UI" : "Show UI";
            foreach (var hideableContent in hideableContents)
            {
                hideableContent.ToggleContent();
            }
        }

        public static void RegisterContent(IQAHideableContent hideableContent)
        {
            hideableContents.Add(hideableContent);
        }

        public static void UnregisterContent(IQAHideableContent hideableContent)
        {
            hideableContents.Remove(hideableContent);
        }
    }
}
