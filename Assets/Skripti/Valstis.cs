using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Atribūts [System.Serializable] ļauj Unity rediģēt šīs klases laukus.
[System.Serializable]

public class Valstis
{
    public string nosaukums; //Teritorijas nosaukums

    //Enum, kas nosaka iespējamos valsts īpašniekus: LSPR (dators/lietotājs) vai PLAYER (lietotājs).
    public enum Speletaji {LSPR, PLAYER} 

    // Lauks, kas glabā teritorijas pašreizējo īpašnieku.
    public Speletaji speletajs;

}

