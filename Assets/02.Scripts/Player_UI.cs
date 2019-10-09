using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player_UI : MonoBehaviour
{
    public Canvas gameOver;
    public GameObject player;
    public Image playerImg;
    public Text playerNumTxt;
    public GameObject yellow;
    public Image yellowImg;
    public Text yelNumTxt;
    public GameObject red;
    public Image redImg;
    public Text redNumTxt;
    Vector3 redPos;
    float offset = 50;

    public Text timeTxt;
    bool isOver = false;
    float timeLimit = 5f;
    float timePassed;
    public AnimationCurve overPanelCurve;
    public Image gameoverPanel;
    Vector3 endPos;

    public static int playerCnt = 0, redCnt = 0, yelCnt = 0;
    Dictionary<Image, int> ranks = new Dictionary<Image, int>();
    public Image[] images;
    void GameInit()
    {
        playerCnt = 0;
        redCnt = 0;
        yelCnt = 0;
    }
    private void OnEnable()
    {
        Follower.CountingEvent += AddCount;
        Follower.SubCountEvent += SubCount;
        PlayerAI.OnAiDisable += TurnOffAI;
        Player.PlayerDeathEvent += OnGameOver;
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
    void Start()
    {
        GameInit();
        playerNumTxt.text = playerCnt.ToString();
        redNumTxt.text = redCnt.ToString();
        yelNumTxt.text = yelCnt.ToString();
        gameOver.gameObject.SetActive(false);
        endPos = gameoverPanel.transform.position;

        images = new Image[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLimit - Time.timeSinceLevelLoad > 0)
            timeTxt.text = (timeLimit - Time.timeSinceLevelLoad).ToString("0.#");
        if (Time.timeSinceLevelLoad > timeLimit)
        {
            if(!isOver)
            {
                Debug.Log("is over");
                OnGameOver();
            }
            isOver = true;
            timePassed += Time.deltaTime;
            gameoverPanel.transform.position =
            Vector3.Lerp(gameoverPanel.transform.position - Vector3.up * 100, endPos, overPanelCurve.Evaluate(timePassed));
        }
        DirectionUI();
    }
    void DirectionUI()
    {
        yellowImg.gameObject.transform.position = CalculateScreenPos(yellow.transform.position);
        redImg.gameObject.transform.position = CalculateScreenPos(red.transform.position);
        playerImg.gameObject.transform.position = CalculateScreenPos(player.transform.position) + Vector2.up * offset * 10;
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
    void TurnOffAI(string name)
    {
        Debug.Log(name + " Turning Off");
        switch (name)
        {
            case "Red":
                redImg.gameObject.SetActive(false);
                break;
            case "Yellow":
                yellowImg.gameObject.SetActive(false);
                break;
            case "Player":
                playerImg.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    void OnGameOver()
    {
        gameOver.gameObject.SetActive(true);
        GetRank();
        //Time.timeScale = 0.2f;
        
    }
    void GetRank()
    {
        if (playerCnt >= redCnt && playerCnt >= yelCnt)
        {
            images[0] = playerImg;
            if (redCnt >= yelCnt)
            {
                images[1] = redImg;
                images[2] = yellowImg;
            }
            else
            {
                images[1] = yellowImg;
                images[2] = redImg;
            }
        }
        else if (playerCnt >= redCnt && playerCnt <= yelCnt)
        {
            images[0] = yellowImg;
            images[1] = playerImg;
            images[2] = redImg;
        }
        else if (playerCnt >= yelCnt && playerCnt <= redCnt)
        {
            images[0] = redImg;
            images[1] = playerImg;
            images[2] = yellowImg;
        }
        else
        {
            images[2] = playerImg;
            if (redCnt >= yelCnt)
            {
                images[0] = redImg;
                images[1] = yellowImg;
            }
            else
            {
                images[0] = yellowImg;
                images[1] = redImg;
            }
        }
    }
    public void Restart()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnDisable()
    {
        Follower.CountingEvent -= AddCount;
        Follower.SubCountEvent -= SubCount;
        PlayerAI.OnAiDisable -= TurnOffAI;
        Player.PlayerDeathEvent -= OnGameOver;
    }
}
