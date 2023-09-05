using UnityEngine;

namespace TFPlay.DeveloperUtilities
{
    public class GameCheats : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private KeyCode winLevelKeyCode = KeyCode.W;
        [SerializeField] private KeyCode loseLevelKeyCode = KeyCode.L;
        [SerializeField] private KeyCode addCoinsKeyCode = KeyCode.C;
        [SerializeField] private KeyCode nextLevelKeyCode = KeyCode.N;
        [SerializeField] private KeyCode restartLevelKeyCode = KeyCode.R;
        [SerializeField] private KeyCode qaConsoleKeyCode = KeyCode.Q;

        private void Update()
        {
            if (Input.GetKeyDown(winLevelKeyCode))
            {
                GameController.Instance.LevelEnd(true);
            }
            if (Input.GetKeyDown(loseLevelKeyCode))
            {
                GameController.Instance.LevelEnd(false);
            }
            if (Input.GetKeyDown(addCoinsKeyCode))
            {
                SLS.Data.Game.Coins.Value += 10000;
            }
            if (Input.GetKeyDown(nextLevelKeyCode))
            {
                GameController.Instance.NextLevel();
            }
            if (Input.GetKeyDown(restartLevelKeyCode))
            {
                GameController.Instance.RestartLevel();
            }
            if (Input.GetKeyDown(qaConsoleKeyCode))
            {
                DeveloperPanel.Instance.ShowTools();
            }
        }
#endif
    }
}
