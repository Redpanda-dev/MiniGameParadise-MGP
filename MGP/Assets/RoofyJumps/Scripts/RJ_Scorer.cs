using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJ_Scorer : MonoBehaviour
{
    [SerializeField] GameObject character;
    public UiHandler ui;
    private float tempHeight = 0;
    private float height;

    // Update is called once per frame
    void Update()
    {
        if(character != null){
            if(tempHeight < character.transform.position.y)
            {
                height = Mathf.Round(character.transform.position.y) + 3f;
                tempHeight = character.transform.position.y;
                AchievementUpdater();
            }
        }
    }

    private void AchievementUpdater()
    {
        int achFour = PlayerPrefs.GetInt("AchFour", 0);
        int achFive = PlayerPrefs.GetInt("AchFive", 0);
        int achSix = PlayerPrefs.GetInt("AchSix", 0);

        if (height > 50 && achFour == 0)
        {
            ui.AchievementReached("Babys first steps", "Reach height of over 50 meters");
            PlayerPrefs.SetInt("AchFour", 1);
        }

        if (height > 300 && achFive == 0)
        {
            ui.AchievementReached("Half-way there", "Reach height of over 300 meters");
            PlayerPrefs.SetInt("AchFive", 1);
        }

        if (height > 600 && achSix == 0)
        {
            ui.AchievementReached("Just kidding", "Reach height of over 600 meters");
            PlayerPrefs.SetInt("AchSix", 1);
        }
    }

    public float getHeight()
    {
        return height;
    }
}
