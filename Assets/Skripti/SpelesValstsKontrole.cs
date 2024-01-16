using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class SpelesValstsKontrole : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Color32 vecaKrasa;
    public Color32 hoverKrasa;
    public Color32 sakumaKrasa;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Debug.Log(sprite);
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

}
