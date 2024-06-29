using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UiHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text _HeightText;
    [SerializeField] GameObject pauseWindow;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject player;
    bool soundToggle;
    bool windowToggle = false;
    public GameObject audioObject;
    public GameObject achievementAudioObject;
    public AudioSource audioSource;
    public GameObject musicSource;

    public GameObject AchievementNotification;
    public TMP_Text AchievementName;
    public TMP_Text AchievementText;

    public Sprite mutedImage;
    public Sprite unmutedImage;
    public Image SoundToggleImg;

    public AudioClip VictoryClip;
    public AudioClip BackClip;
    public AudioClip NormalClip;

    void Start()
    {
        pauseWindow.SetActive(windowToggle);
        gameOver.SetActive(false);
        AchievementNotification.SetActive(false);
    }

    void Awake()
    {
        soundToggle = (PlayerPrefs.GetInt("isSound") != 0);
        checkAudioStatus();
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }

    void updateScore(){
        if(player != null){
            _HeightText.text = "Height: " + GameObject.Find("Logic").GetComponent<RJ_Scorer>().getHeight().ToString();
        }
    }

    public void AchievementReached(string Title, string Explanation)
    {
        PlayVictoryClip();
        AchievementName.text = Title;
        AchievementText.text = Explanation;

        AchievementNotification.SetActive(true);
        StartCoroutine(ExecuteAfterTime(3f));
    }

     IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        AchievementNotification.SetActive(false);
    
        // Code to execute after the delay
    }

    public void MenuClicked(){
        PlayBackClip();
        pauseWindow.SetActive(!windowToggle);
        windowToggle = !windowToggle;
    }

    public void SoundToggle(){
        ToggleSound();
    }

    public void ReturnToMenu(){
        PlayNormalClip();
        SceneManager.LoadScene(0);
    }

     public void OptionsClicked(){
        PlayNormalClip();
        Debug.Log("Options clicked");
    }

    public void ResumeClicked(){
        PlayNormalClip();
        pauseWindow.SetActive(false);
    }

    void ToggleSound()
    {
        soundToggle = !soundToggle;
        PlayerPrefs.SetInt("isSound", (soundToggle ? 1 : 0));
        checkAudioStatus();
    }

    private void checkAudioStatus()
    {
        if (soundToggle)
        {
            SoundToggleImg.sprite = unmutedImage;
            musicSource.GetComponent<AudioSource>().volume = 0.5f;
            if (!musicSource.GetComponent<AudioSource>().isPlaying) { musicSource.GetComponent<AudioSource>().Play(); }
            audioObject.SetActive(true);
        }
        else
        {
            SoundToggleImg.sprite = mutedImage;
            musicSource.GetComponent<AudioSource>().volume = 0;
            audioObject.SetActive(false);
        }
    }

    public void RetryClicked(){
        PlayNormalClip();
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    private void PlayBackClip()
    {
        if(soundToggle){
            audioSource.Stop();
            audioSource.clip = BackClip;
            audioSource.Play();
        }
    }

    private void PlayNormalClip()
    {
        if(soundToggle){
            audioSource.Stop();
            audioSource.clip = NormalClip;
            audioSource.Play();
        }
    }

    private void PlayVictoryClip()
    {
        if(soundToggle){
            achievementAudioObject.GetComponent<AudioSource>().Stop();
            musicSource.GetComponent<AudioSource>().volume = 0.8f;
            achievementAudioObject.GetComponent<AudioSource>().clip = VictoryClip;
            achievementAudioObject.GetComponent<AudioSource>().Play();
        }
    }

}
