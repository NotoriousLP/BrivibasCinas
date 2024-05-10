using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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


    
    public void vesturisksNotikums(){
        if(objekti.lietotajuKarta){   
            if (objekti.noklikBlakusState == GameObject.Find("States_5")){
                vesturisksApraksts.text = "<style=H1>Vēsturisks fakts</style><br><#555>3. martā sākās plašs uzbrukums Sarkanajai Armijai ar mērķi sasniegt Lielupi. Latviešu bataljonam bija jāveic uzbrukums Saldus virzienā. 6. martā negaidītā savstarpējā latviešu un vāciešu apšaudē gāja bojā Atsevišķā latviešu bataljona komandieris <b>Oskars Kalpaks</b>, Atsevišķās Studentu rotas komandieris kapteinis <b>Nikolajs Grundmanis</b>, jātnieku nodaļas virsleitnants <b>Pēteris Krievs</b> un piekomandētās artilērijas pusbaterijas leitnants <b>Šrinders</b>; arī vācu puse cieta zaudējumus. <br Pēc Kalpaka nāves bataljona komandēšana tika uzticēta kapteinim <b>Jānim Balodim</b>.";
            }

          }
        }

}
