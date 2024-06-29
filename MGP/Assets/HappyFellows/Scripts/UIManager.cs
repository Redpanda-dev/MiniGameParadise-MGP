using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _healthText;
    GameObject Window;
    public GameObject musicSource;
    public GameObject soundSource;

    public Sprite mutedImage;
    public Sprite unmutedImage;
    public Image SoundToggleImg;

    bool soundToggle;
    bool windowToggle = false;
    void Start()
    {
        Window = GameObject.FindGameObjectWithTag("MainMenu");
        Window.SetActive(windowToggle);
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
        updateHealth();
    }

    void updateScore(){
        _scoreText.text = "Enemies Left: " + GameObject.Find("GameLogic").GetComponent<Scorer>().getEnemiesLeft().ToString();
    }

    void updateHealth(){
        _healthText.text = "Health: " + GameObject.Find("GameLogic").GetComponent<Scorer>().getHealth().ToString();
    }

    public void MenuClicked(){
        Window.SetActive(!windowToggle);
        windowToggle = !windowToggle;
    }

    public void SoundToggle(){
        ToggleSound();
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }

     public void OptionsClicked(){
        Debug.Log("Options clicked");
    }

    public void ResumeClicked(){
        Window.SetActive(false);
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
            soundSource.SetActive(true);
        }
        else
        {
            SoundToggleImg.sprite = mutedImage;
            musicSource.GetComponent<AudioSource>().volume = 0;
            soundSource.SetActive(false);
        }
    }

    public void RetryClicked(){
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    public void LoadNextLevel(){
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }
}
