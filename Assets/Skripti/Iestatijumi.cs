using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Iestatijumi : MonoBehaviour
{
    //Atsauce uz UI elementu, kurā parādīsies izšķirtspējas
    [SerializeField] private TMP_Dropdown resolutionDropdown;


    // Masīvs, kurā tiks glabātas izšķirtspējas
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0; 

    void Start()
    {
        //Definē atbalstītās izšķirtspējas
        resolutions = new Resolution[]
        {
            new Resolution { width = 3840, height = 2160},
            new Resolution { width = 1920, height = 1080},
            new Resolution { width = 1366, height = 768},
             new Resolution { width = 1600, height = 900},
        };

        //Notīra iepriekšējās izvēlnes opcijas
        resolutionDropdown.ClearOptions();
        //Pievieno izšķirtspējas kā izvēlnes opcijas
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);
            // Nosaka, kura izšķirtspējas ir pašreizējā
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //Pievieno izveidotās opcijas dropdown izvēlnē
        resolutionDropdown.AddOptions(options);
        //Uzstāda izvēlnes sākotnējo vērtību uz pašreizējo izšķirtspēju
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        //Pievieno notikumu, kas reaģēs uz izšķirtspējas maiņu izvēlnē
        resolutionDropdown.onValueChanged.AddListener(SetResolution); 
    }

    //Funkcija, kas uzstāda jaunu izšķirtspēju, kad tiek mainīta izvēlne
    void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
         //Uzstāda jauno izšķirtspēju, izmantojot Screen klasi
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
