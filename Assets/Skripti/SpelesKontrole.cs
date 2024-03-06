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
    void OnMouseEnter()
    {
        vecaKrasa = sprite.color;
         if (valsts.speletajs == Valstis.Speletaji.PLAYER){
              hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 180);
         }
        if (valsts.speletajs == Valstis.Speletaji.LSPR)
        {
            hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 235);
        }
        sprite.color = hoverKrasa;
    }

  public void iekrasoBlakusTeritoriju()
{
        Debug.Log("Funkcija nostrādā");
        GameObject[] visiStates = GameObject.FindGameObjectsWithTag("Valsts");
        foreach (GameObject stateObject in visiStates)
        {
            if (stateObject != gameObject && Vector2.Distance(transform.position, stateObject.transform.position) < 1.9f)
            {
                SpelesKontrole stateController = stateObject.GetComponent<SpelesKontrole>();
                if (stateController != null)
                {
                    Debug.Log("Found state: " + stateObject.name + ", Owner: " + stateController.valsts.speletajs);
                    if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR) 
                    {
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
        Debug.Log(objekti.irIzvelesLauksIeslegts);

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
