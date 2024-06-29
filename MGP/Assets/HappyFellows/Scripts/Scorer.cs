using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scorer : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    public int health;
    int enemiesLeft;
    private bool gameEnded = false;

    AudioHandler _audioHandler;

    GameObject victoryWindow;
    GameObject loseWindow;
    [SerializeField] GameObject inputSystem;

    void Start()
    {

        _audioHandler = GameObject.FindGameObjectWithTag("AudioHandle").GetComponent<AudioHandler>();
        score = 0;
        health = 6;
        checkEnemies();
        victoryWindow = GameObject.FindGameObjectWithTag("VictoryScreen");
        victoryWindow.SetActive(false);
        loseWindow = GameObject.FindGameObjectWithTag("DefeatScreen"); 
        loseWindow.SetActive(false);
    }

    void Update()
    {
        checkEnemies();
        checkHealth();
    }

    public void AddPoints(int amount){
        score += amount;
    }

    public void RemoveHealth(int amount){
        health -= amount;
    }

    public int getPoints(){
        return score;
    }

    public int getHealth(){
        return health;
    }

    public int getEnemiesLeft(){
        return enemiesLeft;
    }

    void checkEnemies(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
         if(enemiesLeft == 0)
         {
            if(!gameEnded) {
                winGame();
            }
         }
    }
    void winGame()
    {
        int levelInteger = SceneManager.GetActiveScene().buildIndex;
        _audioHandler.PlayVictorySound();
        gameEnded = true;
        inputSystem.SetActive(false);
        victoryWindow.SetActive(true);
        Debug.Log("All enemies killed!");
    }

    void loseGame()
    {
        if(victoryWindow.activeSelf){ return; }

        gameEnded = true;
        loseWindow.SetActive(true);
        Debug.Log("Game Over! No more health left.");
        
    }

    void checkHealth(){
        if(health == 0) {
            loseGame();
        }
    }
}
