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
    [Header("Game Over Section")]
    public float timeLimit = 5f;
    float timePassed;
    public AnimationCurve overPanelCurve;
    public Image gameoverPanel;
    Vector3 endPos;
    [Header("Score Board")]
    public Transform entryContainer;
    public Transform entryTemplate;

    public static int playerCnt = 0, redCnt = 0, yelCnt = 0;
    Dictionary<Image, int> ranks = new Dictionary<Image, int>();
    public Image[] images;
    List<HighscoreEntry> highscoreEntryList;
    List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }
    class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    [System.Serializable]
    class HighscoreEntry
    {
        public int score;
        public string name;
    }
    void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 220f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1st";break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd";break;
            default:
                rankString = rank + "TH";break;
        }
        // insert variables
        entryTransform.Find("Pos").GetComponent<Text>().text = rankString;
        int score = highscoreEntry.score;
        entryTransform.Find("Score").GetComponent<Text>().text = score.ToString();
        string name = highscoreEntry.name;
        entryTransform.Find("Name").GetComponent<Text>().text = name;

        // Coloring Name
        //Color blue = new Color(0.38f, 0.67f, 0.88f);
        Color yellow = new Color(0.99f, 0.87f, 0.08f);
        Color red = new Color(0.99f, 0.23f, 0.09f);
        switch (name)
        {
            case "Player": entryTransform.Find("Name").GetComponent<Text>().color = new Color(0.38f, 0.67f, 0.88f); break;
            case "Red": entryTransform.Find("Name").GetComponent<Text>().color = red; break;
            case "Yellow": entryTransform.Find("Name").GetComponent<Text>().color = yellow; break;
            default:
                break;
        }

        transformList.Add(entryTransform);
    }
    
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
        entryTemplate.gameObject.SetActive(false);
        playerNumTxt.text = playerCnt.ToString();
        redNumTxt.text = redCnt.ToString();
        yelNumTxt.text = yelCnt.ToString();
        gameOver.gameObject.SetActive(false);
        endPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);

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
                isOver = true;
                Debug.Log("is over");
                OnGameOver();
            }
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
        playerImg.gameObject.transform.position = CalculateScreenPos(player.transform.position) + Vector2.up * offset * 5;
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
        //if (red.GetComponent<PlayerAI>() != null)
        //    red.GetComponent<PlayerAI>().enabled = false;
        //if (yellow.GetComponent<PlayerAI>() != null)
        //    yellow.GetComponent<PlayerAI>().enabled = false;
    }
    void GetRank()
    {
        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ score = playerCnt, name = "Player" },
            new HighscoreEntry{ score = redCnt, name = "Red" },
            new HighscoreEntry{ score = yelCnt, name = "Yellow" }
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
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        #region old Ranking
        //if (playerCnt >= redCnt && playerCnt >= yelCnt)
        //{
        //    images[0] = playerImg;
        //    if (redCnt >= yelCnt)
        //    {
        //        images[1] = redImg;
        //        images[2] = yellowImg;
        //    }
        //    else
        //    {
        //        images[1] = yellowImg;
        //        images[2] = redImg;
        //    }
        //}
        //else if (playerCnt >= redCnt && playerCnt <= yelCnt)
        //{
        //    images[0] = yellowImg;
        //    images[1] = playerImg;
        //    images[2] = redImg;
        //}
        //else if (playerCnt >= yelCnt && playerCnt <= redCnt)
        //{
        //    images[0] = redImg;
        //    images[1] = playerImg;
        //    images[2] = yellowImg;
        //}
        //else
        //{
        //    images[2] = playerImg;
        //    if (redCnt >= yelCnt)
        //    {
        //        images[0] = redImg;
        //        images[1] = yellowImg;
        //    }
        //    else
        //    {
        //        images[0] = yellowImg;
        //        images[1] = redImg;
        //    }
        //}
        #endregion
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
