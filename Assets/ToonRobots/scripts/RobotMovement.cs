using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour {

    Transform trans;
    Animator anim;
    Rigidbody rigid;
    float run;
    bool grounded;
    bool jumping;
    string[] Shead = new string[6] { "PLAYER/HEAD_A", "PLAYER/HEAD_B", "PLAYER/HEAD_H", "PLAYER/HEAD_L", "PLAYER/HEAD_S", "PLAYER/HEAD_W", };
    string[] Sbody = new string[6] { "PLAYER/BODY_A", "PLAYER/BODY_B", "PLAYER/BODY_H", "PLAYER/BODY_L", "PLAYER/BODY_S", "PLAYER/BODY_W", };
    string[] Sarms = new string[6] { "PLAYER/ARMS_A", "PLAYER/ARMS_B", "PLAYER/ARMS_H", "PLAYER/ARMS_L", "PLAYER/ARMS_S", "PLAYER/ARMS_W", };
    string[] Slegs = new string[6] { "PLAYER/LEGS_A", "PLAYER/LEGS_B", "PLAYER/LEGS_H", "PLAYER/LEGS_L", "PLAYER/LEGS_S", "PLAYER/LEGS_W", };
    string[] Sface = new string[6] { "PLAYER/FACE_A", "PLAYER/FACE_B", "PLAYER/FACE_H", "PLAYER/FACE_L", "PLAYER/FACE_S", "PLAYER/FACE_W", };
    GameObject[] Ghead = new GameObject[6];
    GameObject[] Gbody = new GameObject[6];
    GameObject[] Garms = new GameObject[6];
    GameObject[] Glegs = new GameObject[6];
    GameObject[] Gface = new GameObject[6];
    int head;
    int body;
    int arms;
    int legs;
    bool active;
    float tic;
    float tac;
    int weapon;
    public GameObject weaponA;
    public GameObject weaponB;
    public float jumpforce;
    RaycastHit gohit;
    public float runspeed;
     
    
    void Start ()
    {
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        run = 1f;
        active = true;

        for (int inx = 0; inx < 6; inx++)
        {
            Ghead[inx] = GameObject.Find(Shead[inx]);
            Gbody[inx] = GameObject.Find(Sbody[inx]);
            Garms[inx] = GameObject.Find(Sarms[inx]);
            Glegs[inx] = GameObject.Find(Slegs[inx]);
            Gface[inx] = GameObject.Find(Sface[inx]);
        }

        deactivateall();
        weaponA.SetActive(false);
        weaponB.SetActive(false);
        randomize();
        weapon = 0;
    }

    void Update ()
    {   
        //RANDOMIZE
        if (Input.GetKeyDown("r"))
        {
            deactivateall();
            randomize();
        }

        //CHECK GROUND
        if (Physics.Raycast(trans.position + new Vector3(0f, 0.05f, 0f) + trans.forward * 0.125f, Vector3.down, 0.125f)||
            Physics.Raycast(trans.position + new Vector3(0f, 0.05f, 0f) - trans.forward * 0.125f, Vector3.down, 0.125f) ||
            Physics.Raycast(trans.position + new Vector3(0f, 0.05f, 0f), Vector3.down, 0.125f))
        {
            grounded = true;            
        }
        else
        {
            grounded = false;            
        }
        anim.SetBool("grounded", grounded);
        anim.SetInteger("weapon", weapon);

        if (active)
        {
            if (grounded)
            {              
                if (Input.GetAxis("Vertical") != 0f && !jumping)
                    rigid.velocity = (trans.forward * Input.GetAxis("Vertical") * 1.35f + trans.forward * run * 0.25f * anim.GetFloat("walk"));
                if (Input.GetAxis("Horizontal") != 0f) trans.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * Time.deltaTime * 110f, 0f));
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (anim.GetFloat("walk") != 0f || anim.GetFloat("strafe") != 0f) run = Mathf.Lerp(run, 20f, 0.025f);
                }
                else run = Mathf.Lerp(run, 0f, 0.075f);
                if (Input.GetKey("q")) anim.SetFloat("strafe", -1f);
                if (Input.GetKey("e")) anim.SetFloat("strafe", 1f);
                if (!Input.GetKey("e") && !Input.GetKey("q")) anim.SetFloat("strafe", 0f);
                if (anim.GetFloat("walk") == 0f && anim.GetFloat("strafe") != 0f) rigid.velocity = trans.right * anim.GetFloat("strafe") + trans.right * run * 0.075f * anim.GetFloat("strafe");
                anim.SetFloat("walk", Input.GetAxis("Vertical"));
                anim.SetFloat("turn", Input.GetAxis("Horizontal"));
                anim.SetFloat("run", run);
                
                //JUMP
                if (Input.GetKeyDown("space"))
                {
                    anim.SetTrigger("jump");
                    StartCoroutine("wait", 0.15f);
                    jumping = true;
                    StartCoroutine("nojump");

                }

            }

            if (!grounded)
            {
                trans.Rotate(0f, Input.GetAxis("Horizontal")*0.5f, 0f);
                rigid.velocity += trans.forward * (Input.GetAxis("Vertical")*0.1f);
            }            
            
            
            //FIRE1
            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
                {                
                if (weapon == 1 )
                {
                    anim.SetBool("gun", true);
                    //layer1 = 1;
                }
                if (weapon == 2 )
                {
                    anim.SetBool("fire", true);
                }                    
            }            
            //FIRE2
            if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0) && weapon==2 )
                {
                anim.SetBool("shot", true);
                }
            if (Input.GetMouseButtonUp(0))
                {
                anim.SetBool("gun", false);
                anim.SetBool("fire", false);
                }
            if (Input.GetMouseButtonUp(1))
                {
                anim.SetBool("shot", false);
                }
            //CHANGE WEAPON
            if (Input.GetKey("1"))
            {
                
                pickweapon(1);
                rigid.velocity = new Vector3(0f, 0f, 0f);
                StartCoroutine("wait", 1f);
            }
            if (Input.GetKey("2"))
            {
                
                pickweapon(2);
                rigid.velocity = new Vector3(0f, 0f, 0f);
                StartCoroutine("wait", 1f);
            }
            if (Input.GetKey("0"))
            {
                pickweapon(0);
            }
        }
    }         
    
    void deactivateall()
    {
        for (int inx = 0; inx < 6; inx++)
        {
            Ghead[inx].SetActive(false);
            Gbody[inx].SetActive(false);
            Garms[inx].SetActive(false);
            Glegs[inx].SetActive(false);
            Gface[inx].SetActive(false);            
        }
    }
    void randomize()
    {        
        head = Random.Range(0, 6);
        body = Random.Range(0, 6);
        arms = Random.Range(0, 6);
        legs = Random.Range(0, 6);                
        Ghead[head].SetActive(true);
        Gbody[body].SetActive(true);
        Garms[arms].SetActive(true);
        Glegs[legs].SetActive(true);
        if (Random.Range(0,2)==0) Gface[head].SetActive(true);
    }      
    public void changehead(int newhead)
    {
        Ghead[head].SetActive(false);
        Gface[head].SetActive(false);
        Ghead[newhead].SetActive(true);
        head = newhead;
    }
    public void changearms(int newarms)
    {        
        Garms[arms].SetActive(false);
        Garms[newarms].SetActive(true);
        arms = newarms;        
    }
    public void changebody(int newbody)
    {
        Gbody[body].SetActive(false);
        Gbody[newbody].SetActive(true);
        body = newbody;
    }
    public void changelegs(int newlegs)
    {
        Glegs[legs].SetActive(false);
        Glegs[newlegs].SetActive(true);
        legs = newlegs;
    }    
    public void pickweapon(int newweapon)
    {
        if (newweapon == 2)
        {
            StartCoroutine(wait(0.5f));
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 1);
            anim.Play("grab",2);
            StartCoroutine("delaypickwb");
        }
        if (newweapon == 1)
        {
            StartCoroutine(wait(0.5f));
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
            anim.Play("grab",0);
            StartCoroutine("delaypickwa");
        }
        if (newweapon == 0)
        {
            weapon = 0;
            weaponA.SetActive(false);
            weaponB.SetActive(false);
            anim.SetLayerWeight(1, (Mathf.Lerp(anim.GetLayerWeight(1), 0, 0.25f)));
            anim.SetLayerWeight(2, (Mathf.Lerp(anim.GetLayerWeight(2), 0, 0.25f)));          
        }
    }
    public void jump()
    {
        rigid.velocity += new Vector3(0f,jumpforce,0f);
    }
    public void gun()
    {
        if (weaponA.activeSelf) weaponA.gameObject.GetComponent<TRweapon>().shot();
    }
    public void shot()
    {
        if (weaponB.activeSelf) weaponB.gameObject.GetComponent<TRweapon>().shot();
    }
    public void fire()
    {
        if (weaponB.activeSelf) weaponB.gameObject.GetComponent<TRweapon>().fire();
    }
    IEnumerator wait(float wait)
    {
        active = false;
        yield return new WaitForSeconds(wait);
        active = true;        
    }
    IEnumerator nojump()
    {        
        yield return new WaitForSeconds(0.25f);
        jumping = false;
    }
    IEnumerator delaypickwa()
    {
            weapon = 1;
        weaponB.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        weaponA.SetActive(true);
        
    }
    IEnumerator delaypickwb()
    {        
        weapon = 2;
        weaponA.SetActive(false);
        yield return new WaitForSeconds(0.5f);        
        weaponB.SetActive(true);
     
    }

}
