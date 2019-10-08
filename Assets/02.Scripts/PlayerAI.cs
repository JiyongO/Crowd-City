using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : PlayerControl
{
    public delegate void AIDisableDel(string name);
    public static event AIDisableDel OnAiDisable;
    GameObject target;
    string citizenTag = "CITIZEN";

    private void Update()
    {
        
        if (target == null)
        {
            target = GameObject.FindWithTag(citizenTag);
        }
        else
        {
            if(target.tag!=citizenTag)
                target = GameObject.FindWithTag(citizenTag);
            nav.SetDestination(target.transform.position);
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, nav.speed * Time.deltaTime);
        }
    }
    private void OnDisable()
    {
        OnAiDisable?.Invoke(name);
    }
}
