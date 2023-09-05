using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TFPlay.UI;

namespace TFPlay.DeveloperUtilities
{
    public class DeveloperPanelEnabler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const float timeToOpen = 5;

        [SerializeField]
        private BaseSettingsUI settingsUI;

        private Coroutine waitAndShowCoroutine;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (DeveloperPanel.Instance != null && settingsUI != null && settingsUI.IsOpened)
            {
                waitAndShowCoroutine = StartCoroutine(WaitAndShowDeveloperPanel());
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (waitAndShowCoroutine != null)
            {
                StopCoroutine(waitAndShowCoroutine);
            }
        }

        private IEnumerator WaitAndShowDeveloperPanel()
        {
            yield return new WaitForSeconds(timeToOpen);
            DeveloperPanel.Instance.TogglePanel();
        }
    }
}
