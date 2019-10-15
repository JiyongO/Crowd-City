using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject citizen;
    GameObject parent;
    public GameObject powerupSpeed;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Citizens");
        StartCoroutine(SpawnCitizen());
        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnCitizen()
    {
        while (true)
        {
            Instantiate(citizen, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity, parent.transform);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator SpawnPowerup()
    {
        while (true)
        {
            Debug.Log("powerup Spawned");
            Instantiate(powerupSpeed, new Vector3(-33, 1, -37), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnDisable()
    {
        StopCoroutine(SpawnCitizen());
    }
}
