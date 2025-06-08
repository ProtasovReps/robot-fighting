using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights : MonoBehaviour
{
    GameObject clicklights;
    bool onoff;
    Color ambient;

    void Start ()
    {
        clicklights = GameObject.Find("lights/onofflight");
        onoff = true;
        ambient = RenderSettings.ambientLight;
    }	
	
	public void onofflights()
    {
		if (onoff)
        {
            clicklights.SetActive(false);
            onoff = false;
            RenderSettings.ambientLight = new Color(0f, 0f, 0f);
        }
        else
        {
            clicklights.SetActive(true);
            onoff = true;
            RenderSettings.ambientLight = ambient;
        }
    }
}
