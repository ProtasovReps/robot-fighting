using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRweapon : MonoBehaviour {

    public GameObject player;
    public GameObject effects;
    public GameObject firespot;
    public GameObject laser;
    public GameObject altlaser;
    float temp;
    RaycastHit gohit;
    RaycastHit hit;
    bool rapidfire;
    public GameObject sparks;


    void Start ()
    {
        effects.SetActive(false);           
    }
    

    public void fire() //RAPIDFIRE
    {        
        StartCoroutine("multilaser");
    }  
    public void shot()  //SINGLE SHOT
    {
        StartCoroutine("singlelaser");
    }
    
    IEnumerator singlelaser()
    {
        yield return new WaitForSeconds(0.09f);
        effects.SetActive(true);
        if (Physics.Raycast(firespot.GetComponent<Transform>().transform.position, player.GetComponent<Transform>().transform.forward, out hit))
        {
            GameObject newimpact = Instantiate(sparks, hit.point, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            GameObject newlaser = Instantiate(altlaser, hit.point, Quaternion.LookRotation(firespot.GetComponent<Transform>().transform.position - hit.point));
            newlaser.transform.localScale = new Vector3(1f, 1f, (firespot.GetComponent<Transform>().transform.position - hit.point).magnitude * 0.39f);
            Destroy(newlaser, 0.05f);
            if (hit.transform.tag == "enemy")
            {
                hit.transform.GetComponent<enemy>().hit();
            }
        }
        else
        {
            GameObject newlaser = Instantiate(altlaser, firespot.GetComponent<Transform>().transform.position, player.transform.rotation);
            newlaser.transform.localScale = new Vector3(1f, 1f, 5f);
            Destroy(newlaser, 0.05f);
        }
        yield return new WaitForSeconds(0.125f);
        effects.SetActive(false);
    }
    IEnumerator multilaser()
    {        
        float counter = 0f;        
        effects.SetActive(true);   
        while (counter < 0.5f)
        {
            if (Physics.Raycast(firespot.GetComponent<Transform>().transform.position, player.GetComponent<Transform>().transform.forward, out hit))
            {
                GameObject newimpact = Instantiate(sparks, hit.point, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                Destroy(newimpact, 0.2f);
                if (Random.Range(-2000f, 2000f) > 1000f)
                {
                    GameObject newlaser = Instantiate(laser, hit.point, Quaternion.LookRotation(firespot.GetComponent<Transform>().transform.position - hit.point));
                    newlaser.transform.localScale = new Vector3(1f, 1f, (firespot.GetComponent<Transform>().transform.position - hit.point).magnitude * 0.39f);
                    Destroy(newlaser, 0.05f);
                }
                if (hit.transform.tag == "enemy")
                {
                    hit.transform.GetComponent<enemy>().multyhit();
                }
            }
            else
            {
                if (Random.Range(-2000f, 2000f) > 1000f)
                {
                    GameObject newlaser = Instantiate(laser, firespot.GetComponent<Transform>().transform.position, player.transform.rotation);
                    newlaser.transform.localScale = new Vector3(1f, 1f, 5f);
                    Destroy(newlaser, 0.05f);
                }
            }
            counter += Time.deltaTime;
            yield return null;
        }
        effects.SetActive(false);        
    }

}
