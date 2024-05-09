using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Teksts : MonoBehaviour
{
    //Teksti
    public TextMeshProUGUI rotuTeksts;

    public TextMeshProUGUI skaitaLauks;
    public Objekti objekti;
    public SpelesKontrole kontrole;

    public TextMeshProUGUI vesturisksApraksts;

    public TextMeshProUGUI cinasTeksts;

     void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
    }
    // Update is called once per frame
    void Update()
    {
        if(objekti.lietotajuKarta == true){
        rotuTeksts.text = objekti.rotuSkaitsPlayer.ToString();
        }else if(objekti.otraSpeletajaKarta == true){
        rotuTeksts.text = objekti.rotuSkaitsLSPR.ToString();
        }

        skaitaLauks.text = objekti.rotuSkaitsIzv.ToString();
    }
}
