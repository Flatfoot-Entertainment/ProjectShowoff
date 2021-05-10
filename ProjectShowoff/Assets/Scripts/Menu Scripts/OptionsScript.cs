using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] private RenderPipelineAsset[] qualityPresets;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Resolution[] resolutions;
    [SerializeField] private Toggle fullscreenToggle;
    // Start is called before the first frame update
    void Start()
    {
        //sets the dropdown's setting to whatever quality setting we have
        graphicsDropdown.value = QualitySettings.GetQualityLevel();

        //sets the fullscreen toggle based on the screen's state
        fullscreenToggle.isOn = Screen.fullScreen;

        //setting the resolution dropdown's options
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        List<string> resolutionStrings = new List<string>();
        int currentResolutionIndex = 0;

        //loop through the resolutions to set as string values to the dropdown
        for(int i = 0; i < resolutions.Length; i++)
        {
            Resolution resolution = resolutions[i];
            string resolutionString = resolution.width + "x" + resolution.height;
            resolutionStrings.Add(resolutionString);
            if(resolution.width == Screen.currentResolution.width &&
               resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionStrings);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        //changes quality level as well as the URP preset
        QualitySettings.SetQualityLevel(qualityIndex);
        QualitySettings.renderPipeline = qualityPresets[qualityIndex];
    }

    public void SetFullscreen(bool isFullscreen)
    {
        //self explanatory
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        //also self explanatory
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
