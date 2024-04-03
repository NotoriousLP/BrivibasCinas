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
         objekti.vaiIrIzvele = false;
    }

    public void mobilizetPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.mobilizet.gameObject.SetActive(true);
        objekti.plus.gameObject.SetActive(true);
        objekti.minus.gameObject.SetActive(true);
        objekti.skaits.gameObject.SetActive(true);
        objekti.rotuSkaitsIzv = 0;

    }

    public void kustinatPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.kurVietaKustinat.gameObject.SetActive(true);
    }

    public void uzbruktPoga()
    {
        objekti.izvele.gameObject.SetActive(false);
        objekti.kurVietaUzbrukt.gameObject.SetActive(true);
        kontrole.iekrasoBlakusTeritoriju(objekti.noklikState);
    }

    public void plusPoga()
    {
        if(objekti.rotuSkaits > 0 && objekti.rotuSkaits <= 5 && objekti.rotuSkaitsIzv >= 0 && objekti.rotuSkaitsIzv < objekti.rotuSkaits)
        {
            objekti.rotuSkaitsIzv++;
        }
    }

    public void minusPoga()
    {
        if (objekti.rotuSkaits > 0 && objekti.rotuSkaits <= 5 && objekti.rotuSkaitsIzv > 0 && objekti.rotuSkaitsIzv <= objekti.rotuSkaits)
        {
            objekti.rotuSkaitsIzv--;
        }
    }
    GameObject[] rotasPozicijas;
    int izmantotasPozicijas;
    public void mobilizetRotas(){
        if(objekti.rotuSkaits != 0 && objekti.rotuSkaitsIzv !=0){
                if(objekti.noklikState == GameObject.Find("States_1")){
                 rotasPozicijas = GameObject.FindGameObjectsWithTag("state1Pozicijas");
                }else if(objekti.noklikState == GameObject.Find("States_2")){
                 rotasPozicijas = GameObject.FindGameObjectsWithTag("state2Pozicijas");
                }else if(objekti.noklikState == GameObject.Find("States_3")){
                 rotasPozicijas = GameObject.FindGameObjectsWithTag("state3Pozicijas");
                }

            for(int i=0; i<rotasPozicijas.Length; i++){
            if (!objekti.izmantotasPozicijas.Contains(rotasPozicijas[i]) && objekti.rotuSkaitsIzv > 0){ 
            Instantiate(objekti.rotasPrefs, rotasPozicijas[i].transform.position, Quaternion.identity, objekti.noklikState.transform);
            objekti.izmantotasPozicijas.Add(rotasPozicijas[i]);
            objekti.rotuSkaitsIzv--;
            }
            }
        }
    }


    public void okPogaUzbrukt(){
        objekti.noklikBlakusState.GetComponent<SpelesKontrole>().valsts.speletajs = Valstis.Speletaji.PLAYER;
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER)
            {
                stateController.tintesKrasa(new Color32(139, 221, 51, 255));
            }
        }
        objekti.vaiIrIzvele = false;
        objekti.izvelesLauks.gameObject.SetActive(false);
        objekti.izvele.gameObject.SetActive(false);
        objekti.uzbrukt.gameObject.SetActive(false);
        objekti.plus.gameObject.SetActive(false);
        objekti.minus.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
    }

    public void atpakalUzIzveli()
    {
        objekti.izvele.gameObject.SetActive(true);
        objekti.kurVietaUzbrukt.gameObject.SetActive(false);
        objekti.uzbrukt.gameObject.SetActive(false);
        objekti.kurVietaKustinat.gameObject.SetActive(false);
        objekti.kustinat.gameObject.SetActive(false);
        objekti.mobilizet.gameObject.SetActive(false);
        objekti.plus.gameObject.SetActive(false);
        objekti.minus.gameObject.SetActive(false);
        objekti.skaits.gameObject.SetActive(false);
        kontrole.atgriezPretiniekuKrasas();
        objekti.vaiIrIzvele = false;
    }


}