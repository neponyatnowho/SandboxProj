using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TFPlay.DeveloperUtilities
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FPSCounter : MonoBehaviour
    {
        private const string DisplayFormat = "{0} FPS";

        [SerializeField]
        private TextMeshProUGUI fpsText;

        private float timer;
        private float refresh;

        public float AverageFramerate { get; private set; }

        private void OnValidate()
        {
            fpsText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            float timelapse = Time.smoothDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;
            if (timer <= 0)
            {
                AverageFramerate = (int)(1f / timelapse);
            }
            fpsText.text = string.Format(DisplayFormat, AverageFramerate);
        }
    }
}
