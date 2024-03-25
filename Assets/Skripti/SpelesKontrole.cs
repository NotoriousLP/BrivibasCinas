using System.Collections;
using System.Collections.Generic;
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
    //Mainīgie

    void Start(){
        objekti = FindObjectOfType<Objekti>();
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
         if (valsts.speletajs == Valstis.Speletaji.PLAYER){
              hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 170);
         }
        if (valsts.speletajs == Valstis.Speletaji.LSPR)
        {
            hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 235);
        }
        sprite.color = hoverKrasa;
    }

  public void iekrasoBlakusTeritoriju()
{
        //Debug.Log("Funkcija nostrādā");
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        foreach (GameObject stateObject in visiStates)
        {
            Debug.Log(Vector2.Distance(transform.position, stateObject.transform.position)); 
            SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
            Debug.Log(visiStates);
            if (stateController == visiStates[0])
            {
                if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && stateController == visiStates[1])
                {

                }
                {
                    Debug.Log("Found state: " + stateObject.name + ", Owner: " + stateController.valsts.speletajs + "Pozīcija:" + Vector2.Distance(transform.position, stateObject.transform.position));
                    stateController.tintesKrasa(new Color32(255, 10, 0, 255));
                }
            }

            if (stateObject != gameObject && Vector2.Distance(transform.position, stateObject.transform.position) > 1.68f)
            {

                if (stateController != null)
                {
                    if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR) 
                    {
                        Debug.Log("Found state: " + stateObject.name + ", Owner: " + stateController.valsts.speletajs + "Pozīcija:" + Vector2.Distance(transform.position, stateObject.transform.position));
                        stateController.tintesKrasa(new Color32(255, 10, 0, 255));
                    }
         
                }
            }
        }
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
                stateController.tintesKrasa(new Color32(139, 221, 51, 255));
            }
        }
    }


    void OnMouseDown()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //Debug.Log(objekti.irIzvelesLauksIeslegts);

        if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.PLAYER && objekti.irIzvelesLauksIeslegts == false)
        {
            Debug.Log("Collider hit: " + hit.collider.name);
            objekti.izvelesLauks.gameObject.SetActive(true);
            objekti.izvele.gameObject.SetActive(true);
            objekti.irIzvelesLauksIeslegts = true;
            objekti.kurVietaUzbrukt.gameObject.SetActive(false);
            objekti.uzbrukt.gameObject.SetActive(false);
            objekti.kurVietaKustinat.gameObject.SetActive(false);
            objekti.kustinat.gameObject.SetActive(false);
            objekti.mobilizet.gameObject.SetActive(false);
        }
       if (hit.collider != null && valsts.speletajs == Valstis.Speletaji.LSPR) {
            Debug.Log("Collider hit: " + hit.collider.name);
            objekti.kurVietaUzbrukt.gameObject.SetActive(false);
            objekti.uzbrukt.gameObject.SetActive(true);
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



}
