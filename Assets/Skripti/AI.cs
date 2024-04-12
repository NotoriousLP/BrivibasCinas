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

            }  if(!mobilizet && objekti.uzbruksanasState == null){
                if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 0
                  && distance < 1.96f && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] != 5){
                 objekti.noKuraStateLSPRKontrole = stateObject.GetComponent<SpelesKontrole>();
                 objekti.noKuraStateLSPR = stateObject;                
                 }
                Debug.Log("AI šoreiz tad izvēlās: "+objekti.noKuraStateLSPR);
                AIMobilize();
                mobilizet = true;
                }
        }
        if(!mobilizet){
            uzbrukt = true;
        }
        Debug.Log("AI uzbruks uz: "+objekti.uzbruksanasState);
    }

    public void AIUzbruk(){
        bool irUzbrucis = false;
        noKuraStateLSPRGajiens();
        LSPRUzbrukums();
        if(uzbrukt){
            Debug.Log("Notiek uzbrukšana uz lietotāju teritoriju");
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
            for(int i=0; i<visiStates.Length; i++){
            if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
        kontrole.rotuAtkapsanas(Valstis.Speletaji.LSPR);
        objekti.noklikBlakusState.GetComponent<SpelesKontrole>().valsts.speletajs = Valstis.Speletaji.PLAYER;
            for(int i=0; i<visiStates.Length; i++){
            if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            foreach (GameObject stateObject in visiStates){
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && stateObject == objekti.noklikBlakusState)
                {
                    stateController.tintesKrasa(new Color32(139, 221, 51, 255));
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.PLAYER, 1);
                        objekti.rotuSkaitsIzv--;
                    }
                    irUzbrucis = true;
                    }
                }
            }
                if(irUzbrucis == true){
                int uzbrukusoSkaits = 0;
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.noklikState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                   SpelesKontrole stateController1 = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && stateObject.Equals(objekti.noklikState))
                {
                    uzbrukusoSkaits = stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                      for(int i=0; i<stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]; i++){
                        if (objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && stateObject == objekti.noklikState){ 
                         foreach (Transform child in objekti.noklikState.transform){
                         if (child.CompareTag("PLAYERTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            irUzbrucis = false;
                          }
                        }
                        objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                        stateController.NonemtRotas(Valstis.Speletaji.PLAYER, 1);

                    }
                }
                }
            }
        }
        }
        objekti.lietotajuKarta = true;
    }

    public void AIMobilize(){
    //Debug.Log("Izsauc mobilizāciju");
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
