using System;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    public event Action OnInitCompleted;
    public event Action OnShowFadeUI;
    public event Action<int> OnLevelStartLoading;
    public event Action<int> OnLevelLoaded;
    public event Action<bool> OnLevelEnd;
    public event Action OnLevelStarted;

    private void Start()
    {
        LevelsController.Instance.OnLevelLoaded += LevelsController_LevelLoaded;
        Application.targetFrameRate = 60;
        AnalyticsHelper.Init();
        LateStart();
    }

    private void LateStart()
    {
        this.DoAfterNextFrameCoroutine(() =>
        {
            OnInitCompleted?.Invoke();
            LoadLevel();
        });
    }

    private void LevelsController_LevelLoaded(int levelNumber)
    {
        Taptic.Light();
        AnalyticsHelper.StartLevel();
        OnLevelLoaded?.Invoke(levelNumber);
    }

    public void InvokeOnStartLevelLoading()
    {
        var levelNumber = SLS.Data.Game.Level.Value;
        LevelsController.Instance.LoadLevel(levelNumber);
        OnLevelStartLoading?.Invoke(levelNumber);
    }

    public void LevelStart()
    {
        OnLevelStarted?.Invoke();
    }

    public void LevelEnd(bool playerWin)
    {
        if (playerWin)
        {
            Taptic.Success();
            AnalyticsHelper.CompleteLevel();
        }
        else
        {
            Taptic.Failure();
            AnalyticsHelper.FailLevel();
        }

        OnLevelEnd?.Invoke(playerWin);
    }

    public void LoadLevel()
    {
        OnShowFadeUI?.Invoke();
    }

    public void NextLevel()
    {
        SLS.Data.Game.Level.Value++;
        LoadLevel();
    }

    public void RestartLevel()
    {
        LoadLevel();
    }
}