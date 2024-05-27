using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    public Objekti objekti;
    public Teksts  teksts;
     public float laiks = 0f;
    void Start()
    {
        objekti = FindObjectOfType<Objekti>(); 
        teksts = FindAnyObjectByType<Teksts>();
    }

    // Update is called once per frame
        void Update()
        {
            if(objekti.LSPRUzvarejis == false && objekti.playerUzvarejis == false){
            laiks += Time.deltaTime; 
            TimeSpan time = TimeSpan.FromSeconds(laiks);
            teksts.timerText = time.ToString(@"mm\:ss");
                }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(objekti.vaiIrEsc == true){
                    ESCoff();
                }
               else if (objekti.vaiIrEsc == false) // Ieslēdz ESC menu
                {
                    ESCon();
                }
            }
        }

    void ESCoff(){
         objekti.ESCMenu.gameObject.SetActive(false);
         objekti.fons.gameObject.SetActive(false);
         objekti.vaiIrEsc = false;
         Debug.Log("ESC izslēdzas");
    }
    void ESCon(){
        objekti.ESCMenu.gameObject.SetActive(true);
        objekti.fons.gameObject.SetActive(true);
         Debug.Log("ESC nostrādā");
         objekti.vaiIrEsc = true;
    }
}
