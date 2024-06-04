using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pogas : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;
    public Valstis.Speletaji speletaji;
    public datuGlabasana datuGlabasana;
    public AI ai;
    public Teksts  teksts;

    void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
        ai = FindAnyObjectByType<AI>();
        teksts = FindAnyObjectByType<Teksts>();
        datuGlabasana = FindAnyObjectByType<datuGlabasana>();
    }

    public void saglabatSpeli(){
        datuGlabasana.saglabatSpeli();
        objekti.ESCMenu.gameObject.SetActive(false);
         objekti.fons.gameObject.SetActive(false);
         objekti.vaiIrEsc = false;
        AudioSistema.Instance.speletSFX("poga");
    }

    public void ieprieksejaisProgress(){
        datuGlabasana.ieprieksejaisProgress();
        objekti.ESCMenu.gameObject.SetActive(false);
        objekti.fons.gameObject.SetActive(false);
        objekti.vaiIrEsc = false; 
        AudioSistema.Instance.speletSFX("poga");    
    }


    public void uzIestatijumuAinu()
    {
        SceneManager.LoadScene("iestatijumi", LoadSceneMode.Single);
        AudioSistema.Instance.speletSFX("poga");
    }
    public void uzMainMenu()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        AudioSistema.Instance.speletSFX("poga");
    }
    public void uzSpeli()
    {
        SceneManager.LoadScene("spelesAina", LoadSceneMode.Single);
        AudioSistema.Instance.speletSFX("poga");
    }

    public void uzSpelesIzveli(){
       SceneManager.LoadScene("spelesIzvele", LoadSceneMode.Single);
       AudioSistema.Instance.speletSFX("poga");
    }

    public void izietNoSpeles()
    {
        Application.Quit();
        AudioSistema.Instance.speletSFX("poga");
    }

    public void izietNoLauka()
    {
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.irIzvelesLauksIeslegts = false;
        kontrole.atgriezPretiniekuKrasas();
         kontrole.atgriezLietotajuKrasas();
         objekti.vaiIrIzveleUzbr = false;
        objekti.vaiIrIzveleKust = false;
        AudioSistema.Instance.speletSFX("poga");
    }

    public void mobilizetPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.mobilizet.gameObject.SetActive(true);
        objekti.skaits.gameObject.SetActive(true);
        objekti.plusMob.gameObject.SetActive(true);
        objekti.minusMob.gameObject.SetActive(true);
        objekti.rotuSkaitsIzv = 0;
        AudioSistema.Instance.speletSFX("poga");
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
        AudioSistema.Instance.speletSFX("poga");
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
        AudioSistema.Instance.speletSFX("poga");
    }

    public void plusPogaMob()
    {
        if(objekti.lietotajuKarta == true){
        if(objekti.rotuSkaitsPlayer > 0 && objekti.rotuSkaitsPlayer <= 5 && objekti.rotuSkaitsIzv >= 0 && objekti.rotuSkaitsIzv < objekti.rotuSkaitsPlayer)
        {
            objekti.rotuSkaitsIzv++;
        }
        }else if(objekti.otraSpeletajaKarta == true){
        if(objekti.rotuSkaitsLSPR > 0 && objekti.rotuSkaitsLSPR <= 5 && objekti.rotuSkaitsIzv >= 0 && objekti.rotuSkaitsIzv < objekti.rotuSkaitsLSPR)
        {
            objekti.rotuSkaitsIzv++;
        } 
        }
        AudioSistema.Instance.speletSFX("poga");
    }

    public void minusPogaMob()
    {

        if(objekti.lietotajuKarta == true){
        if (objekti.rotuSkaitsPlayer > 0 && objekti.rotuSkaitsPlayer <= 5 && objekti.rotuSkaitsIzv > 0 && objekti.rotuSkaitsIzv <= objekti.rotuSkaitsPlayer)
        {
            objekti.rotuSkaitsIzv--;
        }
        }else if(objekti.otraSpeletajaKarta == true){
        if (objekti.rotuSkaitsLSPR > 0 && objekti.rotuSkaitsLSPR <= 5 && objekti.rotuSkaitsIzv > 0 && objekti.rotuSkaitsIzv <= objekti.rotuSkaitsLSPR)
        {
            objekti.rotuSkaitsIzv--;
        }
        }
        AudioSistema.Instance.speletSFX("poga");
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
    AudioSistema.Instance.speletSFX("poga");
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
    AudioSistema.Instance.speletSFX("poga");
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
        AudioSistema.Instance.speletSFX("poga");
    }


    public void mobilizetRotas(){
        if(objekti.rotuSkaitsLSPR != 0 || objekti.rotuSkaitsPlayer != 0 && objekti.rotuSkaitsIzv !=0){
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
                    if (objekti.rotuSkaitsIzv > 0){ 
                    bool pozicijaAiznemta = false;
                      foreach (Transform child in objekti.noklikState.transform) {
                        if (child.transform.position == objekti.rotasPozicijas[i].transform.position) {
                              pozicijaAiznemta = true;
                              break;
                          }
                    }
                    if (!pozicijaAiznemta) {
                    if(speletaji == Valstis.Speletaji.PLAYER){
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikState.transform);
                        objekti.rotuSkaitsPlayer--;
                        }else if(speletaji == Valstis.Speletaji.LSPR){
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikState.transform);
                        objekti.rotuSkaitsLSPR--;
                        }
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);

                        stateController.PievienotRotas(speletaji, 1);
                        objekti.rotuSkaitsIzv--;
                    }
                    }

            }
                }
            }
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.izvele.gameObject.SetActive(false);
        objekti.mobilizet.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
        objekti.plusMob.gameObject.SetActive(false);
        objekti.minusMob.gameObject.SetActive(false);
        objekti.bridinajumaTeksts.gameObject.SetActive(false);
        teksts.rotasTeksts.text = "Tagad jūs var mobilizēt tikai "+objekti.rotuSkaitsIzv.ToString()+" rotas";
        teksts.infoTeksts.text = "Jūs mobilizējāt rotas!";
        objekti.pazRotuLauks.gameObject.SetActive(true);
        teksts.rotasTeksts.gameObject.SetActive(true);
        objekti.apraksts = true;
        }else if(objekti.rotuSkaitsIzv == 0){
           objekti.bridinajumaTeksts.gameObject.SetActive(true);
        }
        AudioSistema.Instance.speletSFX("poga");
    }

    public void parvietotRotas(){
        if(objekti.rotuSkaitsIzv!=0){
        bool irParvietojis = false;
        int pieejamaVieta = 0;
        int rotaParvietosana = 0;
        objekti.rotasPozicijas = null;
        
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
            foreach (GameObject stateObject in visiStates) {
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == speletaji && stateObject == objekti.noklikBlakusState) {
                    pieejamaVieta = 5 - stateController.rotasSkaitsByPlayer[speletaji];
                    rotaParvietosana = Mathf.Min(objekti.rotuSkaitsIzv, pieejamaVieta);

                    int parvietotasRotas = 0;
                    for (int i = 0; i < objekti.rotasPozicijas.Length && parvietotasRotas < rotaParvietosana; i++) {
                        bool pozicijaAiznemta = false;
                        foreach (Transform child in objekti.noklikBlakusState.transform) {
                            if (child.transform.position == objekti.rotasPozicijas[i].transform.position) {
                                pozicijaAiznemta = true;
                                break;
                            }
                        }
                        if (!pozicijaAiznemta) {
                            if (speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta) {
                                Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                            } else if (speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta) {
                                Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                            }

                            objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                            stateController.PievienotRotas(speletaji, 1);
                            objekti.rotuSkaitsIzv--;
                            parvietotasRotas++;
                        }
                    }
                    irParvietojis = true;
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
                    parvietojumoSkaits = rotaParvietosana;
                    Debug.Log("Pārvietojumo skaits: "+parvietojumoSkaits);
                      for(int i=0; i<parvietojumoSkaits; i++){
                        if (stateObject == objekti.noklikState){ 
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
            }
        }
        objekti.vaiIrIzveleKust = false;
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.izvele.gameObject.SetActive(false);
        objekti.uzbrukt.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
        objekti.plusParvietot.gameObject.SetActive(false);
        objekti.minusParvietot.gameObject.SetActive(false);
        objekti.bridinajumaTeksts.gameObject.SetActive(false);
        kontrole.atgriezLietotajuKrasas();
        AudioSistema.Instance.speletSFX("parvietot");
        teksts.infoTeksts.text = "Jūs pārvietojāt rotas!";
        objekti.pazRotuLauks.gameObject.SetActive(true);
        teksts.rotasTeksts.gameObject.SetActive(false);
        objekti.apraksts = true;
        }else if(objekti.rotuSkaitsIzv == 0){
           objekti.bridinajumaTeksts.gameObject.SetActive(true);
        }

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
        teksts.vesturisksApraksts.text = " ";
        SpelesKontrole noklikBlakusState = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
        SpelesKontrole klikState = objekti.noklikState.GetComponent<SpelesKontrole>();

        if(objekti.lietotajuKarta == true){
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
        if(objekti.lietotajuKarta){

        }else if(objekti.otraSpeletajaKarta){
            
        }
        teksts.cinasTeksts.text = "Tu esi zaudējis šo cīņu!";
        objekti.pazinojumaLauks.gameObject.SetActive(true);
         objekti.apraksts = true;
           AudioSistema.Instance.speletSFX("zaudet");
        }


        if(irUzvarejis){
            int uzbrukusoSkaits = objekti.rotuSkaitsIzv;
            objekti.noklikBlakusState.GetComponent<SpelesKontrole>().valsts.speletajs = speletaji;
            if(speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
            kontrole.rotuAtkapsanas(Valstis.Speletaji.LSPR);
            }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
             kontrole.rotuAtkapsanas(Valstis.Speletaji.PLAYER);   
            }
            objekti.rotasPozicijas = null;

            for(int i=0; i<=visiStates.Length; i++){
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
                        if (objekti.rotuSkaitsIzv > 0){ 
                         Debug.Log("rotu Skaits if 460.rinda "+objekti.rotuSkaitsIzv);   
                        if(speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        Debug.Log("irUzvarejis if iziet PLAYER");

                        }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        Debug.Log("irUzvarejis if iziet LSPR");
                        }

                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(speletaji, 1);
                        objekti.rotuSkaitsIzv--;
                    }
                    irUzbrucis = true;
                    Debug.Log("irUzvarejis Cikls iziet "+i);
                    }
                Debug.Log("Rotu skaits: "+stateController.rotasSkaitsByPlayer[speletaji]);
                }
                teksts.cinasTeksts.text = "Tu esi uzvarējis šo cīņu!";
                objekti.pazinojumaLauks.gameObject.SetActive(true);
                teksts.vesturisksNotikums();
                objekti.apraksts = true;         
            }

                if(irUzbrucis == true){
               
                objekti.rotasPozicijas = null;
                for(int i=0; i<=visiStates.Length; i++){
                 if(objekti.noklikState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                   SpelesKontrole stateController1 = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == speletaji && stateObject.Equals(objekti.noklikState))
                {
                    Debug.Log("Uzbrūkošo skaits: "+uzbrukusoSkaits);
                      for(int i=0; i<stateController1.rotasSkaitsByPlayer[speletaji]; i++){
                        if (stateObject == objekti.noklikState){ 
                         foreach (Transform child in objekti.noklikState.transform){
                            if(speletaji == Valstis.Speletaji.PLAYER){
                         if (child.CompareTag("PLAYERTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            irUzbrucis = false;
                            objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                            stateController.NonemtRotas(speletaji, 1);
                            Debug.Log("Lietotāju rotas izdzēšs lai pārvietotu");
                          }
                        }else if(speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                         if (child.CompareTag("LSPRTroop") && uzbrukusoSkaits > 0){
                            Destroy(child.gameObject);
                            uzbrukusoSkaits--;
                            irUzbrucis = false;
                            objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                            stateController.NonemtRotas(speletaji, 1);
                            Debug.Log("Pretinieku rotas izdzēšs lai pārvietotu");
                          }
                        }
                         }
                    }
                    Debug.Log("Cikls nostrāda "+i+ " reizi");
                }
                }
            }
        }
            AudioSistema.Instance.speletSFX("uzvarets");
        }
        objekti.vaiIrIzveleUzbr = false;
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.izvele.gameObject.SetActive(false);
        objekti.uzbrukt.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
        objekti.plusUzb.gameObject.SetActive(false);
        objekti.minusUzb.gameObject.SetActive(false);
        objekti.bridinajumaTeksts.gameObject.SetActive(false);
        }else if(objekti.rotuSkaitsIzv == 0){
           objekti.bridinajumaTeksts.gameObject.SetActive(true);
        }
        
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
        objekti.bridinajumaTeksts.gameObject.SetActive(false);
        kontrole.atgriezPretiniekuKrasas();
        kontrole.atgriezLietotajuKrasas();
        objekti.vaiIrIzveleUzbr = false;
        objekti.vaiIrIzveleKust = false;
        AudioSistema.Instance.speletSFX("poga");
    }

    public void okPogaApraksts(){
        objekti.pazinojumaLauks.gameObject.SetActive(false);
        objekti.pazRotuLauks.gameObject.SetActive(false);
        teksts.rotasTeksts.gameObject.SetActive(false);
        objekti.apraksts = false;
         if(objekti.AIieslegts == false){
        if(objekti.lietotajuKarta == true){
        objekti.lietotajuKarta = false;
        objekti.otraSpeletajaKarta = true;
        objekti.LatvijasKarogs.gameObject.SetActive(false);
        objekti.LSPRKarogs.gameObject.SetActive(true);
        }else if(objekti.otraSpeletajaKarta == true){
        objekti.otraSpeletajaKarta = false;
          objekti.lietotajuKarta = true;
        objekti.LatvijasKarogs.gameObject.SetActive(true);
        objekti.LSPRKarogs.gameObject.SetActive(false);
        }
        }else if(objekti.AIieslegts == true){
        objekti.lietotajuKarta = false;
         ai.AIKustiba();
        }
        uzvaretajaParbaude();
        AudioSistema.Instance.speletSFX("poga");
    }

    public void uzvaretajaParbaude(){
                GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
           int LSPRTeritorijuSkaits = 0;
           int PlayerTeritorijuSkaits = 0;
            teksts.uzvaretajuTeksts.text = "";
            teksts.uzvaretajuApraksts.text = "";
                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                    if(stateController.valsts.speletajs == Valstis.Speletaji.PLAYER){
                        PlayerTeritorijuSkaits++;
                    }else if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR){
                        LSPRTeritorijuSkaits++;
                    }

                }
                if(PlayerTeritorijuSkaits == 12){
                    objekti.rotuSkaitsLSPR = 4;
                }
                Debug.Log("Teritoriju skaits Player: "+PlayerTeritorijuSkaits);
                if(PlayerTeritorijuSkaits == 1){
                    objekti.LSPRUzvarejis = true;
                    teksts.uzvaretajuTeksts.text = "Lielinieki ir pārāki un Latvija padodas";
                    teksts.uzvaretajuApraksts.text = "<style=h1>Kas notika?</style><br><#555>Latvija padodas un pēc 3 dienām ienāk lielinieku armija pēdejā brīvajā Latvijas teritorijā.<br><b>Pēteris Stučka</b> saglabā savu valdību Rīgā.<br><b>Latvijas pagaidu valdība</b> aizbēg uz Lielbritāniju.<br><b>1921. gadā</b> PSRS anektē Latvijas Sociālisto Padomju Republiku.";  
                    if(objekti.AIieslegts == true){
                        objekti.uzvaretajuLauks.gameObject.SetActive(true);
                        objekti.fons.gameObject.SetActive(true); 
                        objekti.segVards.gameObject.SetActive(false);
                        objekti.segVardsTeksts.gameObject.SetActive(false);
                        objekti.okPogaAI.gameObject.SetActive(false);
                        objekti.retryPogaAI.gameObject.SetActive(true);
                        objekti.izietPogaAI.gameObject.SetActive(true);
                    }else if(objekti.AIieslegts == false){
                        objekti.uzvaretajuLauks.gameObject.SetActive(true);
                        objekti.fons.gameObject.SetActive(true); 
                        objekti.segVards.gameObject.SetActive(true);
                        objekti.segVardsTeksts.gameObject.SetActive(true);
                        objekti.okPogaAI.gameObject.SetActive(true);
                        objekti.retryPogaAI.gameObject.SetActive(false);
                        objekti.izietPogaAI.gameObject.SetActive(false);
                    }
                    AudioSistema.Instance.speletSFX("notikums");
                }else if(LSPRTeritorijuSkaits == 5){
                    objekti.playerUzvarejis = true;
                    teksts.uzvaretajuTeksts.text = "Lielinieki padodas un mūk prom uz Padomju Krieviju!";
                teksts.uzvaretajuApraksts.text = "<#555><style=H1>Kas notika?</style><br><b>Kapēc parakstīja miera līgumu?</b><br>Iniciatīva nāca no Padomju Krievijas puses, lai izjauktu koalīciju pret to Baltijā un novērstu draudus Petrogradai.<br>Latvijas mērķis bija panākt neatkarības atzīšanu un izbeigt karu.<br><b>Vienošanās gaita:</b><br>Sākotnēji bija paredzētas kopīgas Baltijas valstu sarunas, tomēr Igaunija uzsāka atsevišķas sarunas.<br>Latvija parakstīja pamieru 1920. gada 30. janvārī, kas atļāva turpināt Latgales atbrīvošanu.<br>Miera sarunas notika Maskavā un Rīgā no 1920. gada aprīļa līdz augustam.<br>Krievijas delegācija sākotnēji izvirzīja nepieņemamas prasības, tomēr Latvijas panākumi kaujas laukā un Polijas uzbrukums Krievijai piespieda to piekāpties.<br><b>Līguma rezultāti:</b><br>Parakstīts 1920. gada 11. augustā Rīgā.<br>Atzina Latvijas neatkarību un noteica robežas.<br>Krievija atteicās no kara izdevumu atlīdzināšanas un atļāva daļēju Latvijas iedzīvotāju īpašuma reevakuāciju.<br>Līgums kalpoja par pamatu Latvijas starptautiskai atzīšanai un okupācijas neatzīšanai pēc Otrā pasaules kara.<br><b>Vēsturiskā nozīme:</b><br>Noslēdza Latvijas Neatkarības karu.<br>Bija nozīmīgs solis ceļā uz de jure atzīšanu.<br>Kalpo par starptautiskas tiesības pamatu Latvijas okupācijas neatzīšanai.<br><b>Piemiņa:</b><br>11. augusts ir Latvijas brīvības cīnītāju piemiņas diena.";
                    objekti.uzvaretajuLauks.gameObject.SetActive(true);
                    objekti.fons.gameObject.SetActive(true);
                    AudioSistema.Instance.speletSFX("notikums");
                }
    }

        public void uzvaretajaParbaudeAI(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
           int PlayerTeritorijuSkaits = 0;
            teksts.uzvaretajuTeksts.text = "";
            teksts.uzvaretajuApraksts.text = "";
                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                    if(stateController.valsts.speletajs == Valstis.Speletaji.PLAYER){
                        PlayerTeritorijuSkaits++;
                    }
                }
                Debug.Log("Teritoriju skaits Player: "+PlayerTeritorijuSkaits);
                if(PlayerTeritorijuSkaits == 1){
                    objekti.LSPRUzvarejis = true;
                    teksts.uzvaretajuTeksts.text = "Lielinieki ir pārāki un Latvija padodas";
                    teksts.uzvaretajuApraksts.text = "<style=h1>Kas notika?</style><br><#555>Latvija padodas un pēc 3 dienām ienāk lielinieku armija pēdejā brīvajā Latvijas teritorijā.<br><b>Pēteris Stučka</b> saglabā savu valdību Rīgā.<br><b>Latvijas pagaidu valdība</b> aizbēg uz Lielbritāniju.<br><b>1921. gadā</b> PSRS anektē Latvijas Sociālisto Padomju Republiku.";  
                        objekti.uzvaretajuLauks.gameObject.SetActive(true);
                        objekti.fons.gameObject.SetActive(true); 
                        objekti.segVards.gameObject.SetActive(false);
                        objekti.segVardsTeksts.gameObject.SetActive(false);
                        objekti.okPogaAI.gameObject.SetActive(false);
                        objekti.retryPogaAI.gameObject.SetActive(true);
                        objekti.izietPogaAI.gameObject.SetActive(true);
                    AudioSistema.Instance.speletSFX("notikums");
                }
        }

    public void infoPoga(){
        if(objekti.nospiestaInfo == false){
            objekti.nospiestaInfo = true;
            objekti.infoPanelis.gameObject.SetActive(true);
        }else if(objekti.nospiestaInfo == true){
            objekti.nospiestaInfo = false;
            objekti.infoPanelis.gameObject.SetActive(false);
        }
        AudioSistema.Instance.speletSFX("poga");
    }
        public void rezultatuPoga(){  
        if(!string.IsNullOrEmpty(objekti.segVards.text)){      
        datuGlabasana.pievienotDatus();
        objekti.uzvaretajuLauks.gameObject.SetActive(false);
        objekti.lideruLauks.gameObject.SetActive(true);
        objekti.kluduTeksts.gameObject.SetActive(false);
        }else{
            objekti.kluduTeksts.gameObject.SetActive(true);
        }
        AudioSistema.Instance.speletSFX("poga");
       }

}