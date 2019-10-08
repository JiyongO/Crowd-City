using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Follower : PlayerControl
{
    public delegate void CountingDel(string tag);
    public static event CountingDel CountingEvent;
    public static event CountingDel SubCountEvent;
    public GameObject parent;
    private void OnEnable()
    {
        CountingEvent?.Invoke(tag);
    }
    //new void Start()
    //{
    //    base.Start();
    //    parent = GetComponentInParent<Player>()?.gameObject;
    //    if (parent == null)
    //    {
    //        Debug.Log("el if" + name);
    //        parent = GetComponentInParent<PlayerAI>()?.gameObject;
    //    }
    //    transform.parent = parent.transform;
    //}

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(parent.transform.position);
    }
    private void OnDisable()
    {
        SubCountEvent?.Invoke(tag);
    }
}
