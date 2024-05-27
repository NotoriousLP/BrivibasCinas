using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;
    bool mobilizet = false;
    int mobilizacijuSkaitsLSPR = 0;
    bool uzbrukt = false;
    public void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
    }
    
    public void noKuraStateLSPRGajiens(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
        float territoryWidth = territoryBounds.size.x;
        float territoryHeight = territoryBounds.size.y;
        Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);
        float distance  = 0;
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
        distance = Vector2.Distance(objekti.lietotajuState.transform.position, stateObject.transform.position - (Vector3)offset); 
            if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] > 0 && distance < 1.96f){
                 objekti.noKuraStateLSPRKontrole = stateObject.GetComponent<SpelesKontrole>();
                 objekti.noKuraStateLSPR = stateObject;                
            }
        }
        Debug.Log("AI izvēlās: "+objekti.noKuraStateLSPR);
    }

    public void noKuraStateLSPRVelreiz(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
        float territoryWidth = territoryBounds.size.x;
        float territoryHeight = territoryBounds.size.y;
        Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);
        float distance  = 0;
        foreach (GameObject stateObject in visiStates)
        {
         SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();   
             distance = Vector2.Distance(objekti.lietotajuState.transform.position, stateObject.transform.position - (Vector3)offset);   
                if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && distance < 2.1f && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] != 5 &&
                stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] < 0){
                 objekti.noKuraStateLSPRKontrole = stateObject.GetComponent<SpelesKontrole>();
                 objekti.noKuraStateLSPR = stateObject;                
                 }
        }
    }

public void LSPRUzbrukums()
{
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
    Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
    float territoryWidth = territoryBounds.size.x;
    float territoryHeight = territoryBounds.size.y;
    Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);

    mobilizet = false;

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
            noKuraStateLSPRVelreiz(); 
        }

        Debug.Log("AI šoreiz tad izvēlās: " + objekti.noKuraStateLSPR);

        // If still no valid state, mobilize
        if (objekti.uzbruksanasState == null)
        {
            AIMobilize(); 
            mobilizet = true;
        }
    }

    Debug.Log("AI uzbruks uz: " + (objekti.uzbruksanasState != null ? objekti.uzbruksanasState.name : "nevienu"));
}


    public void AIUzbruk(){
        bool irUzbrucis = false;
        int skaits = 0;
        noKuraStateLSPRGajiens();
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
                        //Debug.Log("Rotas pievienojas");
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
          objekti.lietotajaGajieni = 0;
    }

    public void AIMobilize(){
    Debug.Log("Izsauc mobilizāciju");
    mobilizacijuSkaitsLSPR = Random.Range(1, 2);
            GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
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
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && mobilizacijuSkaitsLSPR > 0){ 
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noKuraStateLSPR.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.LSPR, 1);
                        mobilizacijuSkaitsLSPR--;
                        Debug.Log("AI pievieno rotas!");
                    }

            }
                }
            }
    objekti.lietotajuKarta = true;
      objekti.lietotajaGajieni = 0;
    }

    public void AIParvietojas(){
    

        objekti.lietotajuKarta = true;
          objekti.lietotajaGajieni = 0;
    }

    public void AIKustiba(){
         objekti.noKuraStateLSPR = null;
         objekti.noKuraStateLSPRKontrole = null;
         objekti.uzbruksanasState = null;
         objekti.uzbruksanasStateKontrole = null;
        if(objekti.lietotajuKarta == false){
            int skaitlis = 0;
            //skaitlis = Random.Range(skaitlis, 3);
            switch(skaitlis){
                case 0: AIUzbruk(); break;
                case 1: AIMobilize(); break;
                case 2: AIParvietojas(); break;
            }
        }
    }

}
