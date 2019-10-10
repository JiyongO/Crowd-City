using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Follower : PlayerControl
{
    public delegate void CountingDel(string tag);
    public static event CountingDel CountingEvent;
    public static event CountingDel SubCountEvent;
    public GameObject parent;
    public GameObject ps;
    private void OnEnable()
    {
        CountingEvent?.Invoke(tag);
        switch (tag)
        {
            case "BLUE":
                ps = Instantiate(Resources.Load("BlueEffect") as GameObject, transform);
                break;
            case "RED":
                ps = Instantiate(Resources.Load("RedEffect") as GameObject, transform);
                break;
            case "YELLOW":
                ps = Instantiate(Resources.Load("YellowEffect") as GameObject, transform);
                break;

            default:
                break;
        }        
    }
    private void Start()
    {
        base.Start();
    }
    void Update()
    {
        nav.SetDestination(parent.transform.position);
    }
    private void OnDisable()
    {
        SubCountEvent?.Invoke(tag);
    }
}
