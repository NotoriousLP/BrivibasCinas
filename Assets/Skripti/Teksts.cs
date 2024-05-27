using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Teksts : MonoBehaviour
{

    //Save/Load teksti
    public TextMeshProUGUI saveTeksts;
    public TextMeshProUGUI loadTeksts;

    public string timerText;
    //Teksti
    public TextMeshProUGUI rotuTeksts;

    public TextMeshProUGUI skaitaLauks;
    public Objekti objekti;
    public SpelesKontrole kontrole;

    public TextMeshProUGUI vesturisksApraksts;
    public TextMeshProUGUI cinasTeksts;

    //Uzvarētāju lauks
    public TextMeshProUGUI uzvaretajuTeksts;
    public TextMeshProUGUI uzvaretajuApraksts;

    public TextMeshProUGUI mobilizacijasTeksts;

    //Datu bāzei
    public string Valstis;

    public TextMeshProUGUI valstsNos;
    public TextMeshProUGUI segVardsNos;

    public TextMeshProUGUI laiksNos;



     void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
       if (PlayerPrefs.HasKey("saglabasanasLaiks")){
          loadTeksts.text = "Iepriekšējais progress: " + PlayerPrefs.GetString("saglabasanasLaiks");
          saveTeksts.text = "Saglabāts progress: "+ PlayerPrefs.GetString("saglabasanasLaiks");
        }   
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
                objekti.vesturiskaAprakstaSkaits[1]++;
                if( objekti.vesturiskaAprakstaSkaits[1] == 1){
                    mobilizacijasTeksts.text = "Jūs zaudat 1 mobilizācijas iespēju";
                    objekti.rotuSkaitsPlayer--;
                }
            }
            if( objekti.noklikBlakusState == GameObject.Find("States_3")){
  vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Pāvilostas un Aizputes pusē norisinājās vairāki notikumi Brīvības cīņu laikā
<b>Kaujas:</b> Šajā reģionā notika vairākas kaujas starp Latvijas armiju un vācu spēkiem (Landesvēru un Dzelzsdivīziju).
 Piemēram, 1919. gada jūnijā notika Aizpute skauja, kurā Latvijas armija guva uzvaru.
<b>Bēgļu gaitas:</b> Pāvilostas un Aizputes apkārtnē bēgļu gaitās devās daudzi cilvēki, bēgot no kara darbības.
Pāvilostas ostas ieņemšana: 1919. gada janvārī lielinieki ieņēma Pāvilostas ostu, bet vēlāk to atguva Latvijas armija.
Lai gan šajā reģionā nenotika tik lielas kaujas kā citviet Latvijā, tomēr arī šeit cilvēki piedzīvoja kara grūtības un deva savu ieguldījumu Latvijas brīvības iegūšanā."; 
                    mobilizacijasTeksts.text = "Jūs iegūstat 1 mobilizācijas iespēju";
                objekti.vesturiskaAprakstaSkaits[2]++;
                if( objekti.vesturiskaAprakstaSkaits[2] == 1){
                   objekti.rotuSkaitsPlayer++;
                }
            }
            if( objekti.noklikBlakusState == GameObject.Find("States_20")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style><br>
Lai gan Siguldas novads nebija lielu kauju centrs Brīvības cīņu laikā, tomēr arī šajā apvidū risinājās notikumi, kas ietekmēja kara gaitu un Latvijas valsts veidošanos:
<b>Inčukalna kauja:</b> 1919. gada 10. novembrī Inčukalnā notika viena no izšķirošajām Brīvības cīņu kaujām, kurā Latvijas armija sakāva Bermonta armiju. Šī uzvara pavēra ceļu Rīgas atbrīvošanai un bija nozīmīgs pagrieziena punkts Latvijas neatkarības iegūšanā.
<b>Bēgļu plūsmas:</b> Siguldas novads, tāpat kā daudzas citas vietas Latvijā, uzņēma bēgļus, kas bija spiesti pamest savas mājas kara dēļ. Tas radīja papildu slogu vietējiem iedzīvotājiem, bet arī veicināja savstarpēju atbalstu un solidaritāti.
<b>Mobilizācija un cīņas:</b> Arī Siguldas novada iedzīvotāji tika mobilizēti Latvijas armijā un piedalījās Brīvības cīņās. Lai gan lielākās kaujas nenotika tieši Siguldā, vietējo vīru devums bija nozīmīgs Latvijas neatkarības iegūšanā.
Lai gan Siguldas novadā nav tik daudz piemiņas vietu par Brīvības cīņām kā citās Latvijas vietās, tomēr arī šeit var atrast liecības par šo laiku. Piemēram, Inčukalna kapos ir piemineklis Brīvības cīņās kritušajiem karavīriem.";
          }

        }

    }

}
