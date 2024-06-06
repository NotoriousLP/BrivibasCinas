using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValstsParvalde : MonoBehaviour
{
     //Statiska instance, lai nodrošinātu piekļuvi šim skriptam no citiem skriptiem.
    public static ValstsParvalde Instance;

    //Saraksts, kurā glabāsies visas spēles valstu objekti.
    public List<GameObject> valstsSaraksts = new List<GameObject>();

    //Funkcija, kas tiek izsaukta, kad objekts tiek izveidots.
     void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        pievienotValstsDatus(); //Pievieno valstu datus sarakstam un iekrāso tās.
    }

    // Funkcija, kas pievieno visas teritorijas objektus.
    void pievienotValstsDatus()
    {
        //Atrodam visus spēles objektus ar tagu "Valsts".
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Valsts");
        //Pievienojam katru atrasto valsts objektu sarakstam.
        foreach (GameObject valsts in theArray)
        {
            valstsSaraksts.Add(valsts);
        }
        KrasotValstis();
    }

    //Funkcija, kas iekrāso katru valsti atkarībā no tās īpašnieka
    void KrasotValstis()
    {
        //Pārbauda katru teritoriju sarakstā
        for(int i=0; i<valstsSaraksts.Count; i++)
        {
            SpelesKontrole kontroleValsts = valstsSaraksts[i].GetComponent<SpelesKontrole>();

            if(kontroleValsts.valsts.speletajs == Valstis.Speletaji.LSPR)
            {
                kontroleValsts.tintesKrasa(new Color32(243, 43, 43, 235));
            }

            if (kontroleValsts.valsts.speletajs == Valstis.Speletaji.PLAYER)
            {
                kontroleValsts.tintesKrasa(new Color32(139, 221, 51, 210));
            }
        }
    }
}
