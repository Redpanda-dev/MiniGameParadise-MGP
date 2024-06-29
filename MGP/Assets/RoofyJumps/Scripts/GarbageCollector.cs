using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarbageCollector : MonoBehaviour
{

    AudioHandler _audioHandler;
    public GameObject RetryScreen;
    public GameObject player;
    public GameObject bottomEdge;
    public GameObject platformPrefab;
    public GameObject spikes;
    public GameObject boostPlatform;
    public GameObject movingPlatform;
    public GameObject crumblingPlatform;
    private GameObject newPlatform;
    public UiHandler ui;
    int maxPlatforms = 8;
    int currentPlatforms = 1;
    int selection;
    int selectMax;
    int selectMin;
    bool firstReached = false;
    bool secondReached = false;
    bool thirdReached = false;
    bool fourthReached = false;
    bool fifthReached = false;
    bool sixthReached = false;
    bool seventhReached = false;
    bool eightReached = false;
    bool ninthReached = false;
    bool tenthReached = false;
    bool eleventhReached = false;
    bool twelvethReached = false;

    [SerializeField] private TMP_Text _FinalScoreText;
    void Start()
    {   
        _audioHandler = GameObject.FindGameObjectWithTag("AudioHandle").GetComponent<AudioHandler>();
        selectMin = 0;
        selectMax = 3;
    }

    private void Update() {
        currentPlatforms = GameObject.FindGameObjectsWithTag("Platform").Length;
        if(currentPlatforms <= maxPlatforms) 
        {
            SpawnPlatform();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player")
        {
            Destroy(other.gameObject);
        }
        else {
            LoseGame();
            Destroy(other.gameObject);
        }
    }

    public void LoseGame()
    {
        
        float score = GameObject.Find("Logic").GetComponent<RJ_Scorer>().getHeight();
        float previousHighScore = PlayerPrefs.GetFloat("HighScore", 0);
        int deathCount = PlayerPrefs.GetInt("DeathCount", 0);
        int newDeathCount = deathCount+1;
        PlayerPrefs.SetInt("DeathCount", newDeathCount);

        int achOne = PlayerPrefs.GetInt("AchOne", 0);
        int achTwo = PlayerPrefs.GetInt("AchTwo", 0);
        int achThree = PlayerPrefs.GetInt("AchThree", 0);

        if(newDeathCount >= 1 && achOne == 0) {
            ui.AchievementReached("The Beginning", "Fall once.");
            PlayerPrefs.SetInt("AchOne", 1);
        }

        if(newDeathCount >= 10 && achTwo == 0) {
            ui.AchievementReached("The-not-gonna-give-up dude", "Fall over 10 times.");
            PlayerPrefs.SetInt("AchTwo", 1);
        }

        if(newDeathCount >= 50 && achThree == 0) {
            ui.AchievementReached("The Masochist", "Fall over 50 times.");
            PlayerPrefs.SetInt("AchThree", 1);
        }

        if(previousHighScore < score) {
            PlayerPrefs.SetFloat("HighScore", score);
            PlayerPrefs.Save();
        }

        _audioHandler.PlayFallingSound();
        _FinalScoreText.text = "Height reached: " + score.ToString() + "m";
        RetryScreen.SetActive(true);
        Debug.Log("You Died!");
    }

    private void SpawnPlatform()
    {
        Vector2 spawnPosition;
        GameObject selected;
        ChoosePlatform(out spawnPosition, out selected);

        newPlatform = Instantiate(selected, spawnPosition, Quaternion.identity);
    }

    private void ChoosePlatform(out Vector2 spawnPosition, out GameObject selected)
    {
        spawnPosition = new Vector2();
        spawnPosition.y += bottomEdge.transform.position.y + Random.Range(0f, 5f);
        spawnPosition.x += Random.Range(-6.5f, 6.5f);

        if (player.transform.position.y > 30 && !firstReached)
        {
            firstReached = true;
            incrementMax(); // +1
            Debug.Log("Height over 30");
        }
        else if (player.transform.position.y > 70 && !secondReached)
        {
            secondReached = true;
            incrementMax(); // +2
            Debug.Log("Height over 70");
        }

        else if (player.transform.position.y > 100 && !thirdReached)
        {
            thirdReached = true;
            incrementMin();
            Debug.Log("Height over 100");
        }

        else if (player.transform.position.y > 150 && !fourthReached)
        {
            fourthReached = true;
            incrementMax(); // +3
            Debug.Log("Height over 150");
        }

        else if (player.transform.position.y > 230 && !fifthReached)
        {
            fifthReached = true;
            incrementMax(); // +3
            Debug.Log("Height over 230");
        }

        else if (player.transform.position.y > 300 && !sixthReached)
        {
            sixthReached = true;
            incrementMin();
            Debug.Log("Height over 300");
        }
        
        else if (player.transform.position.y > 400 && !seventhReached)
        {
            seventhReached = true;
            incrementMax(); // +4
            Debug.Log("Height over 400");
        }

        else if (player.transform.position.y > 500 && !eightReached)
        {
            eightReached = true;
            decreasePlatforms(); // platforms - 1
            Debug.Log("Height over 500");
        }

        else if (player.transform.position.y > 600 && !ninthReached)
        {
            ninthReached = true;
            incrementMin(); 
            Debug.Log("Height over 600");
        }
        else if (player.transform.position.y > 700 && !tenthReached)
        {
            tenthReached = true;
            decreasePlatforms(); // platforms - 2
            Debug.Log("Height over 700");
        }
        else if (player.transform.position.y > 800 && !eleventhReached)
        {
            eleventhReached = true;
            decreasePlatforms(); // platforms - 3
            Debug.Log("Height over 800");
        }
        else if (player.transform.position.y > 900 && !twelvethReached)
        {
            twelvethReached = true;
            decreasePlatforms(); // platforms - 4
            Debug.Log("Height over 900");
        }

        selection = Random.Range(selectMin, selectMax);
        switch (selection)
        {
            case 7:
                selected = spikes;
                break;
            case 6:
                selected = crumblingPlatform;
                break;
            case 5:
                selected = movingPlatform;
                break;
            case 4:
                selected = boostPlatform;
                break;
            default:
                selected = platformPrefab;
                break;
        }
    }

    private void decreasePlatforms()
    {
        maxPlatforms -= 1;
    }

    private void incrementMin()
    {
        selectMin += 1;
    }

    private void incrementMax()
    {
        selectMax += 1;
    }
}
