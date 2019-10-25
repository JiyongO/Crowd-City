using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tornado : MonoBehaviour
{
    string citizen = "CITIZEN";

    public Material mat;
    public ParticleSystem effect;
    RaycastHit hit;
    LayerMask layerMask;
    Collider[] colliders;
    int[] counts;
    Vector3 desPos;
    List<HighscoreEntry> highscoreEntryList;
    float passedTime;
    float coolTime = 1f;
    float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        counts = new int[] { Player_UI.playerCnt, Player_UI.redCnt, Player_UI.yelCnt };
        layerMask = LayerMask.NameToLayer("UNIT");
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime > coolTime)
        {
            passedTime = 0;
            desPos = GetTargetPos();
        }
        transform.position = Vector3.MoveTowards(transform.position, desPos, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerMask)
        {
            Instantiate(effect, other.transform);
            if (other.tag == citizen)
                other.GetComponent<Citizen>().IOnTornado(mat, transform.position);
            else
                other.GetComponent<Follower>().IOnTornado(mat, transform.position);
        }
    }
    class HighscoreEntry
    {
        public int score;
        public string name;
    }
    Vector3 GetTargetPos()
    {
        highscoreEntryList = new List<HighscoreEntry>()
            {
            new HighscoreEntry{ score =Player_UI. playerCnt, name = "Player" },
            new HighscoreEntry{ score = Player_UI.redCnt, name = "Red" },
            new HighscoreEntry{ score = Player_UI.yelCnt, name = "Yellow" }
            };
        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    HighscoreEntry temp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = temp;
                }
            }
        }
        return GameObject.Find(highscoreEntryList[0].name).transform.position;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, 4);
    //}
}
