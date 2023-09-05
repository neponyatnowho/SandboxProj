using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

namespace TFPlay.DeveloperUtilities
{
    public class PerfomanceTester : BaseDeveloperTool
    {
        [SerializeField]
        private Transform buttonsHolder;
        [SerializeField]
        private TextMeshProUGUI resolutionText;
        [SerializeField]
        private Slider resolutionSlider;

        [SerializeField]
        private Light directionalLight;
        [SerializeField]
        private Volume postProcessingVolume;
        [SerializeField]
        private PerfomanceTesterButton perfomanceTesterButtonPrefab;
        [SerializeField]
        private UniversalRendererData forwardRendererData;

        private Vector2 initialScreenResolution;
        private Material skyboxMaterial;

        protected override void InitInternal()
        {
            resolutionSlider.onValueChanged.AddListener(OnResolutionValueChanged);

            skyboxMaterial = RenderSettings.skybox;
            initialScreenResolution = new Vector2(Screen.width, Screen.height);

            if (directionalLight != null)
            {
                var shadowsButton = Instantiate(perfomanceTesterButtonPrefab, buttonsHolder);
                shadowsButton.Init("Shadows", () => directionalLight.shadows != LightShadows.None, e => directionalLight.shadows = e ? LightShadows.Soft : LightShadows.None);
            }

            if (skyboxMaterial != null)
            {
                var skyboxButton = Instantiate(perfomanceTesterButtonPrefab, buttonsHolder);
                skyboxButton.Init("Skybox", () => RenderSettings.skybox != null, e => RenderSettings.skybox = e ? skyboxMaterial : null);
            }

            if (postProcessingVolume != null)
            {
                var postProcessingVolumeButton = Instantiate(perfomanceTesterButtonPrefab, buttonsHolder);
                postProcessingVolumeButton.Init("Post Processing", () => postProcessingVolume.enabled, e => postProcessingVolume.enabled = e);

                var ppComponents = postProcessingVolume.profile.components;
                foreach (var component in ppComponents)
                {
                    if (!component.active)
                        continue;

                    var ppComponentButton = Instantiate(perfomanceTesterButtonPrefab, buttonsHolder);
                    var ppComponentName = component.name.Replace("(Clone)", "");
                    ppComponentButton.Init(ppComponentName, () => component.active, e => component.active = e);
                }
            }

            if (forwardRendererData != null && forwardRendererData.rendererFeatures.Count > 0)
            {
                var renderFeaturesButton = Instantiate(perfomanceTesterButtonPrefab, buttonsHolder);
                renderFeaturesButton.Init("Render Features", () => forwardRendererData.rendererFeatures.Any(f => f.isActive), e => forwardRendererData.rendererFeatures.ForEach(f => f.SetActive(e)));

                var rfComponents = forwardRendererData.rendererFeatures;
                foreach (var feature in rfComponents)
                {
                    var rfComponentButton = Instantiate(perfomanceTesterButtonPrefab, buttonsHolder);
                    var rfComponentName = feature.name;
                    rfComponentButton.Init(rfComponentName, () => feature.isActive, e => feature.SetActive(e));
                }
            }
        }

        private void OnResolutionValueChanged(float value)
        {
            var percent = (int)value / 10f;
            var screenSize = new Vector2(initialScreenResolution.x, initialScreenResolution.y);
            var newScreenSize = new Vector2Int((int)(screenSize.x * percent), (int)(screenSize.y * percent));
            Screen.SetResolution(newScreenSize.x, newScreenSize.y, true);
            resolutionText.text = "Resolution: " + percent;
        }
    }
}
