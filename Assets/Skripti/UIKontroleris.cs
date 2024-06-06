using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKontroleris : MonoBehaviour
{
    // Publiskie mainīgie, kas atspoguļo mūzikas un SFX(efektu) skaļuma sliderus. 
    public Slider _muzikasSliders, _sfxSliders;

    void Start(){
        //Ielādē iepriekš saglabātās skaļuma vērtības ar PlayerPrefs palīdzību.
        _muzikasSliders.value = PlayerPrefs.GetFloat("lietotajaSkalums");
        _sfxSliders.value = PlayerPrefs.GetFloat("SFXSkalums");
    }

    //Funkcija, kas tiek izsaukta, kad mainās mūzikas skaļuma slidera vērtība.
    public void muzikasSkalums(){
        //Nodod jauno skaļuma vērtību AudioSistema instancei, lai tā varētu atjaunināt mūzikas skaļumu.
        AudioSistema.Instance.muzikasSkalums(_muzikasSliders.value);
    }

    // Funkcija, kas tiek izsaukta, kad mainās SFX(efekta) skaļuma slidera vērtība.
    public void SFXSkalums(){
        //Nodod jauno skaļuma vērtību AudioSistema instancei, lai tā varētu atjaunināt SFX skaļumu.
        AudioSistema.Instance.SFXSkalums(_sfxSliders.value);
    }
}
