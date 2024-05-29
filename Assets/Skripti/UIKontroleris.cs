using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKontroleris : MonoBehaviour
{
    public Slider _muzikasSliders, _sfxSliders;

    void Start(){
        _muzikasSliders.value = PlayerPrefs.GetFloat("lietotajaSkalums");
        _sfxSliders.value = PlayerPrefs.GetFloat("SFXSkalums");
    }

    public void muzikasSkalums(){
        AudioSistema.Instance.muzikasSkalums(_muzikasSliders.value);
    }
    public void SFXSkalums(){
        AudioSistema.Instance.SFXSkalums(_sfxSliders.value);
    }
}
