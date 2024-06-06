using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;
    public Pogas pogas;

    public void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
        pogas = FindAnyObjectByType<Pogas>();
    }
    
public void noKuraStateLSPRGajiens() 
{
    //Atiestata iepriekš izvēlēto LSPR teritoriju.
    objekti.noKuraStateLSPR = null;
    objekti.noKuraStateLSPRKontrole = null;
    //Atrodam visus spēles objektus ar tagu "Valsts".
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
    SpelesKontrole bestStateController = null; 
    float tuvakaDistance = float.MaxValue;  // Sākotnēji tuvākā iespējamā distance
         //Atrodam spēlētāja teritoriju, kurā ir vismaz viena rota
        SpelesKontrole lietotajaState = null;
        foreach (GameObject stateObject in visiStates)
        {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();   
        // Pārbauda, vai teritorija pieder spēlētājam un vai tajā ir rotas  
        if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.PLAYER 
        && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] > 0){
            lietotajaState = stateObject.GetComponent<SpelesKontrole>();
             objekti.lietotajuState = stateObject;  // Saglabā atrasto spēlētāja teritoriju     

        }
        }
        lietotajaState = objekti.lietotajuState.GetComponent<SpelesKontrole>();
         //Iziet caur visām teritorijām, lai atrastu tuvāko LSPR teritoriju
    foreach (GameObject stateObject in visiStates)
    {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            //Pārbauda, vai teritorija pieder LSPR un vai tajā ir rotas
        if (stateController != null && 
            stateController.valsts.speletajs == Valstis.Speletaji.LSPR && 
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] > 0)
        {
            //Aprēķina attālumu starp spēlētāja teritoriju un LSPR teritoriju
            float distance = Vector2.Distance(objekti.lietotajuState.transform.position,
             stateObject.transform.position);
            //Pārbauda, vai šī teritorija ir tuvāk nekā iepriekš atrastā
            if (distance < tuvakaDistance)
            {
                tuvakaDistance = distance;
                bestStateController = stateController;
            }
        }
    }
    //Saglabā atrasto tuvāko LSPR teritoriju
    if (bestStateController != null)
    {
        objekti.noKuraStateLSPRKontrole = bestStateController;
        objekti.noKuraStateLSPR = bestStateController.gameObject;
    }

    Debug.Log("AI izvēlās: " + (objekti.noKuraStateLSPR != null ? objekti.noKuraStateLSPR.name : "null"));
}


public void noKuraStateLSPRVelreiz(GameObject lietotajuState) 
{
    //Atrodam visus spēles objektus ar tagu "Valsts".
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
     
    SpelesKontrole bestStateController = null;
    float tuvakaDistance = float.MaxValue; 
    int lielakaisRotasSkaits = int.MinValue; 

     //Iziet cauri visām teritorijām.
    foreach (GameObject stateObject in visiStates)
    {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        
        // Pārbauda, vai teritorija pieder LSPR, satur rotas un tajā ir mazāk par 5 rotām
        if (stateController != null &&
            stateController.valsts.speletajs == Valstis.Speletaji.LSPR &&
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] > 0 &&
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] < 5) 
        {
            //Aprēķina attālumu starp spēlētāja teritoriju un LSPR teritoriju
            float distance = Vector2.Distance(lietotajuState.transform.position, stateObject.transform.position);
            //Iegūst rotu skaitu šajā teritorijā
            int rotasSkaits = stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
            // Pārbauda, vai šī teritorija ir tuvāk, vai arī vienādā attālumā, bet ar vairāk rotām
            if (distance < tuvakaDistance || (distance == tuvakaDistance && rotasSkaits > lielakaisRotasSkaits)) 
            {
                //Ja jā, atjaunina mainīgos
                tuvakaDistance = distance;
                lielakaisRotasSkaits = rotasSkaits;
                bestStateController = stateController;
            }
        }
    }

    //Saglabā atrasto labāko teritoriju
    if (bestStateController != null)
    {
        objekti.noKuraStateLSPRKontrole = bestStateController;
        objekti.noKuraStateLSPR = bestStateController.gameObject;
    }
}

public void LSPRUzbrukums()
{
    // Atrodam LSPR teritoriju, no kuras sākt uzbrukumu.
    noKuraStateLSPRGajiens();
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
    // Iegūstam teritorijas robežas, lai aprēķinātu nobīdi.
    Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
    float territoryWidth = territoryBounds.size.x;
    float territoryHeight = territoryBounds.size.y;
    Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);

    //Ja atrasta LSPR teritorija, no kuras sākt uzbrukumu.
    if (objekti.noKuraStateLSPRKontrole != null) 
    {
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            //Aprēķina attālumu starp LSPR teritoriju un pašreizējo teritoriju.
            float distance = Vector2.Distance(objekti.noKuraStateLSPRKontrole.transform.position, 
                                               stateObject.transform.position - (Vector3)offset);
            //Pārbauda, vai teritorija pieder spēlētājam, ir pietiekami tuvu un tajā ir mazāk rotu.
            if (stateController != null && 
                stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && 
                distance < 1.96f &&
                stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] < 
                objekti.noKuraStateLSPRKontrole.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR])
            {
                //Saglabājam atrasto teritoriju kā uzbrukuma mērķi.
                objekti.uzbruksanasStateKontrole = stateController;
                objekti.uzbruksanasState = stateObject;
                break;
            }
        }
    }
    // a neatrod piemērotu uzbrukuma mērķi, meklējam atkārtoti.
    if (objekti.uzbruksanasState == null)
    {
        // Ja nav atrasta LSPR teritorija, meklējam atkārtoti.
        if (objekti.noKuraStateLSPRKontrole == null)
        {
            noKuraStateLSPRGajiens();
        }
        else
        {
             // Ja ir atrasta LSPR teritorija, bet nebija piemērota mērķa, meklējam atkārtoti, ņemot vērā arī rotu skaitu.
            noKuraStateLSPRVelreiz(objekti.lietotajuState); 
        }

        Debug.Log("AI šoreiz tad izvēlās: " + objekti.noKuraStateLSPR);
        // Ja joprojām nav atrasts uzbrukuma mērķis, tad izsauc mobilizāciju.
        if (objekti.uzbruksanasState == null)
        {
            AIMobilize(); 
        }
    }

    Debug.Log("AI uzbruks uz: " + (objekti.uzbruksanasState != null ? objekti.uzbruksanasState.name : "nevienu"));
}

    public void AIUzbruk(){
        bool irUzbrucis = false;
        Debug.Log("AI izdomā uzbrukt lietotājam");
        int skaits = 0;
        // Izsauc funkciju, lai noteiktu, no kuras teritorijas LSPR veiks uzbrukumu.
        LSPRUzbrukums();

        //Ja ir atrasta teritorija, uz kuru uzbrukt.
        if(objekti.uzbruksanasState != null){
        //  Debug.Log("Notiek uzbrukšana uz lietotāju teritoriju");

        //Iegūst uzbrūkošo rotu skaitu.
        skaits = objekti.noKuraStateLSPRKontrole.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];

        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");

        //AI pārņem uzbruktās teritorijas kontroli.
        objekti.uzbruksanasState.GetComponent<SpelesKontrole>().valsts.speletajs = Valstis.Speletaji.LSPR;

        //Lietotājs atkāpšanās funkcija.
         kontrole.rotuAtkapsanasPlayer(Valstis.Speletaji.PLAYER, skaits);

          //Atrodam uzbruktās teritorijas rotu pozīcijas.

            for(int i=0; i<visiStates.Length; i++){
            if(objekti.uzbruksanasState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            //Izvieto LSPR rotas uzbruktās teritorijā.
            foreach (GameObject stateObject in visiStates){
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject == objekti.uzbruksanasState)
                {
                    stateController.tintesKrasa(new Color32(243, 43, 43, 235));
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (skaits > 0){ 
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position,
                         Quaternion.identity, objekti.uzbruksanasState.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.LSPR, 1);
                        skaits--;
                    }
                     //Atzīmē, ka uzbrukums ir noticis.
                    irUzbrucis = true;
                    }
                }
            }
             //Ja uzbrukums notika, pārvieto rotas no sākotnējās LSPR teritorijas.
                if(irUzbrucis == true){
                int uzbrukusoSkaits = 0;
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.noKuraStateLSPR == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
                 //Pārvietotās LSPR rotas tiks izdzēstas
                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                   SpelesKontrole stateController1 = objekti.uzbruksanasState.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject.Equals(objekti.noKuraStateLSPR))
                {
                    uzbrukusoSkaits = stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    Debug.Log("AI uzbrūkošo skaits: "+uzbrukusoSkaits);
                      for(int i=0; i<stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]; i++){
                        if (stateObject == objekti.noKuraStateLSPR){ 
                         foreach (Transform child in objekti.noKuraStateLSPR.transform){
                         if (child.CompareTag("LSPRTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            irUzbrucis = false;
                            //Debug.Log("AI rotas pārvietojas");
                          }
                        }
                        objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                        stateController.NonemtRotas(Valstis.Speletaji.LSPR, 1);

                    }
                }
                }
            }
        }
        }
        // Pārbauda, vai AI ir uzvarējis.
        pogas.uzvaretajaParbaudeAI();
        objekti.lietotajuKarta = true;
    }
    private List<GameObject> prioritatesState(GameObject[] visiStates)
    {
         // Izveido sarakstu, kurā glabās draudētās teritorijas.
        List<GameObject> draudetasTeritorijas = new List<GameObject>();
        // Pārbauda katru teritoriju
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR)
            {
                // Pārbauda, vai teritorija ir "tukša" (blakus ir pretinieka teritorija ar vairāk karavīriem).
                if (vaiStateTukssDraudam(stateObject))
                {
                     //Ja teritorija ir "tukša", pievieno to draudēto teritoriju sarakstam.
                    draudetasTeritorijas.Add(stateObject);
                }
            }
        }
        //Atgriež sarakstu ar draudētajām teritorijām
        return draudetasTeritorijas;
    }

    // Pārbauda, vai LSPR teritorija ir "tukša" (mazāk par 3 rotām).
    private bool vaiStateTukssDraudam(GameObject stateObject){
    SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
    return stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] < 3; 
    }

    public void AIMobilize(){

    if (objekti.rotuSkaitsLSPR == 0 && objekti.noKuraStateLSPR == null){
        objekti.rotuSkaitsLSPR = 1;
    }

    if(objekti.rotuSkaitsLSPR > 0 && !objekti.lietotajuKarta){
     //Atrodam teritoriju, no kuras mobilizēt spēkus.
    noKuraStateLSPRGajiens();
    Debug.Log("Izsauc mobilizāciju");
        // Nosaka nejaušu mobilizējamo rotu skaitu (1 līdz 2).
        int mobilizacijuSkaits = new System.Random().Next(1, Mathf.Min(objekti.rotuSkaitsLSPR + 1, 3));
        objekti.rotuSkaitsLSPR =  objekti.rotuSkaitsLSPR - mobilizacijuSkaits;
        Debug.Log("Mob skaits: "+mobilizacijuSkaits);
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
         // Iegūst sarakstu ar prioritārajām teritorijām (draudētās teritorijas).
        List<GameObject> prioritatesStates = prioritatesState(visiStates);

        // Ja nav draudētu teritoriju, izvēlas teritoriju pēc noklusējuma.
        if (prioritatesStates.Count == 0)
        {
            noKuraStateLSPRGajiens();
        }
        else
        {
             //Ja ir draudētas teritorijas, izvēlas pirmo no tām.
            objekti.noKuraStateLSPR = prioritatesStates[0]; 
            objekti.noKuraStateLSPRKontrole = objekti.noKuraStateLSPR.GetComponent<SpelesKontrole>();
        }   
             //Atrodam izvēlētās teritorijas rotu pozīcijas.
            for(int i=0; i<visiStates.Length; i++){
                if(objekti.noKuraStateLSPR == GameObject.Find("States_"+i)){
                    objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            //Pievieno rotas izvēlētajā teritorijā.
            foreach (GameObject stateObject in visiStates){
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR 
                && stateObject == objekti.noKuraStateLSPR)
                {

                 for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (mobilizacijuSkaits > 0){ 
                        //Pārbauda, vai pozīcija jau nav aizņemta.
                        bool pozicijaAiznemta = false;
                      foreach (Transform child in objekti.noKuraStateLSPR.transform) {
                        if (child.transform.position == objekti.rotasPozicijas[i].transform.position) {
                              pozicijaAiznemta = true;
                              break;
                          }
                    }
                    //Ja pozīcija nav aizņemta un teritorijā nav maksimālais rotu skaits, pievieno rotu.
                    if (!pozicijaAiznemta && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]!=5) {
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, 
                        Quaternion.identity, objekti.noKuraStateLSPR.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.LSPR, 1);
                        mobilizacijuSkaits--;
                        Debug.Log("AI pievieno rotas!");
                    }
                 }

            }
                }
            }
            //Atjauno spēlētāja kārtu.
            objekti.lietotajuKarta = true;
            }else{
                // Ja nav iespējams mobilizēt, izsauc AI atkāpšanās funkciju.
                AIAtkapjas();
            }
        }

    public void AIAtkapjas(){
        Debug.Log("AI atkapjas");
        if(!objekti.lietotajuKarta){
        //Atrod teritoriju, no kuras atkāpties.    
        noKuraStateLSPRGajiens();   
        //Izsauc funkciju, kas veic atkāpšanos.
        kontrole.AIAtkapsanas(objekti.noKuraStateLSPR);
        }
        objekti.lietotajuKarta = true;
    }

    public void AIKustiba(){
        //Atiestata mainīgos, kas glabā informāciju par iepriekšējiem gājieniem.
        objekti.noKuraStateLSPR = null;
        objekti.noKuraStateLSPRKontrole = null;
        objekti.uzbruksanasState = null;
        objekti.uzbruksanasStateKontrole = null;
        if (!objekti.lietotajuKarta)
        {
            // Prioritāte ir uzbrukšana.
            LSPRUzbrukums();

            //Ja ir atrasts uzbrukuma mērķis, veic uzbrukumu.
            if (objekti.uzbruksanasState != null)
            {
                AIUzbruk();
            }
            else
            {
                // ja nav iespējams uzbrukt, mobilizē.
                if (objekti.rotuSkaitsLSPR > 0)
                {
                    AIMobilize();
                }
                else
                {
                    // Ja nav mobilizācija iespējama, atkāpjas.
                    AIAtkapjas();
                }
            }
        }
    }

}
