using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : PlayerControl
{
    float h, v;
    int? myCnt;

    new void Start()
    {
        myCnt = GetMyCount(tag);
        nav = GetComponent<NavMeshAgent>();
        mat = GetComponent<MeshRenderer>().material;
    }
    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        nav.velocity = dir.normalized * nav.speed;        
    }    
}
