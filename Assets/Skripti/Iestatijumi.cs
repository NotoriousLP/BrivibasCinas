using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Iestatijumi : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private int currentResolutionIndex = 0; 

    void Start()
    {
        resolutions = new Resolution[]
        {
            new Resolution { width = 3840, height = 2160},
            new Resolution { width = 1920, height = 1080},
            new Resolution { width = 1366, height = 768},
             new Resolution { width = 1600, height = 900},
        };

        // Clear the dropdown options and add the custom resolutions
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            // Find the current resolution index
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Add listener for dropdown value changes
        resolutionDropdown.onValueChanged.AddListener(SetResolution); 
    }

    // Method to set the resolution
    void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
