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
    public Pogas pogas;
    public List<GameObject> valstsSaraksts = new List<GameObject>();

    public Color32 vecaKrasa;
    public Color32 hoverKrasa;

    public AI ai;

    
    //Mainīgie    
    int skaits;

    public Teksts  teksts;
    public Dictionary<Valstis.Speletaji, int> rotasSkaitsByPlayer = new Dictionary<Valstis.Speletaji, int>()
    {
        { Valstis.Speletaji.LSPR, 0 },
        { Valstis.Speletaji.PLAYER, 0 }
    };


    void Start(){
        objekti = FindObjectOfType<Objekti>();
        ai = FindObjectOfType<AI>();
        pogas = FindAnyObjectByType<Pogas>();
        teksts = FindAnyObjectByType<Teksts>();
             objekti.lietotajuKarta = true;
             objekti.otraSpeletajaKarta = false;
            objekti.vaiIrEsc = false;
            SpelesKontrole stateController = GameObject.Find("States_1").GetComponent<SpelesKontrole>();
            objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state1Pozicijas");

            if (stateController.valsts.speletajs == Valstis.Speletaji.PLAYER && !objekti.vaiIrSakumaRotas)    
            {
                sakumaRotas(stateController, objekti.rotasPozicijas, objekti.rotasPrefs, Valstis.Speletaji.PLAYER, 5);
            }

            SpelesKontrole stateController1 = GameObject.Find("States_2").GetComponent<SpelesKontrole>();
            if (stateController1.valsts.speletajs == Valstis.Speletaji.PLAYER && !objekti.vaiIrSakumaRotas)    
            {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state2Pozicijas");
                sakumaRotas(stateController1, objekti.rotasPozicijas, objekti.rotasPrefs, Valstis.Speletaji.PLAYER, 3);
            }

             objekti.vaiIrSakumaRotas = true;


            SpelesKontrole stateController2 = GameObject.Find("States_3").GetComponent<SpelesKontrole>();
            if (stateController2.valsts.speletajs == Valstis.Speletaji.LSPR && !objekti.vaiIrSakumaRotasLSPR)    
            {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state3Pozicijas");
                sakumaRotas(stateController2, objekti.rotasPozicijas, objekti.rotasPrefsLSPR, Valstis.Speletaji.LSPR, 1);
              
            }


               SpelesKontrole stateController4 = GameObject.Find("States_4").GetComponent<SpelesKontrole>();
            if (stateController4.valsts.speletajs == Valstis.Speletaji.LSPR && !objekti.vaiIrSakumaRotasLSPR)    
            {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state4Pozicijas");
                sakumaRotas(stateController4, objekti.rotasPozicijas, objekti.rotasPrefsLSPR, Valstis.Speletaji.LSPR, 1);
              
            }

               SpelesKontrole stateController5 = GameObject.Find("States_5").GetComponent<SpelesKontrole>();
            if (stateController5.valsts.speletajs == Valstis.Speletaji.LSPR && !objekti.vaiIrSakumaRotasLSPR)    
            {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state5Pozicijas");
                sakumaRotas(stateController5, objekti.rotasPozicijas, objekti.rotasPrefsLSPR, Valstis.Speletaji.LSPR, 2);
              
            }
                  objekti.vaiIrSakumaRotasLSPR = true;
        objekti.LatvijasKarogs.gameObject.SetActive(true);
        objekti.LSPRKarogs.gameObject.SetActive(false);
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

        Debug.Log("State Object: " + stateObject + " Distance: " + distance);

        if (stateController != null && stateController.valsts.speletajs == Valstis.Speletaji.LSPR && distance < 1.96f)
        {
            stateController.tintesKrasa(new Color32(255, 10, 0, 255));
            if(stateObject == objekti.noklikState){
                stateController.tintesKrasa(new Color32(243, 43, 43, 235));
            } 
            if(objekti.lietotajuKarta == true){
            objekti.vaiIrIzveleUzbr = true;
            }else if(objekti.otraSpeletajaKarta == true){
             objekti.vaiIrIzveleKust = true;
            }
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
            if(objekti.lietotajuKarta == true){
            objekti.vaiIrIzveleKust = true;
            }else if(objekti.otraSpeletajaKarta == true){
             objekti.vaiIrIzveleUzbr = true;
            }
        }
    }

public SpelesKontrole Atpaksanas(SpelesKontrole kontrole, Valstis.Speletaji speletajs) //Funkcija, kas atrod teritoriju, kurā atkāpties, ja ir zaudēta cīņa
{
    // Iegūst visu Valsts objektu sarakstu
    GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");

    //Iegūst kontroles objekta robežu izmērus
    Bounds territoryBounds = kontrole.GetComponent<PolygonCollider2D>().bounds;
    float territoryWidth = territoryBounds.size.x;
    float territoryHeight = territoryBounds.size.y;
    objekti.irIelenkti = false;

    //Pārbaudes mainīgie
    bool irAtrasts = false;  // Vai ir atrasta teritorija atkāpšanai
    bool navAtrasts = false; 

    // Objektu nobīdes vektors robežu atkāpei
    Vector2 offset = new Vector2(territoryWidth * 0.001f, territoryHeight * 0.001f);

    //Foreach pārbaude, kas pārmeklē visas state objektus
    foreach (GameObject stateObject in visiStates)
    {
        SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();

        // Aprēķināt attālumu starp state kontroles objektu un valsts objektu
        float distance = Vector2.Distance(kontrole.transform.position, stateObject.transform.position - (Vector3)offset);

        // Pārbaudīt vai atrasta valsts atkāpšanai pietiekami tuva un vai teritorija rotas ir mazāk par 0
        if (stateController != null && stateController.valsts.speletajs == speletajs && distance < 1.96f && 
            stateController.rotasSkaitsByPlayer[speletajs] == 0 && kontrole!=stateController)
        {
            objekti.atpakpesState = stateObject.GetComponent<SpelesKontrole>();
            objekti.stateAtkapes = stateObject;
            // Debug.Log("Pašreizējais skaits: " + stateController.rotasSkaitsByPlayer[speletajs]);
            irAtrasts = true;
        }
    }

        //Ja nav teritorijas kurā atkāpties, kur nav 0 rotas, tad iziet ciklu vēlreiz.
    if (!irAtrasts)
    {
        foreach (GameObject stateObject in visiStates)
        {
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();

            // Aprēķināt attālumu starp kontroles objektu un Valsts objektu
            float distance = Vector2.Distance(kontrole.transform.position, stateObject.transform.position - (Vector3)offset);

            // Pārbaudīt vai atrasta valsts atkāpšanai (mazāk par 5 rotām un pietiekami tuva)
            if (stateController != null && 
                stateController.valsts.speletajs == speletajs && 
                distance < 1.96f && 
                stateController.rotasSkaitsByPlayer[speletajs] < 5
                && kontrole!=stateController)
            {
                objekti.atpakpesState = stateObject.GetComponent<SpelesKontrole>();
                objekti.stateAtkapes = stateObject;
                // Debug.Log("Pašreizējais skaits: " + stateController.rotasSkaitsByPlayer[speletajs]);
                irAtrasts = true;
                navAtrasts = true;
            }
        }
    }

    // Ja nav atrasta ne tukša, ne valsts ar vismaz vienu vienību, tad rotas ir ielenktas
    if (!irAtrasts && !navAtrasts)
    {
        objekti.irIelenkti = true;
        Debug.Log("Objekti ir ielenkti: " + objekti.irIelenkti);
    }
    Debug.Log("Atkāpsies uz: " + objekti.atpakpesState);
        return objekti.atpakpesState;
    }

    public void atgriezPretiniekuKrasas() //Šī funkcija atgriež krāsas, kad vairs nav neviena darbība, vai ir bijusi kāda darbība, vai kautkas ir mainījies
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


    public void atgriezLietotajuKrasas() //Šī funkcija atgriež krāsas, kad vairs nav neviena darbība, vai ir bijusi kāda darbība, vai kautkas ir mainījies
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

        if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.PLAYER && objekti.vaiIrIzveleUzbr == false && objekti.vaiIrIzveleKust == false && 
        objekti.apraksts == false && objekti.lietotajuKarta == true && objekti.LSPRUzvarejis == false && objekti.playerUzvarejis == false && objekti.vaiIrEsc == false)
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
            objekti.bridinajumaTeksts.gameObject.SetActive(false);
            atgriezPretiniekuKrasas();
            atgriezLietotajuKrasas();
            AudioSistema.Instance.speletSFX("staties");
        }
       if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.LSPR && objekti.vaiIrIzveleUzbr == true && objekti.lietotajuKarta == true && sprite.color == new Color32(255, 10, 0, 235) && objekti.vaiIrEsc == false) {
            //Debug.Log("Collider hit: " + hit.collider.name);
            objekti.noklikBlakusState = hit.collider.gameObject;
            objekti.kurVietaUzbrukt.gameObject.SetActive(false);
            objekti.uzbrukt.gameObject.SetActive(true);
            objekti.skaits.gameObject.SetActive(true);
            objekti.plusUzb.gameObject.SetActive(true);
            objekti.minusUzb.gameObject.SetActive(true);
            objekti.rotuSkaitsIzv = 0;
            atgriezPretiniekuKrasas();
        }
    


         if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.PLAYER && objekti.vaiIrIzveleKust == true && objekti.lietotajuKarta == true && sprite.color == new Color32(196, 255, 134, 170)  && objekti.vaiIrEsc == false){
          objekti.noklikBlakusState = hit.collider.gameObject;
            objekti.kustinat.gameObject.SetActive(true);
            objekti.kurVietaKustinat.gameObject.SetActive(false);
            objekti.skaits.gameObject.SetActive(true);
            objekti.plusParvietot.gameObject.SetActive(true);
            objekti.minusParvietot.gameObject.SetActive(true);
            objekti.rotuSkaitsIzv = 0;
            atgriezLietotajuKrasas();
         }
         if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.LSPR && objekti.vaiIrIzveleKust == true && objekti.otraSpeletajaKarta == true && sprite.color == new Color32(255, 10, 0, 235) && objekti.vaiIrEsc == false){
          objekti.noklikBlakusState = hit.collider.gameObject;
            objekti.kustinat.gameObject.SetActive(true);
            objekti.kurVietaKustinat.gameObject.SetActive(false);
            objekti.skaits.gameObject.SetActive(true);
            objekti.plusParvietot.gameObject.SetActive(true);
            objekti.minusParvietot.gameObject.SetActive(true);
            objekti.rotuSkaitsIzv = 0;
            atgriezPretiniekuKrasas();
         }

         if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.LSPR && objekti.vaiIrIzveleUzbr == false && objekti.vaiIrIzveleKust == false
          && objekti.apraksts == false && objekti.lietotajuKarta == false && objekti.otraSpeletajaKarta == true && objekti.LSPRUzvarejis == false && objekti.playerUzvarejis == false && objekti.vaiIrEsc == false)
          { 
            objekti.noklikState = hit.collider.gameObject;
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
            objekti.bridinajumaTeksts.gameObject.SetActive(false);
            atgriezPretiniekuKrasas();
            atgriezLietotajuKrasas();
        }

       if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.PLAYER && objekti.vaiIrIzveleUzbr == true && objekti.otraSpeletajaKarta == true 
       && sprite.color == new Color32(196, 255, 134, 170) && objekti.vaiIrEsc == false) {
            objekti.noklikBlakusState = hit.collider.gameObject;
            objekti.kurVietaUzbrukt.gameObject.SetActive(false);
            objekti.uzbrukt.gameObject.SetActive(true);
            objekti.skaits.gameObject.SetActive(true);
            objekti.plusUzb.gameObject.SetActive(true);
            objekti.minusUzb.gameObject.SetActive(true);
            objekti.rotuSkaitsIzv = 0;
            atgriezLietotajuKrasas();
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

    public void rotuAtkapsanas(Valstis.Speletaji speletajs){
        //Atrod visus spēles objektus ar tagu "Valsts" un iegūstam SpelesKontrole komponentu.
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        SpelesKontrole noklikBlakusState = objekti.noklikBlakusState.GetComponent<SpelesKontrole>();
        //Atiestatām mainīgos, kas saistīti ar rotu pozīcijām un skaitu
        objekti.rotasPozicijas = null;
            int rotasSkaits = 0;
            int atkapsanasSkaits = 0;
             //Izsaucam Atkāpšanās funkciju, lai sagatavotu atkāpšanās procesu.
             noklikBlakusState.Atpaksanas(noklikBlakusState, speletajs);
             //Ar for ciklu saņems informāciju, lai dabūtu rotas pozīcijas pareizās vietās
                for(int i=0; i<=visiStates.Length; i++){
                 if(objekti.noklikBlakusState == GameObject.Find("States_"+i)){
                //Dabū rotas atkāpšanās pozīcijas
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }

                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateObject.Equals(objekti.noklikBlakusState))
                {
                    //Pārbaudā, vai spēlētājs ir pirmais lietotājs.
                    if(pogas.speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){

                    //Šeit pārbaudīs visus pretinieku/lietotāju zaudējumus
                    rotasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    Debug.Log("Rotas skaits pretinieka: "+rotasSkaits);
                    if(objekti.rotuSkaitsIzv >= 1 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 1){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    }else if(objekti.rotuSkaitsIzv == 2 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    }else if(objekti.rotuSkaitsIzv == 3 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    }else if(objekti.rotuSkaitsIzv == 4 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] - 1;
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] - 1;
                    }else if(objekti.rotuSkaitsIzv == 3 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 3){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    }else if(objekti.rotuSkaitsIzv == 4 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 3){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] - 1;
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 3){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] - 1;
                    }else if(objekti.rotuSkaitsIzv== 4 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 4){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 4){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] == 5){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
                    }

                    Debug.Log("474.rindaa Atkāpšanās skaits: "+atkapsanasSkaits);
                    //Pārbauda, vai spēlētājs ir otrais lietotājs.
                    }else if(pogas.speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                    //Šeit pārbaudīs visus pretinieku/lietotāju zaudējumus
                    rotasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]; 
                   if(objekti.rotuSkaitsIzv>= 1 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 1){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(objekti.rotuSkaitsIzv == 2 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(objekti.rotuSkaitsIzv == 3 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(objekti.rotuSkaitsIzv == 4 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(objekti.rotuSkaitsIzv == 3 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 3){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(objekti.rotuSkaitsIzv == 4 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 3){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 3){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(objekti.rotuSkaitsIzv == 4 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 4){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 4){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(objekti.rotuSkaitsIzv == 5 && noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 5){
                        atkapsanasSkaits = noklikBlakusState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }
                    Debug.Log("Atkāpšanās skaits: "+atkapsanasSkaits);
                    if(atkapsanasSkaits < 0){
                    atkapsanasSkaits = Mathf.Max(atkapsanasSkaits, 0);
                    }
                    }
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (stateObject == objekti.noklikBlakusState){
                        Debug.Log("Skaits pretinieka: "+rotasSkaits); 
                        //Šajā for loopā izdzesīs visas rotas, kuras atkāpās
                         foreach (Transform child in objekti.noklikBlakusState.transform){
                            //Pārbauda vai ir pirmā lietotāja kārta.
                         if(pogas.speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){  
                         if (child.CompareTag("LSPRTroop") && rotasSkaits > 0){
                            //Izdzēšs rotas objektu no kartes.
                            Destroy(child.gameObject);
                            rotasSkaits--;
                            Debug.Log("Nostrādā 440rinda");
                            stateController.NonemtRotas(speletajs, 1);
                            //Noņem no izmantotām pozīcījām atkāpušās rotas.
                            objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                          }
                          //Pārbauda vai ir otrā lietotāja kārta.
                         }else if(pogas.speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                         if (child.CompareTag("PLAYERTroop") && rotasSkaits > 0){
                            Destroy(child.gameObject);
                            rotasSkaits--;
                            Debug.Log("Nostrādā 446rinda");
                            stateController.NonemtRotas(speletajs, 1);
                            objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                          }
                         }
                        }
                     }            

                }
                Debug.Log("Atkāpšanās skaits 455rinda: "+atkapsanasSkaits);
                //Debug.Log(objekti.atpakpesState);
                }
            }
            //Ja rotas nav ielenktas, tad veic atkāpšanos.
            if(!objekti.irIelenkti){
                 objekti.rotasPozicijas = null;
                 //Nosaka, no kuras state atkāpsies un dabūs rotas pozīcijas.
                for(int i=0; i<=visiStates.Length; i++){
                 if(objekti.stateAtkapes == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
                foreach (GameObject stateObject in visiStates){
                 SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                  if (stateController.valsts.speletajs == speletajs && stateObject.Equals(objekti.stateAtkapes)){
                    //Pārbaudām katru iespējamo pozīciju atkāpšanās valstī.
                 for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                     //Pārbaudām, vai pozīcija ir brīva
                    bool pozicijaAiznemta = false;
                        foreach (Transform child in objekti.stateAtkapes.transform) {
                            if (child.transform.position == objekti.rotasPozicijas[i].transform.position) {
                                pozicijaAiznemta = true;
                                Debug.Log("Šis aizgāja");
                                break;
                            }
                        }
                        // Ja pozīcija ir brīva un ir atkāpšanās iespējas, izveidojam jaunu rotu. 
                        if (!pozicijaAiznemta && atkapsanasSkaits > 0 && stateController.rotasSkaitsByPlayer[speletajs] !=5){
                            //Parādīsies jaunas rotas, atkarībā kurš no lietotājiem tikko atkāpās. 
                        if(pogas.speletaji == Valstis.Speletaji.PLAYER && objekti.lietotajuKarta){
                        Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, 
                        Quaternion.identity, objekti.stateAtkapes.transform);
                        
                        }else if(pogas.speletaji == Valstis.Speletaji.LSPR && objekti.otraSpeletajaKarta){
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, 
                        Quaternion.identity, objekti.stateAtkapes.transform);
                        }
                        //Pievieno jaunas rotas izmantotās pozīcījās
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        //Ar šo funkciju kontrolē katrā state skaitu
                        stateController.PievienotRotas(speletajs, 1);
                        atkapsanasSkaits--;
                        Debug.Log("Lietotājs atkāpjas");
                    }

                }                  
             }
                }
        }else{
            //Ja rotas ir ielenktas, tad visas rotas, kas zaudēja cīņu, tika iznīcinātas
            noklikBlakusState.NonemtRotas(speletajs, noklikBlakusState.rotasSkaitsByPlayer[speletajs]);
            Debug.Log("Rotas bija ielenktas, un visas tika izdzēstas");
        }
    }

 public void rotuAtkapsanasPlayer(Valstis.Speletaji speletajs, int LSPRskaits){
    //Atrod visus spēles objektus ar tagu "Valsts" un iegūstam SpelesKontrole komponentu.
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        SpelesKontrole uzbruksanasState = objekti.uzbruksanasState.GetComponent<SpelesKontrole>();
        objekti.rotasPozicijas = null;
            int rotasSkaits = 0;
            int atkapsanasSkaits = 0;
             objekti.atpakpesState = null;
            objekti.stateAtkapes = null;
             uzbruksanasState.Atpaksanas(uzbruksanasState, speletajs);
            for(int i=0; i<=visiStates.Length; i++){
                 if(objekti.uzbruksanasState == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }

                foreach (GameObject stateObject in visiStates){
                  SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateObject.Equals(objekti.uzbruksanasState))
                {
            
                   if(LSPRskaits>= 1 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 1){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(LSPRskaits == 2 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(LSPRskaits == 3 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(LSPRskaits == 4 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(LSPRskaits == 5 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 2){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(LSPRskaits == 3 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 3){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(LSPRskaits == 4 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 3){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(LSPRskaits == 5 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 3){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] - 1;
                    }else if(LSPRskaits == 4 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 4){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(LSPRskaits == 5 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 4){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }else if(LSPRskaits == 5 && uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] == 5){
                        atkapsanasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER];
                    }
                    Debug.Log("Atkāpšanās skaits: "+atkapsanasSkaits);
                    if(atkapsanasSkaits < 0){
                    atkapsanasSkaits = Mathf.Max(atkapsanasSkaits, 0);
                    }
                    rotasSkaits = uzbruksanasState.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER]; 
                      for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                        if (stateObject == objekti.uzbruksanasState){
                        Debug.Log("Skaits pretinieka: "+rotasSkaits); 
                         foreach (Transform child in objekti.uzbruksanasState.transform){
                         if (child.CompareTag("PLAYERTroop") && rotasSkaits > 0){
                            Destroy(child.gameObject);
                            rotasSkaits--;
                            Debug.Log("Nostrādā 446rinda");
                            stateController.NonemtRotas(speletajs, 1);
                            objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                          }
                         }
                        
                     }            

                }
                Debug.Log("Atkāpšanās skaits 455rinda: "+atkapsanasSkaits);
                }
            }
            if(!objekti.irIelenkti){
                 objekti.rotasPozicijas = null;
                for(int i=0; i<=visiStates.Length; i++){
                 if(objekti.stateAtkapes == GameObject.Find("States_"+i)){
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                }
                }
                foreach (GameObject stateObject in visiStates){
                 SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                  if (stateController.valsts.speletajs == speletajs && stateObject.Equals(objekti.stateAtkapes)){
                 for(int i=0; i<objekti.rotasPozicijas.Length; i++){
                    bool pozicijaAiznemta = false;
                        foreach (Transform child in objekti.stateAtkapes.transform) {
                            if (child.transform.position == objekti.rotasPozicijas[i].transform.position) {
                                pozicijaAiznemta = true;
                                Debug.Log("Šis aizgāja");
                                break;
                            }
                        } 
                        if (!pozicijaAiznemta && atkapsanasSkaits > 0 && stateController.rotasSkaitsByPlayer[Valstis.Speletaji.PLAYER] !=5){ 
                        Instantiate(objekti.rotasPrefs, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.stateAtkapes.transform);       
                        objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                        stateController.PievienotRotas(speletajs, 1);
                        atkapsanasSkaits--;
                        Debug.Log("Lietotājs atkāpjas");
                        }

                }                  
             }
                }
        }else{
            uzbruksanasState.NonemtRotas(speletajs, uzbruksanasState.rotasSkaitsByPlayer[speletajs]);
            Debug.Log("Rotas bija ielenktas, un visas tika izdzēstas");
        }
    }

        public void AIAtkapsanas(GameObject noKuraStateLSPR){
        // Atrodam visus spēles objektus ar tagu "Valsts".
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");

        // Iegūstam SpelesKontrole komponentu valstij, no kuras LSPR atkāpjas.
        SpelesKontrole LSPRState = noKuraStateLSPR.GetComponent<SpelesKontrole>();

        // Atiestatām mainīgos, kas saistīti ar rotu pozīcijām un skaitu.
        objekti.rotasPozicijas = null;
        int rotasSkaits = 0;
        int atkapsanasSkaits = 0;
        objekti.atpakpesState = null;
        objekti.stateAtkapes = null;

        // Sagatavojam atkāpšanās procesu.
        LSPRState.Atpaksanas(LSPRState, Valstis.Speletaji.LSPR);

        // Nosakām, no kuras valsts LSPR atkāpjas, un atrodam visas iespējamās rotu pozīcijas šajā valstī.
        for (int i = 0; i <= visiStates.Length; i++) {
            if (noKuraStateLSPR == GameObject.Find("States_" + i)) {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state" + i + "Pozicijas");
            }
        }

        // Iegūstam kopējo rotu skaitu valstī, no kuras LSPR atkāpjas, un piešķiram to atkāpšanās skaitam.
        rotasSkaits = LSPRState.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR];
        atkapsanasSkaits = rotasSkaits;

        // Noņemam visas LSPR rotas no valsts, no kuras tiek atkāptos.
        foreach (Transform child in LSPRState.transform) {
            if (child.CompareTag("LSPRTroop")) {
                Destroy(child.gameObject); 
                rotasSkaits--;           
                LSPRState.NonemtRotas(Valstis.Speletaji.LSPR, 1); //Atjauninam informāciju par rotu skaitu.
                //Atbrīvojam izmantotās pozīcijas.
                for (int i = 0; i < objekti.rotasPozicijas.Length; i++) {
                    objekti.izmantotasPozicijas.Remove(objekti.rotasPozicijas[i]);
                }
            }
        }
                    Debug.Log("Atkāpšanās skaits 455rinda: "+atkapsanasSkaits);
                if(!objekti.irIelenkti){
                    objekti.rotasPozicijas = null;
                    for(int i=0; i<=visiStates.Length; i++){
                    if(objekti.stateAtkapes == GameObject.Find("States_"+i)){
                    objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state"+i+"Pozicijas");
                    }
                    }
                            //Pārbaudām katru valsti un veicam atkāpšanās loģiku.
            foreach (GameObject stateObject in visiStates) {
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                //Ja valsts pieder LSPR un ir izvēlētā atkāpšanās valsts, tad:
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateObject.Equals(objekti.stateAtkapes)) {
                    //Pārbaudām katru iespējamo pozīciju atkāpšanās valstī.
                    for (int i = 0; i < objekti.rotasPozicijas.Length; i++) {
                        //Pārbaudām, vai pozīcija ir brīva.
                        bool pozicijaAiznemta = false;
                        foreach (Transform child in objekti.stateAtkapes.transform) {
                            if (child.transform.position == objekti.rotasPozicijas[i].transform.position) {
                                pozicijaAiznemta = true;
                                break;
                            }
                        }

                        //Ja pozīcija ir brīva un ir atkāpšanās iespējas, izveidojam jaunu LSPR rotu.
                        if (!pozicijaAiznemta && atkapsanasSkaits > 0) {
                            Instantiate(objekti.rotasPrefsLSPR, objekti.rotasPozicijas[i].transform.position, Quaternion.identity, objekti.stateAtkapes.transform);
                            objekti.izmantotasPozicijas.Add(objekti.rotasPozicijas[i]);
                            stateController.PievienotRotas(Valstis.Speletaji.LSPR, 1);
                            atkapsanasSkaits--;
                        }
                    }
                }
            }
        }

    }

}
