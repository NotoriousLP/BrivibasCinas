using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objekti : MonoBehaviour
{
    //Izvēles lauks
    public GameObject izvelesLauks;
    public bool irIzvelesLauksIeslegts = false;
    public Button izietPoga;

    public InputField inputSkaits;
    public Button plus;
    public Button minus;


    //okPogas
    public Button okPogaMob;
    public Button okPogaPar;
    public Button okPogaUzb;

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
}


