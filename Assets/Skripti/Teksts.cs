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
            if (objekti.noklikBlakusState == GameObject.Find("States_3")){
                vesturisksApraksts.text = "Latvijas Republikas un vāciešu pretuzbrukums no Ventas līdz Lielupei 3. martā sākās plašs uzbrukums Sarkanajai Armijai ar mērķi sasniegt Lielupi. Latviešu bataljonam bija jāveic uzbrukums Saldus virzienā. 6. martā negaidītā savstarpējā latviešu un vāciešu apšaudē gāja bojā Atsevišķā latviešu bataljona komandieris Oskars Kalpaks, Atsevišķās Studentu rotas komandieris kapteinis Nikolajs Grundmanis, jātnieku nodaļas virsleitnants Pēteris Krievs un piekomandētās artilērijas pusbaterijas leitnants Šrinders; arī vācu Dzelzsdivīzijas Borha bataljons cieta zaudējumus. Pēc Kalpaka nāves bataljona komandēšana tika uzticēta kapteinim Jānim Balodim. 10. martā atsevišķais latviešu bataljons ieņēma Saldu, 16. martā bataljona Studentu rota atguva Jaunpili. Landesvēra spēki pēc Tukuma ieņemšanas nesaskaņoti devās uz Jelgavu, ko ieņēma 18. martā. Ziemeļu virziens palika nenosegts un latviešu bataljonam nācās pārgrupēties uz ziemeļiem, 19. martā tas ieņēma Līvbērzes muižu. 20. martā bataljonam pienāca papildinājums divu rotu (300 vīru) sastāvā. 1919. gada 21. martā Latviešu atsevišķais bataljons tika pārformēts par 1. Latviešu atsevišķo brigādi ar trīs bataljoniem (1. Neatkarības bataljonu, 2. Cēsu bataljonu, 3. Studentu bataljonu). Brigāde devās uzbrukumā Kalnciema un Slokas virzienā un 22. martā notika kauja pie Batariem pret 10. Padomju Latvijas strēlnieku pulku. 24. martā brigādes daļas ieņēma Kalnciemu un Ķemerus, bet pēc pāris dienām Sloku un sasniedza plānoto Lielupes līniju, kur uzbrukums apstājās. 30. martā Baltijas landesvēra pakļautībā Lielupes frontē bija pāri par 2200 vācbaltiešu un 849 latviešu karavīru, nesakaitot vācu Dzelzsdivīzijas daļas.";
            }

          }
        }

}
