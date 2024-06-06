using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class AudioSistema : MonoBehaviour
{
  
  //Statiska instance, lai nodrošinātu piekļuvi šim skriptam no citiem skriptiem.
    public static AudioSistema Instance;


    //Masīvi, kur glabāt mūzikas un skaņas efektu klipus.
    public Skana[] muzika, sfxSkanas;

    //AudioSource komponenti mūzikas un skaņas efektu atskaņošanai.
    public AudioSource muzikasAvots, sfxAvots;

    //Funkcija, kas tiek izsaukta, kad objekts tiek izveidots (pirms Start).
    private void Awake(){
      //Ja vēl nav AudioSistema instances, padara šo par instanci.
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
        //Ja jau ir cita instance, iznīcina šo objektu.
            Destroy(gameObject);
        }
    }

    public void Start()
    {
       //Ielādē mūzikas un SFX skaļumu no PlayerPrefs.
        muzikasAvots.volume = PlayerPrefs.GetFloat("lietotajaSkalums");
        sfxAvots.volume = PlayerPrefs.GetFloat("SFXSkalums");
         //Sāk atskaņot mūzikas gabalu ar nosaukumu "Tema".
        speletMuziku("Tema");
    }

  public void speletMuziku(string vards){
    //Meklē mūzikas klipu masīvā pēc nosaukuma.
    Skana s = Array.Find(muzika, x => x.vards == vards);

    if(s == null){
        Debug.Log("Skana nav atrasta");
    }else{
      //Ja klips ir atrasts, iestata to kā atskaņojamo un sāk atskaņot.
        muzikasAvots.clip = s.clip;
        muzikasAvots.Play();
    }
  }
  //Funkcija skaņas efekta atskaņošanai pēc nosaukuma.
  public void speletSFX(string vards){
    //Meklē skaņas efekta klipu masīvā pēc nosaukuma.
    Skana s = Array.Find(sfxSkanas, x => x.vards == vards);

    if(s == null){
        Debug.Log("Skana nav atrasta");
    }else{
       //Ja klips ir atrasts, atskaņo to vienu reizi.
        sfxAvots.PlayOneShot(s.clip);
    }
  }

  //Funkcija mūzikas skaļuma iestatīšanai un saglabāšanai PlayerPrefs.
  public void muzikasSkalums(float skalums){
    muzikasAvots.volume = skalums;
    PlayerPrefs.SetFloat("lietotajaSkalums", skalums);
  }
  
  //Funkcija skaņas efektu skaļuma iestatīšanai un saglabāšanai PlayerPrefs.
  public void SFXSkalums(float skalums){
    sfxAvots.volume = skalums;
    PlayerPrefs.SetFloat("SFXSkalums", skalums);
  }


}
