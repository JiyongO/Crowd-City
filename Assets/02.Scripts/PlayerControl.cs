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
                    case "RED":
                        if (Player_UI.redCnt < GetMyCount(tag))
                        {
                            Debug.Log("case red");
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
                return Player_UI.playerCnt;
            case "RED":
                return Player_UI.redCnt;
            case "YELLOW":
                return Player_UI.yelCnt;
            default:
                return null;
        }
    }
    public void ChangeColor(Collider other)
    {
        other.tag = tag;
        Debug.Log("t enter from " + name);
        Follower f = other.GetComponent<Follower>();
        other.transform.parent = transform;
        other.GetComponent<MeshRenderer>().material = mat;
        if (f != null)
        {
            Destroy(other.GetComponent<Follower>());
            Debug.Log("destroyed");
        }
        f = other.gameObject.AddComponent<Follower>();
        f.parent = gameObject;
    }
}
