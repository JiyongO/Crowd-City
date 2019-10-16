using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : MonoBehaviour
{
    NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveCitizen());
    }
    IEnumerator MoveCitizen()
    {
        while (true)
        {
            nav.SetDestination(new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50)));
            yield return new WaitForSeconds(3f);
        }
    }
}
