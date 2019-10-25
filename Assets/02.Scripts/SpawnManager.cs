using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float mapWidth;
    public float mapHeight;
    public GameObject citizen;
    GameObject parent;
    public GameObject powerupSpeed;
    public GameObject powerupAdd;
    public GameObject tornado;
    public float tornadoShowtime = 8f;
    bool tornadoOn;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Citizens");
        StartCoroutine(SpawnCitizen());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnTornado());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnCitizen()
    {
        while (true)
        {
            Instantiate(citizen, new Vector3(Random.Range(-mapWidth, mapHeight), 1, Random.Range(-mapWidth, mapHeight)), Quaternion.identity, parent.transform);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator SpawnPowerup()
    {
        while (true)
        {
            //Debug.Log("powerup Spawned");
            Instantiate(powerupSpeed, new Vector3(-33, 1, -37), Quaternion.identity, gameObject.transform);
            Instantiate(powerupAdd, new Vector3(Random.Range(-mapWidth, mapHeight), 1, Random.Range(-mapWidth, mapHeight)), Quaternion.identity, gameObject.transform);
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator SpawnTornado()
    {
        float passedTime = 0;
        while (true)
        {
            passedTime += Time.deltaTime;
            if (passedTime > tornadoShowtime)
            {
                tornado.transform.position = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
                passedTime = 0;
                // On Off Tornado
                //tornadoOn = !tornadoOn;
                //tornado.SetActive(tornadoOn);
            }
            yield return null;
        }
    }
    private void OnDisable()
    {
        StopCoroutine(SpawnCitizen());
    }
}
