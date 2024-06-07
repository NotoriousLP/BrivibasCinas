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

    public TextMeshProUGUI rotasTeksts;
    public TextMeshProUGUI infoTeksts;
    //Datu bāzei
    public string Valstis;

    public TextMeshProUGUI valstsNos;
    public TextMeshProUGUI segVardsNos;

    public TextMeshProUGUI laiksNos;

    public string spelesVeids;


     void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
       if (PlayerPrefs.HasKey("saglabasanasLaiks")){
            loadTeksts.text = "Iepriekšējais progress: " + PlayerPrefs.GetString("saglabasanasLaiks") + " Veids: "+PlayerPrefs.GetString("pogasIzvele"); //Dabū iepriekšējas saglabāšanas nosaukums, ja tāda ir
            saveTeksts.text = "Saglabāts progress: "+ PlayerPrefs.GetString("saglabasanasLaiks") + " Veids: "+PlayerPrefs.GetString("pogasIzvele"); //Dabū iepriekšējas saglabāšanas nosaukums, ja tāda ir
        }   
    }
    void Update()
    {
        if(objekti.lietotajuKarta == true){
        rotuTeksts.text = objekti.rotuSkaitsPlayer.ToString(); //Mobilizācijas iespējas, kas tiek parādītas augšā
        }else if(objekti.otraSpeletajaKarta == true){
        rotuTeksts.text = objekti.rotuSkaitsLSPR.ToString(); //Mobilizācijas iespējas, kas tiek parādītas augšā
        }

        skaitaLauks.text = objekti.rotuSkaitsIzv.ToString();

    }


    
    public void vesturisksNotikums(){ //Funckija, kas pārbauda vēsturisku notikumu.
        if(objekti.lietotajuKarta){   
            if (objekti.noklikBlakusState == GameObject.Find("States_5")){
                vesturisksApraksts.text = "<style=H1>Vēsturisks fakts</style><br><#555>3. martā sākās plašs uzbrukums Sarkanajai Armijai ar mērķi sasniegt Lielupi. Latviešu bataljonam bija jāveic uzbrukums Saldus virzienā. 6. martā negaidītā savstarpējā latviešu un vāciešu apšaudē gāja bojā Atsevišķā latviešu bataljona komandieris <b>Oskars Kalpaks</b>, Atsevišķās Studentu rotas komandieris kapteinis <b>Nikolajs Grundmanis</b>, jātnieku nodaļas virsleitnants <b>Pēteris Krievs</b> un piekomandētās artilērijas pusbaterijas leitnants <b>Šrinders</b>; arī vācu puse cieta zaudējumus. <br Pēc Kalpaka nāves bataljona komandēšana tika uzticēta kapteinim <b>Jānim Balodim</b>.";
                
                if( objekti.vesturiskaAprakstaSkaits[1] == false){
                    mobilizacijasTeksts.text = "Jūs zaudat 1 mobilizācijas iespēju";
                    objekti.rotuSkaitsPlayer--;
                    objekti.vesturiskaAprakstaSkaits[1] = true;
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
                if( objekti.vesturiskaAprakstaSkaits[2] == false){
                mobilizacijasTeksts.text = "Jūs iegūstat 1 mobilizācijas iespēju";
                   objekti.rotuSkaitsPlayer++;
                   objekti.vesturiskaAprakstaSkaits[2] = true;
                }
            }
            if( objekti.noklikBlakusState == GameObject.Find("States_20")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Lai gan Siguldas novads nebija lielu kauju centrs Brīvības cīņu laikā, tomēr arī šajā apvidū risinājās notikumi, kas ietekmēja kara gaitu un Latvijas valsts veidošanos:
<b>Inčukalna kauja:</b> 1919. gada 10. novembrī Inčukalnā notika viena no izšķirošajām Brīvības cīņu kaujām, kurā Latvijas armija sakāva Bermonta armiju. Šī uzvara pavēra ceļu Rīgas atbrīvošanai un bija nozīmīgs pagrieziena punkts Latvijas neatkarības iegūšanā.
<b>Bēgļu plūsmas:</b> Siguldas novads, tāpat kā daudzas citas vietas Latvijā, uzņēma bēgļus, kas bija spiesti pamest savas mājas kara dēļ. Tas radīja papildu slogu vietējiem iedzīvotājiem, bet arī veicināja savstarpēju atbalstu un solidaritāti.
<b>Mobilizācija un cīņas:</b> Arī Siguldas novada iedzīvotāji tika mobilizēti Latvijas armijā un piedalījās Brīvības cīņās. Lai gan lielākās kaujas nenotika tieši Siguldā, vietējo vīru devums bija nozīmīgs Latvijas neatkarības iegūšanā.
Lai gan Siguldas novadā nav tik daudz piemiņas vietu par Brīvības cīņām kā citās Latvijas vietās, tomēr arī šeit var atrast liecības par šo laiku. Piemēram, Inčukalna kapos ir piemineklis Brīvības cīņās kritušajiem karavīriem.";
                    if( objekti.vesturiskaAprakstaSkaits[3] == false){
                mobilizacijasTeksts.text = "Jūs iegūstat 1 mobilizācijas iespēju";
                   objekti.rotuSkaitsPlayer++;
                   objekti.vesturiskaAprakstaSkaits[3] = true;
                }
          }
            if( objekti.noklikBlakusState == GameObject.Find("States_6")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
<b>Ventspils ieņemšana (1919. gada janvāris):</b> Lielinieku spēki ieņēma Ventspili, kas uz laiku kļuva par padomju varas centru Kurzemē.
<b>Ventspils atbrīvošana (1919. gada maijs):</b> Apvienotais latviešu un igauņu karaspēks atbrīvoja Ventspili no lieliniekiem. Šī uzvara bija svarīgs solis Latvijas armijas tālākajā virzībā uz Kurzemes atbrīvošanu.
<b>Kaujas Ventspils apkārtnē:</b> Pēc Ventspils atbrīvošanas reģionā notika vairākas sadursmes starp Latvijas armiju un vācu spēkiem (Landesvēru un Dzelzsdivīziju), kas atbalstīja Bermonta armiju.
<b>Bēgļu gaitas:</b> Līdzīgi kā citviet Latvijā, arī Ventspils apkārtnē kara darbība izraisīja bēgļu plūsmas. Daudzi cilvēki bija spiesti pamest savas mājas, meklējot drošību citur.
<b>Mobilizācija:</b> Ventspils pusē, tāpat kā visā Latvijā, notika mobilizācija. Vietējie vīrieši tika iesaukti Latvijas armijā, lai piedalītos cīņās par valsts neatkarību.
Lai gan Ventspilī nenotika tik vērienīgas kaujas kā, piemēram, Cēsu kaujas, tomēr arī šī pilsēta un tās apkārtne bija nozīmīga Brīvības cīņu norises vieta. Ventspils ieņemšana un atbrīvošana bija svarīgi pagrieziena punkti Kurzemes atbrīvošanas procesā.";
                    if( objekti.vesturiskaAprakstaSkaits[4] == false){
                mobilizacijasTeksts.text = "Jūs iegūstat 1 mobilizācijas iespēju";
                   objekti.rotuSkaitsPlayer++;
                   objekti.vesturiskaAprakstaSkaits[4] = true;
                }
          }
            if( objekti.noklikBlakusState == GameObject.Find("States_11")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Lai gan Jelgavas novads nebija lielu kauju centrs Brīvības cīņu laikā, tomēr arī šajā apvidū risinājās notikumi, kas ietekmēja kara gaitu un Latvijas valsts veidošanos:
<b>Pagaidu galvaspilsēta:</b> Pēc tam, kad 1919. gada janvārī lielinieki ieņēma Rīgu, Jelgava uz laiku kļuva par Latvijas Republikas pagaidu galvaspilsētu, kļūstot par svarīgu politisko un administratīvo centru.
<b>Jelgavas atbrīvošanas kaujas:</b> 1919. gada novembrī Jelgavas apkārtnē notika sīvas kaujas starp Latvijas armiju un Rietumkrievijas Brīvprātīgo armiju (Bermonta armiju). Šīs kaujas bija izšķirošas Latvijas brīvības iegūšanā, un 21. novembrī Jelgava tika atbrīvota.
<b>Bēgļu plūsmas un mobilizācija:</b> Līdzīgi kā citviet Latvijā, arī Jelgavas novadu skāra kara radītās bēgļu plūsmas. Vietējie iedzīvotāji sniedza atbalstu bēgļiem, kā arī aktīvi iesaistījās Latvijas armijā, piedaloties cīņās par valsts neatkarību.
Lai gan Jelgavas novadā nav tik daudz piemiņas vietu par Brīvības cīņām kā citās Latvijas vietās, tomēr arī šeit var atrast liecības par šo laiku. Piemēram, Jelgavas Brāļu kapos atrodas piemineklis Brīvības cīņās kritušajiem karavīriem.";
                    if(objekti.vesturiskaAprakstaSkaits[5] == false){
                mobilizacijasTeksts.text = "Jūs iegūstat 1 mobilizācijas iespēju";
                   objekti.rotuSkaitsPlayer++;
                   objekti.vesturiskaAprakstaSkaits[5] = true;
                }
          }   
        if(objekti.noklikBlakusState == GameObject.Find("States_12")){
                 vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
<b>Rīga Brīvības cīņu laikā (1918-1920)</b> bija nozīmīgs centrs, kurā risinājās vairāki būtiski notikumi:
<b>1918. gads:</b> 
<b>18. novembris:</b> Latvijas Nacionālajā teātrī tika proklamēta neatkarīga Latvijas Republika.
<b>Decembris:</b> Sākās lielinieku uzbrukums Rīgai, un Latvijas Pagaidu valdība bija spiesta atkāpties uz Liepāju.
<b>1919. gads:</b> 
<b>3. janvāris:</b> Lielinieki ieņēma Rīgu, kas kļuva par Latvijas Sociālistiskās Padomju Republikas galvaspilsētu. Sākās represijas un terors pret iedzīvotājiem.
<b>22. maijs:</b> Apvienotais Baltijas landesvēra un vācu Dzelzsdivīzijas spēks ieņēma Rīgu, padzenot lieliniekus. Tika atjaunota Latvijas Pagaidu valdība.
<b>3. jūlijs</b>: Pēc konflikta ar Latvijas Pagaidu valdību landesvērs un Dzelzsdivīzija atstāja Rīgu.
<b>Oktobris-novembris:</b> Bermontiāde - Rietumkrievijas Brīvprātīgo armijas uzbrukums Rīgai. Latvijas armija sīvās cīņās, īpaši Pārdaugavā, apturēja Bermonta karaspēku.
<b>11. novembris:</b> Latvijas armija guva izšķirošu uzvaru pār Bermonta spēkiem, atbrīvojot Pārdaugavu. Šis datums tiek atzīmēts kā Lāčplēša diena.
<b>1920. gads:</b>
<b>1. maijs:</b> Rīgā svinīgi atklāja Satversmes sapulci, kas vēlāk izstrādāja un pieņēma Latvijas Republikas Satversmi.
Rīga Brīvības cīņu laikā piedzīvoja gan okupācijas, gan atbrīvošanas, gan politisku lēmumu pieņemšanas brīžus. Šie notikumi veidoja pamatu Latvijas valsts tālākajai attīstībai un nostiprināja tās neatkarību.";
                    if( objekti.vesturiskaAprakstaSkaits[6] == false){
                mobilizacijasTeksts.text = "Jūs iegūstat 1 mobilizācijas iespēju";
                   objekti.rotuSkaitsPlayer++;
                   objekti.vesturiskaAprakstaSkaits[6] = true;
                }
          }   
        if(objekti.noklikBlakusState == GameObject.Find("States_32")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Lai gan Daugavpils novads nebija lielu kauju centrs Brīvības cīņu sākumā, tomēr 1919. gada janvārī pilsēta nonāca lielinieku kontrolē.
<b>Kaujas:</b> 1920. gada janvārī notika Daugavpils atbrīvošanas kaujas, kurās Latvijas armija ar Poļu armijas atbalstu guva uzvaru pār lieliniekiem. Šīs kaujas bija vienas no pēdējām lielajām Brīvības cīņu kaujām.
<b>Miera līgums:</b> 1920. gada 11. augustā tika parakstīts Latvijas-Krievijas miera līgums, kas oficiāli noslēdza Brīvības cīņas un atzina Latvijas neatkarību.
Lai gan Daugavpilī nenotika tik daudz sīvu kauju kā citviet Latvijā, tomēr šī pilsēta bija nozīmīga Brīvības cīņu norises vieta. Daugavpils atbrīvošana bija svarīgs pagrieziena punkts Latvijas valsts neatkarības nostiprināšanā.";
                    if( objekti.vesturiskaAprakstaSkaits[6] == false){
                mobilizacijasTeksts.text = "Jūs iegūstat 1 mobilizācijas iespēju";
                   objekti.rotuSkaitsPlayer++;
                   objekti.vesturiskaAprakstaSkaits[6] = true;
                }
          } 
          if(objekti.noklikBlakusState == GameObject.Find("States_10")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Lai gan Tukuma novads nebija lielu kauju centrs Brīvības cīņu laikā, tomēr arī šajā apvidū risinājās notikumi, kas ietekmēja kara gaitu un Latvijas valsts veidošanos:
<b>Kaujas:</b> Tukuma apkārtnē notika vairākas kaujas starp Latvijas armiju un vācu spēkiem (Landesvēru un Dzelzsdivīziju). Piemēram, 1919. gada jūnijā notika kauja pie Smārdes, kurā Latvijas armija guva uzvaru.
<b>Bēgļu plūsmas:</b> Tukuma apkārtnē bēgļu gaitās devās daudzi cilvēki, bēgot no kara darbības.
<b>Mobilizācija:</b> Vietējie iedzīvotāji tika mobilizēti Latvijas armijā, lai piedalītos cīņās par Latvijas neatkarību.
<b>Tukuma atbrīvošana:</b> 1919. gada augustā Tukumu atbrīvoja Latvijas armija.
Lai gan Tukumā nenotika tik vērienīgas kaujas kā, piemēram, Cēsu kaujas, tomēr arī šī pilsēta un tās apkārtne bija nozīmīga Brīvības cīņu norises vieta. Tukuma atbrīvošana bija svarīgs solis Latvijas armijas tālākajā virzībā uz Kurzemes atbrīvošanu.";
                    if( objekti.vesturiskaAprakstaSkaits[7] == false){
                    mobilizacijasTeksts.text = "";
                   objekti.vesturiskaAprakstaSkaits[7] = true;
                }
          }
                    if(objekti.noklikBlakusState == GameObject.Find("States_8")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Lai gan Talsu novads nebija lielu kauju centrs Brīvības cīņu laikā, tomēr arī šajā apvidū risinājās notikumi, kas ietekmēja kara gaitu un Latvijas valsts veidošanos:
<b>Kaujas:</b> Talsu apkārtnē notika vairākas kaujas starp Latvijas armiju un vācu spēkiem (Landesvēru un Dzelzsdivīziju). Piemēram, 1919. gada jūnijā notika kauja pie Lībagiem, kurā Latvijas armija guva uzvaru.
<b>Bēgļu plūsmas:</b> Talsu apkārtnē bēgļu gaitās devās daudzi cilvēki, bēgot no kara darbības.
<b>Mobilizācija:</b> Vietējie iedzīvotāji tika mobilizēti Latvijas armijā, lai piedalītos cīņās par Latvijas neatkarību.
<b>Talsu atbrīvošana:</b> 1919. gada jūnijā Talsus atbrīvoja Latvijas armija.
Lai gan šajā reģionā nenotika tik lielas kaujas kā citviet Latvijā, tomēr arī šeit cilvēki piedzīvoja kara grūtības un deva savu ieguldījumu Latvijas brīvības iegūšanā.";
                    if( objekti.vesturiskaAprakstaSkaits[7] == false){
                    mobilizacijasTeksts.text = "";
                   objekti.vesturiskaAprakstaSkaits[7] = true;
                }
          }
                if(objekti.noklikBlakusState == GameObject.Find("States_21")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Lai gan Limbažu novads nebija lielu kauju centrs Brīvības cīņu laikā, tomēr arī šajā apvidū risinājās notikumi, kas ietekmēja kara gaitu un Latvijas valsts veidošanos:
<b>Kaujas:</b> Limbažu apkārtnē notika vairākas kaujas starp Latvijas armiju un vācu spēkiem (Landesvēru un Dzelzsdivīziju). Piemēram, 1919. gada jūnijā notika kauja pie Limbažiem, kurā Latvijas armija guva uzvaru.
<b>Bēgļu plūsmas:</b> Limbažu apkārtnē bēgļu gaitās devās daudzi cilvēki, bēgot no kara darbības.
<b>Mobilizācija:</b> Vietējie iedzīvotāji tika mobilizēti Latvijas armijā, lai piedalītos cīņās par Latvijas neatkarību.
<b>Limbažu atbrīvošana:</b> 1919. gada jūnijā Limbažus atbrīvoja Latvijas armija.";
                    if( objekti.vesturiskaAprakstaSkaits[7] == false){
                    mobilizacijasTeksts.text = "";
                   objekti.vesturiskaAprakstaSkaits[7] = true;
                }
          }
            if(objekti.noklikBlakusState == GameObject.Find("States_22")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Valmieras pusē Brīvības cīņu laikā (1918-1920) norisinājās vairāki nozīmīgi notikumi:
<b>1. Valmieras kājnieku pulka formēšana (1919. gada marts):</b> Valmierā tika izveidots 1. Valmieras kājnieku pulks, kas aktīvi piedalījās cīņās par Latvijas neatkarību.
<b>Ziemeļlatvijas brigādes izveidošana (1919. gada marts):</b> Tartu tika nodibināta Ziemeļlatvijas brigāde, kuras sastāvā bija arī 1. Valmieras kājnieku pulks. Brigādes uzdevums bija atbrīvot Vidzemi un Latgali no lieliniekiem.
<b>Kaujas par Valku un Valmieru (1919. gada aprīlis-maijs):</b> Ziemeļlatvijas brigāde piedalījās sīvās kaujās par Valku un Valmieru, atbrīvojot šīs pilsētas no lieliniekiem.
<b>Cēsu kaujas (1919. gada jūnijs):</b> 1. Valmieras kājnieku pulks piedalījās arī izšķirošajās Cēsu kaujās, kurās Latvijas un Igaunijas apvienotais karaspēks sakāva vācu Landesvēru un Dzelzsdivīziju.
<b>Bermontiāde (1919. gada oktobris-novembris):</b> 1. Valmieras kājnieku pulks piedalījās arī cīņās pret Rietumkrievijas Brīvprātīgo armiju (Bermonta armiju).
Papildus šiem notikumiem Valmieras pusē notika arī citas sadursmes un militāras operācijas starp dažādiem spēkiem, kas cīnījās par kontroli pār šo reģionu.";
                    if( objekti.vesturiskaAprakstaSkaits[7] == false){
                    mobilizacijasTeksts.text = "";
                   objekti.vesturiskaAprakstaSkaits[7] = true;
                }
          }
            if(objekti.noklikBlakusState == GameObject.Find("States_27")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Abrene (tagadējā Pitalova) Brīvības cīņu laikā atradās sarežģītā situācijā, jo tā bija pierobežas pilsēta, kuru ietekmēja gan Latvijas, gan Krievijas politiskās un militārās aktivitātes.
<b>1920. gads:</b>
<b>1. februāris:</b> Tika noslēgts Latvijas-Krievijas miera līgums, kurā Abrene tika atzīta par Latvijas teritoriju.
<b>Apriņķa izveidošana:</b> Tika izveidots Abrenes apriņķis ar centru Abrenes pilsētā.
<b>1944. gads:</b>
<b>Padomju okupācija:</b> Sarkanā armija atkārtoti okupēja Abreni un iekļāva to Krievijas PFSR sastāvā.
<b>1945. gads:</b>
<b>Teritorijas nodošana:</b> Latvijas PSR Augstākās Padomes Prezidija lūgumā Abrene tika oficiāli nodota Krievijas PFSR.
Abrenes jautājums joprojām ir sensitīva tēma Latvijas vēsturē, jo tās nodošana Krievijai tika veikta, pārkāpjot starptautiskās tiesības un Latvijas Satversmi.
Lai gan Abrenē pašā Brīvības cīņu laikā nenotika lielas kaujas, tomēr tās liktenis bija cieši saistīts ar kara sekām un politiskajām norisēm.";
                    if( objekti.vesturiskaAprakstaSkaits[7] == false){
                    mobilizacijasTeksts.text = "";
                   objekti.vesturiskaAprakstaSkaits[7] = true;
                }
          }
            if(objekti.noklikBlakusState == GameObject.Find("States_29")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Lai gan Rēzeknes novads nebija lielu kauju centrs Brīvības cīņu sākumā, tomēr tā ģeogrāfiskā atrašanās vieta un politiskā situācija padarīja to par nozīmīgu punktu šajā vēsturiskajā periodā:
<b>Latgales Pagaidu zemes padome:</b> 1918. gada decembrī Rēzeknē tika nodibināta Latgales Pagaidu zemes padome, kas atbalstīja Latvijas Republikas ideju un centās apvienot latgaliešu centienus neatkarīgas Latvijas valsts izveidē.
<b>Lielinieku ieņemšana un atbrīvošana:</b> 1919. gada janvārī Sarkanā armija ieņēma Rēzekni, taču 1920. gada janvārī pilsētu atbrīvoja Latvijas un Polijas armijas kopīgiem spēkiem. Šīs kaujas bija vienas no pēdējām lielajām Brīvības cīņu kaujām.
<b>Miera līgums un Latgales atbrīvošana:</b> 1920. gada augustā parakstītais Latvijas-Krievijas miera līgums noslēdza Brīvības cīņas un noteica Latvijas robežas, iekļaujot arī Latgali.
Rēzeknes atbrīvošana bija nozīmīgs solis ceļā uz Latvijas valstiskās neatkarības nostiprināšanu.";
                    if( objekti.vesturiskaAprakstaSkaits[7] == false){
                    mobilizacijasTeksts.text = "";
                   objekti.vesturiskaAprakstaSkaits[7] = true;
                }
          }
            if(objekti.noklikBlakusState == GameObject.Find("States_24")){
                vesturisksApraksts.text = @"<#555><style=H1>Vēsturisks fakts</style>
Alūksnes pusē Brīvības cīņu laikā (1918-1920) norisinājās vairāki svarīgi notikumi, kas atspoguļoja šī reģiona ģeopolitisko stāvokli un ietekmi uz Latvijas valsts tapšanu:
<b>1919. gada janvāris:</b> Sarkanā armija ieņēma Alūksni, nostiprinot padomju varu.
<b>1919. gada maijs:</b> Alūksne tika atbrīvota no lieliniekiem, pateicoties Igaunijas armijas darbībai. Šī uzvara bija daļa no plašākas Ziemeļlatvijas atbrīvošanas kampaņas.
<b>1919. gada oktobris:</b> Alūksnē tika izveidota Ziemeļlatvijas brigāde, kas vēlāk piedalījās cīņās pret Bermonta armiju.
<b>1920. gada janvāris:</b> Pēc Daugavpils atbrīvošanas Latvijas armija turpināja virzību uz Alūksni, taču pilsēta jau bija atbrīvota.
Lai gan Alūksnē pašā nenotika lielas kaujas, tā tomēr bija nozīmīgs stratēģisks punkts Brīvības cīņu laikā, jo atradās Latvijas un Igaunijas pierobežā un kontrolēja svarīgus ceļu tīklus. Pilsētas ieņemšana un atbrīvošana ietekmēja kara gaitu Ziemeļlatvijā un veicināja Latvijas armijas tālāko virzību uz Latgales atbrīvošanu.";
                    if( objekti.vesturiskaAprakstaSkaits[7] == false){
                    mobilizacijasTeksts.text = "";
                   objekti.vesturiskaAprakstaSkaits[7] = true;
                }
          }else{
            mobilizacijasTeksts.text= "";
          }

        }

    }

}
