using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class SpelesValstsKontrole : MonoBehaviour
{

    public Valstis valsts;
    private SpriteRenderer sprite;

    public Color32 vecaKrasa;
    public Color32 hoverKrasa;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

     void OnMouseEnter()
    {
        vecaKrasa = sprite.color;
        sprite.color = hoverKrasa;
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
