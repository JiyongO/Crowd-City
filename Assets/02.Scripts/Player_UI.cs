using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_UI : MonoBehaviour
{
    public GameObject player;
    public Image playerImg;
    public Text playerNumTxt;
    public GameObject yellow;
    public Image yellowimg;
    public Text yelNumTxt;
    public GameObject red;
    public Image redImg;
    public Text redNumTxt;
    Vector3 redPos;
    float offset = 15;

    public static int playerCnt = 0, redCnt = 0, yelCnt = 0;
    private void OnEnable()
    {
        Follower.CountingEvent += AddCount;
        Follower.SubCountEvent += SubCount;
    }

    private void SubCount(string tag)
    {
        switch (tag)
        {
            case "BLUE":
                playerCnt--;
                playerNumTxt.text = playerCnt.ToString();
                break;
            case "RED":
                redCnt--;
                redNumTxt.text = redCnt.ToString();
                break;
            case "YELLOW":
                yelCnt--;
                yelNumTxt.text = yelCnt.ToString();
                break;

            default:
                break;
        }
    }

    void AddCount(string tag)
    {
        switch (tag)
        {
            case "BLUE":
                playerCnt++;
                playerNumTxt.text = playerCnt.ToString();
                break;
            case "RED":
                redCnt++;
                redNumTxt.text = redCnt.ToString();
                break;
            case "YELLOW":
                yelCnt++;
                yelNumTxt.text = yelCnt.ToString();
                break;

            default:
                break;
        }
    }
    void Start()
    {
        playerNumTxt.text = playerCnt.ToString();
        redNumTxt.text = redCnt.ToString();
        yelNumTxt.text = yelCnt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Camera.main.transform.position);
        DirectionUI();
    }
    void DirectionUI()
    {
        yellowimg.gameObject.transform.position = CalculateScreenPos(yellow.transform.position);
        redImg.gameObject.transform.position = CalculateScreenPos(red.transform.position);
        playerImg.gameObject.transform.position = CalculateScreenPos(player.transform.position) + Vector2.up * offset * 3;
    }
    Vector2 CalculateScreenPos(Vector3 pos)
    {
        pos = Camera.main.WorldToScreenPoint(pos);
        if (pos.x > Camera.main.pixelWidth)
            pos.x = Camera.main.pixelWidth - offset;
        else if (pos.x < 0)
            pos.x = offset;

        if (pos.y > Camera.main.pixelHeight)
            pos.y = Camera.main.pixelHeight - offset;
        else if (pos.y < 0)
            pos.y = offset;
        return pos;
    }
    private void OnDisable()
    {
        Follower.CountingEvent -= AddCount;
        Follower.SubCountEvent -= SubCount;
    }
}
