using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSpele : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] muzikasObj = GameObject.FindGameObjectsWithTag("gameMusic");
        if (muzikasObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
