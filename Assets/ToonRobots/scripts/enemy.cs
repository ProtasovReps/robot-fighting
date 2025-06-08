using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    Animator anim;
    Rigidbody rigid;
    GameObject player;
    public int life;
    public int weapon;
    bool hited;
    bool lookaway;
    bool alive;
    float counter;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        alive = true;
        if (weapon == 2) anim.SetLayerWeight(1, 1);
    }
	
	
	void Update ()
    {
        if (alive)
        {
            //DIE
            if (life <= 0 && alive)
            {
                alive = false;
                anim.SetLayerWeight(1, 0);

                if (lookaway)
                {
                    transform.rotation = player.transform.rotation;
                    rigid.AddForce(transform.forward * 8f, ForceMode.VelocityChange);
                    anim.Play("dieforward");
                }
                if (!lookaway)
                {
                    transform.LookAt(player.transform);
                    //transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles);
                    rigid.AddForce(transform.forward * -8f, ForceMode.VelocityChange);
                    anim.Play("diebackwards");
                }
                counter = 0f;
            }
            
            if (Vector3.Angle(transform.position - player.transform.position, transform.forward) < 90f)
                {
                    lookaway = true;
                }
            else lookaway = false;

            if (hited)
            {
                counter += Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((player.transform.position - transform.position)), 0.0125f);
                if (counter > 0.1f)
                {
                    hited = false;
                    anim.SetBool("hit", false);
                    counter = 0f;
                }
            }
        }
        if (!alive && rigid.velocity.magnitude == 0f)
        {
            GetComponent<Collider>().enabled = false;
            rigid.Sleep();
        }
    }




    public void multyhit()   //FROM FIRE
    {
        if (alive)
        {
            anim.SetBool("hit", true);
            hited = true;
            counter = 0f;
            if (lookaway) rigid.velocity = (transform.forward * 1f);
            if (!lookaway) rigid.velocity = (transform.forward * -1f);
            life--;
        }
    }
    public void hit()  //FROM SHOT
    {
        if (alive)
        {
            if (!lookaway)
            {
                anim.Play("hitbackwards",weapon-1);
                rigid.velocity = (transform.forward * -2f);
                transform.LookAt(player.transform);
            }
            if (lookaway)
            {
                anim.Play("hitforward", weapon - 1);
                rigid.velocity = (transform.forward * 2f);
                transform.LookAt(player.transform);
            }
            life -= 10;
        }
    }

    IEnumerator wait()
    {
        while (counter < 0.9f)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        GetComponent<Collider>().enabled = false;
        rigid.Sleep();
        yield return null;
    }
}

