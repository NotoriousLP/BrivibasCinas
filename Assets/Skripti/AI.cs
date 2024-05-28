using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;

    public void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
    }
    
public void noKuraStateLSPRGajiens() 
{
    objekti.noKuraStateLSPR = null;
    objekti.noKuraStateLSPRKontrole = null;
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
    SpelesKontrole bestStateController = null; 
    float tuvakaDistance = float.MaxValue;     
        
        SpelesKontrole lietotajaState = null;
        foreach (GameObject stateObject in visiStates)
        {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();     
        if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] > 0){
            lietotajaState = stateObject.GetComponent<SpelesKontrole>();
             objekti.lietotajuState = stateObject;        

        }
        }
        lietotajaState = objekti.lietotajuState.GetComponent<SpelesKontrole>();
    foreach (GameObject stateObject in visiStates)
    {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();

        if (stateController != null && 
            stateController.valsts.speletajs == Valstis.Speletaji.LSPR && 
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] > 0)
        {
            float distance = Vector2.Distance(objekti.lietotajuState.transform.position, stateObject.transform.position);

            if (distance < tuvakaDistance)
            {
                tuvakaDistance = distance;
                bestStateController = stateController;
            }
        }
    }

    if (bestStateController != null)
    {
        objekti.noKuraStateLSPRKontrole = bestStateController;
        objekti.noKuraStateLSPR = bestStateController.gameObject;
    }

    Debug.Log("AI izvēlās: " + (objekti.noKuraStateLSPR != null ? objekti.noKuraStateLSPR.name : "null"));
}


public void noKuraStateLSPRVelreiz(GameObject lietotajuState) 
{
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
    
    
    SpelesKontrole bestStateController = null;
    float tuvakaDistance = float.MaxValue; 
    int lielakaisRotasSkaits = int.MinValue; 

    // Iterate through all states
    foreach (GameObject stateObject in visiStates)
    {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        

        if (stateController != null &&
            stateController.valsts.speletajs == Valstis.Speletaji.LSPR &&
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] > 0 &&
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] < 5) 
        {

            float distance = Vector2.Distance(lietotajuState.transform.position, stateObject.transform.position);
            int unitCount = stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];

            if (distance < tuvakaDistance || (distance == tuvakaDistance && unitCount > lielakaisRotasSkaits)) 
            {
                tuvakaDistance = distance;
                lielakaisRotasSkaits = unitCount;
                bestStateController = stateController;
            }
        }
    }

    // Assign the chosen state
    if (bestStateController != null)
    {
        objekti.noKuraStateLSPRKontrole = bestStateController;
        objekti.noKuraStateLSPR = bestStateController.gameObject;
    }
}


public void LSPRUzbrukums()
{
    noKuraStateLSPRGajiens();
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
    Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
    float territoryWidth = territoryBounds.size.x;
    float territoryHeight = territoryBounds.size.y;
    Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);

    if (objekti.noKuraStateLSPRKontrole != null) 
    {
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            float distance = Vector2.Distance(objekti.noKuraStateLSPRKontrole.transform.position, 
                                               stateObject.transform.position - (Vector3)offset);

            if (stateController != null && 
                stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && 
                distance < 1.96f &&
                stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] < objekti.noKuraStateLSPRKontrole.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR])
            {
                objekti.uzbruksanasStateKontrole = stateController;
                objekti.uzbruksanasState = stateObject;
                break;
            }
        }
    }

    if (objekti.uzbruksanasState == null)
    {
        if (objekti.noKuraStateLSPRKontrole == null)
        {
            noKuraStateLSPRGajiens();
        }
        else
        {
            noKuraStateLSPRVelreiz(objekti.lietotajuState); 
        }

        Debug.Log("AI šoreiz tad izvēlās: " + objekti.noKuraStateLSPR);

        // If still no valid state, mobilize
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
        LSPRUzbrukums();
        if(objekti.uzbruksanasState != null){
        Debug.Log("Notiek uzbrukšana uz lietotāju teritoriju");
        skaits = objekti.noKuraStateLSPRKontrole.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
        Debug.Log("AI skaits: "+skaits);
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        objekti.uzbruksanasState.GetComponent<SpelesKontrole>().valsts.speletajs = Valstis.Speletaji.LSPR;
         kontrole.rotuAtkapsanasPlayer(Valstis.Speletaji.PLAYER, skaits);
            for(int i=0; i<visiStates.Length; i++){
            if(objekti.uzbruksanasState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            foreach (GameObject stateObject in visiStates){
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject == objekti.uzbruksanasState)
                {
                    stateController.tintesKrasa(new Color32(243, 43, 43, 235));
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (skaits > 0){ 
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.uzbruksanasState.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.LSPR, 1);
                        skaits--;
                    }
                    irUzbrucis = true;
                    }
                }
            }
                if(irUzbrucis == true){
                int uzbrukusoSkaits = 0;
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.noKuraStateLSPR == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
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
        objekti.lietotajuKarta = true;
    }
    private List<GameObject> prioritatesState(GameObject[] visiStates)
    {
        List<GameObject> draudetasTeritorijas = new List<GameObject>();
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR)
            {
                if (vaiStateTukssDraudam(stateObject))
                {
                    draudetasTeritorijas.Add(stateObject);
                }
            }
        }
        return draudetasTeritorijas;
    }

    private bool vaiStateTukssDraudam(GameObject stateObject){
    SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
    return stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] < 3; 
}
    public void AIMobilize(){
    if(objekti.rotuSkaitsLSPR > 0 && !objekti.lietotajuKarta){
    noKuraStateLSPRGajiens();
    Debug.Log("Izsauc mobilizāciju");
        int mobilizacijuSkaits = new System.Random().Next(1, Mathf.Min(objekti.rotuSkaitsLSPR + 1, 3));
        objekti.rotuSkaitsLSPR =  objekti.rotuSkaitsLSPR - mobilizacijuSkaits;
        Debug.Log("Mob skaits: "+mobilizacijuSkaits);
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        List<GameObject> threatenedStates = prioritatesState(visiStates);

        if (threatenedStates.Count == 0)
        {
            noKuraStateLSPRGajiens();
        }
        else
        {
            objekti.noKuraStateLSPR = threatenedStates[0]; 
            objekti.noKuraStateLSPRKontrole = objekti.noKuraStateLSPR.GetComponent<SpelesKontrole>();
        }   
            for(int i=0; i<visiStates.Length; i++){
                if(objekti.noKuraStateLSPR == GameObject.Find("States_"+i)){
                    objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            foreach (GameObject stateObject in visiStates){
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject == objekti.noKuraStateLSPR)
                {
                 for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (mobilizacijuSkaits > 0){ 
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noKuraStateLSPR.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.LSPR, 1);
                        mobilizacijuSkaits--;
                        Debug.Log("AI pievieno rotas!");
                    }

            }
                }
            }
    objekti.lietotajuKarta = true;
    }else{
        AIAtkapjas();
    }
    }
    public void AIAtkapjas(){
        Debug.Log("AI atkapjas");
        noKuraStateLSPRGajiens();
        if(!objekti.lietotajuKarta){
        kontrole.AIAtkapsanas(objekti.noKuraStateLSPR);
        }
        objekti.lietotajuKarta = true;
    }

    public void AIKustiba(){
        objekti.noKuraStateLSPR = null;
        objekti.noKuraStateLSPRKontrole = null;
        objekti.uzbruksanasState = null;
        objekti.uzbruksanasStateKontrole = null;
        if (!objekti.lietotajuKarta)
        {
            // Prioritāte ir uzbrukšana
            LSPRUzbrukums();

            if (objekti.uzbruksanasState != null)
            {
                AIUzbruk();
            }
            else
            {
                // ja nav iespējams uzbrukt, atkāpjas
                if (objekti.rotuSkaitsLSPR > 0)
                {
                    AIMobilize();
                }
                else
                {
                    // Ja nav mobilizācija iespējama, atkāpjas
                    AIAtkapjas();
                }
            }
        }
    }

}
