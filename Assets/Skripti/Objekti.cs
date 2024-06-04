using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Objekti : MonoBehaviour
{

    //Info
    public bool nospiestaInfo = false;
    public GameObject infoPanelis;

    //Lietotāju kārta
    public bool lietotajuKarta = false;

    public GameObject LatvijasKarogs;
    public GameObject LSPRKarogs;



    //Izvēles lauks
    public GameObject izvelesLauks;
    public bool irIzvelesLauksIeslegts = false;
    public Button izietPoga;


    //Izvele
   
    public GameObject skaits;
    public Button plusMob;
    public Button minusMob;
    public Button plusUzb;
    public Button minusUzb;
    public Button plusParvietot;
    public Button minusParvietot;
    public GameObject bridinajumaTeksts;
    public int rotuSkaitsIzv;

    //Izvēles lauks
    public GameObject izvele;
    //mobilizet lauks
    public GameObject mobilizet;

    //kustinat lauks
    public GameObject kurVietaKustinat;
    public GameObject kustinat;

    //uzbrukt lauks
    public GameObject kurVietaUzbrukt;
    public GameObject uzbrukt;

    //atpakal lauks
    public GameObject atpakalUzIzveli;


    //Vēsturiskā apraksta lietas

    public GameObject pazinojumaLauks;

    public bool apraksts = false;


    //ESC
    public GameObject ESCMenu;
    public GameObject fons;
    public bool vaiIrEsc = false;


    //Rotas skaits
    public int rotuSkaitsLSPR = 1;
    public int rotuSkaitsPlayer = 2;
    public GameObject noklikState;
    
    public GameObject noklikBlakusState;
    public bool vaiIrIzveleUzbr = false;

    public bool vaiIrIzveleKust = false;

    //Rotas sistēmas mainīgie
    public GameObject rotasPrefs;
    public GameObject rotasPrefsLSPR;
    public GameObject[] rotasPozicijas;
    public List<GameObject> izmantotasPozicijas = new List<GameObject>();

    public bool vaiIrSakumaRotas = false;
    public bool vaiIrSakumaRotasLSPR = false;

    //Teritorijas, kuras skatās uz kurieni atkāpties.
    public SpelesKontrole atpakpesState;
    public GameObject stateAtkapes;
    public bool irIelenkti = false;

    //AI blakus pie lietotāja state
    public int lietotajaGajieni = 0;
    public GameObject uzbruksanasState;
    public SpelesKontrole uzbruksanasStateKontrole;
    public GameObject noKuraStateLSPR;
    public SpelesKontrole noKuraStateLSPRKontrole;

    public GameObject lietotajuState;

    public bool otraSpeletajaKarta = false;



    //Uzvarētāju lietas

    public bool playerUzvarejis = false;
    public bool LSPRUzvarejis = false;
    public GameObject uzvaretajuLauks;

    public bool[] vesturiskaAprakstaSkaits = new bool[32];

     public TMP_InputField segVards;
     public GameObject  segVardsTeksts;
     public GameObject retryPogaAI;
     public GameObject izietPogaAI;
     public GameObject okPogaAI;

     public GameObject lideruLauks;
     public GameObject kluduTeksts;


     //AI opcija
      public bool AIieslegts = false;
      public int spelesGrutiba;
      public GameObject sakumaIzvele;
      public GameObject spelesIzvele;

     //Mob un pārv lauks
     public GameObject pazRotuLauks;



        void Awake(){
		spelesGrutiba = PlayerPrefs.GetInt("Grutiba");
        if(spelesGrutiba == 1){
            AIieslegts = false;
        }else if(spelesGrutiba == 2){
            AIieslegts = true;
        }
	    }
}


