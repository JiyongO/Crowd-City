using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAdd : MonoBehaviour
{
    string playerTag = "Player";
    string obstacleTag = "Obstacle";
    public int addCount = 10;
    public GameObject player;
    public GameObject citizen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == obstacleTag)
        {
            Debug.Log("position switch");
            transform.position = new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50));
        }
        if (other.tag == playerTag)
        {
            for (int i = 0; i < addCount; i++)
                Instantiate(citizen, player.transform);

            transform.parent = other.transform;
            transform.localPosition = Vector3.up * 5;
            Destroy(gameObject, 1f);
        }
        
    }
}
