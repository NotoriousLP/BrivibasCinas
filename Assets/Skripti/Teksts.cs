using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Teksts : MonoBehaviour
{
    //Teksti
    public TextMeshProUGUI rotuTeksts;

    public Objekti objekti;
    public SpelesKontrole kontrole;

     void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
    }
    // Update is called once per frame
    void Update()
    {
        rotuTeksts.text = objekti.rotuSkaits.ToString();
        objekti.skaitaLauks.text = objekti.rotuSkaitsIzv.ToString();
    }
}
