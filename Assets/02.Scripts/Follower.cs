using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Follower : MonoBehaviour
{
    public GameObject des;
    public GameObject parent;
    NavMeshAgent nav;
    NavMeshAgent parentNav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        parentNav = parent.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(parent.transform.position);
        //nav.velocity = parentNav.velocity;
    }
}
