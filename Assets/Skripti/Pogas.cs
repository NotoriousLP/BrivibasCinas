using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pogas : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;
    void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
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
        kontrole.iekrasoBlakusLietotajuTeritoriju(objekti.noklikState);
    }

    public void uzbruktPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.kurVietaUzbrukt.gameObject.SetActive(true);
        kontrole.iekrasoBlakusPretiniekuTeritoriju(objekti.noklikState);
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
        foreach (GameObject stateObject in visiStates){
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        if(stateObject == objekti.noklikState && 
        stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] > 0 && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] <= 5 && objekti.rotuSkaitsIzv < stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]){
              objekti.rotuSkaitsIzv++;
        }
    
    }
    }
  public void minusPogaUzb(){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        foreach (GameObject stateObject in visiStates){
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        if(stateObject == objekti.noklikState && 
        stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] > 0 && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] <= 5 && objekti.rotuSkaitsIzv <= stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] && objekti.rotuSkaitsIzv > 0){
              objekti.rotuSkaitsIzv--;
        }
    
    }
    }

public void plusPogaParvietot()
{
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");

    foreach (GameObject stateObject in visiStates)
    {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        SpelesKontrole stateController1 = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();

            if (stateObject == objekti.noklikState)
        {
            // Calculate the maximum number of troops that can be moved (minimum of available troops and 5 - current troop count)
            int availableTroops = stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
            int maxTroopsToMove = Mathf.Min(5 - stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER], availableTroops);
            if (maxTroopsToMove > 0 && objekti.rotuSkaitsIzv < maxTroopsToMove)
            {
                objekti.rotuSkaitsIzv++;
            }
        }
    }
}


    public void mobilizetRotas(){
        if(objekti.rotuSkaits != 0 && objekti.rotuSkaitsIzv !=0){
            GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
            for(int i=0; i<visiStates.Length; i++){
                if(objekti.noklikState == GameObject.Find("States_"+i)){
                    objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
           
            foreach (GameObject stateObject in visiStates){
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && stateObject == objekti.noklikState)
                {
                 for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikState.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);

                        stateController.PievienotRotas(Valstis.Speletaji.PLAYER, 1);
                        objekti.rotuSkaitsIzv--;
                    }

            }
                }
            }
        }
    }

    public void parvietotRotas(){
        if(objekti.rotuSkaitsIzv!=0){
        bool irParvietojis = false;
        objekti.noklikBlakusState.GetComponent<SpelesKontrole>().valsts.speletajs = Valstis.Speletaji.PLAYER;
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
            for(int i=0; i<visiStates.Length; i++){
            if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
            }
            foreach (GameObject stateObject in visiStates){
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && stateObject == objekti.noklikBlakusState)
                {
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (!objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikBlakusState.transform);
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(Valstis.Speletaji.PLAYER, 1);
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
                if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && stateObject.Equals(objekti.noklikState))
                {
                    parvietojumoSkaits = stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                      for(int i=0; i<stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]; i++){
                        if (objekti.izmantotasPozicijas.Contains(objekti.rotasPozicijas[i]) && stateObject == objekti.noklikState){ 
                         foreach (Transform child in objekti.noklikState.transform){
                         if (child.CompareTag("PLAYERTroop") && parvietojumoSkaits > 0){
                            Destroy(child.gameObject);
                            parvietojumoSkaits--;
                            irParvietojis = false;
                          }
                        }
                         objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                         stateController.NonemtRotas(Valstis.Speletaji.PLAYER, 1);
                    }
                }
                }
                Debug.Log("Pašreizējais state: "+stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]);
                Debug.Log("Blakus state"+stateController1.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]);
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
    }



    public void okPogaUzbrukt(){ //Funkcija kad uzbrūk pretiniekam.
    if(objekti.rotuSkaitsIzv!=0){
        bool irUzbrucis = false;
        objekti.noklikBlakusState.GetComponent<SpelesKontrole>().valsts.speletajs = Valstis.Speletaji.PLAYER;
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
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