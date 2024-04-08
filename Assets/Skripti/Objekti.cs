using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Objekti : MonoBehaviour
{
    //Izvēles lauks
    public GameObject izvelesLauks;
    public bool irIzvelesLauksIeslegts = false;
    public Button izietPoga;


    //Izvele
    public TextMeshProUGUI skaitaLauks;
    public GameObject skaits;
    public Button plusMob;
    public Button minusMob;
    public Button plusUzb;
    public Button minusUzb;
    public Button plusParvietot;
    public Button minusParvietot;

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


    //ESC
    public GameObject ESCMenu;
    public GameObject fons;
    public bool vaiIrEsc = false;


    //Rotas skaits
    public int rotuSkaits = 2;
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
}


