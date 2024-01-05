using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Iestatijumi : MonoBehaviour
{

    // Lietas prieks resolution
    [SerializeField] private TMP_Dropdown resolutionDropdown;


    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    //Resolution vçrtîbas

    //Skaòas vçrtîbas
     public Slider volumeSlider;
     public GameObject ObjektuMuzika;

    public float MusicVolume = 0.5f;
    public AudioSource AudioSource;



    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++) {

            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++) {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }


        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        //Resolution iestatîjuma beigas

        //Skaòas iestatîjumi
        ObjektuMuzika = GameObject.FindWithTag("gameMusic");
        AudioSource = ObjektuMuzika.GetComponent<AudioSource>();

        MusicVolume = PlayerPrefs.GetFloat("volume");
        AudioSource.volume = MusicVolume;
        volumeSlider.value = MusicVolume;
        //Skaòas beigas
    }
    private void Awake()
    {
        GameObject[] muzikasObj = GameObject.FindGameObjectsWithTag("gameMusic");
        if (muzikasObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        /*Scene tagadejaisScene = SceneManager.GetActiveScene();
         if (tagadejaisScene.name == "spelesAina")
        {
            Destroy(this.gameObject);
        }*/
        AudioSource.volume = MusicVolume;
        PlayerPrefs.SetFloat("volume", MusicVolume);
    }



    public void VolumeUpdater(float volume)
        {
        MusicVolume = volume;
        }

        public void setResolution(int resolutionIndex) {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        }


 
}
