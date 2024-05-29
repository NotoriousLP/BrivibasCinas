using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class AudioSistema : MonoBehaviour
{
    public static AudioSistema Instance;

    public Skana[] muzika, sfxSkanas;
    public AudioSource muzikasAvots, sfxAvots;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        muzikasAvots.volume = PlayerPrefs.GetFloat("lietotajaSkalums");
        sfxAvots.volume = PlayerPrefs.GetFloat("SFXSkalums");
        speletMuziku("Tema");
    }
  public void speletMuziku(string vards){
    Skana s = Array.Find(muzika, x => x.vards == vards);

    if(s == null){
        Debug.Log("Skana nav atrasta");
    }else{
        muzikasAvots.clip = s.clip;
        muzikasAvots.Play();
    }
  }
  public void speletSFX(string vards){
    Skana s = Array.Find(sfxSkanas, x => x.vards == vards);

    if(s == null){
        Debug.Log("Skana nav atrasta");
    }else{
        sfxAvots.PlayOneShot(s.clip);
    }
  }

  public void muzikasSkalums(float skalums){
    muzikasAvots.volume = skalums;
    PlayerPrefs.SetFloat("lietotajaSkalums", skalums);
  }
  public void SFXSkalums(float skalums){
    sfxAvots.volume = skalums;
    PlayerPrefs.SetFloat("SFXSkalums", skalums);
  }


}
