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
    // Start is called before the first frame update
    new void Start()
    {
        Debug.Log(parent);
        if (GetComponentInParent<Player>())
        {
            Debug.Log("if");
            parent = GetComponentInParent<Player>().gameObject;
            transform.parent = parent.transform;
        }
        else if (GetComponentInParent<PlayerAI>())
        {
            Debug.Log("el if");
            parent = GetComponentInParent<PlayerAI>().gameObject;
            transform.parent = parent.transform;
        }
        Debug.Log(parent);
        mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        gameObject.tag = parent.tag;
        CountingEvent?.Invoke(tag);
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(parent.transform.position);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag != tag)
    //    {
    //        //    other.tag = tag;
    //        //    Debug.Log("t enter from Follower"+name);
    //        //    Follower f;
    //        //    other.transform.parent = parent.transform;
    //        //    other.gameObject.GetComponent<MeshRenderer>().material = mat;
    //        //    f = other.gameObject.AddComponent<Follower>();
    //        //    f.parent = parent;

    //        other.tag = tag;
    //        Debug.Log("t enter from " + name);
    //        Follower f = other.GetComponent<Follower>();
    //        other.transform.parent = parent.transform;
    //        other.GetComponent<MeshRenderer>().material = mat;
    //        if (f != null)
    //        {
    //            Destroy(other.GetComponent<Follower>());
    //            Debug.Log("destroyed");
    //        }
    //        f = other.gameObject.AddComponent<Follower>();
    //        f.parent = gameObject;
    //    }
    //}
    private void OnDestroy()
    {
        SubCountEvent?.Invoke(tag);
    }
}
