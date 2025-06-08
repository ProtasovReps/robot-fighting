using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menutextures : MonoBehaviour {

    
    bool rollto;
    int texture;
    float counter;
    Vector3[] view=new Vector3[8] {new Vector3 ( 90f,0f, 0f), new Vector3 (45f,0f, 0f),  new Vector3(0f,0f, 0f),
                                   new Vector3 (315f,0f, 0f), new Vector3 (270f,0f, 0f), new Vector3(225f,0f, 0f),
                                   new Vector3 (180f,0f, 0f), new Vector3 (135f,0f, 0f)};

    

    void Update ()
    {   
        if (rollto)
        {
            counter += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(view[texture]), 0.25f);
            if (counter > 1.5f)
            {
                rollto = false;
                counter = 0f;
            }
             
        }
	}
    
    public void setrotation(int valor)
    {
        rollto = true;
        if (valor > -1) texture = valor;
        else texture = 7;
        counter = 0;     
    }

    public void rolltotext(int valor)
    {
        rollto = true;
        texture = valor;
    }
}
