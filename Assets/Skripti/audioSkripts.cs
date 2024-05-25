using UnityEngine;
using UnityEngine.UI;

public class audioSkripts : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicSource;

    void Start()
    {
        GameObject musicObj = GameObject.FindWithTag("gameMusic");
        if (musicObj != null) 
            musicSource = musicObj.GetComponent<AudioSource>();
        LoadUnUzstaditSkalumu("volume", musicSource, volumeSlider);
        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener((value) => UpdateUnSaglabatSkalumu("volume", musicSource, value));
    }


    private void LoadUnUzstaditSkalumu(string playerPrefsKey, AudioSource audioSource, Slider slider)
    {
        if (audioSource == null || slider == null)
            return; 
        float skalums = PlayerPrefs.GetFloat(playerPrefsKey, 0.5f);
        audioSource.volume = skalums;
        slider.value = skalums;
    }


    private void UpdateUnSaglabatSkalumu(string playerPrefsKey, AudioSource audioSource, float skalums)
    {
        if (audioSource != null)
            audioSource.volume = skalums;

        PlayerPrefs.SetFloat(playerPrefsKey, skalums);
    }
}
