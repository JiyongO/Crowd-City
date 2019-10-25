using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Citizen : MonoBehaviour, IOnTornado
{
    NavMeshAgent nav;
    bool isOnTornado;
    string obstacleTag = "Obstacle";
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveCitizen());
    }
    IEnumerator MoveCitizen()
    {
        while (!isOnTornado)
        {
            if (nav.isOnNavMesh)
                nav.SetDestination(new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50)));
            yield return new WaitForSeconds(3f);
        }
    }

    public void IOnTornado(Material mat, Vector3 tornadoPos)
    {
        isOnTornado = true;
        GetComponent<MeshRenderer>().material = mat;
        nav.enabled = false;
        GetComponent<Rigidbody>().AddForce((tornadoPos - transform.position) * 50 + Vector3.up * 400);
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == obstacleTag)
        {
            transform.position = new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50));
        }
    }
}
