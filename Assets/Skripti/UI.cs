using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    //Publiskas atsauces uz citiem skriptiem
    public Objekti objekti;
    public Teksts  teksts;

     //Mainīgais laika uzskaitei.
     public float laiks = 0f;

    void Start()
    {
        objekti = FindObjectOfType<Objekti>(); 
        teksts = FindAnyObjectByType<Teksts>();
    }

        void Update()
        {
             //Pārbauda, vai spēle vēl nav beigusies (neviens spēlētājs nav uzvarējis).
            if(objekti.LSPRUzvarejis == false && objekti.playerUzvarejis == false){
            laiks += Time.deltaTime; 
            //Pārveido laiku TimeSpan formātā un iestata to teksta elementā, kas parāda laiku(Datu bāzei).
            TimeSpan time = TimeSpan.FromSeconds(laiks);
            teksts.timerText = time.ToString(@"mm\:ss");
                }
            // Pārbauda, vai spēlētājs ir nospiedis ESC taustiņu.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(objekti.vaiIrEsc == true){ //Izslēdz ESC menu
                    ESCoff();
                }
               else if (objekti.vaiIrEsc == false) // Ieslēdz ESC menu
                {
                    ESCon();
                }
            }
        }

    //Funkcija, kas aizver ESC izvēlni un fonu.
    void ESCoff(){
         objekti.ESCMenu.gameObject.SetActive(false);
         objekti.fons.gameObject.SetActive(false);
         objekti.vaiIrEsc = false;
         Debug.Log("ESC izslēdzas");
    }
     // Funkcija, kas atver ESC izvēlni.
    void ESCon(){
        objekti.ESCMenu.gameObject.SetActive(true);
        objekti.fons.gameObject.SetActive(true);
         Debug.Log("ESC nostrādā");
         objekti.vaiIrEsc = true;
    }
}
