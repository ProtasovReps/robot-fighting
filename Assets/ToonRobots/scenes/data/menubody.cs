using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menubody : MonoBehaviour {

    public GameObject robot;
    string[] Shead = new string[6] { "ToonRobot/HEAD_A", "ToonRobot/HEAD_B", "ToonRobot/HEAD_H", "ToonRobot/HEAD_L", "ToonRobot/HEAD_S", "ToonRobot/HEAD_W", };
    string[] Sbody = new string[6] { "ToonRobot/BODY_A", "ToonRobot/BODY_B", "ToonRobot/BODY_H", "ToonRobot/BODY_L", "ToonRobot/BODY_S", "ToonRobot/BODY_W", };
    string[] Sarms = new string[6] { "ToonRobot/ARMS_A", "ToonRobot/ARMS_B", "ToonRobot/ARMS_H", "ToonRobot/ARMS_L", "ToonRobot/ARMS_S", "ToonRobot/ARMS_W", };
    string[] Slegs = new string[6] { "ToonRobot/LEGS_A", "ToonRobot/LEGS_B", "ToonRobot/LEGS_H", "ToonRobot/LEGS_L", "ToonRobot/LEGS_S", "ToonRobot/LEGS_W", };
    string[] Sface = new string[6] { "ToonRobot/FACE_A", "ToonRobot/FACE_B", "ToonRobot/FACE_H", "ToonRobot/FACE_L", "ToonRobot/FACE_S", "ToonRobot/FACE_W", };
    GameObject[] Ghead = new GameObject[6];
    GameObject[] Gbody = new GameObject[6];
    GameObject[] Garms = new GameObject[6];
    GameObject[] Glegs = new GameObject[6];
    GameObject[] Gface = new GameObject[6];
    Renderer[] Rhead = new Renderer[6];
    Renderer[] Rbody = new Renderer[6];
    Renderer[] Rarms = new Renderer[6];
    Renderer[] Rlegs = new Renderer[6];
    Renderer[] Rface = new Renderer[6];
    int head;
    int body;
    int arms;
    int legs;    
    bool face;
    int wings;
    public GameObject weaponA;
    public GameObject weaponB;
    public Material[] material;
    public GameObject[] textparts;
    public Animator anim;
    public Button special1;
    public Button special2;
    

    
    int[,] headmatindex = new int[6, 3] { { 1, 0, 0 }, { 1, 1, 3 }, { 1, 2, 2 }, { 3, 3, 0 }, { 0, 4, 2 }, { 2, 5, 3 } };
    int[,] armsmatindex = new int[6, 3] { { 2, 0, 1 }, { 0, 1, 2 }, { 1, 2, 0 }, { 2, 3, 1 }, { 2, 4, 0 }, { 2, 5, 0 } };
    int[,] bodymatindex = new int[6, 3] { { 0, 0, 2 }, { 1, 1, 3 }, { 0, 2, 1 }, { 0, 3, 2 }, { 1, 4, 2 }, { 0, 5, 2 } };
    int[,] legsmatindex = new int[6, 3] { { 0, 0, 1 }, { 1, 1, 0 }, { 2, 2, 0 }, { 0, 3, 1 }, { 2, 4, 0 }, { 0, 5, 1 } };
    int[,] facematindex = new int[6, 3] { { 1, 0, 2 }, { 0, 1, 0 }, { 0, 2, 3 }, { 0, 3, 2 }, { 0, 4, 2 }, { 1, 5, 0 } };
    int newmaterial;
    Material basematerial;
    int basematindx;
    public GameObject[] wingsA = new GameObject[6];
    public GameObject[] wingsB = new GameObject[6];
    public GameObject wingslink;
    GameObject wingsaux;

    float counter;


    void Start ()
    {
        special1.interactable = false;
        special2.interactable = false;
        basematindx = 6;
        basematerial = material[basematindx];
        textparts[4].gameObject.GetComponent<menutextures>().setrotation(basematindx);
        for (int inx = 0; inx < 6; inx++)
        {
            Ghead[inx] = GameObject.Find(Shead[inx]);
            Rhead[inx] = Ghead[inx].GetComponent<Renderer>();           
            Gbody[inx] = GameObject.Find(Sbody[inx]);
            Rbody[inx] = Gbody[inx].GetComponent<Renderer>();
            Garms[inx] = GameObject.Find(Sarms[inx]);
            Rarms[inx] = Garms[inx].GetComponent<Renderer>();
            Glegs[inx] = GameObject.Find(Slegs[inx]);
            Rlegs[inx] = Glegs[inx].GetComponent<Renderer>();
            Gface[inx] = GameObject.Find(Sface[inx]);
            Rface[inx] = Gface[inx].GetComponent<Renderer>();
            wingsA[inx].SetActive(false);
            wingsB[inx].SetActive(false);
        }
        deactivateall();
        randomize();
        weaponA.SetActive(false);
        weaponB.SetActive(false);
        face = false;
        Gface[head].SetActive(false);
    }	
	
	

             
    

    public void deactivateall()
    {
        for (int inx = 0; inx < 6; inx++)
        {
            Ghead[inx].SetActive(false);
            Gbody[inx].SetActive(false);
            Garms[inx].SetActive(false);
            Glegs[inx].SetActive(false);
            Gface[inx].SetActive(false);
        }
        if (wings > 0)
        {
            if (wings == 1) wingsA[body].SetActive(false);
            if (wings == 2) wingsB[body].SetActive(false);
            wings = 0;
            anim.SetBool("fly", false);
        }
    }
    public void randomize()
    {
        if (wings>0)
        {
            if (wings == 1) wingsA[body].SetActive(false);
            if (wings == 2) wingsB[body].SetActive(false);
            wings = 0;
            anim.SetBool("fly", false);
        }
        head = Random.Range(0, 6);
        body = Random.Range(0, 6);
        arms = Random.Range(0, 6);
        legs = Random.Range(0, 6);

        Ghead[head].SetActive(true);
        Gbody[body].SetActive(true);
        Garms[arms].SetActive(true);
        Glegs[legs].SetActive(true);
        if (Random.Range(0, 2) == 0)
        {
            Gface[head].SetActive(true);
            face = true;
        }
        else face = false;
       
        textparts[0].gameObject.GetComponent<menutextures>().setrotation(headmatindex[head,1]);
        textparts[1].gameObject.GetComponent<menutextures>().setrotation(armsmatindex[arms,1]);
        textparts[2].gameObject.GetComponent<menutextures>().setrotation(bodymatindex[body,1]);
        textparts[3].gameObject.GetComponent<menutextures>().setrotation(legsmatindex[legs,1]);
        checkset();
    }
    public void faceonoff()
    {
        face = !face;
        if (face) Gface[head].SetActive(true);
        if (!face) Gface[head].SetActive(false);
    }
    public void wingsonoff()
    {
        wings += 1;
        if (wings > 2) wings = 0;
        
        if (wings==1)
        {
            wingsA[body].SetActive(true);
            anim.SetBool("fly", true);
            anim.SetInteger("idle", 0);
            wingsmaterial();
        }
        if (wings == 2)
        {
            wingsA[body].SetActive(false);
            wingsB[body].SetActive(true);
            anim.SetBool("fly", true);
            anim.SetInteger("idle", 0);
            wingsmaterial();
        }
        if (wings==0)
        {
            StartCoroutine("wingsoff");
            anim.SetBool("fly", false);
        }
    }    
    public void increasehead()
    {
        changehead(1);
    }
    public void decreasehead()
    {
        changehead(-1);
    }
    public void changehead(int goup)
    {
        if (face) Gface[head].SetActive(false);
        Ghead[head].SetActive(false);
        head += goup;
        if (head > 5) head = 0;
        if (head < 0) head = 5;
        Ghead[head].SetActive(true);
        textparts[0].gameObject.GetComponent<menutextures>().setrotation(headmatindex[head, 1]);
        checkset();
        if (face) Gface[head].SetActive(true);
    }
    public void increasearms()
    {
        changearms(1);
    }
    public void decreasearms()
    {
        changearms(-1);
    }
    public void changearms(int goup)
    {
        Garms[arms].SetActive(false);
        arms += goup;
        if (arms > 5) arms = 0;
        if (arms < 0) arms = 5;
        Garms[arms].SetActive(true);
        textparts[1].gameObject.GetComponent<menutextures>().setrotation(armsmatindex[arms, 1]);
        checkset();
    }
    public void increasebody()
    {
        changebody(1);
    }
    public void decreasebody()
    {
        changebody(-1);
    }
    public void changebody(int goup)
    {
        if(wings == 1) wingsA[body].SetActive(false);
        if(wings == 2) wingsB[body].SetActive(false);
        Gbody[body].SetActive(false);
        body += goup;
        if (body > 5) body = 0;
        if (body < 0) body = 5;
        Gbody[body].SetActive(true);
        textparts[2].gameObject.GetComponent<menutextures>().setrotation(bodymatindex[body, 1]);
        checkset();
        if (wings == 1 ) wingsA[body].SetActive(true);
        if (wings == 2) wingsB[body].SetActive(true);
        wingsmaterial();
    }
    public void increaselegs()
    {
        changelegs(1);
    }
    public void decreaselegs()
    {
        changelegs(-1);        
    }
    public void changelegs(int goup)
    {
        Glegs[legs].SetActive(false);
        legs += goup;
        if (legs > 5)  legs = 0;
        if (legs < 0) legs = 5;
        Glegs[legs].SetActive(true);
        textparts[3].gameObject.GetComponent<menutextures>().setrotation(legsmatindex[legs, 1]);
        checkset();
    }
    public void headmaterial()
    {              
        if(headmatindex[head, 1]<7)  newmaterial = headmatindex[head, 1] +1;
        else newmaterial = 0;
        Material[] promaterials = Rhead[head].materials;        
        promaterials[headmatindex[head, 0]] = material[newmaterial];
        Rhead[head].materials = promaterials;
        headmatindex[head, 1]= newmaterial;
        textparts[0].gameObject.GetComponent<menutextures>().setrotation(newmaterial);
        promaterials = Rface[head].materials;
        promaterials[facematindex[head, 0]] = material[newmaterial];
        Rface[head].materials = promaterials;
        facematindex[head, 1] = newmaterial;
    }
    public void armsmaterial()
    {
        if (armsmatindex[arms, 1] < 7) newmaterial = armsmatindex[arms, 1] + 1;
        else newmaterial = 0;
        Material[] promaterials = Rarms[arms].materials;
        promaterials[armsmatindex[arms, 0]] = material[newmaterial];
        Rarms[arms].materials = promaterials;
        armsmatindex[arms, 1] = newmaterial;
        textparts[1].gameObject.GetComponent<menutextures>().setrotation(newmaterial);
    }
    public void bodymaterial()
    {
        if (bodymatindex[body, 1] < 7) newmaterial = bodymatindex[body, 1] + 1;
        else newmaterial = 0;
        Material[] promaterials = Rbody[body].materials;
        promaterials[bodymatindex[body, 0]] = material[newmaterial];
        Rbody[body].materials = promaterials;
        bodymatindex[body, 1] = newmaterial;
        textparts[2].gameObject.GetComponent<menutextures>().setrotation(newmaterial);
        wingsmaterial();
    }
    public void legsmaterial()
    {
        if (legsmatindex[legs, 1] < 7) newmaterial = legsmatindex[legs, 1] + 1;
        else newmaterial = 0;
        Material[] promaterials = Rlegs[legs].materials;
        promaterials[legsmatindex[legs, 0]] = material[newmaterial];
        Rlegs[legs].materials = promaterials;
        legsmatindex[legs, 1] = newmaterial;
        textparts[3].gameObject.GetComponent<menutextures>().setrotation(newmaterial);
    }
    public void wingsmaterial()
    {
        if (wings == 1)
        { 
        Material[] promaterials = wingsA[body].GetComponent<Renderer>().materials;
        promaterials[0] = material[bodymatindex[body, 1]];
        wingsA[body].GetComponent<Renderer>().materials = promaterials;
        }
        if (wings == 2)
        {
            Material[] promaterials = wingsB[body].GetComponent<Renderer>().materials;
            promaterials[0] = material[bodymatindex[body, 1]];
            wingsB[body].GetComponent<Renderer>().materials = promaterials;
        }
    }
    public void basetexture()
    {
        if (basematindx < 7) basematindx++;
        else basematindx = 0;
        basematerial = material[basematindx];    
                             
        for (int f = 0; f < 6; f++)
        {
            Material[] promaterials = Rhead[f].materials;
            promaterials[headmatindex[f, 2]] = basematerial;
            Rhead[f].materials = promaterials;

            promaterials = Rarms[f].materials;
            promaterials[armsmatindex[f, 2]] = basematerial;
            Rarms[f].materials = promaterials;

            promaterials = Rbody[f].materials;
            promaterials[bodymatindex[f, 2]] = basematerial;
            Rbody[f].materials = promaterials;

            promaterials = Rlegs[f].materials;
            promaterials[legsmatindex[f, 2]] = basematerial;
            Rlegs[f].materials = promaterials;
        }
            textparts[4].gameObject.GetComponent<menutextures>().setrotation(basematindx);
    }
    public void assasin()
    {
        completeset(0);
        anim.SetInteger("robot", 0);
    }
    public void basic()
    {
        completeset(1);
        anim.SetInteger("robot", 1);
    }
    public void heavy()
    {
        completeset(2);
        anim.SetInteger("robot", 2);
    }
    public void rlight()
    {
        completeset(3);
        anim.SetInteger("robot", 3);
    }
    public void service()
    {
        completeset(4);
        anim.SetInteger("robot", 4);
    }
    public void work()
    {
        completeset(5);
        anim.SetInteger("robot", 5);
    }
    public void completeset(int set)
    {
        deactivateall();
        if (wings>0)
        {
            wingsonoff();
        }
        anim.SetBool("fly", false);
        head = set;
        body = set;
        arms = set;
        legs = set;
        wingsmaterial();
        Ghead[head].SetActive(true);
        Gbody[body].SetActive(true);
        Garms[arms].SetActive(true);
        Glegs[legs].SetActive(true);
        face = true;
        Gface[head].SetActive(true);
        textparts[0].gameObject.GetComponent<menutextures>().setrotation(headmatindex[head, 1]);
        textparts[1].gameObject.GetComponent<menutextures>().setrotation(armsmatindex[arms, 1]);
        textparts[2].gameObject.GetComponent<menutextures>().setrotation(bodymatindex[body, 1]);
        textparts[3].gameObject.GetComponent<menutextures>().setrotation(legsmatindex[legs, 1]);

        //materials
        while (headmatindex[set, 1] != set)
        {
            headmaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (armsmatindex[set, 1] != set)
        {
            armsmaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (bodymatindex[set, 1] != set)
        {
            bodymaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (legsmatindex[set, 1] != set)
        {
            legsmaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (basematindx != set)
        {
            basetexture();
            special1.interactable = true;
            special2.interactable = true;
        }
    }
    public void checkset()
    {
        if (head == arms && arms == legs && legs == body)
        {
            special1.interactable = true;
            special2.interactable = true;
            anim.SetInteger("robot", head);
        }
        else
        {
            special1.interactable = false;
            special2.interactable = false;
        }
    }
    public void neonrobot()
    {
        while (headmatindex[head, 1] != 7)
        {
            headmaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (armsmatindex[arms, 1] != 7)
        {
            armsmaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (bodymatindex[body, 1] != 7)
        {
            bodymaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (legsmatindex[legs, 1] != 7)
        {
            legsmaterial();
            special1.interactable = true;
            special2.interactable = true;
        }
        while (basematindx != 7)
        {
            basetexture();
            special1.interactable = true;
            special2.interactable = true;
        }
        // LIGHTSOFF
        GameObject.Find("lights").gameObject.GetComponent<lights>().onofflights();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator wingsoff()
    {
        int bodyaux = body;        
        yield return new WaitForSeconds(0.9f);
        wingsB[bodyaux].SetActive(false);
        wings = 0;
        
    }
}

