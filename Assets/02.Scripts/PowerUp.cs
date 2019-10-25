using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerUp : MonoBehaviour
{
    NavMeshAgent nav;
    string playerTag = "Player";
    string obstacleTag = "Obstacle";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == obstacleTag)
        {
            transform.position = new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50));
        }
        if (other.tag == playerTag)
        {
            nav = other.gameObject.GetComponent<NavMeshAgent>();
            nav.speed += 1;
            Destroy(gameObject);
        }
    }
}
  public interface IOnTornado {void IOnTornado (Material mat, Vector3 tornadoPos);}
