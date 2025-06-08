using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuanimations : MonoBehaviour {

    public Animator anim;
    public GameObject weaponA;
    public GameObject weaponB;
    public GameObject robot;
    float speed;
    float turn;
    float strafe;
    int weapon;
    float counter;
    float counter2;
    float counterstop;
    public Button fire1;
    public Button fire2;
    //float layer1;
    //float layer2;
    //float layer3;
    //float layer4;    
    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;
    public GameObject impact;
    public GameObject elctriclight;
    GameObject newlaser;



    void Start ()
    {
        elctriclight.SetActive(false);
        fire1.interactable = false;
        fire2.interactable = false;
        //layer1 = 0f;
        //layer2 = 0f;
        //layer3 = 0f;
       // layer4 = 0f;
        anim.SetFloat("energy",100f);
    }	
	
	void Update ()
    {    
        anim.SetFloat("speed", Mathf.Lerp(anim.GetFloat("speed"), speed, 0.25f));
        anim.SetFloat("turn", Mathf.Lerp(anim.GetFloat("turn"), turn, 0.35f));
        anim.SetFloat("strafe", Mathf.Lerp(anim.GetFloat("strafe"), strafe, 0.35f));
        robot.transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * Time.deltaTime * -110f, 0f));
        robot.transform.Rotate(new Vector3(0f, -turn * Time.deltaTime *2.5f, 0f));
        if (speed==0f && turn== 0f && strafe == 0 && weapon==0)
        {
            counter += Time.deltaTime;
            if (counter > counterstop)
            {                
                anim.SetInteger("idle", Random.Range(1, 8));
                counterstop = Random.Range(1f, 3f);
                counter = 0f;
            }
        }
            else anim.SetInteger("idle", 0);
        
        if (anim.GetFloat("energy") < 1) StartCoroutine(resucitate());
        if (Vector3.SignedAngle(robot.transform.forward, Vector3.right, Vector3.up) < 0f) anim.SetBool("lookaway",true);
        else anim.SetBool("lookaway", false);        
    }

         
    // SHOTS
    public void singleshot()
    {        
        if (weapon == 2)
        {
            anim.SetLayerWeight(3, 2);
            //layer3 = 0;
            anim.Play("shot");
            weaponB.gameObject.GetComponent<TRweapon>().shot();
        }
        if (weapon == 1)
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(4, 2);
            //layer1 = 1;
            //layer4 = 0;
            anim.Play("shotgun");
            weaponA.gameObject.GetComponent<TRweapon>().shot();
        }
    }
    public void fire()
    {       
       // layer3 = 0;
        anim.SetLayerWeight(3, 1);
        anim.Play("fire");
        weaponB.gameObject.GetComponent<TRweapon>().fire();
    }
       
    //  WEAPON
    public void increaseweapon()
    {
        if (weapon < 2) weapon++;
        else weapon = 0;
        setweapon(weapon);
    }
    public void decreaseweapon()
    {
        if (weapon > 0) weapon--;
        else weapon = 2;        
        setweapon(weapon);
    }
    public void setweapon(int nowweapon)
    {        
        if (nowweapon == 1)
        {
            weaponA.SetActive(true);
            weaponB.SetActive(false);
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
            fire1.interactable = true;
            fire2.interactable = false;
        }
        if (nowweapon == 2)
        {
            weaponA.SetActive(false);
            weaponB.SetActive(true);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 1);
            fire1.interactable = true;
            fire2.interactable = true;
        }
        if (nowweapon == 0)
        {
            weaponA.SetActive(false);
            weaponB.SetActive(false);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            fire1.interactable = false;
            fire2.interactable = false;
        }
    }

    // HITS
    public void hit()
    {
        anim.SetInteger("idle", 0);
        Vector3 deviation = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
        newlaser = Instantiate(laser1, robot.transform.position + new Vector3(0f,1.25f,0f)+ deviation, Quaternion.Euler(Vector3.forward));
        Instantiate(impact, robot.transform.position + new Vector3(0f, 1.25f, 0f) + deviation, robot.transform.rotation);
        Destroy(newlaser, 0.1f);
        if (!anim.GetBool("fly"))
        { 
            if (weapon <= 1)
            { 
            if (Vector3.SignedAngle(robot.transform.forward, Vector3.right, Vector3.up) < 0f) anim.Play("hitforward");
            else anim.Play("hitbackwards");
            }
            if (weapon == 2)
            {
                if (Vector3.SignedAngle(robot.transform.forward, Vector3.right, Vector3.up) < 0f) anim.Play("hitforward", 2);
                else anim.Play("hitbackwards", 2);
            }
        }        
        else
        {
            if (weapon <= 1)
            {
                robot.transform.position = new Vector3(robot.transform.position.x, 0.3f, robot.transform.position.z);
                if (Vector3.SignedAngle(robot.transform.forward, Vector3.right, Vector3.up) < 0f) anim.Play("flyhitbackwards");
                else anim.Play("flyhitforward");
            }
            else
            {
                robot.transform.position = new Vector3(robot.transform.position.x, 0.3f, robot.transform.position.z);
                if (Vector3.SignedAngle(robot.transform.forward, Vector3.right, Vector3.up) < 0f) anim.Play("flyhitbackwards",2);
                else anim.Play("flyhitforward",2);
            }
        }
        anim.SetFloat("energy", anim.GetFloat("energy") - 10f);
    }

    public void multyhit()
    {
            anim.SetInteger("idle", 0);
            StartCoroutine("IEmultihit");

        if (!anim.GetBool("fly"))
        {
            if (weapon <= 1) anim.Play("multihit");
            else anim.Play("multihit",2);
        }
        else
        {
            robot.transform.position = new Vector3(robot.transform.position.x, 0.3f, robot.transform.position.z);
            if (weapon <= 1) anim.Play("electrocuted");
            else anim.Play("electrocuted", 2);
        }
    }
    public void taser()
    {
        anim.SetInteger("idle", 0);
        StartCoroutine(taserray());
    }          

    // ANIMATIONS
    public void jump()
    {        
        if (weapon == 2)
        {
            if (speed <= 0) anim.Play("jump", 2);
            if (speed > 0) anim.Play("runjumpIN", 2);
        }
        else
        {
            if (speed <= 0) anim.Play("jump", 0);
            if (speed > 0) anim.Play("runjumpIN", 0);
        }
    }
    public void speedup()
    {
        if (speed < 40f) speed += 20f;
    }
    public void speeddown()
    {
        if (speed > -40f) speed -= 20f;
    }
    public void turnup()
    {
        if (turn < 40f) turn += 20f;
    }
    public void turndown()
    {
        if (turn > -40f) turn -= 20f;
    }
    public void strafeup()
    {
        if (strafe < 20f) strafe += 10f;
        turn = 0f;
        speed = 0f;
    }
    public void strafedown()
    {
        if (strafe > -20f) strafe -= 10f;
        turn = 0f;
        speed = 0f;
    }
    public void special1()
    {
        anim.Play("special1");
    }
    public void special2()
    {
        anim.Play("special2");
    }

    IEnumerator IEmultihit()
    {
        float counter = 0;
        while (counter<2)
        {         
        Vector3 deviation = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.35f, 0.35f), 0f);
        if (Random.Range(0f,50f)<18f) newlaser = Instantiate(laser2, robot.transform.position + new Vector3(0f, 1f, 0f) + deviation, Quaternion.Euler( Vector3.forward));
            Destroy(newlaser, 0.1f);
            Instantiate(impact, robot.transform.position + new Vector3(0f, 1f, 0f) + deviation, robot.transform.rotation);
            anim.SetFloat("energy", anim.GetFloat("energy") - 0.5f);
            counter += Time.deltaTime;
            yield return null;
        }
        counter = 0f;
        yield return null;
    }
    IEnumerator taserray()
    {
        Vector3 deviation = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.2f, 0.2f), 0f);
        GameObject newtaserray = Instantiate(laser3, robot.transform.position + new Vector3(0f, 1f, 0f) + deviation, Quaternion.Euler(Vector3.forward));
        anim.Play("electrocuted");
        anim.SetFloat("energy", anim.GetFloat("energy") - 34f);
        elctriclight.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        elctriclight.SetActive(false);
        Destroy(newtaserray);
    }
    IEnumerator resucitate()
    {
        yield return new WaitForSeconds(2);
        anim.SetFloat("energy",100f);
    }    
    IEnumerator changelayer(int layer,float value)
    {
        float auxvalue = anim.GetLayerWeight(layer);        
        while (auxvalue != value)
        {
            auxvalue = Mathf.Lerp(auxvalue, value, 0.05f);            
            anim.SetLayerWeight(layer, value);
            yield return null;
        }        
        yield return null;
    }              
}
