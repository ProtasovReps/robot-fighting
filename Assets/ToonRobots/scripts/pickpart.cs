using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickpart : MonoBehaviour {

    public int head;
    public int arms;
    public int body;
    public int legs;
    public int weapon;    


    void Start ()
    {
        GetComponent<Transform>().rotation=Quaternion.Euler(new Vector3(0f, (GetComponent<Transform>().position.x+ GetComponent<Transform>().position.z)* 42f, 0f));        	
    }
	
	
	void Update ()
    {
        GetComponent<Transform>().Rotate(new Vector3(0f, 1f, 0f));
	}


    private void OnTriggerEnter(Collider other)
    
    {
        if (other.gameObject.transform.tag == "Player")
        { 
        if (arms > 0) other.gameObject.GetComponent<RobotMovement>().changearms(arms - 1);
        if (body > 0) other.gameObject.GetComponent<RobotMovement>().changebody(body - 1);
        if (legs > 0) other.gameObject.GetComponent<RobotMovement>().changelegs(legs - 1);
        if (head > 0) other.gameObject.GetComponent<RobotMovement>().changehead(head - 1);
        if (weapon > 0) other.gameObject.GetComponent<RobotMovement>().pickweapon(weapon);
        if (weapon !=0) Destroy(gameObject,0.5f);
        else Destroy(gameObject);
        }
    }
}
