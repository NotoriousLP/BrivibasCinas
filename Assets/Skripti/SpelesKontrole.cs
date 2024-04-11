using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]

public class SpelesKontrole : MonoBehaviour
{

    public Valstis valsts;
    public ValstsParvalde parvalde;
    public Objekti objekti;
    private SpriteRenderer sprite;

    public List<GameObject> valstsSaraksts = new List<GameObject>();

    public Color32 vecaKrasa;
    public Color32 hoverKrasa;

    public AI ai;

    
    //Mainīgie
    public Dictionary<Valstis.Speletaji, int> rotasSkaitsByPlayer = new Dictionary<Valstis.Speletaji, int>()
    {
        { Valstis.Speletaji.LSPR, 0 },
        { Valstis.Speletaji.PLAYER, 0 }
    };




    void Start(){
        objekti.lietotajuKarta = true;
        objekti = FindObjectOfType<Objekti>();
        ai = FindObjectOfType<AI>();
            SpelesKontrole stateController = GameObject.Find("States_1").GetComponent<SpelesKontrole>();
            objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state1Pozicijas");
            if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && !objekti.vaiIrSakumaRotas)    
            {
                sakumaRotas(stateController, objekti.rotasPozicijas, objekti.rotasPrefs, Valstis.Speletaji.PLAYER, 3);
                objekti.vaiIrSakumaRotas = true;
            }

            SpelesKontrole stateController1 = GameObject.Find("States_2").GetComponent<SpelesKontrole>();
            if (stateController1.valsts.speletajs == Valstis.Speletaji.LSPR && !objekti.vaiIrSakumaRotasLSPR)    
            {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state2Pozicijas");
                sakumaRotas(stateController1, objekti.rotasPozicijas, objekti.rotasPrefsLSPR, Valstis.Speletaji.LSPR, 2);
              
            }
                        
            SpelesKontrole stateController2 = GameObject.Find("States_3").GetComponent<SpelesKontrole>();
            if (stateController2.valsts.speletajs == Valstis.Speletaji.LSPR && !objekti.vaiIrSakumaRotasLSPR)    
            {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state3Pozicijas");
                sakumaRotas(stateController2, objekti.rotasPozicijas, objekti.rotasPrefsLSPR, Valstis.Speletaji.LSPR, 2);
              
            }
                  objekti.vaiIrSakumaRotasLSPR = true;


        }


    public void PievienotRotas(Valstis.Speletaji speletajs, int count)
    {
        if (rotasSkaitsByPlayer.ContainsKey(speletajs))
        {
            rotasSkaitsByPlayer[speletajs] += count;
        }

    }
    
    public void NonemtRotas(Valstis.Speletaji speletajs, int count)
    {
        if (rotasSkaitsByPlayer.ContainsKey(speletajs))
        {
            rotasSkaitsByPlayer[speletajs] -= count;
        }
    }


    public void sakumaRotas(SpelesKontrole stateController, GameObject[] pozicijasRotas, GameObject prefs, Valstis.Speletaji speletajs, int skaits){
            rotasSkaitsByPlayer[speletajs] = skaits;
            if (rotasSkaitsByPlayer[speletajs] <= 5){
                   for(int i=0; i<pozicijasRotas.Length; i++){
                    if (!objekti.izmantotasPozicijas.Contains(pozicijasRotas[i]) && rotasSkaitsByPlayer[speletajs] > 0 && rotasSkaitsByPlayer[speletajs] <=5){ 
                    Instantiate(prefs, pozicijasRotas[i].transform.position, Quaternion.identity, stateController.transform);
                    objekti.izmantotasPozicijas.Add(pozicijasRotas[i]);
                    rotasSkaitsByPlayer[speletajs]--;
                    stateController.PievienotRotas(speletajs, 1);
                    }
                   }
        }

    }


    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && objekti.vaiIrEsc == false)
        {
            objekti.ESCMenu.gameObject.SetActive(true);
            objekti.fons.gameObject.SetActive(true);
            objekti.vaiIrEsc = true;
        }else if (Input.GetKeyDown(KeyCode.Escape) && objekti.vaiIrEsc == true)
        {
            objekti.ESCMenu.gameObject.SetActive(false);
            objekti.fons.gameObject.SetActive(false);
            objekti.vaiIrEsc = false;
        }


    }

    void OnMouseEnter()
    {
        vecaKrasa = sprite.color;
         if (valsts.speletajs == Valstis.Speletaji.PLAYER)
        {
              hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 170);
         }
        if (valsts.speletajs == Valstis.Speletaji.LSPR)
        {
            hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 235);
        }
        sprite.color = hoverKrasa;
    }

public void iekrasoBlakusPretiniekuTeritoriju(GameObject noklikState)
{
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");

    Bounds territoryBounds = noklikState.GetComponent<PolygonCollider2D>().bounds;
    float territoryWidth = territoryBounds.size.x;
    float territoryHeight = territoryBounds.size.y;
    Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);

    foreach (GameObject stateObject in visiStates)
    {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        float distance = Vector2.Distance(noklikState.transform.position, stateObject.transform.position - (Vector3)offset);

        // Debug.Log("State Object: " + stateObject + " Distance: " + distance);

        if (stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && distance < 1.96f)
        {
            //Debug.Log("Nokrasojas State Object: " + stateObject + " Distance: " + blakusState);
            //Debug.Log("Nokrasojas Distance: " + distance);
            stateController.tintesKrasa(new Color32(255, 10, 0, 255)); // Highlight color
            objekti.vaiIrIzveleUzbr = true;
        }
    }
}


        public void iekrasoBlakusLietotajuTeritoriju(GameObject noklikState)
    {
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");

        Bounds territoryBounds = noklikState.GetComponent<PolygonCollider2D>().bounds;
        float territoryWidth = territoryBounds.size.x;
        float territoryHeight = territoryBounds.size.y;
        Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);
        foreach (GameObject stateObject in visiStates)
        {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            float distance = Vector2.Distance(noklikState.transform.position, stateObject.transform.position - (Vector3)offset);
         
            if (stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && distance < 1.96f)
            {
                stateController.tintesKrasa(new Color32(196, 255, 134, 217));
                if(stateObject == objekti.noklikState){
                    stateController.tintesKrasa(new Color32(139, 221, 51, 255));
                }
                
            }
            objekti.vaiIrIzveleKust = true;
        }
    }

    public SpelesKontrole Atpaksanas(SpelesKontrole kontrole){
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
        float territoryWidth = territoryBounds.size.x;
        float territoryHeight = territoryBounds.size.y;
        Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);
        foreach (GameObject stateObject in visiStates)
        {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
        
            float distance = Vector2.Distance(kontrole.transform.position, stateObject.transform.position - (Vector3)offset);
         
            if (stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && distance < 1.96f && stateController != objekti.noklikBlakusState  && 
            stateController != objekti.noklikState && objekti.atpakpesState == null && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 0)
            {
                 objekti.atpakpesState = stateObject.GetComponent<SpelesKontrole>();
                 objekti.stateAtkapes = stateObject;
            }
        }
        Debug.Log(objekti.atpakpesState);
        return objekti.atpakpesState;
    }

    public void atgriezPretiniekuKrasas()
    {
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR)
            {
                stateController.tintesKrasa(new Color32(243, 43, 43, 235));
            }
        }
    }


    public void atgriezLietotajuKrasas()
    {
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER)
            {
                stateController.tintesKrasa(new Color32(139, 221, 51, 210));
            }
        }
    }


    void OnMouseDown()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //Debug.Log(objekti.irIzvelesLauksIeslegts);

        if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.PLAYER && objekti.vaiIrIzveleUzbr == false && objekti.vaiIrIzveleKust == false && objekti.lietotajuKarta == true)
        {
            objekti.noklikState = hit.collider.gameObject;
            //Debug.Log("Collider hit: " + hit.collider.name);
            objekti.izvelesLauks.gameObject.SetActive(true);
            objekti.izvele.gameObject.SetActive(true);
            objekti.irIzvelesLauksIeslegts = true;
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
        }
       if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.LSPR && objekti.vaiIrIzveleUzbr == true && objekti.lietotajuKarta == true) {
            //Debug.Log("Collider hit: " + hit.collider.name);
            objekti.noklikBlakusState = hit.collider.gameObject;
            objekti.kurVietaUzbrukt.gameObject.SetActive(false);
            objekti.uzbrukt.gameObject.SetActive(true);
            objekti.skaits.gameObject.SetActive(true);
            objekti.plusUzb.gameObject.SetActive(true);
            objekti.minusUzb.gameObject.SetActive(true);
            objekti.rotuSkaitsIzv = 0;
            GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
            foreach (GameObject stateObject in visiStates)
            {
                    SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                    if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR)
                    {
                        stateController.tintesKrasa(new Color32(243, 43, 43, 235));
                    }
            }


        }
         if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.PLAYER && objekti.vaiIrIzveleKust == true && objekti.lietotajuKarta == true){
          objekti.noklikBlakusState = hit.collider.gameObject;
            objekti.kustinat.gameObject.SetActive(true);
            objekti.kurVietaKustinat.gameObject.SetActive(false);
            objekti.skaits.gameObject.SetActive(true);
            objekti.plusParvietot.gameObject.SetActive(true);
            objekti.minusParvietot.gameObject.SetActive(true);
            objekti.rotuSkaitsIzv = 0;
            atgriezLietotajuKrasas();
            GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
            foreach (GameObject stateObject in visiStates)
            {
                    SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                    if (stateObject == objekti.noklikBlakusState)
                    {
                        stateController.tintesKrasa(new Color32(139, 221, 51, 210));
                    }
            }
         }
    }

    void OnMouseExit()
    {
        sprite.color = vecaKrasa;
    }

     void OnDrawGizmos()
    {
        valsts.nosaukums = name;
        this.tag = "Valsts";
    }

     public void tintesKrasa(Color32 krasa)
    {
        sprite.color = krasa;
    }

    public static explicit operator GameObject(SpelesKontrole v)
    {
        throw new NotImplementedException();
    }
}
