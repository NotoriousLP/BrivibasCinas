using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public Objekti objekti;
    public SpelesKontrole kontrole;

    void Start()
    {
        objekti = FindObjectOfType<Objekti>();
        kontrole = FindAnyObjectByType<SpelesKontrole>();
    }
        public void jaunasRotas(){
        bool jaunaRota = false;
        SpelesKontrole stateController = GameObject.Find("States_2").GetComponent<SpelesKontrole>();
        Debug.Log(stateController);
            if (stateController.valsts.speletajs == Valstis.Speletaji.LSPR && !jaunaRota)    
            {
                objekti.rotasPozicijas = GameObject.FindGameObjectsWithTag("state2Pozicijas");
                stateController.rotasSkaitsByPlayer[Valstis.Speletaji.LSPR] = 3;
                stateController.sakumaRotas(stateController, objekti.rotasPozicijas, objekti.rotasPrefsLSPR, Valstis.Speletaji.LSPR);
                jaunaRota = true;
            }
    }

    void Update()
    {
        
    }
}
