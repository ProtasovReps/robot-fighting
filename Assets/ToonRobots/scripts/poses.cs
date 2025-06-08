using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poses : MonoBehaviour {

    
    public int pose;
    string[] posename = new string[24] { "TR_pose01", "TR_pose02", "TR_pose03", "TR_pose04", "TR_pose05", "TR_pose06", "TR_pose07", "TR_pose08"
                                       , "TR_pose09", "TR_pose10", "TR_pose11", "TR_pose12", "TR_pose13", "TR_pose14", "TR_pose15", "TR_pose16"
                                       , "TR_pose17", "TR_pose18", "TR_pose19", "TR_pose20", "TR_pose21", "TR_pose22", "TR_pose23", "TR_pose24"
                                       };
    void Start ()
    {
        if (pose > posename.Length-1) pose = Random.Range(0, posename.Length-1);        
        GetComponent<Animator>().Play(posename[pose],0);
    }	
}
