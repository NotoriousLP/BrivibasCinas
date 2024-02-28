using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pogas : MonoBehaviour
{

    public Objekti objekti;

    void Start()
    {
        objekti = FindObjectOfType<Objekti>();
    }

    public void uzIestatijumuAinu()
    {
        SceneManager.LoadScene("iestatijumi", LoadSceneMode.Single);
    }
    public void uzMainMenu()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    public void izietNoSpeles()
    {
        Application.Quit();
    }

    public void izietNoLauka()
    {
        objekti.izvelesLauks.gameObject.SetActive(false);
    }


}