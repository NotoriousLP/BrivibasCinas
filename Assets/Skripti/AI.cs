using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;
    bool mobilizet = false;
    public void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
    }
    
    public void rotuAtkapsanas(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        SpelesKontrole noklikBlakusState = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
            int uzbrukusoSkaits = 0;
            int atkapsanasSkaits = 0;
        noklikBlakusState.Atpaksanas(noklikBlakusState);
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }

                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject.Equals(objekti.noklikBlakusState))
                {
                    uzbrukusoSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    Debug.Log("uzbrūkošo skaits: "+uzbrukusoSkaits);
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && stateObject == objekti.noklikBlakusState){
                            //Debug.Log("Skaits pretinieka: "+uzbrukusoSkaits); 
                         foreach (Transform child in objekti.noklikBlakusState.transform){
                         if (child.CompareTag("LSPRTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            atkapsanasSkaits++;
                          }
                        }
                        stateController.NonemtRotas(Valstis.Speletaji.LSPR, 1);
                        objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                     }            

                }

                //Debug.Log(objekti.atpakpesState);
                }
            }
            if(!objekti.irIelenkti){
                //Debug.Log(objekti.stateAtkapes);
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.stateAtkapes == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }

               //Debug.Log(atkapsanasSkaits);
                foreach (GameObject stateObject in visiStates){
                 SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                  if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject.Equals(objekti.stateAtkapes)){
                 for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && atkapsanasSkaits > 0){ 
                        Debug.Log("Šo pārbaudi iziet");
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.atpakpesState.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.LSPR, 1);
                        atkapsanasSkaits--;
                    }

                }                  
             }
                }
        }else{
            noklikBlakusState.NonemtRotas(Valstis.Speletaji.LSPR, noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]);
        }
           Debug.Log("Kopējais skaits uz state: "+objekti.atpakpesState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]);
         Debug.Log("Kopējais skaits uz noklik state: "+noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]);
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
        Debug.Log(objekti.noKuraStateLSPR);
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
            if(stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && distance < 1.96f && 
            stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] < stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]!=0){
                 objekti.uzbruksanasStateKontrole = stateObject.GetComponent<SpelesKontrole>();
                 objekti.uzbruksanasState = stateObject;                
            }else{
                if(!mobilizet){
                AIMobilize();
                mobilizet = true;
                }
            }
        }
        Debug.Log(objekti.uzbruksanasState);
    }

    public void AIUzbruk(){
    noKuraStateLSPRGajiens();
    LSPRUzbrukums();
    objekti.lietotajuKarta = true;
    }

    public void AIMobilize(){
    Debug.Log("Izsauc mobilizāciju");
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
