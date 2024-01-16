using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamerasSkripts : MonoBehaviour
{


    public Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

     void Update()
    {
        float scrollData;
        scrollData = Input.GetAxisRaw("Mouse ScrollWheel");
        Debug.Log(scrollData);

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 4.5f, 8f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime);
    }

}


