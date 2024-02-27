using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]

public class SpelesKontrole : MonoBehaviour, IPointerClickHandler
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
              hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 200);
         }
        if (valsts.speletajs == Valstis.Speletaji.LSPR)
        {
            hoverKrasa = new Color32(vecaKrasa.r, vecaKrasa.g, vecaKrasa.b, 255);
        }
        sprite.color = hoverKrasa;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Collider hit: " + hit.collider.name);
            Debug.Log("Hit point: " + hit.point);
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
