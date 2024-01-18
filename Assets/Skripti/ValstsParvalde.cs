using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ValstsParvalde : MonoBehaviour
{
    public static ValstsParvalde Instance;

    public List<GameObject> valstsSaraksts = new List<GameObject>();


     void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pievienotValstsDatus(); 
    }

    void pievienotValstsDatus()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Valsts") as GameObject[];
        foreach (GameObject valsts in theArray)
        {
            valstsSaraksts.Add(valsts);
        }
        KrasotValstis();
    }

    void KrasotValstis()
    {
        for(int i=0; i<valstsSaraksts.Count; i++)
        {
            SpelesValstsKontrole kontroleValsts = valstsSaraksts[i].GetComponent<SpelesValstsKontrole>();

            if(kontroleValsts.valsts.speletajs == Valstis.Speletaji.USSR)
            {
                kontroleValsts.tintesKrasa(new Color32(243, 43, 43, 255));
            }

            if (kontroleValsts.valsts.speletajs == Valstis.Speletaji.PLAYER)
            {
                kontroleValsts.tintesKrasa(new Color32(139, 221, 51, 255));
            }
        }
    }
}
