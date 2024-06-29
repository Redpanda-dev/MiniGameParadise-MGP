using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJ_GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public int platformCount;
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        for(int i = 0; i < platformCount; i++){
            spawnPosition.y += Random.Range(-1.5f, 2f);
            spawnPosition.x += Random.Range(-2f, 2f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

}
