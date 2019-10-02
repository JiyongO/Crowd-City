using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent nav;
    float h, v;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        nav.velocity = dir.normalized * nav.speed;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CITIZEN"))
        {
            Debug.Log("t enter");
            Follower f;
            other.transform.parent = transform;
            other.gameObject.GetComponent<MeshRenderer>().material = mat;
            f = other.gameObject.AddComponent<Follower>();
            f.parent = gameObject;
        }
    }
}
