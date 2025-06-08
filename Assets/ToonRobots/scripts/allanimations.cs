using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allanimations : MonoBehaviour {
	
	public int weapon;
    public bool fly;
    int anim;

    string[] animname = new string[11] {  "0flyA","0flyB",
                                          "0groundA", "0groundB", "0groundC", "0groundD",
                                          "1ground",
                                          "2fly",
                                          "2groundA", "2groundB","2groundC"};

    void Start()
    {
        anim = 1;
        if (weapon == 0) if (fly) anim = Random.Range(0,2); else anim = Random.Range(2, 6);

        if (weapon == 1)
        {
            GetComponent<Animator>().SetLayerWeight(1, 1);
            if (fly) anim = Random.Range(0, 2);
            else anim = 6;
        }
        if (weapon == 2) if (fly) anim = 7; else anim = Random.Range(8, 11);


        GetComponent<Animator>().Play(animname[anim], 0);
    }
}
	
	

