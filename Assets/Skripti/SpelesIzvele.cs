using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum grutibasPakapes{
    bezAI = 1,
	arAI = 2,
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

        poga1v1.onClick.AddListener(() => SpelesIzvele(grutibasPakapes.bezAI));
		pogaAI.onClick.AddListener(() => SpelesIzvele(grutibasPakapes.arAI));
    }

    void SpelesIzvele(grutibasPakapes pakapes){
        izveletaPoga = (int)pakapes;
        if(pakapes == grutibasPakapes.bezAI){
            PlayerPrefs.SetInt("Grutiba", izveletaPoga);
        }else{
            PlayerPrefs.SetInt("Grutiba", izveletaPoga);
        }
        AudioSistema.Instance.speletSFX("poga");
        SceneManager.LoadScene("spelesAina", LoadSceneMode.Single);
    }
}
