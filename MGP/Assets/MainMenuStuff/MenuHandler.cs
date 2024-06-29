using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    GameObject MainMenu;
    GameObject GameSelection;
    GameObject Achievements;
    GameObject Confirmation;
    public GameObject audioMusicObject;
    public GameObject audioEffectObject;
    public AudioClip BackClip;
    public AudioClip NormalClip;

    public Sprite mutedImage;
    public Sprite unmutedImage;
    public Image SoundToggle;

    public TMP_Text highScoreText;
    public TMP_Text deathCount;
    public TMP_Text launchCount;
    public TMP_Text breakCount;
    public Sprite unlockedImage;
    public Sprite lockedImage;
    public Image AchievementOne;
    public Image AchievementTwo;
    public Image AchievementThree;
    public Image AchievementFour;
    public Image AchievementFive;
    public Image AchievementSix;
    bool soundToggle;

    void Start()
    {
        GameSelection = GameObject.FindGameObjectWithTag("GameSelection");
        MainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        Achievements = GameObject.FindGameObjectWithTag("Options");
        Confirmation = GameObject.FindGameObjectWithTag("Confirmation");
        
        GameSelection.SetActive(false);
        Confirmation.SetActive(false);
        Achievements.SetActive(false);
        MainMenu.SetActive(true);
    }

    void Awake()
    {
        audioMusicObject.GetComponent<AudioSource>().Play();
        soundToggle = (PlayerPrefs.GetInt("isSound") != 0);
        UpdateAchievements();
        checkAudioStatus();
    }

    // FOR DEBUGGING ONLY
    public void initializeSave()
    {
        // Init Roofy Jump
        PlayerPrefs.SetFloat("HighScore", 0);
        PlayerPrefs.SetInt("DeathCount", 0);

        // Init Happy Fellow
        PlayerPrefs.SetInt("BreakCount", 0);
        PlayerPrefs.SetInt("LaunchCount", 0);

        // Init Achievements
        PlayerPrefs.SetInt("AchOne", 0);
        PlayerPrefs.SetInt("AchTwo", 0);
        PlayerPrefs.SetInt("AchThree", 0);
        PlayerPrefs.SetInt("AchFour", 0);
        PlayerPrefs.SetInt("AchFive", 0);
        PlayerPrefs.SetInt("AchSix", 0);
        UpdateAchievements();
        PlayNormalClip();
        Confirmation.SetActive(false);
    }


    private void UpdateAchievements()
    {
        highScoreText.text = "Current Highscore: " + PlayerPrefs.GetFloat("HighScore", 0).ToString();
        deathCount.text = "Death Count: " + PlayerPrefs.GetInt("DeathCount", 0).ToString();
        breakCount.text = "Things obliterated: " + PlayerPrefs.GetInt("BreakCount", 0).ToString();
        launchCount.text = "Bir... Fellows launced: " + PlayerPrefs.GetInt("LaunchCount", 0).ToString();
        int achOne = PlayerPrefs.GetInt("AchOne", 0);
        int achTwo = PlayerPrefs.GetInt("AchTwo", 0);
        int achThree = PlayerPrefs.GetInt("AchThree", 0);
        int achFour = PlayerPrefs.GetInt("AchFour", 0);
        int achFive = PlayerPrefs.GetInt("AchFive", 0);
        int achSix = PlayerPrefs.GetInt("AchSix", 0);
        // First Achievement
        if (achOne > 0)
        {
            AchievementOne.sprite = unlockedImage;
        } 
        else if (achOne == 0)
        {
            AchievementOne.sprite = lockedImage;
        }

        // Second Achievement
        if (achTwo > 0)
        {
            AchievementTwo.sprite = unlockedImage;
        }
        else if (achTwo == 0)
        {
            AchievementTwo.sprite = lockedImage;
        }

        // Third Achievement
        if (achThree > 0)
        {
            AchievementThree.sprite = unlockedImage;
        }
        else if (achThree == 0)
        {
            AchievementThree.sprite = lockedImage;
        }

        // Fourth Achievement
        if (achFour > 0)
        {
            AchievementFour.sprite = unlockedImage;
        }
        else if (achFour == 0)
        {
            AchievementFour.sprite = lockedImage;
        }

        // Fifth Achievement
        if (achFive > 0)
        {
            AchievementFive.sprite = unlockedImage;
        }
        else if (achFive == 0)
        {
            AchievementFive.sprite = lockedImage;
        }

        // Sixth Achievement
        if (achSix > 0)
        {
            AchievementSix.sprite = unlockedImage;
        }
        else if (achSix == 0)
        {
            AchievementSix.sprite = lockedImage;
        }
    }

    public void QuitGame()
    {
        PlayBackClip();
        Application.Quit();
    }

    private void PlayBackClip()
    {
        if(audioEffectObject.activeSelf){
            audioEffectObject.GetComponent<AudioSource>().Stop();
            audioEffectObject.GetComponent<AudioSource>().clip = BackClip;
            audioEffectObject.GetComponent<AudioSource>().Play();
        }
    }

    public void ToGameSeleciton()
    {
        PlayNormalClip();
        MainMenu.SetActive(false);
        GameSelection.SetActive(true);
    }

    private void PlayNormalClip()
    {
        if(audioEffectObject.activeSelf){
            audioEffectObject.GetComponent<AudioSource>().Stop();
            audioEffectObject.GetComponent<AudioSource>().clip = NormalClip;
            audioEffectObject.GetComponent<AudioSource>().Play();
        }
    }

    public void ToggleSound()
    {
        soundToggle = !soundToggle;
        PlayerPrefs.SetInt("isSound", (soundToggle ? 1 : 0));
        checkAudioStatus();
    }

    private void checkAudioStatus()
    {
        if (soundToggle)
        {
            SoundToggle.sprite = unmutedImage;
            audioMusicObject.GetComponent<AudioSource>().volume = 0.5f;
            if (!audioMusicObject.GetComponent<AudioSource>().isPlaying) { audioMusicObject.GetComponent<AudioSource>().Play(); }
            audioEffectObject.SetActive(true);
        }
        else
        {
            SoundToggle.sprite = mutedImage;
            audioMusicObject.GetComponent<AudioSource>().volume = 0;
            audioEffectObject.SetActive(false);
        }
    }

    public void ToMainMenu(){
        PlayBackClip();
        GameSelection.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OpenOptions(){
        PlayNormalClip();
        Achievements.SetActive(true);
    }
    public void CloseOptions(){
        PlayBackClip();
        Achievements.SetActive(false);
    }

    public void OpenConfrimation(){
        PlayNormalClip();
        Confirmation.SetActive(true);
    }
    public void CloseConfrimation(){
        PlayBackClip();
        Confirmation.SetActive(false);
    }

public void HappyFellows(){
    PlayNormalClip();
    SceneManager.LoadScene(2);
}

public void RoofyJumps(){
    PlayNormalClip();
    SceneManager.LoadScene(1);
}
}
