using TFPlay.UI;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoSingleton<MenuUI>
{
    [SerializeField] private WinUI winUI;
    [SerializeField] private LoseUI loseUI;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private CoinsUI coinsUI;

    private void Start()
    {
        GameController.Instance.OnInitCompleted += Init;
    }

    private void Init()
    {
        GameController.Instance.OnLevelEnd += OnLevelEnd;
    }

    private void OnLevelEnd(bool playerWon)
    {
        if (playerWon)
        {
            winUI.Show();
        }
        else
        {
            loseUI.Show();
        }
    }
}