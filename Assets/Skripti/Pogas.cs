using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pogas : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;
    public Valstis.Speletaji speletaji;
    public AI ai;
    void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
        ai = FindAnyObjectByType<AI>();
    }

    public void uzIestatijumuAinu()
    {
        SceneManager.LoadScene("iestatijumi", LoadSceneMode.Single);
    }
    public void uzMainMenu()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
    public void uzSpeli()
    {
        SceneManager.LoadScene("spelesAina", LoadSceneMode.Single);
    }
    public void izietNoSpeles()
    {
        Application.Quit();
    }

    public void izietNoLauka()
    {
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.irIzvelesLauksIeslegts = false;
        kontrole.atgriezPretiniekuKrasas();
         kontrole.atgriezLietotajuKrasas();
         objekti.vaiIrIzveleUzbr = false;
        objekti.vaiIrIzveleKust = false;
    }

    public void mobilizetPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.mobilizet.gameObject.SetActive(true);
        objekti.skaits.gameObject.SetActive(true);
        objekti.plusMob.gameObject.SetActive(true);
        objekti.minusMob.gameObject.SetActive(true);
        objekti.rotuSkaitsIzv = 0;
    }

    public void kustinatPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.kurVietaKustinat.gameObject.SetActive(true);
        if(objekti.lietotajuKarta == true){
        kontrole.iekrasoBlakusLietotajuTeritoriju(objekti.noklikState);
        }else if(objekti.otraSpeletajaKarta == true){
         kontrole.iekrasoBlakusPretiniekuTeritoriju(objekti.noklikState);  
        }
    }

    public void uzbruktPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.kurVietaUzbrukt.gameObject.SetActive(true);
        if(objekti.lietotajuKarta == true){
        kontrole.iekrasoBlakusPretiniekuTeritoriju(objekti.noklikState);
        }else if(objekti.otraSpeletajaKarta == true){
         kontrole.iekrasoBlakusLietotajuTeritoriju(objekti.noklikState);  
        }
    }

    public void plusPogaMob()
    {
        if(objekti.rotuSkaits > 0 && objekti.rotuSkaits <= 5 && objekti.rotuSkaitsIzv >= 0 && objekti.rotuSkaitsIzv < objekti.rotuSkaits)
        {
            objekti.rotuSkaitsIzv++;
        }
    }

    public void minusPogaMob()
    {
        if (objekti.rotuSkaits > 0 && objekti.rotuSkaits <= 5 && objekti.rotuSkaitsIzv > 0 && objekti.rotuSkaitsIzv <= objekti.rotuSkaits)
        {
            objekti.rotuSkaitsIzv--;
        }
    }

    public void plusPogaUzb(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        if(objekti.lietotajuKarta == true){
            speletaji = Valstis.Speletaji.PLAYER;
        }else if(objekti.otraSpeletajaKarta == true){
             speletaji = Valstis.Speletaji.LSPR;
        }
        foreach (GameObject stateObject in visiStates){
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        if(stateObject == objekti.noklikState && 
        stateController.rotasSkaitsByPlayer[speletaji] > 0 && stateController.rotasSkaitsByPlayer[speletaji] <= 5 && objekti.rotuSkaitsIzv < stateController.rotasSkaitsByPlayer[speletaji]){
              objekti.rotuSkaitsIzv++;
        }
    
    }
    }
  public void minusPogaUzb(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
                if(objekti.lietotajuKarta == true){
            speletaji = Valstis.Speletaji.PLAYER;
        }else if(objekti.otraSpeletajaKarta == true){
             speletaji = Valstis.Speletaji.LSPR;
        }
        foreach (GameObject stateObject in visiStates){
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        if(stateObject == objekti.noklikState && 
        stateController.rotasSkaitsByPlayer[speletaji] > 0 && stateController.rotasSkaitsByPlayer[speletaji] <= 5 && objekti.rotuSkaitsIzv <= stateController.rotasSkaitsByPlayer[speletaji] && objekti.rotuSkaitsIzv > 0){
              objekti.rotuSkaitsIzv--;
        }
    
    }
    }

    public void plusPogaParvietot()
    {
            if(objekti.lietotajuKarta == true){
            speletaji = Valstis.Speletaji.PLAYER;
          }else if(objekti.otraSpeletajaKarta == true){
             speletaji = Valstis.Speletaji.LSPR;
              }
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");

        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            SpelesKontrole stateController1 = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();

                if (stateObject == objekti.noklikState)
            {
                int pieejamasRotas = stateController.rotasSkaitsByPlayer[speletaji];
                int maksimalasRotasKustinat = Mathf.Min(5 - stateController1.rotasSkaitsByPlayer[speletaji], pieejamasRotas);
                if (maksimalasRotasKustinat > 0 && objekti.rotuSkaitsIzv < maksimalasRotasKustinat)
                {
                    objekti.rotuSkaitsIzv++;
                }
            }
        }
    }


    public void mobilizetRotas(){
        if(objekti.rotuSkaits != 0 && objekti.rotuSkaitsIzv !=0){
             if(objekti.lietotajuKarta == true){
            speletaji = Valstis.Speletaji.PLAYER;
          }else if(objekti.otraSpeletajaKarta == true){
             speletaji = Valstis.Speletaji.LSPR;
              }
            GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
            
            for(int i=0; i<visiStates.Length; i++){
                if(objekti.noklikState == GameObject.Find("States_"+i)){
                    objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
           
            foreach (GameObject stateObject in visiStates){
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == speletaji && stateObject == objekti.noklikState)
                {
                 for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
                    if(speletaji == Valstis.Speletaji.PLAYER){
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikState.transform);
                        }else if(speletaji == Valstis.Speletaji.LSPR){
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikState.transform);
                        }
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);

                        stateController.PievienotRotas(speletaji, 1);
                        objekti.rotuSkaitsIzv--;
                    }

            }
                }
            }
        }
        if(objekti.lietotajuKarta == true){
        objekti.lietotajuKarta = false;
        objekti.otraSpeletajaKarta = true;
        }else if(objekti.otraSpeletajaKarta == true){
        objekti.otraSpeletajaKarta = false;
          objekti.lietotajuKarta = true;
        }
          //ai.AIKustiba();
    }

    public void parvietotRotas(){
        if(objekti.rotuSkaitsIzv!=0){
        bool irParvietojis = false;
         if(objekti.lietotajuKarta == true){
            speletaji = Valstis.Speletaji.PLAYER;
          }else if(objekti.otraSpeletajaKarta == true){
             speletaji = Valstis.Speletaji.LSPR;
            }
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
            for(int i=0; i<visiStates.Length; i++){
            if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            foreach (GameObject stateObject in visiStates){
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == speletaji && stateObject == objekti.noklikBlakusState)
                {
                        int availableSpace = 5 - stateController.rotasSkaitsByPlayer[speletaji];
                         int actualTroopsToMove = Mathf.Min(objekti.rotuSkaitsIzv, availableSpace);

                      for(int i=0; i<actualTroopsToMove; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
                       if(speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        }
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(speletaji, 1);
                        objekti.rotuSkaitsIzv--;
                    }
                    irParvietojis = true;
                    }
                }
            }
                if(irParvietojis == true){
                int parvietojumoSkaits = 0;
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.noklikState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                   SpelesKontrole stateController1 = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == speletaji && stateObject.Equals(objekti.noklikState))
                {
                    parvietojumoSkaits = stateController1.rotasSkaitsByPlayer[speletaji];
                    Debug.Log("Pārvietojumo skaits: "+parvietojumoSkaits);
                      for(int i=0; i<parvietojumoSkaits; i++){
                        if (objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && stateObject == objekti.noklikState){ 
                         foreach (Transform child in objekti.noklikState.transform){
                        if(speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
                         if (child.CompareTag("PLAYERTroop") && parvietojumoSkaits > 0){
                            Destroy(child.gameObject);
                            parvietojumoSkaits--;
                            stateController.NonemtRotas(speletaji, 1);
                            objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                            irParvietojis = false;
                          }
                        }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                         if (child.CompareTag("LSPRTroop") && parvietojumoSkaits > 0){
                            Destroy(child.gameObject);
                            parvietojumoSkaits--;
                            stateController.NonemtRotas(speletaji, 1);
                            objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                            irParvietojis = false;
                          }
                        }
                        }
                         Debug.Log("Rotas skaits uz uzsķlikšķinātā state: "+stateController.rotasSkaitsByPlayer[speletaji]);
                    }
                }
                }
                //Debug.Log("Pašreizējais state: "+stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]);
                //Debug.Log("Blakus state"+stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]);
            }
        }
        }
        objekti.vaiIrIzveleKust = false;
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.izvele.gameObject.SetActive(false);
        objekti.uzbrukt.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
        objekti.plusParvietot.gameObject.SetActive(false);
        objekti.minusParvietot.gameObject.SetActive(false);
        kontrole.atgriezLietotajuKrasas();
        if(objekti.lietotajuKarta == true){
        objekti.lietotajuKarta = false;
        objekti.otraSpeletajaKarta = true;
        }else if(objekti.otraSpeletajaKarta == true){
        objekti.otraSpeletajaKarta = false;
          objekti.lietotajuKarta = true;
        }
        //ai.AIKustiba();
    }


    public void okPogaUzbrukt(){ //Funkcija kad uzbrūk pretiniekam.
    if(objekti.rotuSkaitsIzv!=0){
        if(objekti.lietotajuKarta == true){
            speletaji = Valstis.Speletaji.PLAYER;
          }else if(objekti.otraSpeletajaKarta == true){
             speletaji = Valstis.Speletaji.LSPR;
            }
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        bool irUzbrucis = false;
        bool irUzvarejis = false;
        bool irVienads = false;
        bool irZaudejis = false;
        objekti.atpakpesState = null;
        objekti.stateAtkapes = null;
        SpelesKontrole noklikBlakusState = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();

        if(objekti.lietotajuKarta==true){
        if(objekti.rotuSkaitsIzv > noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]){
            irUzvarejis = true;   
        }
        
        else if(objekti.rotuSkaitsIzv  == noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]){
            irVienads = true;
        }else if(objekti.rotuSkaitsIzv < noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR]){
            irZaudejis = true;
        }
        }else if(objekti.otraSpeletajaKarta==true){
        if(objekti.rotuSkaitsIzv > noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]){
            irUzvarejis = true;   
             }
        else if(objekti.rotuSkaitsIzv  == noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]){
            irVienads = true;
        }else if(objekti.rotuSkaitsIzv < noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]){
            irZaudejis = true;
        }
        }

        if(irVienads){
             int randomSkaitlis = 0;
            randomSkaitlis = Random.Range(0, 2);
            Debug.Log(randomSkaitlis);
            if(randomSkaitlis == 1){
                irUzvarejis = true;
            }else{
                irZaudejis = true;
            }
        }

       
       if(irZaudejis){
            if(noklikBlakusState.rotasSkaitsByPlayer[speletaji] == 5 && objekti.rotuSkaitsIzv == 1){
            }else if(noklikBlakusState.rotasSkaitsByPlayer[speletaji] == 4 && objekti.rotuSkaitsIzv == 2){
            }else if(noklikBlakusState.rotasSkaitsByPlayer[speletaji] == 5 && objekti.rotuSkaitsIzv == 3){
            }
        }


        if(irUzvarejis){
            objekti.noklikBlakusState.GetComponent<SpelesKontrole>().valsts.speletajs = speletaji;
            if(speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
            kontrole.rotuAtkapsanas(Valstis.Speletaji.LSPR);
            }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
             kontrole.rotuAtkapsanas(Valstis.Speletaji.PLAYER);   
            }
            objekti.rotasPozicijas = null;
            for(int i=0; i<visiStates.Length; i++){

            if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            foreach (GameObject stateObject in visiStates){
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == speletaji && stateObject == objekti.noklikBlakusState)
                {
                    if(speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
                    stateController.tintesKrasa(new Color32(139, 221, 51, 255));
                    }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                    stateController.tintesKrasa(new Color32(243, 43, 43, 235));
                    }
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
                        if(speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        }
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(speletaji, 1);
                        objekti.rotuSkaitsIzv--;
                    }
                    irUzbrucis = true;
                    }
                Debug.Log("Rotu skaits: "+stateController.rotasSkaitsByPlayer[speletaji]);
                }
                
            }
                if(irUzbrucis == true){
                int uzbrukusoSkaits = 0;
                objekti.rotasPozicijas = null;
                for(int i=0; i<visiStates.Length; i++){
                 if(objekti.noklikState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                   SpelesKontrole stateController1 = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == speletaji && stateObject.Equals(objekti.noklikState))
                {
                    uzbrukusoSkaits = stateController1.rotasSkaitsByPlayer[speletaji];
                    Debug.Log("Uzbrūkošo skaits: "+uzbrukusoSkaits);
                      for(int i=0; i<stateController1.rotasSkaitsByPlayer[speletaji]; i++){
                        if (objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && stateObject == objekti.noklikState){ 
                         foreach (Transform child in objekti.noklikState.transform){
                            if(speletaji == Valstis.Speletaji.PLAYER){
                         if (child.CompareTag("PLAYERTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            irUzbrucis = false;
                            Debug.Log("Lietotāju rotas izdzēšs lai pārvietotu");
                          }
                        }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                         if (child.CompareTag("LSPRTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            irUzbrucis = false;
                            //Debug.Log("Pretinieku rotas izdzēšs lai pārvietotu");
                          }
                        }
                         }
                        objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                        stateController.NonemtRotas(speletaji, 1);
                        //Debug.Log("Lietotāju/Pretinieku rotas tiek atkārtoti izdzēstas");
                    }
                    Debug.Log("Cikls nostrāda "+i+ " reizi");
                }
                }
            }
        }
        }
    }
        objekti.vaiIrIzveleUzbr = false;
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.izvele.gameObject.SetActive(false);
        objekti.uzbrukt.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
        objekti.plusUzb.gameObject.SetActive(false);
        objekti.minusUzb.gameObject.SetActive(false);
        if(objekti.lietotajuKarta == true){
        objekti.lietotajuKarta = false;
        objekti.otraSpeletajaKarta = true;
        }else if(objekti.otraSpeletajaKarta == true){
        objekti.otraSpeletajaKarta = false;
          objekti.lietotajuKarta = true;
        }
        //ai.AIKustiba();
    }

    public void atpakalUzIzveli()
    {
        objekti.izvele.gameObject.SetActive(true);
        objekti.kurVietaUzbrukt.gameObject.SetActive(false);
        objekti.uzbrukt.gameObject.SetActive(false);
        objekti.kurVietaKustinat.gameObject.SetActive(false);
        objekti.kustinat.gameObject.SetActive(false);
        objekti.mobilizet.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
        objekti.plusUzb.gameObject.SetActive(false);
        objekti.minusUzb.gameObject.SetActive(false);
        objekti.plusMob.gameObject.SetActive(false);
        objekti.minusMob.gameObject.SetActive(false);
        objekti.plusParvietot.gameObject.SetActive(false);
        objekti.minusParvietot.gameObject.SetActive(false);
        kontrole.atgriezPretiniekuKrasas();
        kontrole.atgriezLietotajuKrasas();
        objekti.vaiIrIzveleUzbr = false;
        objekti.vaiIrIzveleKust = false;
    }


}