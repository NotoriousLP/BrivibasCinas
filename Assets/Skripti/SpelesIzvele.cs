using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum grutibasPakapes{
    bezAI = 1, //Spēle bez AI pretinieka
	arAI = 2, // Spēle ar AI pretinieku
}
public class spelesIzvele : MonoBehaviour
{
    public Button poga1v1;
	public Button pogaAI;
    public int izveletaPoga;
   
    void Start()
    {
        poga1v1 = GameObject.Find("1v1Poga").GetComponent<Button>();
        pogaAI = GameObject.Find("botaPoga").GetComponent<Button>();

        //Pievienojam triggerus, kas reaģēs uz pogu nospiešanu
        poga1v1.onClick.AddListener(() => SpelesIzvele(grutibasPakapes.bezAI));
		pogaAI.onClick.AddListener(() => SpelesIzvele(grutibasPakapes.arAI));
    }

     //Funkcija, kas izpildās, kad tiek nospiesta kāda no pogām
    void SpelesIzvele(grutibasPakapes pakapes){
        //Saglabā izvēlēto spēles veidu ar PlayerPrefs.
        izveletaPoga = (int)pakapes;
        if(pakapes == grutibasPakapes.bezAI){
            PlayerPrefs.SetInt("Grutiba", izveletaPoga);
        }else{
            PlayerPrefs.SetInt("Grutiba", izveletaPoga);
        }
        AudioSistema.Instance.speletSFX("poga");
        //Ielāde spēles ainu
        SceneManager.LoadScene("spelesAina", LoadSceneMode.Single);
    }
}
