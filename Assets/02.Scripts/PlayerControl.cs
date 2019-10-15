using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    protected Material mat;
    protected NavMeshAgent nav;
    protected void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        mat = GetComponent<MeshRenderer>().material;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag != tag)
        {
            string tagName = other.tag;
            if (tagName == "CITIZEN")
            {
                Destroy(other.GetComponent<Citizen>());
                ChangeColor(other);
            }
            else
            {
                switch (tagName)
                {
                    case "BLUE":
                        if (Player_UI.playerCnt < GetMyCount(tag))
                        {
                            ChangeColor(other);
                            break;
                        }
                        else
                            break;
                    case "Player":
                        if (Player_UI.playerCnt == 0)
                        {
                            Destroy(other.GetComponent<Player>(), 1f);
                            ChangeColor(other);
                            break;
                        }
                        else
                            break;
                    case "RED":
                        if (Player_UI.redCnt < GetMyCount(tag))
                        {
                            Debug.Log("case red");
                            ChangeColor(other);
                            break;
                        }
                        else
                            break;
                    case "RED_Agent":
                        if (Player_UI.redCnt == 0)
                        {
                            Debug.Log("case red_Agent");
                            ChangeColor(other);
                            break;
                        }
                        else
                            break;
                    case "YELLOW":
                        if (Player_UI.yelCnt < GetMyCount(tag))
                        {
                            ChangeColor(other);
                            break;
                        }
                        else
                            break;
                    case "YELLOW_Agent":
                        if (Player_UI.yelCnt == 0)
                        {
                            ChangeColor(other);
                            break;
                        }
                        else
                            break;
                    default:
                        break;
                }
            }
        }
    }
    public int? GetMyCount(string tag)
    {
        switch (tag)
        {
            case "BLUE":
            case "Player":
                return Player_UI.playerCnt;
            case "RED":
            case "RED_Agent":
                return Player_UI.redCnt;
            case "YELLOW":
            case "YELLOW_Agent":
                return Player_UI.yelCnt;
            default:
                return null;
        }
    }
    public void ChangeColor(Collider other)
    {   
        Debug.Log("t enter from " + name);
        Follower f = other.GetComponent<Follower>();
        //other.transform.parent = transform;
        other.GetComponent<MeshRenderer>().material = mat;
        if (other.GetComponent<PlayerAI>())
        {
            Destroy(other.GetComponent<PlayerAI>());
        }
        if (f != null)
        {
            Destroy(other.GetComponent<Follower>());
            Debug.Log("destroyed");
        }
        string tagForOthers = null;
        if (tag.Contains("RED"))
            tagForOthers = "RED";
        else if (tag.Contains("YELLOW"))
            tagForOthers = "YELLOW";
        else
            tagForOthers = "BLUE";
        other.tag = tagForOthers;
        f = other.gameObject.AddComponent<Follower>();
        switch (other.tag)
        {
            case "RED":
                f.parent = GameObject.Find("Red");
                other.transform.parent = f.parent.transform;
                break;
            case "YELLOW":
                f.parent = GameObject.Find("Yellow");
                other.transform.parent = f.parent.transform;
                break;
            case "BLUE":
                f.parent = GameObject.Find("Player");
                other.transform.parent = f.parent.transform;
                Debug.Log("parenting P");
                break;
        }
        //f.parent = gameObject;
    }
}
