using System.Collections;
using System.Collections.Generic;
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
    
    public void rotuAtkapsanas(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        SpelesKontrole noklikBlakusState = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
            int uzbrukusoSkaits = 0;
            int atkapsanasSkaits = 0;
            Debug.Log("Uzvarēja");
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                Debug.Log(objekti.rotasPozicijas);
                }
                }

                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject.Equals(objekti.noklikBlakusState))
                {
                    uzbrukusoSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                  
                    atkapsanasSkaits = uzbrukusoSkaits;
                      for(int i=0; i<noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]; i++){
                        if (objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && stateObject == objekti.noklikBlakusState){
                            //Debug.Log("Skaits pretinieka: "+uzbrukusoSkaits); 
                         foreach (Transform child in objekti.noklikBlakusState.transform){
                         if (child.CompareTag("LSPRTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                          }
                        }
                        stateController.NonemtRotas(Valstis.Speletaji.LSPR, 1);
                        objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                     }            

                }



                Debug.Log(objekti.atpakpesState);
                }
            }
                Debug.Log(objekti.stateAtkapes);
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.stateAtkapes == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }

                Debug.Log(atkapsanasSkaits);
                foreach (GameObject stateObject in visiStates){
                 SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                  if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject.Equals(objekti.stateAtkapes)){
                 for(int i=0; i<atkapsanasSkaits; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
                        Debug.Log("Šo pārbaudi iziet");
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.atpakpesState.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        objekti.atpakpesState.PievienotRotas(Valstis.Speletaji.PLAYER, 1);
                    
                    }
                }                  
            }


                }
    }

    
    void Update()
    {
        
    }
}
