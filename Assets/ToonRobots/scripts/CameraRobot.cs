using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRobot : MonoBehaviour {

    public Transform target;
    float zoom;
    Vector3 zoomline;

    void Start()
    {
        zoom = 0f;
        zoomline = (target.transform.position - (new Vector3(-7f, 7f, 14f) ))- transform.position;
    }

    void Update ()
    {      
        //ZOOM
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && zoom < 0.6f)
        {
            zoom +=0.0125f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && zoom > 0f)
        {
            zoom -=0.0125f;
        }        
        transform.position = target.position + (new Vector3(-7f, 7f, 14f)) + (zoomline*zoom);
        transform.LookAt(target.position + new Vector3(0f,1f,0f));
    }
}
