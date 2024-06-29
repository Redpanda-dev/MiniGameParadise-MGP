using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    AudioHandler _audioHandler;

    void Start()
    {
        _audioHandler = GameObject.FindGameObjectWithTag("AudioHandle").GetComponent<AudioHandler>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _audioHandler.PlayCrumbleSound();
        Destroy(gameObject);
    }
}
