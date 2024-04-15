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
        foreach (GameObject stateObject in visiStates)
        {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();        
            if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] > 0){
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
        foreach (GameObject stateObject in visiStates)
        {
         SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();   
         float distance = Vector2.Distance(objekti.noKuraStateLSPRKontrole.transform.position, stateObject.transform.position - (Vector3)offset);
                if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 0
                  && distance < 1.96f && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] != 5){
                 objekti.noKuraStateLSPRKontrole = stateObject.GetComponent<SpelesKontrole>();
                 objekti.noKuraStateLSPR = stateObject;                
                 }
        }
    }

    public void LSPRUzbrukums(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
        float territoryWidth = territoryBounds.size.x;
        float territoryHeight = territoryBounds.size.y;
        Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);
        mobilizet = false;
        foreach (GameObject stateObject in visiStates)
        {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>(); 
        float distance = Vector2.Distance(objekti.noKuraStateLSPRKontrole.transform.position, stateObject.transform.position - (Vector3)offset);
        
        if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && distance < 2f && 
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] <= objekti.noKuraStateLSPRKontrole.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]
            && stateController!=GameObject.Find("States_0")){
                 objekti.uzbruksanasStateKontrole = stateObject.GetComponent<SpelesKontrole>();
                 objekti.uzbruksanasState = stateObject;   
                 uzbrukt = true;
            }
        }
        if(!uzbrukt && objekti.uzbruksanasState == null){
               noKuraStateLSPRVelreiz();
                Debug.Log("AI šoreiz tad izvēlās: "+objekti.noKuraStateLSPR);
                AIMobilize();
                mobilizet = true;
                }
        Debug.Log("AI uzbruks uz: "+objekti.uzbruksanasState);
    }

    public void AIUzbruk(){
        bool irUzbrucis = false;
        int skaits;
        noKuraStateLSPRGajiens();
        LSPRUzbrukums();
        if(uzbrukt){
        Debug.Log("Notiek uzbrukšana uz lietotāju teritoriju");
        skaits = objekti.noKuraStateLSPRKontrole.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        kontrole.rotuAtkapsanasPlayer(Valstis.Speletaji.PLAYER);
        objekti.uzbruksanasState.GetComponent<SpelesKontrole>().valsts.speletajs = Valstis.Speletaji.LSPR;
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
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && skaits > 0){ 
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
                      for(int i=0; i<stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]; i++){
                        if (objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && stateObject == objekti.noKuraStateLSPR){ 
                         foreach (Transform child in objekti.noklikState.transform){
                         if (child.CompareTag("LSPRTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            irUzbrucis = false;
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

    public void AIMobilize(){
    Debug.Log("Izsauc mobilizāciju");
    mobilizacijuSkaitsLSPR = Random.Range(0, 2);
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
                    }

            }
                }
            }
    objekti.lietotajuKarta = true;
    }

    public void AIParvietojas(){
    

        objekti.lietotajuKarta = true;
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


    void Update()
    {
        
    }
}
